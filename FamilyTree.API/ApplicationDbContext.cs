using FamilyTree.API.Model;
using FamilyTree.API.Model.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.API
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<SpousalRelationship> SpousalRelationship { get; set; }
        public DbSet<ParentChildRelationship> ParentChildRelationship { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SpousalRelationship>(entity =>
            {
                entity.HasIndex(r => r.PersonId).IsUnique();
                entity.HasIndex(r => r.SpouseId).IsUnique();
                entity.HasCheckConstraint("CK_Monogamy", "PersonId != SpouseId");
            });

            builder.Entity<ParentChildRelationship>(entity =>
            {
                entity.HasIndex(r => r.ChildId).IsUnique();
                entity.HasCheckConstraint("CK_Paradox", "ParentId != ChildId");
            });
        }
    }
}
