using FamilyTree.API.Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.API.Model.Request
{
    public class FamilyRequest
    {
        [Required]
        public PersonRequest Ancestor { get; set; }

        public Family ConvertToFamily()
        {
            return new Family
            {
                Ancestor = Ancestor.ConvertToPerson()
            };
        }
    }
}
