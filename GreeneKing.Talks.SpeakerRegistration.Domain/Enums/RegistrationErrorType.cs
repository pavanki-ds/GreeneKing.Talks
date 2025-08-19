using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreeneKing.Talks.SpeakerRegistration.Domain.Enums
{
    public enum RegistrationErrorType
    {
        FirstNameRequired = 10,
        LastNameRequired = 20,
        EmailRequired = 30,
        SpeakerDoesNotMeetStandards = 40,
        NoSessionsProvided = 50,
        NoSessionsApproved = 60
    }
}
