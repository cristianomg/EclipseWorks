﻿using TaskManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace TaskManager.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<User> Users { get; private set; }
        public DbSet<Tasks> Tasks { get; private set; }
        public DbSet<Project> Projects { get; private set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

    }
}
