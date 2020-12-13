using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.API.Exceptions
{
    public class FamilyStructureException : Exception
    {
        public FamilyStructureException(string message) : base(message)
        {
        }
    }
}
