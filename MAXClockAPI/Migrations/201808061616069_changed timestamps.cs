namespace MAXClockAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedtimestamps : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "ChekedIn", c => c.Boolean(nullable: false));
            DropColumn("dbo.Students", "CeckedIn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "CeckedIn", c => c.Boolean(nullable: false));
            DropColumn("dbo.Students", "ChekedIn");
        }
    }
}
