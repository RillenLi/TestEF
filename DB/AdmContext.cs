using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TestEF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;


namespace TestEF.DB
{
    public class AdmContext:DbContext
    {
        private string connectionString;
        public DbSet<Domain> Domains { get; set; }
        public DbSet<Alias> Aliases { get; set; }
        public DbSet<Mailbox> Mailboxes { get; set; }
        public AdmContext():base()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();
            connectionString = configuration.GetConnectionString("DefaultConnection").ToString();
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
