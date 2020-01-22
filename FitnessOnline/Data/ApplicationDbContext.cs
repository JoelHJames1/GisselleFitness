using System;
using System.Collections.Generic;
using System.Text;
using FitnessOnline.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessOnline.Data
{
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<MealPlan> MealPlans { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<LocationSchedule> LocationSchedules { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ApplicationUser>().HasMany(s => s.MealPlans).WithOne(s => s.ApplicationUser);


            base.OnModelCreating(modelBuilder);


        }
  

    }
}
