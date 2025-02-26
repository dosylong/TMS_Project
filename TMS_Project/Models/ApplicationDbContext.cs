﻿using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TMS_Project.Models
{

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Topic> Topics { get; set; }

        public DbSet<TraineeToCourse> TraineeToCourses { get; set; }
        public DbSet<TrainerToTopic> TrainerToTopics { get; set; }
        public DbSet<TraineeProfile> TraineeProfiles { get; set; }
        public DbSet<TrainerProfile> TrainerProfiles { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}