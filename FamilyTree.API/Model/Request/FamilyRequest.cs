using FamilyTree.API.Model.Data;
using System.ComponentModel.DataAnnotations;

namespace FamilyTree.API.Model.Request
{
    public class FamilyRequest
    {
        [Required]
        public PersonRequest Ancestor { get; set; }

        public Family ConvertToFamily()
        {
            return new Family(Ancestor.ConvertToPerson());
        }
    }
}
