using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Validation
{
    abstract class BaseValidator : IValidator
    {
        public ValidationResult Validate(Report report)
        {
            ValidationResult commonResoultValidation = ValidateCommonFields(report);
            if (commonResoultValidation.IsValid == false) return commonResoultValidation; //if false return and not countinue to next check

            ValidationResult SpecificFieldsValidation = ValidateSpecificFields(report);
            return SpecificFieldsValidation;
             
        }
        protected ValidationResult ValidateCommonFields(Report report)
        {
            //mguc numbers

            //if (report.Timestamp)
            if ( report.Latitude < 29.5000 ||  report.Latitude > 33.5000)
                return ValidationResult.Failure("the latitde not valid");
            if (report.Longitude < 34.0000 || report.Longitude > 36.0000)
                return ValidationResult.Failure("longitude not valid");
            if (report.Description == null || report.Description.IsWhiteSpace())
                return ValidationResult.Failure("the discription is empty");
            if (report.Description.Length < 10 || report.Description.Length > 50)
                return ValidationResult.Failure("not enough description");
            return ValidationResult.Success();


        }

        protected abstract ValidationResult ValidateSpecificFields(Report report);
    }
}
