using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace IntelligencePipeline.Validation
{
    class SignalValidator : BaseValidator
    {
        protected override ValidationResult ValidateSpecificFields(Report report)
        {
            double minFrequency = 1.0;
            double maxFrequency = 3000.0;

            int minContentLength = 5;
            int maxContentLength = 1000;

            int minSignalStrength = -120;
            int maxSignalStrength = 0;


           if (report is not  SignalReport reportSignal)
                return ValidationResult.Failure("type not allowed");

           if (reportSignal.Frequency < minFrequency || reportSignal.Frequency > maxFrequency)
                return ValidationResult.Failure($"Invalid Frequency: must be between {minFrequency} and {maxFrequency}");
            
           if (reportSignal.Content.Length < minContentLength || reportSignal.Content.Length > maxContentLength)
                return ValidationResult.Failure($"Invalid Content: must be between {minContentLength} and {maxContentLength}");
             
           if (reportSignal.SignalStrength < minSignalStrength || reportSignal.SignalStrength > maxSignalStrength)
                return ValidationResult.Failure($"Invalid SignalStrength: must be between {minSignalStrength} and {maxSignalStrength}");
           return ValidationResult.Success();
        }
    }
}
