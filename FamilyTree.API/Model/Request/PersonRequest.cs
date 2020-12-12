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

        [MaxLength(100)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public Gender? Gender { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }

        public DateTime? DateOfDeath { get; set; }

        public PersonRequest[] Childrens { get; set; }

        public PersonRequest Spouse { get; set; }

        public Person ConvertToPerson(bool isSpouse = false)
        {
            var person = new Person
            {
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                Gender = Gender.Value,
                DateOfBirth = DateOfBirth,
                DateOfDeath = DateOfDeath,
                IsSpouse = isSpouse,
                SpousalRelationship = Spouse == null ? null : new SpousalRelationship
                {
                    Person = new Person(),
                    Spouse = Spouse.ConvertToPerson(isSpouse: true)
                },
                ParentChildRelationships = Childrens?.Select(child => new ParentChildRelationship
                {
                    Parent = new Person(),
                    Child = child.ConvertToPerson()
                }).ToList()
            };

            if (Spouse != null)
            {
                person.SpousalRelationship = new SpousalRelationship
                {
                    Person = new Person(),
                    Spouse = Spouse.ConvertToPerson(isSpouse: true)
                };
            }

            return person;
        }
    }
}
