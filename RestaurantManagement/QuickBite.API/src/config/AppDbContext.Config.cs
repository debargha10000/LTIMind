using System;
using Microsoft.EntityFrameworkCore;
using QuickBite.API.src.models.entities;

namespace QuickBite.API.src.config;

public class AppDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<SessionEntity> Sessions { get; set; }
    public DbSet<MenuItemEntity> MenuItems { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
}
