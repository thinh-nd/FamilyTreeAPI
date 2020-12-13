using System.ComponentModel.DataAnnotations;

namespace FamilyTree.API.Model.Request
{
    public class GrandparentRequest
    {
        [Required]
        public int? GrandchildId { get; set; }

        [Required]
        public PersonRequest Grandparent { get; set; }
    }
}
