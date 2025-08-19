using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreeneKing.Talks.SpeakerRegistration.Domain.ValueObjects
{
    public class RegistrationFeeRules
    {
        public List<FeeRange> Ranges { get; set; } = new();
    }

    public class FeeRange
    {
        public int MinYears { get; set; }
        public int MaxYears { get; set; }
        public decimal Fee { get; set; }
    }
}
