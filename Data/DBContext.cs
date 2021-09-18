using System;
using Microsoft.EntityFrameworkCore;
using DatabaseConnection.Models;

namespace DatabaseConnection.Data {
    public class DBContext : DbContext {
        /*
            In the DBContext we set the DbSet.
        
            public DbSet<ConnectionType> ConnectionTypes { get; set; }
        */
        public DbSet<ConnectionType> ConnectionTypes { get; set; }
        public DbSet<Families> Families { get; set; }
        public DbSet<FamilyConnection> FamilyConnections { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberConnection> MemberConnections { get; set; }

        /*
            Configuring of the connection string to the database.
            Connection should be set in the debug environment variables
            // Here is a guide:

            This is what a tipical connection string would look like. 
            server=ServernameHere;database=DatabaseNameHere;Uid=UsernameHere;Pwd=PasswordHere;
        */
        protected override void OnConfiguring(DbContextOptionsBuilder options) { // If there is a error of it failing ssl handshake try adding SSL Mode=None; at the end of the TreatTinyAsBoolean=True;
            options.UseMySQL(Environment.GetEnvironmentVariable("connection") + "UseAffectedRows=True;TreatTinyAsBoolean=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            /*
                This is the OnModelCreating it is called fluent api.
                Here we set the relations in the database.
                If the primary key is not id then you can use .HasKey(a => a.WhatEverTheVariableIsCalledHere);
            */

            builder.Entity<Member>().ToTable("member").HasKey(a => a.UserID);
            builder.Entity<ConnectionType>().ToTable("connectiontype");
            builder.Entity<Sex>().ToTable("sex");
            builder.Entity<Member>().HasOne(mc => mc.Sex).WithOne(a => a.Member).HasForeignKey<Member>(s => s.SexID).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Families>().ToTable("family");
            builder.Entity<Families>().HasOne(f => f.Creator).WithMany(m => m.CreatorOfFamilies).HasForeignKey(mc => mc.CreatorID).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MemberConnection>().ToTable("memberconnection").HasKey(a => new { a.UserID, a.User2ID });
            builder.Entity<MemberConnection>().HasOne(mc => mc.User).WithMany(a => a.MemberConnections).HasForeignKey(mc => mc.UserID).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<MemberConnection>().HasOne(mc => mc.User2).WithMany(a => a.MemberConnections2).HasForeignKey(mc => mc.User2ID).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<FamilyConnection>().ToTable("familyconnection").HasKey(a => new { a.UserID, a.FamilyID });
            builder.Entity<FamilyConnection>().HasOne(mc => mc.User).WithMany(p => p.FamilyConnections).HasForeignKey(mc => mc.UserID).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<FamilyConnection>().HasOne(mc => mc.Family).WithMany(p => p.FamilyConnections).HasForeignKey(mc => mc.FamilyID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
