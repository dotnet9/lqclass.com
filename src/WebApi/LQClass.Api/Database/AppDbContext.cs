using LQClass.Api.Dtos;
using LQClass.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace LQClass.Api.Database
{
  public class AppDbContext : IdentityDbContext<ApplicationUser> // DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options)
    {

    }

    public DbSet<TouristRoute> TouristRoutes { get; set; }
    public DbSet<TouristRoutePicture> TouristRoutePictures { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<LineItem> LineItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      var touristRouteID = Guid.NewGuid();
      modelBuilder.Entity<TouristRoute>().HasData(new TouristRoute()
      {
        Id = touristRouteID,
        Title = "测试标题",
        Description = "说明",
        OriginalPrice = 0,
        CreateTime = DateTime.UtcNow,
        Rating = 3.8,
        DepartureCity = DepartureCity.ChengDu,
        TravelDays = TravelDays.Three,
        TripType = TripType.Person
      });
      modelBuilder.Entity<TouristRoutePicture>().HasData(new TouristRoutePicture
      {
        Id = 1,
        Url = "test",
        TouristRouteId = touristRouteID
      });

      // 初始化用户与角色的种子数据
      // 1. 更新用户与角色的外键
      modelBuilder.Entity<ApplicationUser>(u =>
        u.HasMany(x => x.UserRoles)
        .WithOne().HasForeignKey(ur => ur.UserId).IsRequired()
      );

      // 2. 添加管理员角色
      var adminRoleId = Guid.NewGuid().ToString();
      modelBuilder.Entity<IdentityRole>().HasData(
        new IdentityRole()
        {
          Id = adminRoleId,
          Name = "Admin",
          NormalizedName = "Admin".ToUpper()
        }
      );

      // 3.添加用户
      var adminUserId = Guid.NewGuid().ToString();
      ApplicationUser adminUser = new ApplicationUser
      {
        Id = adminUserId,
        UserName = "admin@lqclass.com",
        NormalizedUserName = "admin@lqclass.com".ToUpper(),
        Email = "admin@lqclass.com",
        NormalizedEmail = "admin@lqclass.com".ToUpper(),
        TwoFactorEnabled = false,
        EmailConfirmed = true,
        PhoneNumber = "15965893214",
        PhoneNumberConfirmed = false
      };
      var ph = new PasswordHasher<ApplicationUser>();
      adminUser.PasswordHash = ph.HashPassword(adminUser, "LQClass@163.com");
      modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

      // 4.给用户加入管理员角色
      modelBuilder.Entity<IdentityUserRole<string>>().HasData(
        new IdentityUserRole<string>()
        {
          RoleId = adminRoleId,
          UserId = adminUserId
        }
      );

      base.OnModelCreating(modelBuilder);
    }
  }
}
