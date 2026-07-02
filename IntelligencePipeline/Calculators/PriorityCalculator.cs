using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
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

            string[] wordsChecK = ["missile", "explosion", "attack", "fire"];
            string description = report.Description;
            

            if (report is SignalReport siganlReport)
            {
                string contnet = siganlReport.Content;
                if (ContainsKeyword(contnet, wordsChecK)) return Priority.Critical; 
            }

            else if (report is RadarReport radarReport)
            {
                if (radarReport.Speed >= 800) return Priority.Critical;
                else if (radarReport.Speed >= 400) return Priority.High;
                else if (radarReport.Speed >= 120) return Priority.Medium;

            }

            else if (ContainsKeyword(description, wordsChecK)) return Priority.Critical;

            
            if (ContainsKeyword(description, ["weapon", "suspicious", "border"])) return Priority.High;

            else if (report is DroneReport dronReport && dronReport.Altitude < 500) return Priority.High;

            else if (report is SoldierReport soldierReport)
            {
                if (ContainsKeyword(description, ["movement"]) && soldierReport.ConfidenceLevel >= 4) return Priority.High;
            }

            
            if (ContainsKeyword(description, ["movement", "vehicle", "activity"])) return Priority.Medium;
            else if (report.ReliabilityScore >= 7) return Priority.Medium;

            return Priority.Low;

        }

        private bool ContainsKeyword(string text, params string[] keywords)
        {
            foreach (string word in keywords)
            {
                if (text.Contains(word)) return true;

            }
            return false;

        }
    }
}
