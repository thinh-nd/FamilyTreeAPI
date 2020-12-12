using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
