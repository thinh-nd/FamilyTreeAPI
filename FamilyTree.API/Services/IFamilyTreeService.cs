using FamilyTree.API.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.API.Services
{
    public interface IFamilyTreeService
    {
        public string Visualize(Family family);
    }
}
