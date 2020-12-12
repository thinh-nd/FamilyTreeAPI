using FamilyTree.API.Model.Data;
using FamilyTree.API.Model.Request;
using FamilyTree.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyRepository _familyRepository;

        public FamilyController(IFamilyRepository familyRepository)
        {
            _familyRepository = familyRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = new
            {
                Message = "Ok"
            };
            return new ObjectResult(result);
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
            _familyRepository.AddChild(request.ParentId.Value, request.Child.ConvertToPerson());
            return Ok();
        }

        [HttpPost("Parent")]
        public ActionResult PostParent([FromBody] ParentRequest request)
        {
            _familyRepository.AddParent(request.ChildId.Value, request.Parent.ConvertToPerson());
            return Ok();
        }

        [HttpPost("Grandparent")]
        public ActionResult PostGrandparent([FromBody] GrandparentRequest request)
        {
            _familyRepository.AddParent(request.GrandchildId.Value, request.Grandparent.ConvertToPerson());
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
