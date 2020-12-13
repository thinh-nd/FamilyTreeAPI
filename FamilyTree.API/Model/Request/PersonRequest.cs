using FamilyTree.API.Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.API.Model.Request
{
    public class PersonRequest
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public Gender? Gender { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }

        public DateTime? DateOfDeath { get; set; }

        public PersonRequest Parent { get; set; }

        public PersonRequest[] Childrens { get; set; }

        public PersonRequest Spouse { get; set; }

        public Person ConvertToPerson(Person parent = null, bool isSpouse = false)
        {
            var person = new Person
            {
                FirstName = FirstName,
                LastName = LastName,
                Gender = Gender.Value,
                DateOfBirth = DateOfBirth,
                DateOfDeath = DateOfDeath,
                IsSpouse = isSpouse
            };

            person.SpousalRelationship = Spouse == null ? null : new SpousalRelationship
            {
                Person = new Person(),
                Spouse = Spouse.ConvertToPerson(isSpouse: true)
            };

            person.ParentRelationship = parent == null ? null : new ParentRelationship
            {
                Person = new Person(),
                Parent = parent
            };

            person.ChildRelationships = Childrens?.Select(child => new ChildRelationship
            {
                Person = new Person(),
                Child = child.ConvertToPerson(person)
            }).ToList();

            return person;
        }
    }
}
