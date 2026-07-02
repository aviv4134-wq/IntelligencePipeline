using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Validation
{
     class ValidationResult
    {
        private bool _isValid;
        private string _errorMessage;

        public bool IsValid { get =>_isValid; }
        public string ErrorMessage { get => _errorMessage; }

        public ValidationResult(bool isValid, string errorMessage)
        {
            _isValid = isValid;
            _errorMessage = errorMessage;
        }

        public static ValidationResult Success()
        {
             return new ValidationResult(true,"");        
        }
        public static ValidationResult Failure(string errorMessage)
        {
            return new ValidationResult(false, errorMessage);
        }

    }
}
