using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
