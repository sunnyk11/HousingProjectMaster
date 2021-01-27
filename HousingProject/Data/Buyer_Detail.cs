using HousingProject.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HousingProject.Data
{
    public class Buyer_Detail
    {
        [Key]
        public int BuyerId { get; set; } = 1000;

        [Display(Name ="First Name")]
        public string BuyerName { get; set; }

        [Display(Name = "Middle Name")]
        public string BuyerMiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string BuyerLastName { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        [Display(Name = "Aadhar Card")]
        public string AadharCard { get; set; }

        [Display(Name = "Pan Card")]
        public string PanCard { get; set; }

        [Display(Name = "Gross Annual Income")]
        public string GrossAnnualIncome { get; set; }

        [Display(Name = "Gross Monthly Income")]
        public string GrossMonthlyIncome { get; set; }

        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }

        [Display(Name = "Already Loan")]
        public Nullable<bool> AlreadyLoan { get; set; }
        
        [ForeignKey("ManagerDetail")]
        public string ManagerAssigned { get; set; }

        [Display(Name = "Current Status")]
        public string currenStatus { get; set; }

        [Display(Name = "Living In")]
        public string currentlyLivingIn { get; set; }

        //other Information
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name ="Date of Birth")]
        public string dob { get; set; }

        [Display(Name = "Father First Name")]
        public string FatherFirstName { get; set; }

        [Display(Name = "Father Middle Name")]
        public string FatherMiddleName { get; set; }

        [Display(Name = "Father Last Name")]
        public string FatherLastName { get; set; }

        [Display(Name = "Mother First Name")]
        public string MotherFirstName { get; set; }

        [Display(Name = "Mother Middle Name")]
        public string MotherMiddleName { get; set; }

        [Display(Name = "Mother Last Name")]
        public string MotherLastName { get; set; }

        [Display(Name = "Personal Comments")]
        public string PersonalComments { get; set; }

        //Current Residence Details
        [Display(Name = "Ownership")]
        public string Ownership { get; set; }

        [Display(Name = "Living @address Since")]
        public string LivingSince { get; set; }

        [Display(Name = "Living in city Since")]
        public string LivingInCitySince { get; set; }

        [Display(Name = "Home Address")]
        public string Address { get; set; }

        [Display(Name = "Address2")]
        public string Address2 { get; set; }

        [Display(Name = "City")]
        public Nullable<int> City { get; set; }

        [Display(Name = "State")]
        public Nullable<int> State { get; set; }

        [Display(Name = "Pincode")]
        public string pincode { get; set; }

        //employment Details
        [Display(Name = "Employment Type")]
        public string EmploymentStatus { get; set; }

        [Display(Name = "Organization Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Salary Credit Type")]
        public string SalaryCreditType { get; set; }

        [Display(Name = "Month & Year of Joining")]
        public string JoiningYearandMonth { get; set; }

        [Display(Name = "Monthly Salary")]
        public string MonthlySalary { get; set; }

        [Display(Name = "Constitution of Company")]
        public string ConstitutionOfCompany { get; set; }

        //office address
        [Display(Name = "Address Line 1")]
        public string officeAddressLine { get; set; }

        [Display(Name = "Address Line 2")]
        public string officeAddressLine2 { get; set; }

        [Display(Name = "City")]
        public Nullable<int> OfficeCity { get; set; }

        [Display(Name = "State")]
        public Nullable<int> OfficeState { get; set; }

        [Display(Name = "Pincode")]
        public string Officepincode { get; set; }


        [Display(Name= "LoanAmount")]
        public string LoanAmount { get; set; }

        public ApplicationUser ManagerDetail { get; set; }
        public string LeadIncomingFrom { get; set; }
        public string LeadCreatedBy { get; set; }
        public bool IsAlreadyCustomer { get; set; }

        [ForeignKey("LeadStatus")]
        public int LeadStatusId { get; set; }
        public Nullable<bool> IsConverted { get; set; }
        public DateTime CreatedOn { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
        public ICollection<CustomerContacts> Contacts { get; set; }
        public ICollection<Opportunity> Opportunities { get; set; }
        public ICollection<DocumentUploaded> DocumentUploadeds { get; set; }
        public ICollection<LeadNotes> LeadNotes { get; set; }
        public LeadStatus LeadStatus { get; set; }

        [NotMapped]
        public string Title { get; set; }
    }
}