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
            DateTime invalidTimestempPast = new DateTime(2020,01,01);
            double minLatitude = 29.5000;
            double maxLatitude = 33.5000;
            
            double minlongitude = 34.0000;
            double maxlongitude = 36.0000;

            int minDescriptionLength = 10;
            int maxDescriptionLength = 50;

            if (report.Timestamp > DateTime.Now)
                return ValidationResult.Failure("invalid Timestemp: must not be in the future");
            
            if (report.Timestamp < invalidTimestempPast)
                return  ValidationResult.Failure($"invalid Timestemp: must not before {invalidTimestempPast}");

            if ( report.Latitude < minLatitude ||  report.Latitude > maxLatitude)
                return ValidationResult.Failure($"invalid latitude: must be between {minLatitude} and {maxLatitude}");
            
            if (report.Longitude < minLatitude || report.Longitude > maxLatitude)
                return ValidationResult.Failure($"invalid longitude: must be between {minlongitude} and {maxlongitude}");
            
            if (report.Description == null || report.Description.IsWhiteSpace())
                return ValidationResult.Failure("the discription is empty");
            
            if (report.Description.Length < minDescriptionLength || report.Description.Length >  maxDescriptionLength)
                return ValidationResult.Failure($"invalid Description: length must be between {minDescriptionLength} and {maxDescriptionLength}");
            return ValidationResult.Success();


        }

        protected abstract ValidationResult ValidateSpecificFields(Report report);
    }
}
