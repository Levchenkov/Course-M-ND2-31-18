namespace Data.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class keyTwit : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Twits");
            AlterColumn("dbo.Twits", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Twits", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Twits");
            AlterColumn("dbo.Twits", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Twits", "Id");
            
        }
    }
}
