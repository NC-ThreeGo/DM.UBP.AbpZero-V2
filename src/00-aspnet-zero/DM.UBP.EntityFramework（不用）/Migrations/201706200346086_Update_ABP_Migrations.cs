namespace DM.UBP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_ABP_Migrations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpLanguages", "IsDisabled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpLanguages", "IsDisabled");
        }
    }
}
