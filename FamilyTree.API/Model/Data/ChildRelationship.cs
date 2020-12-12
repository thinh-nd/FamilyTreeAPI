using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.API.Model.Data
{
    public class ChildRelationship
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RelationId { get; set; }

        public int PersonId { get; set; }

        public Person Person { get; set; }

        public int ChildId { get; set; }

        public Person Child { get; set; }
    }
}
