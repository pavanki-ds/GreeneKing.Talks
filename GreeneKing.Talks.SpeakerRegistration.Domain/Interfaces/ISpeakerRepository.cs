using GreeneKing.Talks.SpeakerRegistration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreeneKing.Talks.SpeakerRegistration.Domain.Interfaces
{
    public interface ISpeakerRepository
    {
        int Save(Speaker speaker);
    }
}
