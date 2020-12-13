using System.ComponentModel.DataAnnotations;

namespace FamilyTree.API.Model.Request
{
    public class ChildRequest
    {
        [Required]
        public int? ParentId { get; set; }

        [Required]
        public PersonRequest Child { get; set; }
    }
}
