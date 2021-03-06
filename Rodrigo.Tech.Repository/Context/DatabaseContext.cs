﻿using Microsoft.EntityFrameworkCore;
using Rodrigo.Tech.Repository.Tables;

namespace Rodrigo.Tech.Repository.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        #region Tables
        public DbSet<Item> Items { get; set; }
        #endregion
    }
}
