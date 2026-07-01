using IntelligencePipeline.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Models.Reports
{
    class SignalReport : Report
    {
        private double _frequency;
        private string _content;
        private Language _language;
        private int _signalStrength;

        public double Frequency { get => _frequency; protected set { _frequency = value; } }
        public string Content { get => _content; protected set { _content = value; } }
        public Language Language { get => _language; protected set { _language = value; } }
        public int SignalStrength { get => _signalStrength; protected set { _signalStrength = value; } }

        public SignalReport(int reportId, DateTime timestamp, double latitude,
         double longitude, string description,
         double frequency, string content, Language language,
         int signalStrength)
         : base(reportId, timestamp, latitude, longitude, description)
        {
            Frequency = frequency;
            Content = content;
            Language = language;
            SignalStrength = signalStrength;
        }
        public override string GetSourceType() => "Signal";
        public override int CalculateReliabilityScore()
        {
            int baseGrade = 5;
      
            string[] content = Content.Split();
            bool flag = true;
            for (int index = 0;index < content.Length && flag; index++)
            {
                char[] charToTrim = {',','.','?','!',':',';',' '};
                string cleanWord = content[index].Trim(charToTrim);
                if (Enum.TryParse(cleanWord, true, out contentWords _))
                {
                    baseGrade += 1;
                    flag = false;
                }       
            }

            int bestSiganlStrength = -40;
            if (SignalStrength >= bestSiganlStrength) baseGrade += 3;

            int midSiganlStrength = -70;
            if (baseGrade >= midSiganlStrength) baseGrade += 2;

            int lowSignalStrength = -100;
            if (baseGrade < lowSignalStrength) baseGrade -= 2;

            if (baseGrade > 10) baseGrade = 10;
            return baseGrade;
        }

    }
}
