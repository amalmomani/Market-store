using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Market_store.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Market_store.Controllers
{
    public class Shop1Controller : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webhostEnvironment;
        public Shop1Controller(ModelContext context, IWebHostEnvironment _webHostEnvironment)
        {
            _context = context;
            //assign initial value variable
            webhostEnvironment = _webHostEnvironment;
        }

        // GET: Shop1
        public async Task<IActionResult> Index()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var modelContext = _context.Shop1s.Include(s => s.Category);
            return View(await modelContext.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string? shop)
        {
            var modelContext = _context.Shop1s;
            if (shop != null)
            {
                var result = await modelContext.Where(x => x.Shopname.ToUpper().Contains(shop.ToUpper())).ToListAsync();
                return View(result);
            }
            return View(modelContext);
        }
        public IActionResult ShopView( int id)
        {
            var shop =_context.Shop1s.Where(x=>x.Categoryid== id);
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(shop);
        }
        [HttpPost]
        public async Task<IActionResult> ShopView(int id,string? shop)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var shop1 = _context.Shop1s.Where(x => x.Categoryid == id);
            if (shop != null)
            {
                var result =await shop1.Where(x => x.Shopname.ToUpper().Contains(shop.ToUpper())).ToListAsync();
                return View(result);
            }
            return View(shop1);
        }
        public async Task<IActionResult> Shop()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var modelContext = _context.Shop1s.Include(s => s.Category);
            return View(await modelContext.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Shop(int id, string? shop)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var modelContext = _context.Shop1s.Include(s => s.Category);
            if (shop != null)
            {
                var result = await modelContext.Where(x => x.Shopname.ToUpper().Contains(shop.ToUpper())).ToListAsync();
                return View(result);
            }
            return View(await modelContext.ToListAsync());
        }
        // GET: Shop1/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shop1 = await _context.Shop1s
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Shopid == id);
            if (shop1 == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(shop1);
        }

        // GET: Shop1/Create
        public IActionResult Create()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewData["Categoryid"] = new SelectList(_context.Category1s, "Categoryid", "Categoryname");
            return View();
        }

        // POST: Shop1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Shopid,Shopname,Categoryid,Image,Totalsales,Monthlyrent,ImageFile")] Shop1 shop1)
        {
            if (ModelState.IsValid)
            {
                if (shop1.ImageFile != null)
                {
                    //1- get w3rootpath
                    string w3rootpath = webhostEnvironment.WebRootPath;
                    //Guid.NewGuid : generate unique string before image name ;
                    ////2- generate image name and add unique string
                    string fileName = Guid.NewGuid().ToString() + "_" + shop1.ImageFile.FileName;
                    string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                    //4-create Image inside image file in w3root folder
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await shop1.ImageFile.CopyToAsync(fileStream);
                    }

                    shop1.Image = fileName;
                
                _context.Add(shop1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }
            }
            ViewData["Categoryid"] = new SelectList(_context.Category1s, "Categoryid", "Categoryid", shop1.Categoryid);
            return View(shop1);
        }

        // GET: Shop1/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shop1 = await _context.Shop1s.FindAsync(id);
            if (shop1 == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewData["Categoryid"] = new SelectList(_context.Category1s, "Categoryid", "Categoryname", shop1.Categoryid);
            return View(shop1);
        }

        // POST: Shop1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Shopid,Shopname,Categoryid,Image,Totalsales,Monthlyrent,ImageFile")] Shop1 shop1)
        {
            if (id != shop1.Shopid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (shop1.ImageFile != null)
                    {
                        //1- get w3rootpath
                        string w3rootpath = webhostEnvironment.WebRootPath;
                        //Guid.NewGuid : generate unique string before image name ;
                        ////2- generate image name and add unique string
                        string fileName = Guid.NewGuid().ToString() + "_" + shop1.ImageFile.FileName;
                        string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                        //4-create Image inside image file in w3root folder
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await shop1.ImageFile.CopyToAsync(fileStream);
                        }

                        shop1.Image = fileName;
                    }
                        _context.Update(shop1);
                        await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Shop1Exists(shop1.Shopid))
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
            ViewData["Categoryid"] = new SelectList(_context.Category1s, "Categoryid", "Categoryname", shop1.Categoryid);
            return View(shop1);
        }

        // GET: Shop1/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shop1 = await _context.Shop1s
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Shopid == id);
            if (shop1 == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(shop1);
        }

        // POST: Shop1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var shop1 = await _context.Shop1s.FindAsync(id);
            _context.Shop1s.Remove(shop1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Shop1Exists(decimal id)
        {
            return _context.Shop1s.Any(e => e.Shopid == id);
        }

        public IActionResult Search()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var modelContext = _context.Shop1s;
            return View(modelContext);
        }
        [HttpPost]
        public async Task<IActionResult> Search(string store)
        {
            var modelContext = _context.Shop1s;
            if (store != null)
            {
                var result = await modelContext.Where(x => x.Shopname == store).ToListAsync();
                return View(result);
            }
            return View(modelContext);
        }

    }
}
