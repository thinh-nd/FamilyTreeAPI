using FamilyTree.API.Model.Data;
using System;
using System.Linq;

namespace FamilyTree.API.Services
{
    public class FamilyTreeService : IFamilyTreeService
    {
        public string Visualize(Family family)
        {
            var treeView = VisualizePerson(family.Ancestor, 0);
            treeView += "└─────────────────────";
            return treeView;
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
            foreach (var child in children)
            {
                line += GetLineStructure(depth);
                line += VisualizePerson(child, depth);
            }
            return line;
        }

        private string GetLineStructure(int depth)
        {
            var lineStructure = "";
            lineStructure += depth > 1 
                ? string.Join("", Enumerable.Repeat("|   ", depth - 1)) 
                : "";
            lineStructure += depth > 0 ? "├── " : "";
            return lineStructure;
        }
    }
}
