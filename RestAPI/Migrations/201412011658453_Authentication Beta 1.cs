namespace RestAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuthenticationBeta1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ACCESS_TOKENS",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccessToken = c.String(),
                        Created = c.DateTime(nullable: false),
                        Expiration = c.DateTime(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BACKEND_USER", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.BACKEND_USER",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100, unicode: false),
                        Surname = c.String(maxLength: 100, unicode: false),
                        Email = c.String(name: "E-mail", nullable: false, maxLength: 100, unicode: false),
                        Password = c.String(maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ACCESS_TOKENS", "User_Id", "dbo.BACKEND_USER");
            DropIndex("dbo.BACKEND_USER", new[] { "E-mail" });
            DropIndex("dbo.ACCESS_TOKENS", new[] { "User_Id" });
            DropTable("dbo.BACKEND_USER");
            DropTable("dbo.ACCESS_TOKENS");
        }
    }
}
