using GreeneKing.Talks.SpeakerRegistration.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreeneKing.Talks.SpeakerRegistration.Application.Models
{
    public class RegistrationError
    {
        public RegistrationErrorType ErrorType { get; }
        public string? ErrorMessage { get; }

        public RegistrationError(RegistrationErrorType errorType)
        {
            ErrorType = errorType;
        }
        public RegistrationError(RegistrationErrorType errorType, string errorMessage)
        {
            ErrorType = errorType;
            ErrorMessage = errorMessage;
        }
    }
}
