using AspNetCore.StartUpTemplate.Auth;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.StartUpTemplate.Webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IMapper _mapper;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }
    [NeedAuth]
    [HttpGet("get")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpGet("tokentest")]
    public string TokenTest()
    {
       var tm= new UserData() { Id = "123", UserName = "lizhenghao" };
       var token=TokenHelper.CreateToken(tm);
       var res=TokenHelper.ResolveToken(token);
       return token;
    }
}