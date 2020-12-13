using FamilyTree.API.Model.Data;
using FamilyTree.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.API.Services
{
    public class FamilyTreeService : IFamilyTreeService
    {
        public string Visualize(Family family)
        {
            return VisualizePerson(family.Ancestor, 0);
        }

        private string VisualizePerson(Person person, int depth)
        {
            var line = $"{person.FirstName} {person.LastName}";
            line += VisualizeSpouse(person);
            line += Environment.NewLine; depth++;
            line += VisualizeChildren(person, depth);
            return line;
        }

        private string VisualizeSpouse(Person person)
        {
            var spouse = person.SpousalRelationship?.Spouse;
            return spouse != null
                ? $" - {spouse.FirstName} {spouse.LastName}"
                : "";
        }

        private string VisualizeChildren(Person person, int depth)
        {
            var line = "";
            var children = person.ChildRelationships?.Select(r => r.Child).OrderBy(p => p.FirstName).ToList();
            for (int i = 0; i < children.Count; i++)
            {
                line += GetLineStructure(depth, i == children.Count - 1);
                line += VisualizePerson(children[i], depth);
            }
            return line;
        }

        private string GetLineStructure(int depth, bool isLastChild = false)
        {
            var lineStructure = "";
            lineStructure += depth > 1 
                ? string.Join("", Enumerable.Repeat("│   ", depth - 1)) 
                : "";
            lineStructure += depth > 0 
                ? isLastChild ? "└── " : "├── " 
                : "";
            return lineStructure;
        }
    }
}
