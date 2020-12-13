using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyTree.API.Model.Data
{
    public class SpousalRelationship
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RelationId { get; set; }

        public int PersonId { get; set; }

        public Person Person { get; set; }

        public int SpouseId { get; set; }

        public Person Spouse { get; set; }
    }
}
