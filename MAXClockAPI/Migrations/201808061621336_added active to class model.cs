namespace MAXClockAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedactivetoclassmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classes", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Classes", "Active");
        }
    }
}
