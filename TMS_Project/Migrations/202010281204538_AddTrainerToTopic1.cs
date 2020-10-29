namespace TMS_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrainerToTopic1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TrainerToTopics", "TrainerId", "dbo.AspNetUsers");
            DropIndex("dbo.TrainerToTopics", new[] { "TrainerId" });
            AlterColumn("dbo.TrainerToTopics", "TrainerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.TrainerToTopics", "TrainerId");
            AddForeignKey("dbo.TrainerToTopics", "TrainerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainerToTopics", "TrainerId", "dbo.AspNetUsers");
            DropIndex("dbo.TrainerToTopics", new[] { "TrainerId" });
            AlterColumn("dbo.TrainerToTopics", "TrainerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.TrainerToTopics", "TrainerId");
            AddForeignKey("dbo.TrainerToTopics", "TrainerId", "dbo.AspNetUsers", "Id");
        }
    }
}
