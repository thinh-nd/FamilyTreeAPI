using FamilyTree.API.Exceptions;
using FamilyTree.API.Model.Data;
using FamilyTree.API.Model.Request;
using Microsoft.EntityFrameworkCore;
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
                    foreach (var relationship in currentPerson.ChildRelationships)
                    {
                        var child = relationship.Child;
                        _context.Person.Add(relationship.Child);
                        personQueue.Enqueue(child);
                    }
                }
            }
            Save();
        }

        public void Delete()
        {
            _context.Person.RemoveRange(_context.Person);
            Save();
        }

        public void AddChild(int parentId, Person child)
        {
            _context.Person.Add(child);
            _context.ParentRelationship.Add(new ParentRelationship
            {
                ParentId = parentId,
                Person = child
            });
            _context.ChildRelationship.Add(new ChildRelationship
            {
                PersonId = parentId,
                Child = child
            });
            Save();
        }

        public void AddParent(int childId, Person parent)
        {
            var firstParent = GetParent(childId);
            _context.Person.Add(parent);
            firstParent.SpousalRelationship = new SpousalRelationship
            {
                PersonId = firstParent.PersonId,
                Spouse = parent
            };
            Save();
        }

        public void AddGrandparent(int grandchildId, Person grandparent)
        {
            var firstGrandparent = GetGrandparent(grandchildId);
            _context.Person.Add(grandparent);
            firstGrandparent.SpousalRelationship = new SpousalRelationship
            {
                PersonId = firstGrandparent.PersonId,
                Spouse = grandparent
            };
            Save();
        }

        public Family Get()
        {
            var ancestors = (
                from p in _context.Person
                join sr in _context.SpousalRelationship on p.PersonId equals sr.PersonId into psr
                from sr in psr.DefaultIfEmpty()
                join pr in _context.ParentRelationship on p.PersonId equals pr.PersonId into ppr
                from pr in ppr.DefaultIfEmpty()
                join cr in _context.ChildRelationship on p.PersonId equals cr.PersonId into pcr
                from cr in pcr.DefaultIfEmpty()
                select p
            )
            .Include(p => p.SpousalRelationship)
            .Include(p => p.ParentRelationship)
            .Include(p => p.ChildRelationships)
            .ToList().Where(p => p.ParentRelationship == null && !p.IsSpouse)
            .Distinct().ToList();

            if (ancestors.Count != 1)
            {
                throw new FamilyStructureException($"Family can only has 1 ancestor, {ancestors.Count} ancestors found.");
            }

            return new Family(ancestors.First());
        }

        private void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e) when (e is DbUpdateException)
            {
                throw new EntityNotFoundException("Entity not found. This may due to id on request does not exist.", e);
            }
        }

        private Person GetParent(int personId) => GetAncestor(personId, 1);

        private Person GetGrandparent(int personId) => GetAncestor(personId, 2);

        private Person GetAncestor(int personId, int level)
        {
            var ancestors = (
                from p in _context.Person
                join r in _context.ParentRelationship on p.PersonId equals r.PersonId into pr
                from r in pr.DefaultIfEmpty()
                select p
            )
            .Include(p => p.ParentRelationship)
            .ToList();

            var step = 0;
            var ancestor = ancestors.Where(a => a.PersonId == personId).FirstOrDefault();
            while (step++ <= level)
            {
                if (ancestor.ParentRelationship == null)
                {
                    throw new EntityNotFoundException($"Cannot find ancestor level {level} of person with id {personId}.");
                }
                ancestor = ancestor.ParentRelationship.Parent;
            }
            return ancestor;
        }
    }
}
