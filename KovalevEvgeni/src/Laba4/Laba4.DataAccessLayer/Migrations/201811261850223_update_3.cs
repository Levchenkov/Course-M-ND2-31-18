namespace Laba4.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FullNameUser", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "FullNameUser");
        }
    }
}
