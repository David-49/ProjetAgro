using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nomenclatures.Data;
using Nomenclatures.Web.Models;

namespace Nomenclatures.Web
{
    public class FamilleMatierePremiereController
        : Controller
    {
        private NomenclaturesContext _dbContext;
        private const int cstPageSize = 10;

        public FamilleMatierePremiereController(NomenclaturesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult List(int pageIndex)
        {
            return View(_dbContext.FamillesPremieres
                .OrderBy(f => f.Nom)
                .Skip(pageIndex * cstPageSize)
                .Take(cstPageSize));
        }

        public IActionResult Edit(int id)
        {
            var f = _dbContext.FamillesPremieres.Find(id);
            if (f == null) return NotFound();

            return View(f);
        }

        public IActionResult Create()
        {
            return View(nameof(Edit), new Nomenclatures.Data.FamilleMatierePremiere());
        }

        public IActionResult Delete(int id)
        {
            var f = _dbContext.FamillesPremieres.Find(id);
            if (f != null)
            {
                _dbContext.FamillesPremieres.Remove(f);
                _dbContext.SaveChanges();
            }

            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        public IActionResult Save(Nomenclatures.Data.FamilleMatierePremiere f)
        {
            if (f.Id != 0)
            {
                _dbContext.Attach(f).State = EntityState.Modified;
            }
            else
            {
                _dbContext.FamillesPremieres.Add(f);
            }

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(List));
        }
    }
}