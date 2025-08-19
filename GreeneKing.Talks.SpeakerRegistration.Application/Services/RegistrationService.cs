using GreeneKing.Talks.SpeakerRegistration.Application.Models;
using GreeneKing.Talks.SpeakerRegistration.Domain.Entities;
using GreeneKing.Talks.SpeakerRegistration.Domain.Enums;
using GreeneKing.Talks.SpeakerRegistration.Domain.Interfaces;
using GreeneKing.Talks.SpeakerRegistration.Domain.ValueObjects;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreeneKing.Talks.SpeakerRegistration.Application.Services
{
    public class RegistrationService
    {
        private readonly ISpeakerRepository _speakerRepository;
        private readonly RegistrationStandardRules _registrationStandardRules;
        private readonly RegistrationFeeRules _registrationFeeRules;

        public RegistrationService(ISpeakerRepository speakerRepository, IOptions<RegistrationStandardRules> registrationStandardRules, IOptions<RegistrationFeeRules> registrationFeeRules)
        {
            _speakerRepository = speakerRepository;
            _registrationStandardRules = registrationStandardRules.Value;
            _registrationFeeRules = registrationFeeRules.Value;
        }

        public RegistrationResponse Register(Speaker speaker)
        {
            if (string.IsNullOrWhiteSpace(speaker.FirstName))
                return new RegistrationResponse(new RegistrationError(RegistrationErrorType.FirstNameRequired));

            if (string.IsNullOrWhiteSpace(speaker.LastName))
                return new RegistrationResponse(new RegistrationError(RegistrationErrorType.LastNameRequired));

            if (string.IsNullOrWhiteSpace(speaker.Email))
                return new RegistrationResponse(new RegistrationError(RegistrationErrorType.EmailRequired));

            if (!speaker.DoesMeetStandards(_registrationStandardRules))
                return new RegistrationResponse(new RegistrationError(RegistrationErrorType.SpeakerDoesNotMeetStandards));

            speaker.SetSessionApproval(_registrationStandardRules);

            if (!speaker.Sessions.Any(x => x.IsApproved))
                return new RegistrationResponse(new RegistrationError(RegistrationErrorType.NoSessionsApproved));

            speaker.SetRegistrationFee(_registrationFeeRules);

            return new RegistrationResponse(_speakerRepository.Save(speaker));
        }
    }
}
