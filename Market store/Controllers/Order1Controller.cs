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
    public class Order1Controller : Controller
    {
        private readonly ModelContext _context;

        public Order1Controller(ModelContext context)
        {
            _context = context;
        }

        // GET: Order1
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Order1s.Include(o => o.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Order1/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            if (id == null)
            {
                return NotFound();
            }

            var order1 = await _context.Order1s
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Orderid == id);
            if (order1 == null)
            {
                return NotFound();
            }

            return View(order1);
        }

        // GET: Order1/Create
        public IActionResult Create()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewData["Userid"] = new SelectList(_context.Useraccounts, "Userid", "Userid");
            return View();
        }

        // POST: Order1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Orderid,Userid,Orderdate, Status")] Order1 order1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userid"] = new SelectList(_context.Useraccounts, "Userid", "Userid", order1.Userid);
            return View(order1);
        }

        // GET: Order1/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order1 = await _context.Order1s.FindAsync(id);
            if (order1 == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewData["Userid"] = new SelectList(_context.Useraccounts, "Userid", "Userid", order1.Userid);
            return View(order1);
        }

        // POST: Order1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Orderid,Userid,Orderdate,Status")] Order1 order1)
        {
            if (id != order1.Orderid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Order1Exists(order1.Orderid))
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
            ViewData["Userid"] = new SelectList(_context.Useraccounts, "Userid", "Userid", order1.Userid);
            return View(order1);
        }

        // GET: Order1/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order1 = await _context.Order1s
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Orderid == id);
            if (order1 == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(order1);
        }

        // POST: Order1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var order1 = await _context.Order1s.FindAsync(id);
            _context.Order1s.Remove(order1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Order1Exists(decimal id)
        {
            return _context.Order1s.Any(e => e.Orderid == id);
        }

    }
}
