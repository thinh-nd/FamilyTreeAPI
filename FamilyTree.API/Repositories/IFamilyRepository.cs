using FamilyTree.API.Model.Data;

namespace FamilyTree.API.Repositories
{
    public interface IFamilyRepository
    {
        public void Create(Family familyTree);

        public void AddChild(int parentId, Person child);

        public void AddParent(int childId, Person parent);

        public void AddGrandparent(int grandchildId, Person Grandparent);

        public Family Get();

        public void Delete();
    }
}
