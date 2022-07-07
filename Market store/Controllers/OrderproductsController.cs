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
    public class OrderproductsController : Controller
    {
        private readonly ModelContext _context;

        public OrderproductsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Orderproducts
        public async Task<IActionResult> Index()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var modelContext = _context.Orderproducts.Include(o => o.Order).Include(o => o.Product);
            return View(await modelContext.ToListAsync());
        }

        // GET: Orderproducts/Details/5s
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderproduct = await _context.Orderproducts
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderproduct == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(orderproduct);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: Orderproducts/Create
       

        // POST: Orderproducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Orderid,Numberofpieces,Totalamount,Status,Productid")] Orderproduct orderproduct)
        //{
        //    if (ModelState.IsValid)
        //    {
        //         orderproduct.Totalamount = orderproduct.Numberofpieces * orderproduct.Product.Price;
        //        _context.Add(orderproduct);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["Orderid"] = new SelectList(_context.Order1s, "Orderid", "Orderid", orderproduct.Orderid);
        //    ViewData["Productid"] = new SelectList(_context.Product1s, "Productid", "Productid", orderproduct.Productid);
        //    return View(orderproduct);
        //}

        // GET: Orderproducts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderproduct = await _context.Orderproducts.FindAsync(id);
            if (orderproduct == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewData["Orderid"] = new SelectList(_context.Order1s, "Orderid", "Orderid", orderproduct.Orderid);
            ViewData["Productid"] = new SelectList(_context.Product1s, "Productid", "Productid", orderproduct.Productid);
            return View(orderproduct);
        }

        // POST: Orderproducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Orderid,Numberofpieces,Totalamount,Status,Productid")] Orderproduct orderproduct)
        {
            if (id != orderproduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderproduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderproductExists(orderproduct.Id))
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

            ViewData["Orderid"] = new SelectList(_context.Order1s, "Orderid", "Orderid", orderproduct.Orderid);
            ViewData["Productid"] = new SelectList(_context.Product1s, "Productid", "Productid", orderproduct.Productid);
            return View(orderproduct);
        }

        // GET: Orderproducts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderproduct = await _context.Orderproducts
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderproduct == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(orderproduct);
        }

        // POST: Orderproducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var orderproduct = await _context.Orderproducts.FindAsync(id);
            _context.Orderproducts.Remove(orderproduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderproductExists(decimal id)
        {
            return _context.Orderproducts.Any(e => e.Id == id);
        }

    }
}
