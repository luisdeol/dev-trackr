using DevTrackR.API.Entities;
using DevTrackR.API.Models;
using DevTrackR.API.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DevTrackR.API.Controllers
{
    [ApiController]
    [Route("api/packages")]
    public class PackagesController : ControllerBase
    {
        private readonly IPackageRepository _repository;
        public PackagesController(IPackageRepository repository)
        {
            _repository = repository;
        }

        // GET api/packages
        [HttpGet]
        public IActionResult GetAll() {
            var packages = _repository.GetAll();

            return Ok(packages);
        }

        // GET api/packages/1234-5678-1234-3212
        [HttpGet("{code}")]
        public IActionResult GetByCode(string code) {
            var package = _repository.GetByCode(code);

            if (package == null) {
                return NotFound();
            }

            return Ok(package);
        }

        // POST api/packages
        [HttpPost]
        public IActionResult Post(AddPackageInputModel model) {
            if (model.Title.Length < 10) {
                return BadRequest("Title length must be at least 10 characters long.");
            }
            
            var package = new Package(model.Title, model.Weight);

            _repository.Add(package);

            return CreatedAtAction(
                "GetByCode", 
                new { code = package.Code },
                package);
        }

        // POST api/packages/1234-5678-1234-3212/updates
        [HttpPost("{code}/updates")]
        public IActionResult PostUpdate(string code, AddPackageUpdateInputModel model) {
            var package = _repository.GetByCode(code);

            if (package == null) {
                return NotFound();
            }

            package.AddUpdate(model.Status, model.Delivered);

            _repository.Update(package);

            return NoContent();
        }
    }
}