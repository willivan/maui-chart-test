using System;
using Microsoft.Maui.Graphics;

namespace DotnetCharts.Models;

public class ChartDrawable : IDrawable
{
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = Color.FromRgb(128,128,128);
        canvas.SetFillPaint(new SolidPaint(Color.FromRgb(255,0,0)),dirtyRect);
        canvas.StrokeColor = Color.FromRgb(255,0,0);
        canvas.FillCircle(40,40,20);
        canvas.StrokeSize = 4;
        canvas.DrawLine(0,0,100,100);

    }
}