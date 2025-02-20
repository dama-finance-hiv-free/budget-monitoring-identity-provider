using System;
using Microsoft.AspNetCore.Identity;

namespace IdentityProvider.Data.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() { }
        public ApplicationUser(string userName) : base(userName) { }
        public string FullName { get; set; }
        public string Organization { get; set; }
        public string Locale { get; set; }
        public DateTime? LastLogin { get; set; }
        public int LoginCount { get; set; }
        public string UserCode { get; set; }
        public string ImageUrl { get; set; }
    }
}