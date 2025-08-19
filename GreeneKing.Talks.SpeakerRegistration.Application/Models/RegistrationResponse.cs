using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreeneKing.Talks.SpeakerRegistration.Application.Models
{
    public  class RegistrationResponse
    {
        public int? Id { get; set; }
        public RegistrationError? Error { get; }

        public RegistrationResponse(int id)
        {
            Id = id;
        }

        public RegistrationResponse(RegistrationError error)
        {
            Error = error;
        }
    }
}
