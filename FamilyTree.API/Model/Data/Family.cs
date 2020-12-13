using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.API.Model.Data
{
    public class Family
    {
        public Family(Person ancestor)
        {
            Ancestor = ancestor;
        }

        public Person Ancestor { get; set; }
    }
}
