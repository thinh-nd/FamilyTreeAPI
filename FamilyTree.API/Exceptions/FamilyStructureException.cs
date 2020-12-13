using System;

namespace FamilyTree.API.Exceptions
{
    public class FamilyStructureException : Exception
    {
        public FamilyStructureException(string message) : base(message)
        {
        }

        public FamilyStructureException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
