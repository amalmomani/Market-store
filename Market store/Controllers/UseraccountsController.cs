using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Market_store.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Market_store.Controllers
{
    public class UseraccountsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webhostEnvironment;
        public UseraccountsController(ModelContext context, IWebHostEnvironment _webHostEnvironment)
        {
            _context = context;
            //assign initial value variable
            webhostEnvironment = _webHostEnvironment;
        }

        // GET: Useraccounts
        public async Task<IActionResult> Index()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var modelContext = _context.Useraccounts.Include(u => u.Role);
            return View(await modelContext.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string? user)
        {
            var modelContext = _context.Useraccounts;
            if (user != null)
            {
                var result = await modelContext.Where(x => x.Fullname.ToUpper().Contains(user.ToUpper())).ToListAsync();
                return View(result);
            }
            return View(modelContext);
        }
        // GET: Useraccounts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var useraccount = await _context.Useraccounts
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (useraccount == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(useraccount);
        }

        // GET: Useraccounts/Create
        public IActionResult Create()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid");
            return View();
        }

        // POST: Useraccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userid,Fullname,Phonenumber,Image,Email,Password,Roleid,ImageFile")] Useraccount useraccount)
        {
            if (ModelState.IsValid)
            {
                if (useraccount.ImageFile != null)
                {
                    //1- get w3rootpath
                    string w3rootpath = webhostEnvironment.WebRootPath;
                    //Guid.NewGuid : generate unique string before image name ;
                    ////2- generate image name and add unique string
                    string fileName = Guid.NewGuid().ToString() + "_" + useraccount.ImageFile.FileName;
                    string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                    //4-create Image inside image file in w3root folder
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await useraccount.ImageFile.CopyToAsync(fileStream);
                    }

                    useraccount.Image = fileName;
                    useraccount.Roleid = 1;
                    _context.Add(useraccount);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                _context.Add(useraccount);
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", useraccount.Roleid);
            return View(useraccount);
        }

        // GET: Useraccounts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var useraccount = await _context.Useraccounts.FindAsync(id);
            if (useraccount == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", useraccount.Roleid);
            return View(useraccount);
        }

        // POST: Useraccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Userid,Fullname,Phonenumber,Image,Email,Password,Roleid,ImageFile")] Useraccount useraccount)
        {
            if (id != useraccount.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (useraccount.ImageFile != null)
                    {
                        string w3rootpath = webhostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + useraccount.ImageFile.FileName;
                        string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await useraccount.ImageFile.CopyToAsync(fileStream);
                        }
                        useraccount.Image = fileName;
                        await _context.SaveChangesAsync();
                    }
                    _context.Update(useraccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UseraccountExists(useraccount.Userid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Dashboard", "Dashboard");
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", useraccount.Roleid);
            return View(useraccount);
        }


        public async Task<IActionResult> EditAdmin(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var useraccount = await _context.Useraccounts.FindAsync(id);
            if (useraccount == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", useraccount.Roleid);
            return View(useraccount);
        }

        // POST: Useraccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdmin(decimal id, [Bind("Userid,Fullname,Phonenumber,Image,Email,Password,Roleid,ImageFile")] Useraccount useraccount)
        {
            if (id != useraccount.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (useraccount.ImageFile != null)
                    {
                        string w3rootpath = webhostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + useraccount.ImageFile.FileName;
                        string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await useraccount.ImageFile.CopyToAsync(fileStream);
                        }
                        useraccount.Image = fileName;
                        await _context.SaveChangesAsync();
                    }
                    _context.Update(useraccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UseraccountExists(useraccount.Userid))
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
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", useraccount.Roleid);
            return View(useraccount);
        }


        public async Task<IActionResult> UserEdit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var useraccount = await _context.Useraccounts.FindAsync(id);
            if (useraccount == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", useraccount.Roleid);
            return View(useraccount);
        }

        // POST: Useraccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEdit(decimal id, [Bind("Userid,Fullname,Phonenumber,Image,Email,Password,Roleid,ImageFile")] Useraccount useraccount)
        {
            if (id != useraccount.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (useraccount.ImageFile != null)
                    {
                        string w3rootpath = webhostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + useraccount.ImageFile.FileName;
                        string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await useraccount.ImageFile.CopyToAsync(fileStream);
                        }
                        useraccount.Image = fileName;
                        await _context.SaveChangesAsync();
                    }
                    _context.Update(useraccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UseraccountExists(useraccount.Userid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Category", "Category1");
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", useraccount.Roleid);
            return View(useraccount);
        }

        // GET: Useraccounts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var useraccount = await _context.Useraccounts
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (useraccount == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(useraccount);
        }

        // POST: Useraccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var useraccount = await _context.Useraccounts.FindAsync(id);
            _context.Useraccounts.Remove(useraccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UseraccountExists(decimal id)
        {
            return _context.Useraccounts.Any(e => e.Userid == id);
        }

    }
}
