using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
