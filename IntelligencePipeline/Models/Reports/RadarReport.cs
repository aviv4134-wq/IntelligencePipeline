using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Models.Reports
{
    class RadarReport : Report
    {
        private int _speed;
        private int _direction;
        private int _distance;

        public int Speed { get => _speed; protected set { _speed = value; } }
        public int Direction { get => _direction; protected set { _direction = value; } }
        public int Distance { get => _distance; protected set { _distance = value; } }

        public RadarReport(int reportId, DateTime timestamp, double latitude,
        double longitude, string description,
        int speed, int direction, int distance)
        : base(reportId, timestamp, latitude, longitude, description)
        {
            Speed = speed;
            Direction = direction;
            Distance = distance;
        }
        public override string GetSourceType() => "Radar";
        public override int CalculateReliabilityScore()
        {
            int baseGrade = 6;
          
            int maxDistence = 3000;
            int minDistanse = 500;
            if (baseGrade <= maxDistence && baseGrade >= minDistanse) baseGrade += 2;

            int maxSpeed = 900; 
            int minSpeed = 10;
            if (baseGrade <= maxSpeed && baseGrade >= minSpeed) baseGrade += 1;

            int overKillDistance = 70000;
            if (baseGrade > overKillDistance) baseGrade -= 2;

            int overKillSpeed = 1500;
            if (baseGrade > overKillSpeed) baseGrade -= 2;

            return baseGrade;
        }
        public override string GetSummary()
        {
            return $"""
                {base.GetSummary}
                Speed : {Speed}
                Direction : {Direction}
                Distance : {Distance}
                """;
        }


    }
}
