using System;
using System.Collections.Generic;

namespace LibrarySystem.Core.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string MemberId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime MemberSince { get; set; } = DateTime.Now;

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();

        public Member()
        {
        }

        public Member(string memberId, string name, string email)
        {
            MemberId = memberId;
            Name = name;
            Email = email;
            MemberSince = DateTime.Now;
        }

        public string GetInfo()
        {
            return $"Medlem: {Name} (ID: {MemberId})\nEmail: {Email}\nMedlem sedan: {MemberSince:yyyy-MM-dd}\nAntal aktiva lån: {Loans.Count}";
        }
    }
}