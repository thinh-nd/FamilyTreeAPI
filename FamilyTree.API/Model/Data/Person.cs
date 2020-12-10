using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
        public string MiddleName { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? DateOfDeath { get; set; }

        public SpousalRelationship SpousalRelationship { get; set; }

        public IEnumerable<ParentChildRelationship> ParentChildRelationships { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
