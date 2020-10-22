namespace TMS_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrainerProfileToDb1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TrainerProfiles", "TrainerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.TrainerProfiles", "TrainerId");
            AddForeignKey("dbo.TrainerProfiles", "TrainerId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainerProfiles", "TrainerId", "dbo.AspNetUsers");
            DropIndex("dbo.TrainerProfiles", new[] { "TrainerId" });
            AlterColumn("dbo.TrainerProfiles", "TrainerId", c => c.String());
        }
    }
}
