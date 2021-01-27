using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using HousingProject.Models;
using HousingProject.Data;

namespace HousingProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string CurrentlyLivingIn { get; set; }
        public string MaritalStatus { get; set; }
        public string AadharCard { get; set; }
        public string pincode { get; set; }
        public string MobileNo { get; set; }
        public string LeadAssigned { get; set; }
        public int UserType { get; set; }
        public int UserRole { get; set; } = 1;
        public int LeadCreated { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
            set
            {

            }
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ListerStatus> ListerStatuses { get; set; }
        public DbSet<ListerNotes> ListerNotes { get; set; }
        public DbSet<ListerDocumentsUploaded> ListerDocumentsUploadeds { get; set; }
        public DbSet<PropertyLister> PropertyListers { get; set; }
        public DbSet<RejectedLead> RejectedLeads { get; set; }
        public DbSet<OpportunityStatus> OpportunityStatuses { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<DealerToManagerRelation> DealerToManagerRelations { get; set; }
        public DbSet<Buyer_Detail> BuyerDetails { get; set; }
        public DbSet<DealerToLeadRelation> DealerLeadRelations { get; set; }
        public DbSet<BuyerRelation> BuyerRelation { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerContacts> CustomerContacts { get; set; }
        public DbSet<LeadStatus> LeadStatuses { get; set; }
        public DbSet<LeadNotes> LeadNotes { get; set; }
        public DbSet<DocumentUploaded> DocumentUploadeds { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}