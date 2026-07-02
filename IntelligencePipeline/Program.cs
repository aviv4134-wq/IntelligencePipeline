using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Pipeline;
using System;


namespace IntelligencePipeline
{
    class Program
    {
        static void Main()
        {
            DroneReport a = new DroneReport(1,new DateTime(2021,02,04), 29.5000, 34.0000, "hi exehfdsuiohoghishfods", 600, 43);
            Console.WriteLine(a.ToString());
            ReportPipeline p = new ReportPipeline();
            p.ProcessReport(a); 
        }
    }

}