using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Models.Reports
{
    class DroneReport : Report
    {
        private int _altitude;
        private int _imageQuality;

        public int Altitude { get => _altitude; protected set { _altitude = value; } }

        public int ImageQuality { get => _imageQuality; protected set { _imageQuality = value; } }

        public DroneReport(int reportId, DateTime timestamp, double latitude, double longitude, string description, int altitude, int imageQualty) :base(reportId,timestamp,latitude,longitude,description)
        {
            Altitude = altitude;
            ImageQuality = imageQualty;
        
        }

        public override string GetSourceType() => "Drone";
        public override int CalculateReliabilityScore()
        {
            int baseGrade = 5;
            //megic num!!!
            
            if (ImageQuality >= 80 ) baseGrade += 3;
            if (ImageQuality >= 50) baseGrade += 2;
            if (Altitude >= 500 && Altitude <= 3000) baseGrade += 2;
            if (Altitude > 7000) baseGrade -= 2;

            if (baseGrade > 10) baseGrade = 10;
            return baseGrade;

        }

        public override string GetSummary()
        {
            return $"""
                {base.GetSummary()}
                Altitude : {Altitude}
                ImageQuality : {ImageQuality}
                """;
        }





    }
}
