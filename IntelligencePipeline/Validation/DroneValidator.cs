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
            //megic number !!!
            if (report is not DroneReport droneReport)
                return ValidationResult.Failure("type not allowed");

            //if (report.GetSourceType() != "Dron")
            //    return ValidationResult.Failure("type not allowed");

            if (droneReport.Altitude < 10 || droneReport.Altitude > 10000)
                return ValidationResult.Failure("altitude is out of allowed range");
            if (droneReport.ImageQuality < 1 || droneReport.ImageQuality > 100)
                return ValidationResult.Failure("image quality is out of allowed range");
            return ValidationResult.Success();
        }

    }
}
