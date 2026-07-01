using IntelligencePipeline.Models.Reports;
using System;


namespace IntelligencePipeline
{
    class Program
    {
        static void Main()
        {
            DroneReport a = new DroneReport(1,new DateTime(2021,02,04), 3000, 405, "hi exe", 600, 90);
            Console.WriteLine(a.ToString()); 
             
        }
    }

}