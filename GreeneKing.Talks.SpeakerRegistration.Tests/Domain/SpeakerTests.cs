using GreeneKing.Talks.SpeakerRegistration.Domain.Entities;
using GreeneKing.Talks.SpeakerRegistration.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreeneKing.Talks.SpeakerRegistration.Tests.Domain
{
    public class SpeakerTests
    {
        private readonly RegistrationStandardRules _registrationStandardRules = new()
        {
            ExcludeDomains = new List<string> { "aol.com", "prodigy.com", "compuserve.com" },
            ExcludeTechnologies = new List<string> { "Cobol", "Punch Cards", "Commodore", "VBScript" },
            TopEmployers = new List<string> { "GK", "Pluralsight", "Microsoft", "Google" }
        };

        private readonly RegistrationFeeRules _registrationFeeRules = new()
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

        [Fact]
        public void DoesMeetStandards_WhenCorrectYearsOfExperience_ReturnsTrue()
        {
            var speaker = new Speaker("Pavan", "Kurma", "p.k@aol.com", 11, false, "fake");

            Assert.True(speaker.DoesMeetStandards(_registrationStandardRules));
        }

        [Fact]
        public void DoesMeetStandards_WhenHasBlog_ReturnsTrue()
        {
            var speaker = new Speaker("Pavan", "Kurma", "p.k@aol.com", 0, true, "fake");

            Assert.True(speaker.DoesMeetStandards(_registrationStandardRules));
        }

        [Fact]
        public void DoesMeetStandards_WhenCorrectCertificationsCount_ReturnsTrue()
        {
            var speaker = new Speaker("Pavan", "Kurma", "p.k@aol.com", 1, false, "fake");
            speaker.AddCertificaton(new Certification("CRT-A"));
            speaker.AddCertificaton(new Certification("CRT-B"));
            speaker.AddCertificaton(new Certification("CRT-C"));
            speaker.AddCertificaton(new Certification("CRT-D"));

            Assert.True(speaker.DoesMeetStandards(_registrationStandardRules));
        }

        [Fact]
        public void DoesMeetStandards_WhenTopEmployer_ReturnsTrue()
        {
            var speaker = new Speaker("Pavan", "Kurma", "p.k@aol.com", 1, false, "gk");

            Assert.True(speaker.DoesMeetStandards(_registrationStandardRules));
        }

        [Fact]
        public void DoesMeetStandards_WhenNotExcludedDomain_ReturnsTrue()
        {
            var speaker = new Speaker("Pavan", "Kurma", "p.k@gk.com", 1, false, "fake");

            Assert.True(speaker.DoesMeetStandards(_registrationStandardRules));
        }

        [Fact]
        public void SetSessionApproval_WhenNotExcludedTech_IsApprovedTrue()
        {
            var speaker = new Speaker("Pavan", "Kurma", "p.k@aol.com", 1, false, "fake");
            speaker.AddSession(new Session("C#", "Session C# Basics"));
            speaker.SetSessionApproval(_registrationStandardRules);

            Assert.Contains(speaker.Sessions, x => x.IsApproved);
        }

        [Fact]
        public void SetSessionApproval_WhenExcludedTech_IsApprovedFalse()
        {
            var speaker = new Speaker("Pavan", "Kurma", "p.k@aol.com", 1, false, "fake");
            speaker.AddSession(new Session("Cobol", "Session Cobol Basics"));
            speaker.SetSessionApproval(_registrationStandardRules);

            Assert.Contains(speaker.Sessions, x => !x.IsApproved);
        }

        [Theory]
        [InlineData(2, 250)]
        [InlineData(6, 50)]
        [InlineData(11, 0)]
        public void SetRegistrationFee_WhenYearsExperienceInRange_CorrectFeeSet(int yearsOfExperience, decimal expectedFee)
        {
            var speaker = new Speaker("Pavan", "Kurma", "p.k@aol.com", yearsOfExperience, false, "fake");
            speaker.SetRegistrationFee(_registrationFeeRules);

            Assert.Equal(expectedFee, speaker.RegistrationFee);
        }

        [Fact]
        public void SetRegistrationFee_WhenYearsExperienceOutRange_ThrowsArgumentException()
        {
            var speaker = new Speaker("Pavan", "Kurma", "p.k@aol.com", 101, false, "fake");
            
            Assert.Throws<ArgumentException>(() => speaker.SetRegistrationFee(_registrationFeeRules));
        }
    }
}
