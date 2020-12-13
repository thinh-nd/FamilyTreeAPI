using FamilyTree.API.Model.Data;
using FamilyTree.API.Model.Request;
using FamilyTree.API.Repositories;
using FamilyTree.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mime;

namespace FamilyTree.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly IFamilyTreeService _familyTreeService;

        public FamilyController(IFamilyRepository familyRepository, IFamilyTreeService familyTreeService)
        {
            _familyRepository = familyRepository;
            _familyTreeService = familyTreeService;
        }

        [HttpGet]
        [Produces(MediaTypeNames.Text.Plain)]
        public ActionResult Get()
        {
            var family = _familyRepository.Get();
            var treeRepresentation = _familyTreeService.Visualize(family);
            return new ObjectResult(treeRepresentation);
        }

        [HttpPost]
        public ActionResult Post([FromBody] FamilyRequest request)
        {
            var family = request.ConvertToFamily();
            _familyRepository.Create(family);
            return Ok();
        }

        [HttpPost("Child")]
        public ActionResult PostChild([FromBody] ChildRequest request)
        {
            var child = request.Child.ConvertToPerson();
            _familyRepository.AddChild(request.ParentId.Value, child);
            return Ok();
        }

        [HttpPost("Parent")]
        public ActionResult PostParent([FromBody] ParentRequest request)
        {
            var parent = request.Parent.ConvertToPerson(isSpouse: true);
            _familyRepository.AddParent(request.ChildId.Value, parent);
            return Ok();
        }

        [HttpPost("Grandparent")]
        public ActionResult PostGrandparent([FromBody] GrandparentRequest request)
        {
            var grandparent = request.Grandparent.ConvertToPerson(isSpouse: true);
            _familyRepository.AddParent(request.GrandchildId.Value, grandparent);
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            _familyRepository.Delete();
            return Ok();
        }
    }
}
