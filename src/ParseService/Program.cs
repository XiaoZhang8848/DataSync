using System.Text;
using Microsoft.Extensions.DependencyInjection;
using ParseService.Entities;
using ParseService.Helper;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var serviceCollection = new ServiceCollection();
serviceCollection.AddTransient<ICanalHelper, CanalHelper>();

using var serviceProvider = serviceCollection.BuildServiceProvider();
var canalHelper = serviceProvider.GetRequiredService<ICanalHelper>();

var connectionFactory = new ConnectionFactory();
using var connection = connectionFactory.CreateConnection(new[] { "192.168.31.43" });
using var channel = connection.CreateModel();

const string EXCHANGE_NAME = "canal";
const string QUEUE_NAME = "canal";

DeclareExchange(channel);
DeclareQueue(channel);
QueueBind(channel);

var eventingBasicConsumer = new EventingBasicConsumer(channel);
eventingBasicConsumer.Received += (_, eventArgs) =>
{
    var body = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
    var canalUser = canalHelper.ParseData<User>(body);
    Console.WriteLine(body);
    channel.BasicAck(eventArgs.DeliveryTag, false);
};

channel.BasicConsume("canal", false, eventingBasicConsumer);

Console.ReadLine();

void DeclareExchange(IModel channel)
{
    channel.ExchangeDeclare(EXCHANGE_NAME, ExchangeType.Fanout, true, false, null);
}

void DeclareQueue(IModel channel)
{
    channel.QueueDeclare(QUEUE_NAME, true, false, false, null);
}

void QueueBind(IModel channel)
{
    channel.QueueBind(QUEUE_NAME, EXCHANGE_NAME, "");
}