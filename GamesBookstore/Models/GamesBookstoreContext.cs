using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GamesBookstore.Models
{
    public class GamesBookstoreContext : DbContext
    {
        public GamesBookstoreContext(DbContextOptions opts) : base(opts)
        {
            Database.EnsureCreated();
        }
        public DbSet<GamesBookstoreItem> GamesBookstoreItems { get; set; }

        public static readonly ILoggerFactory GBLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseLoggerFactory(GBLoggerFactory);
    }
}
