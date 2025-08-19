using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreeneKing.Talks.SpeakerRegistration.Domain.ValueObjects
{
    public sealed class Certification
    {
        public string Name { get; }

        public Certification(string name)
        {
            Name = name;
        }
    }
}
