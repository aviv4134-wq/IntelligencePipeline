using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Validation
{
    class SoldierValidator : BaseValidator
    {
        protected override ValidationResult ValidateSpecificFields(Report report)
        {
            int minSoldierNameLength = 2;
            int maxSoldierNameLength = 50;

            int SoldierIdLength = 7;

            int minUnitLength = 2;
            int maxUnitLength = 50;

            int minConfidenceLevel = 1;
            int maxConfidenceLevel = 5;

            if (report is not SoldierReport soldierReport)
                return ValidationResult.Failure("type not allowed");
            
            if (soldierReport.SoldierName.Length < minSoldierNameLength || soldierReport.SoldierName.Length > maxSoldierNameLength)
                return ValidationResult.Failure($"Invalid SoldierName : must be between {minSoldierNameLength} and {maxSoldierNameLength}");
            
            if (soldierReport.SoldierID.Count() != SoldierIdLength)
                return ValidationResult.Failure($"invalid SoldierID: must be {SoldierIdLength} char");
            
            if (soldierReport.Unit.Count() < minUnitLength || soldierReport.Unit.Count() > maxUnitLength)
                return ValidationResult.Failure($"invalid Unit: must be between {minUnitLength} and {maxUnitLength}");
            
            if (soldierReport.ConfidenceLevel < minConfidenceLevel || soldierReport.ConfidenceLevel > maxConfidenceLevel)
                return ValidationResult.Failure($"invalid ConfidenceLevel: must be between {minConfidenceLevel} and {maxConfidenceLevel}");

            return ValidationResult.Success();
        }

    }
}
