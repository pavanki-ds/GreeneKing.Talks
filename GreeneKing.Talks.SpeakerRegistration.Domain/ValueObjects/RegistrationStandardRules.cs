using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreeneKing.Talks.SpeakerRegistration.Domain.ValueObjects
{
    public class RegistrationStandardRules
    {
        public List<string> ExcludeTechnologies { get; set; } = new();
        public List<string> ExcludeDomains { get; set; } = new();
        public List<string> TopEmployers { get; set; } = new();
    }
}
