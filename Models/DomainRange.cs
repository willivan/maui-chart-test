using System;
using Microsoft.Maui.Graphics;

namespace DotnetCharts.Models;

public class DomainRange
{
    public float DomainMinimum{get;set;}

    public float DomainMaximum{get;set;}

    public int DomainSteps{get;set;}

    public float RangeMinimum{get;set;}

    public float RangeMaximum{get;set;}

    public float Calculate(float domainValue)
    {
        //Console.WriteLine("DomainMinimum:"+DomainMinimum);
        //Console.WriteLine("DomainMaximum:"+DomainMaximum);
        //Console.WriteLine("DomainSteps:"+DomainSteps);
        //Console.WriteLine("RangeMinimum:"+RangeMinimum);
        //Console.WriteLine("RangeMaximus:"+RangeMaximum);
        //Console.WriteLine("DomainValue:"+domainValue);
        var percent = domainValue / (DomainMaximum-DomainMinimum);
        //Console.WriteLine("Percent:"+percent);
        var amount = RangeMaximum-RangeMinimum;
        //Console.WriteLine("Amount:"+amount);
        var movement = amount*percent;
        //Console.WriteLine("Movement:"+movement);

        return RangeMinimum+movement;
    }
}