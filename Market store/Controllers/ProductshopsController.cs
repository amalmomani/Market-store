using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Market_store.Models;
using Microsoft.AspNetCore.Http;

namespace Market_store.Controllers
{
    public class ProductshopsController : Controller
    {
        private readonly ModelContext _context;

        public ProductshopsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Productshops
        public async Task<IActionResult> Index()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var modelContext = _context.Productshops.Include(p => p.Product).Include(p => p.Shop);
            return View(await modelContext.ToListAsync());
        }

        public IActionResult ProductView(int id)
        {
            var product = _context.Productshops.Where(x => x.Shopid == id);
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(product);
        }

        // GET: Productshops/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productshop = await _context.Productshops
                .Include(p => p.Product)
                .Include(p => p.Shop)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productshop == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(productshop);
        }

        // GET: Productshops/Create
        public IActionResult Create()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewData["Productid"] = new SelectList(_context.Product1s, "Productid", "Productname");
            ViewData["Shopid"] = new SelectList(_context.Shop1s, "Shopid", "Shopname");
            return View();
        }

        // POST: Productshops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Productid,Shopid")] Productshop productshop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productshop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Productid"] = new SelectList(_context.Product1s, "Productid", "Productid", productshop.Productid);
            ViewData["Shopid"] = new SelectList(_context.Shop1s, "Shopid", "Shopid", productshop.Shopid);
            return View(productshop);
        }

        // GET: Productshops/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productshop = await _context.Productshops.FindAsync(id);
            if (productshop == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewData["Productid"] = new SelectList(_context.Product1s, "Productid", "Productname", productshop.Productid);
            ViewData["Shopid"] = new SelectList(_context.Shop1s, "Shopid", "Shopname", productshop.Shopid);
            return View(productshop);
        }

        // POST: Productshops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Productid,Shopid")] Productshop productshop)
        {
            if (id != productshop.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productshop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductshopExists(productshop.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewData["Productid"] = new SelectList(_context.Product1s, "Productid", "Productid", productshop.Productid);
            ViewData["Shopid"] = new SelectList(_context.Shop1s, "Shopid", "Shopid", productshop.Shopid);
            return View(productshop);
        }

        // GET: Productshops/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productshop = await _context.Productshops
                .Include(p => p.Product)
                .Include(p => p.Shop)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productshop == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(productshop);
        }

        // POST: Productshops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var productshop = await _context.Productshops.FindAsync(id);
            _context.Productshops.Remove(productshop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductshopExists(decimal id)
        {
            return _context.Productshops.Any(e => e.Id == id);
        }

    }
}
