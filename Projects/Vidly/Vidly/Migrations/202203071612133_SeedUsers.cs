namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9e5a1925-56f2-4fdc-ab06-68041c71ff55', N'admin@vidly.com', 0, N'ACbAZaml1TqyhE71xVHq4ohoVOMWDRK5VvgKjmGvmFSsVGk/zG7EIaB9GDvLgTIU9A==', N'635ff075-94de-48d7-a55b-a3c39b801d2f', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'bde7bf2d-3948-4d01-b644-cb72f4d03661', N'guest@vidly.com', 0, N'AIT0a5Jqnt/Kn6qZKtIW8P/CGfTFlXV+vWElxQhXwQX68lBelipECamdhqlmRhHMuw==', N'6429df2d-7f37-4893-afe1-46511c6a12f7', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'44432da1-e6f7-4981-a162-f23e648a53ab', N'CanManageMovie')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9e5a1925-56f2-4fdc-ab06-68041c71ff55', N'44432da1-e6f7-4981-a162-f23e648a53ab')
            ");
        }

        public override void Down()
        {
        }
    }
}
