using FamilyTree.API.Model.Data;
using FamilyTree.API.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.API.Repositories
{
    public class FamilyRepository : IFamilyRepository
    {
        private readonly ApplicationDbContext _context;

        public FamilyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Family familyTree)
        {
            var personQueue = new Queue<Person>();
            personQueue.Enqueue(familyTree.Ancestor);
            while (personQueue.Count > 0)
            {
                var currentPerson = personQueue.Dequeue();
                _context.Person.Add(currentPerson);
                if (currentPerson.HasSpouse())
                {
                    var spouse = currentPerson.SpousalRelationship.Spouse;
                    _context.Person.Add(spouse);
                }
                if (currentPerson.HasChildren())
                {
                    foreach (var relationship in currentPerson.ParentChildRelationships)
                    {
                        var child = relationship.Child;
                        _context.Person.Add(relationship.Child);
                        personQueue.Enqueue(child);
                    }
                }
            }
            _context.SaveChanges();
        }

        public void Delete()
        {
            _context.Person.RemoveRange(_context.Person);
            _context.SaveChanges();
        }

        public void AddChild(int parentId, Person child)
        {
            var _ = GetPerson(parentId);
            _context.Person.Add(child);
            _context.ParentChildRelationship.Add(new ParentChildRelationship
            {
                ParentId = parentId,
                Child = child
            });
            _context.SaveChanges();
        }

        public void AddParent(int childId, Person parent)
        {
            var _ = GetPerson(childId);
            var firstParent = GetParent(childId);

            _context.Person.Add(parent);

            firstParent.SpousalRelationship = new SpousalRelationship
            {
                PersonId = firstParent.PersonId,
                Spouse = parent
            };
            _context.SaveChanges();
        }

        public void AddGrandparent(int grandchildId, Person grandparent)
        {
            var _ = GetPerson(grandchildId);
            var parent = GetParent(grandchildId);
            var firstGrandparent = GetParent(parent.PersonId);

            _context.Person.Add(grandparent);

            firstGrandparent.SpousalRelationship = new SpousalRelationship
            {
                PersonId = firstGrandparent.PersonId,
                Spouse = grandparent
            };
            _context.SaveChanges();
        }

        private Person GetPerson(int personId)
        {
            return _context.Person.Where(p => p.PersonId == personId).FirstOrDefault() 
                ?? throw new Exception($"Person with id {personId} does not exist.");
        }

        private Person GetParent(int personId)
        {
            var parent = (from person in _context.Person 
                         join parentChild in _context.ParentChildRelationship
                         on person.PersonId equals parentChild.ParentId
                         where parentChild.ChildId == personId
                         select person).FirstOrDefault();

            if (parent == null)
            {
                throw new Exception($"Cannot find parent of person with id {personId}.");
            }
            return parent;
        }
    }
}
