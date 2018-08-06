namespace MAXClockAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class spellingfixedonstudent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "CheckedIn", c => c.Boolean(nullable: false));
            DropColumn("dbo.Students", "ChekedIn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "ChekedIn", c => c.Boolean(nullable: false));
            DropColumn("dbo.Students", "CheckedIn");
        }
    }
}
