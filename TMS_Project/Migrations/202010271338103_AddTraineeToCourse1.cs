namespace TMS_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTraineeToCourse1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TraineeToCourses", "TraineeId", "dbo.AspNetUsers");
            DropIndex("dbo.TraineeToCourses", new[] { "TraineeId" });
            AlterColumn("dbo.TraineeToCourses", "TraineeId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.TraineeToCourses", "TraineeId");
            AddForeignKey("dbo.TraineeToCourses", "TraineeId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TraineeToCourses", "TraineeId", "dbo.AspNetUsers");
            DropIndex("dbo.TraineeToCourses", new[] { "TraineeId" });
            AlterColumn("dbo.TraineeToCourses", "TraineeId", c => c.String(maxLength: 128));
            CreateIndex("dbo.TraineeToCourses", "TraineeId");
            AddForeignKey("dbo.TraineeToCourses", "TraineeId", "dbo.AspNetUsers", "Id");
        }
    }
}
