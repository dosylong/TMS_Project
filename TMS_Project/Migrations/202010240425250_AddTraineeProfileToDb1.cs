namespace TMS_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTraineeProfileToDb1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TraineeProfiles", "TraineeId", "dbo.AspNetUsers");
            DropIndex("dbo.TraineeProfiles", new[] { "TraineeId" });
            AlterColumn("dbo.TraineeProfiles", "TraineeId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.TraineeProfiles", "TraineeId");
            AddForeignKey("dbo.TraineeProfiles", "TraineeId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TraineeProfiles", "TraineeId", "dbo.AspNetUsers");
            DropIndex("dbo.TraineeProfiles", new[] { "TraineeId" });
            AlterColumn("dbo.TraineeProfiles", "TraineeId", c => c.String(maxLength: 128));
            CreateIndex("dbo.TraineeProfiles", "TraineeId");
            AddForeignKey("dbo.TraineeProfiles", "TraineeId", "dbo.AspNetUsers", "Id");
        }
    }
}
