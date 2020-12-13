using System.ComponentModel.DataAnnotations;

namespace FamilyTree.API.Model.Request
{
    public class ParentRequest
    {
        [Required]
        public int? ChildId { get; set; }

        [Required]
        public PersonRequest Parent { get; set; }
    }
}
