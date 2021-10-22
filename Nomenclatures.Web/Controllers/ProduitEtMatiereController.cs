using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nomenclatures.Data;

namespace Nomenclatures.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitEtMatiereController : ControllerBase
    {
        private NomenclaturesContext _dbContext;

        public ProduitEtMatiereController(NomenclaturesContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{nom}")]
        public IActionResult GetId([FromRoute]string nom)
        {
            var id = _dbContext.ProduitsSemiFinis
                .FirstOrDefault(p => p.Nom == nom)?.Id;

            if (id.HasValue) return Ok(new { id, type = nameof(ProduitSemiFini) });

            id = _dbContext.MatieresPremieres
                .FirstOrDefault(mp => mp.Nom == nom)?.Id;

            if (id.HasValue) return Ok(new { id, type = nameof(MatierePremiere) });

            return NotFound();
        }
    }
}