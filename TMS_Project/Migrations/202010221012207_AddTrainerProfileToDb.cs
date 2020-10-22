namespace TMS_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrainerProfileToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrainerProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrainerId = c.String(),
                        Full_Name = c.String(),
                        External_Internal = c.String(),
                        Education = c.String(),
                        Working_Place = c.String(),
                        Phone_Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TrainerProfiles");
        }
    }
}
