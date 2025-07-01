using System;

namespace MyApp.Data;

using Microsoft.EntityFrameworkCore;
using MyApp.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        this.Database.EnsureCreated();
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Student>()
    //         .HasOne(s => s.Address)
    //         .WithOne(a => a.Student)
    //         .HasForeignKey<Address>(a => a.StudentId);

    //     modelBuilder.Entity<StudentCourse>()
    //         .HasKey(sc => new { sc.StudentId, sc.CourseId });

    //     modelBuilder.Entity<StudentCourse>()
    //         .HasOne(sc => sc.Student)
    //         .WithMany(s => s.StudentCourses)
    //         .HasForeignKey(sc => sc.StudentId);

    //     modelBuilder.Entity<StudentCourse>()
    //         .HasOne(sc => sc.Course)
    //         .WithMany(c => c.StudentCourses)
    //         .HasForeignKey(sc => sc.CourseId);
    // }
}
