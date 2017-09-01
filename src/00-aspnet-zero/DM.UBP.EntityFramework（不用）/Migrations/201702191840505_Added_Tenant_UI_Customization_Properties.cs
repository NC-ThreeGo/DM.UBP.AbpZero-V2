namespace DM.UBP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Tenant_UI_Customization_Properties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpTenants", "CustomCssId", c => c.Guid());
            AddColumn("dbo.AbpTenants", "LogoId", c => c.Guid());
            AddColumn("dbo.AbpTenants", "LogoFileType", c => c.String(maxLength: 64));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpTenants", "LogoFileType");
            DropColumn("dbo.AbpTenants", "LogoId");
            DropColumn("dbo.AbpTenants", "CustomCssId");
        }
    }
}
