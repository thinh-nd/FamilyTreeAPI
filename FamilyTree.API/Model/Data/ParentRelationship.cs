using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyTree.API.Model.Data
{
    public class ParentRelationship
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RelationId { get; set; }

        public int PersonId { get; set; }

        public Person Person { get; set; }

        public int ParentId { get; set; }

        public Person Parent { get; set; }
    }
}
