using IntelligencePipeline.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Models.Reports
{
    abstract class Report 
    {
        private  int _reportId ;
        private DateTime _timestamp;
        private double _latitude;
        private double _longitude;
        private string _description;
        private ReportStatus _status;
        private Priority _priority;
        private Classification _classification;
        private int _reliabilityScore;
        private string _rejectionReason;

        public int ReportId { get => _reportId;  } 
        public DateTime Timestamp { get => _timestamp; protected set { _timestamp = value; } } 
        public double Latitude { get => _latitude; protected set { _latitude = value; } } 
        public double Longitude { get => _longitude; protected set { _longitude = value; } }
        public string Description { get => _description; protected set { _description = value; } } 
        public ReportStatus Status { get => _status;  set { _status = value; } } 
        public Priority Priority { get => _priority;  set { _priority = value; } }
        public Classification Classification { get => _classification; set { _classification = value; } }
        public int ReliabilityScore { get => _reliabilityScore; set { _reliabilityScore = value; } }
        public string RejectionReason { get => _rejectionReason; set { _rejectionReason = value; } }

        public static string SortByField = "date";


        protected Report(int reportId, DateTime timestamp, double latitude,double longitude, string description)
        {
            _reportId = reportId;
            Timestamp = timestamp;
            Latitude = latitude;
            Longitude = longitude;
            Description = description;
            Status = ReportStatus.New;
            Priority = Priority.Low;
            Classification = Classification.Unclassified;
            
        }

        public abstract string GetSourceType();
        public abstract int CalculateReliabilityScore();

        
        //public int CompareTo(Report other)
        //{
        //    if (other == null) return 1;
        //    switch (SortByField)
        //    {
        //        case "timeStemp":
                    
        //        return this.Timestamp.CompareTo(other.Timestamp);
                    

        //        case "priority":
                    
        //            return this.Priority.CompareTo(other.Timestamp);

        //        case "classification":

        //            return this.Classification.CompareTo(other.Classification);

        //        default:
        //            return this.Timestamp.CompareTo(other.Timestamp);

        //    }
            
        //}

        


        public virtual string GetSummary()
        {
            return $"id : {ReportId}\ntype : {GetSourceType()}\nstatus : {Status}";
        }

        public override string ToString()
            =>
            $"""
            {GetSummary()}
            timestamp : {Timestamp}
            latitude : {Latitude}
            longitude : {Longitude}
            description : {Description}
            priority : {Priority}
            classification : {Classification}
            reliabilityScore : {ReliabilityScore}
            rejectionReason : {RejectionReason}
            
            """;

    }
}
