using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nomenclatures.Data;
using Nomenclatures.Web.Models;

namespace Nomenclatures.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private NomenclaturesContext _dbContext;

        public SearchController(NomenclaturesContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Search(SearchViewModel criterias)
        {
            var search = new Search(_dbContext.FamillesPremieres, _dbContext.MatieresPremieres, _dbContext.Produits)
            {
                NomContient = criterias.NomContient,
                EstBio = criterias.EstBio,
                InclureFamilleMatierePremiere = criterias.InclureFamilleMatierePremiere,
                InclureMatierePremiere = criterias.InclureMatierePremiere,
                InclureProduitFini = criterias.InclureProduitFini,
                InclureProduitSemiFini = criterias.InclureProduitSemiFini
            };

            return Ok(search.Execute()
                    .Select(sr => new SearchResultViewModel(sr)
                    {
                        EditUrl = Url.Action("Edit", sr.Type, new { id = sr.Id })
                    }));
        }
    }
}