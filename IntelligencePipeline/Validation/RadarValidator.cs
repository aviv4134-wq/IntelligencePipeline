using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Validation
{
    class RadarValidator : BaseValidator
    {
        protected override ValidationResult ValidateSpecificFields(Report report)
        {
            int minSpeed = 0;
            int maxSpeed = 2000;

            int minDirection = 0;
            int maxDirection = 360;

            int minDistance = 100;
            int maxDistance = 100000;

            if (report is not RadarReport radarReport)
                return ValidationResult.Failure("type not allowed");
            
            if (radarReport.Speed < minSpeed || radarReport.Speed > maxSpeed)
                return ValidationResult.Failure($"invalid Speed: must be between {minSpeed} and {maxSpeed}");
            
            if (radarReport.Direction < minDirection || radarReport.Direction > maxDirection)
                return ValidationResult.Failure($"invalid Direction : must be between {minDirection} and {maxDirection}");

            if (radarReport.Distance < minDistance || radarReport.Distance > maxDistance)
                return ValidationResult.Failure($"invalid Distance : must be between {minDistance} and {maxDistance}");
           
            return ValidationResult.Success();
            
                
            
        }


    }
}
