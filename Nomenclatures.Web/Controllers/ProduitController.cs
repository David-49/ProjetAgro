using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nomenclatures.Data;
using Nomenclatures.Web.Models;

namespace Nomenclatures.Web
{
    public class ProduitController : Controller
    {
        private NomenclaturesContext _dbContext;
        private const int cstPageSize = 10;

        public ProduitController(NomenclaturesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult List(int pageIndex)
        {
            return View(_dbContext.Produits
                .OrderBy(f => f.Nom)
                .Skip(pageIndex * cstPageSize)
                .Take(cstPageSize)
                .Select(p => new ProduitViewModel(p)));
        }

        public IActionResult Edit(int id)
        {
            var p = _dbContext.Produits
                .Include(p => p.Composants)
                    .ThenInclude(c => c.MP)
                .Include(p => p.Composants)
                    .ThenInclude(c => c.PSF)
                .FirstOrDefault(prd => prd.Id == id);
            if (p == null) return NotFound();

            return View(new ProduitViewModel(p));
        }

        public IActionResult Create()
        {
            return View(nameof(Edit), new ProduitViewModel());
        }

        public IActionResult Delete(int id)
        {
            var p = _dbContext.Produits.Find(id);
            if (p != null)
            {
                _dbContext.Produits.Remove(p);
                _dbContext.SaveChanges();
            }

            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        public IActionResult Save(ProduitViewModel p)
        {
            var pData = p.ToData();

            foreach (var key in Request.Form.Keys)
            {
                if (key.StartsWith("comp_qty"))
                {
                    var index = key.Substring("comp_qty".Length);
                    var idc = Convert.ToInt32(Request.Form["comp_idc" + index]);
                    var id = Convert.ToInt32(Request.Form["comp_id" + index]);
                    var qty = Convert.ToInt32(Request.Form["comp_qty" + index]);
                    var type = Request.Form["comp_type" + index];

                    Data.ComponentQty cpqty = null;
                    if (type == "p")
                    {
                        cpqty = new Data.ComponentQty
                        {
                            Id = idc,
                            Qty = qty,
                            PSF = new Data.ProduitSemiFini { Id = id }
                        };
                    }
                    else
                    {
                        cpqty = new Data.ComponentQty
                        {
                            Id = idc,
                            Qty = qty,
                            MP = new Data.MatierePremiere { Id = id }
                        };
                    }

                    pData.Composants.Add(cpqty);

                    if(cpqty.MP != null) 
                        _dbContext.Attach(cpqty.MP).State = EntityState.Unchanged;
                    else
                        _dbContext.Attach(cpqty.PSF).State = EntityState.Unchanged;

                    if (idc != 0) 
                        _dbContext.Attach(pData.Composants.Last()).State = EntityState.Modified;
                }
            }

            if (p.Id != 0)
            {
                _dbContext.Attach(pData).State = EntityState.Modified;
            }
            else
            {
                _dbContext.Produits.Add(pData);
            }

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(List));
        }
    }
}