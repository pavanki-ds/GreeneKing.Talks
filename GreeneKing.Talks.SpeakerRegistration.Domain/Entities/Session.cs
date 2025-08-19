using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreeneKing.Talks.SpeakerRegistration.Domain.Entities
{
    public sealed class Session
    {
        public string Title { get; }
        public string Description { get; }
        public bool IsApproved { get; set; }

        public Session(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
