namespace FamilyTree.API.Model.Data
{
    public class Family
    {
        public Family(Person ancestor)
        {
            Ancestor = ancestor;
        }

        public Person Ancestor { get; set; }
    }
}
