using Market_store.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Market_store.Controllers
{
    public class LoginRegisterController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webhostEnvironment;
        public LoginRegisterController(ModelContext context, IWebHostEnvironment _webHostEnvironment)
        {
            _context = context;
            //assign initial value variable
            webhostEnvironment = _webHostEnvironment;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(decimal id, [Bind("Userid,Fullname,Phonenumber,Image,ImageFile,Email,Password,Roleid")] Useraccount useraccount)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetInt32("flag", 1);
                ViewBag.flag = HttpContext.Session.GetInt32("flag");

                var exist = _context.Useraccounts.Where(data => data.Email == useraccount.Email).SingleOrDefault();
                if (exist == null)
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
                    }
                    useraccount.Roleid = 1;
                    _context.Add(useraccount);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login", "LoginRegister");


                }
                else
                {
                    HttpContext.Session.SetInt32("flag", 0);
                    ViewBag.flag = HttpContext.Session.GetInt32("flag");
                }
            }
            return View(useraccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] Useraccount useraccountt)
        {
            var mainpage = _context.Mainpages.ToList();
            var user = _context.Useraccounts.ToList();
            // Aggregate the 2 models using Tuple 
            var home = Tuple.Create<IEnumerable<Mainpage>, IEnumerable<Useraccount>>(mainpage, user);
            var auth = _context.Useraccounts.Where(data => data.Email == useraccountt.Email && data.Password == useraccountt.Password).SingleOrDefault();
            ViewBag.flag = HttpContext.Session.GetInt32("flag");

            if (auth != null)
            {
                switch (auth.Roleid)
                {
                    case 1:
                        // Customer
                        HttpContext.Session.SetInt32("Userid", (int)auth.Userid);
                        HttpContext.Session.SetString("Fullname", auth.Fullname);
                        HttpContext.Session.SetString("Email", auth.Email);

                        return RedirectToAction("Category", "Category1");
                    case 2:
                        // Admin
                        HttpContext.Session.SetInt32("Userid", (int)auth.Userid);
                        HttpContext.Session.SetString("Fullname", auth.Fullname);
                        HttpContext.Session.SetString("Email", auth.Email);

                        return RedirectToAction("Dashboard", "Dashboard");
                }
            }
            else
            {
                HttpContext.Session.SetInt32("flag", 0);
                ViewBag.flag = HttpContext.Session.GetInt32("flag");

            }
            return View();
        }
    }
}
