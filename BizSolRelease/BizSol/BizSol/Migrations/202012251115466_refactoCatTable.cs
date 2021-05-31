namespace BizSol.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactoCatTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Category", "cat_Image");
            DropColumn("dbo.Category", "ad_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Category", "ad_Id", c => c.Int());
            AddColumn("dbo.Category", "cat_Image", c => c.String());
        }
    }
}
