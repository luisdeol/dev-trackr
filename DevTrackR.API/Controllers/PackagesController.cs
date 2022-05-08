using DevTrackR.API.Entities;
using DevTrackR.API.Models;
using DevTrackR.API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevTrackR.API.Controllers
{
    [ApiController]
    [Route("api/packages")]
    public class PackagesController : ControllerBase
    {
        private readonly DevTrackRContext _context;
        public PackagesController(DevTrackRContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() {
            return Ok(_context.Packages);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var package = _context.Packages.SingleOrDefault(p => p.Id == id);

            if (package == null)
                return NotFound();

            return Ok(package);
        }

        [HttpPost]
        public IActionResult Post(AddPackageInputModel model) {
            var package = new Package(model.Title, model.Weight);

            _context.Packages.Add(package);

            return CreatedAtAction("GetById", new { id = package.Id }, package);
        }

        [HttpPost("{id}/updates")]
        public IActionResult PostUpdate(int id, AddPackageUpdateInputModel model){
            var package = _context.Packages.SingleOrDefault(p => p.Id == id);

            if (package == null)
                return NotFound();
            
            package.AddUpdate(model.Status, model.Delivered);

            return NoContent();
        }
    }
}