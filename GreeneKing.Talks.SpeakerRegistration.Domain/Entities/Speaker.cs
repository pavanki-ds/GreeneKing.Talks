using GreeneKing.Talks.SpeakerRegistration.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreeneKing.Talks.SpeakerRegistration.Domain.Entities
{
    public sealed class Speaker
    {
        public int Id { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public int YearsOfExperience { get; }
        public bool HasBlog { get; }
        public string Employer { get; }
        public decimal RegistrationFee { get; private set; }

        private List<Certification> _certifications = new();
        public IReadOnlyCollection<Certification> Certifications => _certifications.AsReadOnly();

        private List<Session> _sessions = new();
        public IReadOnlyCollection<Session> Sessions => _sessions.AsReadOnly();

        public Speaker(string firstName, string lastName, string email, int yearsOfExperience, bool hasBlog, string employer)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            YearsOfExperience = yearsOfExperience;
            HasBlog = hasBlog;
            Employer = employer;
        }

        public void AddCertificaton(Certification certification)
        {
            if (certification is null)
                throw new ArgumentNullException(nameof(certification));

            _certifications.Add(certification);
        }

        public void AddSession(Session session)
        {
            if (session is null)
                throw new ArgumentNullException(nameof(session));

            _sessions.Add(session);
        }

        public bool DoesMeetStandards(RegistrationStandardRules rules)
        {
            if (YearsOfExperience > 10 
                || HasBlog 
                || Certifications.Count > 3 
                || rules.TopEmployers.Any(x => string.Equals(x, Employer, StringComparison.OrdinalIgnoreCase)))
                return true;

            var domain = Email.Split('@')[1];
            if (!rules.ExcludeDomains.Any(x => string.Equals(x, domain, StringComparison.OrdinalIgnoreCase)))
                return true;

            return false;
        }

        public void SetSessionApproval(RegistrationStandardRules rules)
        {
            foreach (var session in _sessions)
            {
                session.IsApproved = !rules.ExcludeTechnologies.Any(x => session.Title.Contains(x, StringComparison.OrdinalIgnoreCase)
                                                                    || session.Description.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
        }

        public void SetRegistrationFee(RegistrationFeeRules rules)
        {
            int minYearsRange = rules.Ranges.Min(x => x.MinYears);
            int maxYearsRange = rules.Ranges.Max(x => x.MaxYears);

            if (YearsOfExperience < minYearsRange || YearsOfExperience  > maxYearsRange)
                throw new ArgumentException($"Invalid years of experience");

            foreach (var range in rules.Ranges)
            {
                if (YearsOfExperience >= range.MinYears && YearsOfExperience <= range.MaxYears)
                    RegistrationFee = range.Fee;
            }
        }
    }
}
