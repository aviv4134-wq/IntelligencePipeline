using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Validation
{
    class DroneValidator : BaseValidator
    {
        protected override ValidationResult ValidateSpecificFields(Report report)
        {
            int minAltitude = 10;
            int maxAltitude = 10000;

            int minImageQuality = 1;
            int maxImageQuality = 100;


            if (report is not DroneReport droneReport)
                return ValidationResult.Failure("type not allowed");
           
            if (droneReport.Altitude < minAltitude || droneReport.Altitude > maxAltitude)
                return ValidationResult.Failure($"invalid Altitude: must be between {minAltitude} and {maxAltitude}");
            
            if (droneReport.ImageQuality < minImageQuality || droneReport.ImageQuality > maxImageQuality)
                return ValidationResult.Failure($"invalid ImageQuality: must be between {minImageQuality} and {maxImageQuality}");
            
            return ValidationResult.Success();
        }

    }
}
