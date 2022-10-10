using System;
using System.Linq.Expressions;
using Microsoft.Maui.Graphics;

namespace DotnetCharts.Models;

public class ChartDrawable<T> : IDrawable
{

    private DomainRange X{get;set;}

    private DomainRange Y{get;set;}

    private ChartOptions Options{get;set;}

    private List<T> Data{get;set;}

    private Expression<Func<T, float>> XExpression{get;set;}

    private Expression<Func<T, float>> YExpression{get;set;}

    public ChartDrawable(DomainRange x, DomainRange y, ChartOptions options, List<T> dataSource, Expression<Func<T, float>> xExpression, Expression<Func<T, float>> yExpression)
    {
        X = x;
        Data = dataSource;
        XExpression = xExpression;
        YExpression = yExpression;
        Y = y;
        Options = options;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = Colors.White;
        canvas.FillRectangle(0,0,Options.Width,Options.Height);
        canvas.StrokeColor = Options.StrokeColor;
        canvas.DrawLine(new PointF(Options.MarginLeft,Options.MarginTop), new PointF(Options.MarginLeft,Options.Height-Options.MarginBottom));

        canvas.DrawLine(new PointF(Options.MarginLeft,Options.Height-Options.MarginBottom), new PointF(Options.Width-Options.MarginRight,Options.Height-Options.MarginBottom));
        canvas.FontColor = Options.TextColor;
        canvas.FontSize = Options.FontSize;

        canvas.Font = Font.Default;
        canvas.DrawString("0",Options.MarginLeft-2,Options.Height-Options.MarginBottom,HorizontalAlignment.Right);
        canvas.FillColor = Options.PlotColor;
        var x = XExpression.Compile();
        var y = YExpression.Compile();

        float rangeStep = (X.DomainMaximum-X.DomainMinimum)/X.DomainSteps;
        for(int i = 1;i<=X.DomainSteps;i++)
        {
            canvas.StrokeColor = Options.StrokeColor;
            var xPos = X.Calculate(i*rangeStep);
            canvas.DrawLine(xPos,Options.Height-Options.MarginBottom-10,xPos,Options.Height-Options.MarginBottom+10);
            canvas.DrawString(Math.Round(rangeStep*i).ToString(),xPos,Options.Height-Options.MarginBottom+10+Options.FontSize,HorizontalAlignment.Center);
        }
        rangeStep = (Y.DomainMaximum-Y.DomainMinimum)/Y.DomainSteps;
        for(int i = 1;i<=Y.DomainSteps;i++)
        {
            canvas.StrokeColor = Options.StrokeColor;
            var yPos = Y.Calculate(i*rangeStep);
            canvas.DrawLine(Options.MarginLeft-10, yPos,Options.MarginLeft+10,yPos);
            canvas.DrawString(Math.Round(rangeStep*i).ToString(),Options.MarginLeft-15,yPos,HorizontalAlignment.Right);
        }


        foreach(T item in Data)
        {
            Console.WriteLine("");
            Console.WriteLine("X:"+x(item));
            Console.WriteLine("Y:"+y(item));
            canvas.FillCircle(X.Calculate(x(item)),Y.Calculate(y(item)),5);
        }
        

        
        // canvas.FillCircle(40,40,20);
        // canvas.StrokeSize = 4;
        // canvas.DrawLine(0,0,100,100);

    }
}