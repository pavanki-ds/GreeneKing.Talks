using GreeneKing.Talks.SpeakerRegistration.Application.Services;
using GreeneKing.Talks.SpeakerRegistration.Domain.Entities;
using GreeneKing.Talks.SpeakerRegistration.Domain.Interfaces;
using GreeneKing.Talks.SpeakerRegistration.Domain.ValueObjects;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreeneKing.Talks.SpeakerRegistration.Tests.Application
{
    public class RegistrationServiceTests
    {
        private readonly Mock<ISpeakerRepository> _speakerRepositoryMock;
        private readonly RegistrationStandardRules _registrationStandardRules;
        private readonly RegistrationFeeRules _registrationFeeRules;
        private readonly RegistrationService _registrationService;

        public RegistrationServiceTests()
        {
            _speakerRepositoryMock = new Mock<ISpeakerRepository>();

            _registrationStandardRules = new()
            {
                ExcludeDomains = new List<string> { "aol.com", "prodigy.com", "compuserve.com" },
                ExcludeTechnologies = new List<string> { "Cobol", "Punch Cards", "Commodore", "VBScript" },
                TopEmployers = new List<string> { "GK", "Pluralsight", "Microsoft", "Google" }
            };

            _registrationFeeRules = new()
            {
                Ranges = new List<FeeRange>()
                {
                    new FeeRange { MinYears = 0, MaxYears = 1, Fee = 500 },
                    new FeeRange { MinYears = 2, MaxYears = 3, Fee = 250 },
                    new FeeRange { MinYears = 4, MaxYears = 5, Fee = 100 },
                    new FeeRange { MinYears = 6, MaxYears = 9, Fee = 50 },
                    new FeeRange { MinYears = 10, MaxYears = 100, Fee = 0 }
                }
            };

            _registrationService = new RegistrationService(_speakerRepositoryMock.Object, Options.Create(_registrationStandardRules), Options.Create(_registrationFeeRules));
        }

        [Fact]
        public void Register_WhenCorrectSpeaker_SavesReturnsId()
        {
            var speaker = new Speaker("Pavan", "Kurma", "p.k@outlook.com", 11, true, "gk");
            speaker.AddSession(new Session("C#", "C#"));

            _speakerRepositoryMock.Setup(x => x.Save(It.IsAny<Speaker>())).Returns(111);

            var result = _registrationService.Register(speaker);

            Assert.Null(result.Error);
            Assert.Equal(111, result.Id);
        }

        [Fact]
        public void Register_WhenNoFields_ReturnsError()
        {
            //
        }

        [Fact]
        public void Register_WhenDoesNotMeetStandards_ReturnsError()
        {
            //
        }

        [Fact]
        public void Register_WhenNoApprovedSessions_ReturnsError()
        {
            //
        }
    }
}
