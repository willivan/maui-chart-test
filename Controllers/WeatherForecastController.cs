using DotnetCharts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
using Syncfusion.Maui.Charts;

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
        List<Customer> customers = new List<Customer>();
        var rand = new Random((int)DateTime.Now.Ticks);
        for(int i = 0;i<255;i++)
        {
            customers.Add(new Customer(){X = rand.Next(1,100),Y=rand.Next(1,200)});
        }
        
        int width = 1600;
        int height = 1200;
        int margin = 80;
        SkiaBitmapExportContext skiaBitmapExportContext= new(width, height, 1,300,false,true);
        ICanvas canvas = skiaBitmapExportContext.Canvas;

        
        
        ChartDrawable<Customer> chart = new ChartDrawable<Customer>(
            new DomainRange(){DomainMaximum=100,DomainMinimum=0,DomainSteps=5,RangeMaximum=width-margin,RangeMinimum=margin}, 
            new DomainRange(){DomainMaximum=200,DomainMinimum=0,DomainSteps=5,RangeMaximum=margin,RangeMinimum=height-margin},
            new ChartOptions(){
                FontSize=24,
                Height=height,
                Width=width,
                MarginBottom=margin,
                MarginLeft=margin,
                MarginRight=margin,
                MarginTop=margin,
                PlotColor = Colors.Orange,
                TextColor = Colors.Gray,
                StrokeColor = Colors.Gray,
                StrokeSize= 1
            },customers,c=>c.X,c=>c.Y);
        chart.Draw(canvas,RectF.Zero);
        
        MemoryStream stream = new MemoryStream();
        skiaBitmapExportContext.WriteToStream(stream);

        return File(stream.ToArray(),"image/png");
    }
}
