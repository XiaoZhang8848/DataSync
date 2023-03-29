using Microsoft.AspNetCore.Mvc;
using ParseService.Attributes;
using ParseService.Entities;
using ParseService.Helper;

namespace ParseService.Controllers;

[ApiController]
[Route("test")]
public class TestController: ControllerBase
{
    private readonly ICanalHelper _canalHelper;

    public TestController(ICanalHelper canalHelper)
    {
        _canalHelper = canalHelper;
    }

    [NonAction]
    [Queue("canal")]
    public void Get(string message)
    {
        var canalModel = _canalHelper.ParseData<User>(message);
        Console.WriteLine(message);
    }
}