using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;

namespace IntelligencePipeline.Calculators
{
     class PriorityCalculator
    {
        public Priority Calculate(Report report) //siganl have content 
        {
            //take decision in if 
            //megic num

            string[] criticalWords = ["missile", "explosion", "attack", "fire"];
            string[] highWords = ["weapon", "suspicious", "border"];
            string[] mediumWords = ["movement", "vehicle", "activity"];
            string description = report.Description;
            

            if (report is SignalReport signalReport)
            {
                string content = signalReport.Content;
                if (ContainsKeyword(content, criticalWords)) return Priority.Critical; 
            }

            else if (report is RadarReport radarReport)
            {
                if (radarReport.Speed >= 800) return Priority.Critical;
               
            }

            else if (ContainsKeyword(description, criticalWords)) return Priority.Critical;

            
            if (ContainsKeyword(description, highWords)) return Priority.High;

            else if (report is DroneReport dronReport && dronReport.Altitude < 500) return Priority.High;

            else if (report is RadarReport radarReport && radarReport.Speed >= 400) return Priority.High;

            else if (report is SoldierReport soldierReport &&
            ContainsKeyword(description, ["weapon"]) &&
            soldierReport.ConfidenceLevel >= 4)
                    return Priority.High;
            
            
            if (ContainsKeyword(description, mediumWords)) return Priority.Medium;
            else if (report.ReliabilityScore >= 7) return Priority.Medium;
            else if (report is RadarReport radarReport && radarReport.Speed >= 120) return Priority.Medium;
            
            return Priority.Low;

        }

        public bool ContainsKeyword(string text, params string[] keywords)
        {
            char[] charToSplit = { ',', '.', '?', '!', ':', ';', ' ' };

            string[] arryText = text.Split(charToSplit, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in arryText)
            {
                if (keywords.Contains(word.ToLower())) return true;
            }

            return false;

        }
    }
}
