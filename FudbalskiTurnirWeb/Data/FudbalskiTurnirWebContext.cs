using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FudbalskiTurnirWeb.Models;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FudbalskiTurnirWeb.Data
{
    public class FudbalskiTurnirWebContext : IdentityDbContext
    {

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Entity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            e.IsActive = true;
                            e.CreatedAt = DateTime.Now;
                            e.IsDeleted = false;
                            e.UpdatedAt = null;
                            e.DeletedAt = null;
                            break;
                        case EntityState.Modified:
                            e.UpdatedAt = DateTime.Now;
                            break;
                    }
                }
            }
            int result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Match>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Match>()
                .HasOne(x => x.AwayTeam)
                .WithMany(y => y.AwayTeams)
                .HasForeignKey(x => x.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(x => x.HomeTeam)
                .WithMany(x => x.HomeTeams)
                .HasForeignKey(x => x.HomeTeamId);



        }
        public FudbalskiTurnirWebContext (DbContextOptions<FudbalskiTurnirWebContext> options)
            : base(options)
        {
        }

        public DbSet<FudbalskiTurnirWeb.Models.Team> Team { get; set; } = default!;

        public DbSet<FudbalskiTurnirWeb.Models.Player>? Player { get; set; }

        public DbSet<FudbalskiTurnirWeb.Models.Match>? Match { get; set; }
    }
}
