﻿// <auto-generated />
using System;
using FamilyTree.API;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FamilyTree.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201212145822_ParentRelationship")]
    partial class ParentRelationship
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("FamilyTree.API.Model.Data.ChildRelationship", b =>
                {
                    b.Property<int>("RelationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ChildId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("RelationId");

                    b.HasIndex("ChildId")
                        .IsUnique();

                    b.HasIndex("PersonId");

                    b.ToTable("ChildRelationship");

                    b.HasCheckConstraint("CK_ChildParadox", "PersonId != ChildId");
                });

            modelBuilder.Entity("FamilyTree.API.Model.Data.ParentRelationship", b =>
                {
                    b.Property<int>("RelationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ParentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("RelationId");

                    b.HasIndex("ParentId");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("ParentRelationship");

                    b.HasCheckConstraint("CK_ParentParadox", "PersonId != ParentId");
                });

            modelBuilder.Entity("FamilyTree.API.Model.Data.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateOfDeath")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsSpouse")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("varchar(100)");

                    b.HasKey("PersonId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("FamilyTree.API.Model.Data.SpousalRelationship", b =>
                {
                    b.Property<int>("RelationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpouseId")
                        .HasColumnType("INTEGER");

                    b.HasKey("RelationId");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.HasIndex("SpouseId")
                        .IsUnique();

                    b.ToTable("SpousalRelationship");

                    b.HasCheckConstraint("CK_Monogamy", "PersonId != SpouseId");
                });

            modelBuilder.Entity("FamilyTree.API.Model.Data.ChildRelationship", b =>
                {
                    b.HasOne("FamilyTree.API.Model.Data.Person", "Child")
                        .WithMany()
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FamilyTree.API.Model.Data.Person", "Person")
                        .WithMany("ChildRelationships")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Child");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("FamilyTree.API.Model.Data.ParentRelationship", b =>
                {
                    b.HasOne("FamilyTree.API.Model.Data.Person", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FamilyTree.API.Model.Data.Person", "Person")
                        .WithOne("ParentRelationship")
                        .HasForeignKey("FamilyTree.API.Model.Data.ParentRelationship", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("FamilyTree.API.Model.Data.SpousalRelationship", b =>
                {
                    b.HasOne("FamilyTree.API.Model.Data.Person", "Person")
                        .WithOne("SpousalRelationship")
                        .HasForeignKey("FamilyTree.API.Model.Data.SpousalRelationship", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FamilyTree.API.Model.Data.Person", "Spouse")
                        .WithMany()
                        .HasForeignKey("SpouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("Spouse");
                });

            modelBuilder.Entity("FamilyTree.API.Model.Data.Person", b =>
                {
                    b.Navigation("ChildRelationships");

                    b.Navigation("ParentRelationship");

                    b.Navigation("SpousalRelationship");
                });
#pragma warning restore 612, 618
        }
    }
}
