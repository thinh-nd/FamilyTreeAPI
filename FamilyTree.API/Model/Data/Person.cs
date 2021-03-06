﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FamilyTree.API.Model.Data
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonId { get; set; }

        [Column(TypeName="varchar(100)")]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? DateOfDeath { get; set; }

        public bool IsSpouse { get; set; }

        [InverseProperty("Person")]
        public SpousalRelationship SpousalRelationship { get; set; }

        [InverseProperty("Person")]
        public ParentRelationship ParentRelationship { get; set; }

        [InverseProperty("Person")]
        public IEnumerable<ChildRelationship> ChildRelationships { get; set; }

        public bool HasSpouse() => SpousalRelationship?.Spouse != null;

        public bool HasChildren() => ChildRelationships?.Count() > 0;
    }

    public enum Gender
    {
        Male,
        Female
    }
}
