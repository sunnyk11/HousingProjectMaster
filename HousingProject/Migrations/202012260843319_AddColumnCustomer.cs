namespace HousingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnCustomer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buyer_Detail",
                c => new
                    {
                        BuyerId = c.Int(nullable: false, identity: true),
                        BuyerName = c.String(),
                        BuyerMiddleName = c.String(),
                        BuyerLastName = c.String(),
                        Gender = c.String(),
                        MaritalStatus = c.String(),
                        AadharCard = c.String(),
                        PanCard = c.String(),
                        GrossAnnualIncome = c.String(),
                        GrossMonthlyIncome = c.String(),
                        MobileNo = c.String(),
                        AlreadyLoan = c.Boolean(),
                        ManagerAssigned = c.String(maxLength: 128),
                        currenStatus = c.String(),
                        currentlyLivingIn = c.String(),
                        Email = c.String(),
                        dob = c.String(),
                        FatherFirstName = c.String(),
                        FatherMiddleName = c.String(),
                        FatherLastName = c.String(),
                        MotherFirstName = c.String(),
                        MotherMiddleName = c.String(),
                        MotherLastName = c.String(),
                        PersonalComments = c.String(),
                        Ownership = c.String(),
                        LivingSince = c.String(),
                        LivingInCitySince = c.String(),
                        Address = c.String(),
                        Address2 = c.String(),
                        City = c.Int(),
                        State = c.Int(),
                        pincode = c.String(),
                        EmploymentStatus = c.String(),
                        CompanyName = c.String(),
                        SalaryCreditType = c.String(),
                        JoiningYearandMonth = c.String(),
                        MonthlySalary = c.String(),
                        ConstitutionOfCompany = c.String(),
                        officeAddressLine = c.String(),
                        officeAddressLine2 = c.String(),
                        OfficeCity = c.Int(),
                        OfficeState = c.Int(),
                        Officepincode = c.String(),
                        LoanAmount = c.String(),
                        LeadPosition = c.Int(nullable: false),
                        TransferRequestStatus = c.Int(nullable: false),
                        LeadIncomingFrom = c.String(),
                        LeadCreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.BuyerId)
                .ForeignKey("dbo.AspNetUsers", t => t.ManagerAssigned)
                .Index(t => t.ManagerAssigned);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.String(),
                        Gender = c.String(),
                        CurrentlyLivingIn = c.String(),
                        MaritalStatus = c.String(),
                        AadharCard = c.String(),
                        pincode = c.String(),
                        MobileNo = c.String(),
                        LeadAssigned = c.String(),
                        UserType = c.Int(nullable: false),
                        UserRole = c.Int(nullable: false),
                        LeadCreated = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.BuyerRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManagerId = c.String(maxLength: 128),
                        BuyerId = c.Int(nullable: false),
                        MemberId = c.String(maxLength: 128),
                        AssistantManagerId = c.String(maxLength: 128),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AssistantManagerId)
                .ForeignKey("dbo.Buyer_Detail", t => t.BuyerId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ManagerId)
                .ForeignKey("dbo.AspNetUsers", t => t.MemberId)
                .Index(t => t.ManagerId)
                .Index(t => t.BuyerId)
                .Index(t => t.MemberId)
                .Index(t => t.AssistantManagerId);
            
            CreateTable(
                "dbo.CustomerContacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        Contact1Mobile = c.String(),
                        Contact1Name = c.String(),
                        Contact2Mobile = c.String(),
                        Contact2Name = c.String(),
                        Contact3Mobile = c.String(),
                        Contact3Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        MobileNo = c.String(),
                        AadharCard = c.String(),
                        Name = c.String(),
                        PanCard = c.String(),
                        Email = c.String(),
                        BuyerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.DealerToLeadRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuyerId = c.Int(nullable: false),
                        PhoneNo = c.String(),
                        AadharCard = c.String(),
                        PanCard = c.String(),
                        WillGetPayout = c.Boolean(nullable: false),
                        BuyerName = c.String(),
                        AssignedManager = c.String(maxLength: 128),
                        AssignedAssistantManager = c.String(maxLength: 128),
                        AssignedTeamMember = c.String(maxLength: 128),
                        LeadCreatedOn = c.DateTime(nullable: false),
                        LeadStatus = c.String(),
                        LeadCreatedBy = c.String(),
                        LeadVerifiedBy = c.String(),
                        DealerDetails_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AssignedAssistantManager)
                .ForeignKey("dbo.Buyer_Detail", t => t.BuyerId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.DealerDetails_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AssignedManager)
                .ForeignKey("dbo.AspNetUsers", t => t.AssignedTeamMember)
                .Index(t => t.BuyerId)
                .Index(t => t.AssignedManager)
                .Index(t => t.AssignedAssistantManager)
                .Index(t => t.AssignedTeamMember)
                .Index(t => t.DealerDetails_Id);
            
            CreateTable(
                "dbo.DealerToManagerRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DealerId = c.String(maxLength: 128),
                        ManagerId = c.String(maxLength: 128),
                        BuyerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buyer_Detail", t => t.BuyerId)
                .ForeignKey("dbo.AspNetUsers", t => t.DealerId)
                .ForeignKey("dbo.AspNetUsers", t => t.ManagerId)
                .Index(t => t.DealerId)
                .Index(t => t.ManagerId)
                .Index(t => t.BuyerId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.DealerToManagerRelations", "ManagerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DealerToManagerRelations", "DealerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DealerToManagerRelations", "BuyerId", "dbo.Buyer_Detail");
            DropForeignKey("dbo.DealerToLeadRelations", "AssignedTeamMember", "dbo.AspNetUsers");
            DropForeignKey("dbo.DealerToLeadRelations", "AssignedManager", "dbo.AspNetUsers");
            DropForeignKey("dbo.DealerToLeadRelations", "DealerDetails_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.DealerToLeadRelations", "BuyerId", "dbo.Buyer_Detail");
            DropForeignKey("dbo.DealerToLeadRelations", "AssignedAssistantManager", "dbo.AspNetUsers");
            DropForeignKey("dbo.CustomerContacts", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.BuyerRelations", "MemberId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BuyerRelations", "ManagerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BuyerRelations", "BuyerId", "dbo.Buyer_Detail");
            DropForeignKey("dbo.BuyerRelations", "AssistantManagerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Buyer_Detail", "ManagerAssigned", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.DealerToManagerRelations", new[] { "BuyerId" });
            DropIndex("dbo.DealerToManagerRelations", new[] { "ManagerId" });
            DropIndex("dbo.DealerToManagerRelations", new[] { "DealerId" });
            DropIndex("dbo.DealerToLeadRelations", new[] { "DealerDetails_Id" });
            DropIndex("dbo.DealerToLeadRelations", new[] { "AssignedTeamMember" });
            DropIndex("dbo.DealerToLeadRelations", new[] { "AssignedAssistantManager" });
            DropIndex("dbo.DealerToLeadRelations", new[] { "AssignedManager" });
            DropIndex("dbo.DealerToLeadRelations", new[] { "BuyerId" });
            DropIndex("dbo.CustomerContacts", new[] { "CustomerId" });
            DropIndex("dbo.BuyerRelations", new[] { "AssistantManagerId" });
            DropIndex("dbo.BuyerRelations", new[] { "MemberId" });
            DropIndex("dbo.BuyerRelations", new[] { "BuyerId" });
            DropIndex("dbo.BuyerRelations", new[] { "ManagerId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Buyer_Detail", new[] { "ManagerAssigned" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.DealerToManagerRelations");
            DropTable("dbo.DealerToLeadRelations");
            DropTable("dbo.Customers");
            DropTable("dbo.CustomerContacts");
            DropTable("dbo.BuyerRelations");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Buyer_Detail");
        }
    }
}
