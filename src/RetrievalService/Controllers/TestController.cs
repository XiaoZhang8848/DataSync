using Microsoft.AspNetCore.Mvc;
using RetrievalService.Data;
using RetrievalService.Entities;

namespace RetrievalService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly AppDbContext db;

    public TestController(AppDbContext db)
    {
        this.db = db;
    }
    [HttpPost]
    public void Post()
    {
        for (int i = 0; i < 1000; i++)
        {
            var str = "";
            var str1 = Guid.NewGuid().ToString();
            for (int j = 0; j < 10; j++)
            {
                str += str1;
            }
            db.Add(new User(str, str));
        }
        db.SaveChanges();
    }
}
