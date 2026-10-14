using CargoTrack.Entity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoTrack.DataAccess.Context
{
    public class AppDbContext:IdentityDbContext<AppUser,AppRole, Guid>
    {
        public AppDbContext(DbContextOptions options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo>()
                .HasOne(c => c.Sender)
                .WithMany(b => b.SentCargos)
                .HasForeignKey(c => c.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Cargo>()
                .HasOne(c => c.Receiver)
                .WithMany(b => b.ReceivedCargos)
                .HasForeignKey(c => c.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Cargo>()
                .HasOne(c => c.OriginBranch)
                .WithMany(b => b.OriginCargos)
                .HasForeignKey(c => c.OriginBranchId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Cargo>()
                .HasOne(c => c.DestinationBranch)
                .WithMany(b => b.DestinationCargos)
                .HasForeignKey(c => c.DestinationBranchId)
                .OnDelete(DeleteBehavior.Restrict);
            
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Address> Addresses { get; set; }

    }
}
