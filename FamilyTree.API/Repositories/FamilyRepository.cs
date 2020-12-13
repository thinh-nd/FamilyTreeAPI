using FamilyTree.API.Exceptions;
using FamilyTree.API.Model.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

            if (firstParent == null)
            {
                var childRelationship = new ChildRelationship
                {
                    Person = parent,
                    ChildId = childId
                };
                parent.ChildRelationships = new[] { childRelationship };
                _context.ParentRelationship.Add(new ParentRelationship
                {
                    PersonId = childId,
                    Parent = parent
                });
            }
            else
            {
                firstParent.SpousalRelationship = new SpousalRelationship
                {
                    PersonId = firstParent.PersonId,
                    Spouse = parent
                };
                parent.IsSpouse = true;
            }
            _context.Person.Add(parent);

            Save();
        }

        public void AddGrandparent(int grandchildId, Person grandparent)
        {
            Person firstGrandparent = null;
            try
            {
                firstGrandparent = GetGrandparent(grandchildId);
            }
            catch (EntityNotFoundException e)
            {
                throw new FamilyStructureException("Cannot add grandparent for ancestor.", e.InnerException);
            }

            if (firstGrandparent == null)
            {
                var parentId = _context.ParentRelationship.Where(pr => pr.PersonId == grandchildId).First().ParentId;
                _context.ChildRelationship.Add(new ChildRelationship
                {
                    Person = grandparent,
                    ChildId = parentId
                });
                _context.ParentRelationship.Add(new ParentRelationship
                {
                    PersonId = parentId,
                    Parent = grandparent
                });
            }
            else
            {
                firstGrandparent.SpousalRelationship = new SpousalRelationship
                {
                    PersonId = firstGrandparent.PersonId,
                    Spouse = grandparent
                };
                grandparent.IsSpouse = true;
            }

            _context.Person.Add(grandparent);
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
            .ToList().Where(p => IsAncestor(p))
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
            var parent = ancestors.Where(a => a.PersonId == personId).FirstOrDefault();

            while (step++ <= level)
            {
                if (IsAncestor(parent) && step == level)
                {
                    return null;
                }
                if (parent.ParentRelationship == null)
                {
                    throw new EntityNotFoundException($"Cannot find ancestor level {level} of person with id {personId}.");
                }
                parent = parent.ParentRelationship.Parent;
            }
            return parent;
        }

        private bool IsAncestor(Person person)
        {
            return person.ParentRelationship == null && !person.IsSpouse;
        }
    }
}
