using Microsoft.EntityFrameworkCore;
using OneNote.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.SQLite
{
    public class Connection : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<HistoryRecord> HistoryRecords { get; set; }
        public DbSet<HistoryDetail> HistoryDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=mydb.db");
        }
    }
}
