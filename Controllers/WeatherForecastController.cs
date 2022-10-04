using DotnetCharts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;

namespace DotnetCharts.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Get()
    {
        SkiaBitmapExportContext skiaBitmapExportContext= new(800, 600, 1);
        ICanvas canvas = skiaBitmapExportContext.Canvas;
        ChartDrawable chart = new ChartDrawable();
        chart.Draw(canvas,RectF.Zero);
        MemoryStream stream = new MemoryStream();
        skiaBitmapExportContext.WriteToStream(stream);

        return File(stream.ToArray(),"image/png");
    }
}
