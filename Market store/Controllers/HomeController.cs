﻿using Market_store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Market_store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;
        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;
        }
       
        public IActionResult Index()
        {
            var category = _context.Category1s.ToList();
            var shop = _context.Shop1s.ToList();
            var aboutus = _context.Aboutus.ToList();
            var testimonial = _context.Testimonials.Where(x=>x.Status=="Accept");
            var mainpage = _context.Mainpages.ToList();

            // Aggregate the 2 models using Tuple 
            var home = Tuple.Create<IEnumerable<Aboutu>, IEnumerable<Category1>, IEnumerable<Shop1>, IEnumerable<Testimonial>, IEnumerable<Mainpage>>(aboutus, category, shop, testimonial,mainpage);

            return View(home);
        }
        public IActionResult Contact()
        {
            return View();
        }

        // POST: Contactus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact([Bind("Contid,Name,Email,Message")] Contactu contactu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactu);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] Useraccount useraccountt)
        {
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
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Category", "Category1");
                    case 2:
                        // Admin
                        HttpContext.Session.SetInt32("Userid", (int)auth.Userid);
                        HttpContext.Session.SetString("Fullname", auth.Fullname);
                        HttpContext.Session.SetString("Email", auth.Email);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Dashboard", "Dashboard");
                }
            }
            else
            {
                HttpContext.Session.SetInt32("flag", 0);
                ViewBag.flag = HttpContext.Session.GetInt32("flag");

            }
            return RedirectToAction("Login", "LoginRegister");
        }

    }
}
