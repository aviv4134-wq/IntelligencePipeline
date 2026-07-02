using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Validation
{
    //class SoldierValidator : BaseValidator
    //{
    //    protected override ValidationResult ValidateSpecificFields(Report report)
    //    {
    //        if (report.GetSourceType() != "Soldier")
    //            return ValidationResult.Failure("type not allowed");
    //        if (!report.SoldierName.Length >= 2 || !report.SoldierName.Length >= 50)
    //            return ValidationResult.Failure("not enugh char to soldier name");
    //        if (report.SoldierID.Lenght != 7)
    //            return ValidationResult.Failure("must be at least 7 numbers in ID ");
    //        if (report.Unit < 2 || report.Unit > 50 )
    //            return ValidationResult.Failure("unit name must be between 2 - 50 char ");
    //        if (report.ConfidenceLevel < 1 || report.ConfidenceLevel > 5)
    //            return ValidationResult.Failure("confidential must be between 2 - 50");

    //        return ValidationResult.Success();
    //    }    
        
    //}
}
