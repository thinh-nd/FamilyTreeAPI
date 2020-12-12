using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.API.Model.Data
{
    public class ParentRelationship
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RelationId { get; set; }

        public int PersonId { get; set; }

        public Person Person { get; set; }

        public int ParentId { get; set; }

        public Person Parent { get; set; }
    }
}
