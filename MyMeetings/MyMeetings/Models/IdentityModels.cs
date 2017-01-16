using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyMeetings.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public virtual ICollection<Publication> Publications { get; set; }
        //public  ICollection<Publication> Subscriptions { get; set; }

        public ApplicationUser()
        {
            Publications = new List<Publication>();
            //Subscriptions = new List<Publication>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim(new Claim("FirstName", FirstName));
            userIdentity.AddClaim(new Claim("Surname", SurName));
            userIdentity.AddClaim(new Claim(ClaimTypes.Gender,Gender));
            userIdentity.AddClaim(new Claim(ClaimTypes.DateOfBirth,DateOfBirth.ToString()));
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Publication> Publications { get; set; }
        public ApplicationDbContext()
            : base("MyMeetings", throwIfV1Schema: false)
        {
    }

    public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext() {};
        }
    }
}