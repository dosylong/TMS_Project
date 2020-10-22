namespace TMS_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTraineeProfileToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TraineeProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TraineeId = c.String(maxLength: 128),
                        Full_Name = c.String(),
                        Education = c.String(),
                        Programming_Language = c.String(),
                        Experience_Details = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.TraineeId)
                .Index(t => t.TraineeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TraineeProfiles", "TraineeId", "dbo.AspNetUsers");
            DropIndex("dbo.TraineeProfiles", new[] { "TraineeId" });
            DropTable("dbo.TraineeProfiles");
        }
    }
}
