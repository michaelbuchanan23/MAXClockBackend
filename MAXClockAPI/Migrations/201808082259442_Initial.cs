namespace MAXClockAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Active = c.Boolean(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Instructor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Instructors", t => t.Instructor_Id)
                .Index(t => t.Instructor_Id);
            
            CreateTable(
                "dbo.Instructors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        PIN = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        PIN = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Timestamp_Id = c.Int(),
                        Class_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Timestamps", t => t.Timestamp_Id)
                .ForeignKey("dbo.Classes", t => t.Class_Id)
                .Index(t => t.Timestamp_Id)
                .Index(t => t.Class_Id);
            
            CreateTable(
                "dbo.Timestamps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        ClassId = c.Int(nullable: false),
                        PIN = c.Int(nullable: false),
                        TimeIn = c.DateTime(),
                        TimeOut = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "Class_Id", "dbo.Classes");
            DropForeignKey("dbo.Students", "Timestamp_Id", "dbo.Timestamps");
            DropForeignKey("dbo.Classes", "Instructor_Id", "dbo.Instructors");
            DropIndex("dbo.Students", new[] { "Class_Id" });
            DropIndex("dbo.Students", new[] { "Timestamp_Id" });
            DropIndex("dbo.Classes", new[] { "Instructor_Id" });
            DropTable("dbo.Timestamps");
            DropTable("dbo.Students");
            DropTable("dbo.Instructors");
            DropTable("dbo.Classes");
        }
    }
}
