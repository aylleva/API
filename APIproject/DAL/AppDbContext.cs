﻿using APIproject.Entity;
using Microsoft.EntityFrameworkCore;

namespace APIproject.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }

      public DbSet<Category> Categories { get; set; }
    }
}