using System;
using System.Collections;
using System.Collections.Generic;
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
            var types = typeof(Produit).Assembly
                .GetTypes().Where(t => t.BaseType == typeof(Produit))
                .OrderBy(t => t.Name);

            return View(((IEnumerable<Nomenclatures.Data.Produit>)_dbContext.Produits
                .OrderBy(f => f.Nom)
                .Skip(pageIndex * cstPageSize)
                .Take(cstPageSize)
                .Select(p => p), (IEnumerable<Type>)types));
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

            return View(nameof(Edit) + p.GetType().Name, p);
        }

        [HttpPost]
        public IActionResult Create(string type)
        {
            return View(nameof(Edit) + type, 
                typeof(Produit).Assembly
                    .GetType($"Nomenclatures.Data.{type}")
                    .GetConstructor(new Type[]{})
                    .Invoke(null));
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
        public IActionResult SaveProduitFini(Nomenclatures.Data.ProduitFini p)
        {
            return Save(p, new ProduitFini(p));
        }

        [HttpPost]
        public IActionResult SaveProduitSemiFini(Nomenclatures.Data.ProduitSemiFini p)
        {
            return Save(p, new ProduitSemiFini(p));
        }

        private IActionResult Save(Nomenclatures.Data.Produit p, Nomenclatures.Produit produitDomain)
        {
            if (ModelState.IsValid)
            {
                PrepareSave(p);
                
                if (!produitDomain.IsValid)
                {
                    foreach (var me in produitDomain.GetErrors())
                        ModelState.AddModelError(me.Property, me.Message);

                    return View(nameof(Edit), p);
                }

                _dbContext.SaveChanges();

                return RedirectToAction(nameof(List));
            }

            return View("Edit" + p.GetType().Name, p);
        }

        private void PrepareSave(Nomenclatures.Data.Produit p)
        {
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
                    if (type == "ProduitSemiFini")
                    {
                        cpqty = new Data.ComponentQty
                        {
                            Id = idc,
                            Qty = qty,
                            PSF = _dbContext.ProduitsSemiFinis.Find(id)
                        };
                    }
                    else
                    {
                        cpqty = new Data.ComponentQty
                        {
                            Id = idc,
                            Qty = qty,
                            MP = _dbContext.MatieresPremieres.Find(id)
                        };
                    }

                    p.Composants.Add(cpqty);

                    if (cpqty.MP != null)
                        _dbContext.Attach(cpqty.MP).State = EntityState.Unchanged;
                    else
                        _dbContext.Attach(cpqty.PSF).State = EntityState.Unchanged;

                    if (idc != 0)
                        _dbContext.Attach(p.Composants.Last()).State = EntityState.Modified;
                }
            }            

            if (p.Id != 0)
            {
                _dbContext.Attach(p).State = EntityState.Modified;
            }
            else
            {
                _dbContext.Produits.Add(p);
            }
        }

        public IActionResult AugmentationPU()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AugmentationPU(AugmentationPrixProduitViewModel appv)
        {
            var cmd = new AugmentationPrixProduit(_dbContext);
            cmd.PourcentageAugmentation = appv.PourcentageAugmentation;
            cmd.Execute();

            return RedirectToAction(nameof(List));
        }
    }
}