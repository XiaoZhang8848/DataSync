using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParseService.Attributes;
using ParseService.Data;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ParseService.Extensions;

public static class WebApplicationExtension
{
    public static void UseRabbitMQ(this WebApplication app)
    {
        var config = app.Configuration;
        var connectionFactory = new ConnectionFactory();
        var connection = connectionFactory.CreateConnection(new[] { config.GetConnectionString("RabbitMQ")});
        
        var model = connection.CreateModel();
        model.ExchangeDeclare("canal", ExchangeType.Fanout, true, false);
        model.QueueDeclare("canal", true, false, false);
        model.QueueBind("canal", "canal", string.Empty);
        
        foreach (var controller in Assembly.GetExecutingAssembly().GetExportedTypes().Where(x => x.IsAssignableTo(typeof(ControllerBase))))
        {
            var type = app.Services.GetRequiredService(controller);
            foreach (var methodInfo in controller.GetMethods().Where(x => x.IsDefined(typeof(QueueAttribute))))
            {
                var queueName = methodInfo.GetCustomAttribute<QueueAttribute>()!.Name;
                var channel = connection.CreateModel();
                var eventingBasicConsumer = new EventingBasicConsumer(channel);
                eventingBasicConsumer.Received += (_, eventArgs) =>
                {
                    object body = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                    methodInfo.Invoke(type, new[] { body });
                };
                // 由于懒得做本地消息表, 这里靠不考虑业务失败情况, 默认ack
                channel.BasicConsume(queueName, true, eventingBasicConsumer);
            }
        }
    }

    public static void Migre(this WebApplication app)
    {
        var appDbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
        appDbContext.Database.Migrate();
    }
}