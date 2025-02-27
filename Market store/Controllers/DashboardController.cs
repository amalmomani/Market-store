﻿using MailKit.Net.Smtp;
using Market_store.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Market_store.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webhostEnvironment;
        public DashboardController(ModelContext context, IWebHostEnvironment _webHostEnvironment)
        {
            _context = context;
            //assign initial value variable
            webhostEnvironment = _webHostEnvironment;
        }
        public IActionResult Index()
        {
            ViewBag.numberofcustomer = _context.Useraccounts.Count();
            ViewBag.Sales = _context.Orderproducts.Sum(x => x.Totalamount);
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View();
        }

        public IActionResult JoinTable(DateTime? startDate, DateTime? endDate)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var orderproduct = _context.Orderproducts.ToList();
            var useraccount = _context.Useraccounts.ToList();
            var order = _context.Order1s.ToList();
            var product = _context.Product1s.ToList();


            var join = from u in useraccount
                       join o in order
                       on u.Userid equals o.Userid
                       join op in orderproduct
                       on o.Orderid equals op.Orderid
                       join p in product
                       on op.Productid equals p.Productid

                       select new JoinUserOrder { useraccount = u, order = o, orderproduct = op, product = p };
            ViewBag.total = join.Where(x => x.orderproduct.Status == "0").Sum(x => x.orderproduct.Totalamount);
            ViewBag.amount = join.Where(x => x.orderproduct.Status == "0").Sum(x => x.orderproduct.Numberofpieces);

            if (startDate == null && endDate == null)
                return View(join);
            else if (startDate != null && endDate == null)
            {
                var result1 = join.Where(x => x.order.Orderdate.Value.Date >= startDate).ToList();
                ViewBag.total = result1.Where(x => x.orderproduct.Status == "0").Sum(x => x.orderproduct.Totalamount);
                ViewBag.amount = result1.Where(x => x.orderproduct.Status == "0").Sum(x => x.orderproduct.Numberofpieces);
                return View(result1);
            }
            else if (startDate == null && endDate != null)
            {
                var result = join.Where(x => x.order.Orderdate.Value.Date <= endDate).ToList();
                ViewBag.total = result.Where(x => x.orderproduct.Status == "0").Sum(x => x.orderproduct.Totalamount);
                ViewBag.amount = result.Where(x => x.orderproduct.Status == "0").Sum(x => x.orderproduct.Numberofpieces);

                return View(result);
            }
            else
            {
                var result = join.Where(x => x.order.Orderdate.Value.Date <= endDate && x.order.Orderdate.Value.Date >= startDate).ToList();
                ViewBag.total = result.Where(x => x.orderproduct.Status == "0").Sum(x => x.orderproduct.Totalamount);
                ViewBag.amount = result.Where(x => x.orderproduct.Status == "0").Sum(x => x.orderproduct.Numberofpieces);

                return View(result);
            }
        }

        //public IActionResult JoinTable()
        //{
        //    ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
        //    ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
        //    ViewBag.Email = HttpContext.Session.GetString("Email");
        //    var orderproduct = _context.Orderproducts.ToList();
        //    var useraccount = _context.Useraccounts.ToList();
        //    var order = _context.Order1s.ToList();
        //    var product = _context.Product1s.ToList();


        //    var join = from u in useraccount
        //               join o in order
        //               on u.Userid equals o.Userid
        //               join op in orderproduct
        //               on o.Orderid equals op.Orderid
        //               join p in product
        //               on op.Productid equals p.Productid

        //               select new JoinUserOrder { useraccount = u, order = o, orderproduct = op , product= p};
        //    return View(join);
        //}
        public IActionResult PaiedJoinTable(DateTime? startDate, DateTime? endDate)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var orderproduct = _context.Orderproducts.ToList();
            var useraccount = _context.Useraccounts.ToList();
            var order = _context.Order1s.ToList();
            var product = _context.Product1s.ToList();
            ViewBag.Sales = _context.Orderproducts.Sum(x => x.Totalamount);
            ViewBag.orders = _context.Orderproducts.Count(x => x.Status == "1");


            var join = from u in useraccount
                       join o in order
                       on u.Userid equals o.Userid
                       join op in orderproduct
                       on o.Orderid equals op.Orderid
                       join p in product
                       on op.Productid equals p.Productid

                       select new JoinUserOrder { useraccount = u, order = o, orderproduct = op, product = p };
            ViewBag.total = join.Where(x => x.orderproduct.Status == "1").Sum(x => x.orderproduct.Totalamount);
            ViewBag.amount = join.Where(x => x.orderproduct.Status == "1").Sum(x => x.orderproduct.Numberofpieces);

            if (startDate == null && endDate == null)
                return View(join);
            else if (startDate != null && endDate == null)
            {
                var result1 = join.Where(x => x.order.Orderdate.Value.Date >= startDate).ToList();
                ViewBag.total = result1.Where(x => x.orderproduct.Status == "1").Sum(x => x.orderproduct.Totalamount);
                ViewBag.amount = result1.Where(x => x.orderproduct.Status == "1").Sum(x => x.orderproduct.Numberofpieces);
                return View(result1);
            }
            else if (startDate == null && endDate != null)
            {
                var result = join.Where(x => x.order.Orderdate.Value.Date <= endDate).ToList();
                ViewBag.total = result.Where(x => x.orderproduct.Status == "1").Sum(x => x.orderproduct.Totalamount);
                ViewBag.amount = result.Where(x => x.orderproduct.Status == "1").Sum(x => x.orderproduct.Numberofpieces);

                return View(result);
            }
            else
            {
                var result = join.Where(x => x.order.Orderdate.Value.Date <= endDate && x.order.Orderdate.Value.Date >= startDate).ToList();
                ViewBag.total = result.Where(x => x.orderproduct.Status == "1").Sum(x => x.orderproduct.Totalamount);
                ViewBag.amount = result.Where(x => x.orderproduct.Status == "1").Sum(x => x.orderproduct.Numberofpieces);

                return View(result);
            }
        }
        public IActionResult Dashboard()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");

            ViewBag.numberofcustomer = _context.Useraccounts.Count();
            ViewBag.numberofproducts = _context.Product1s.Count();
            ViewBag.numberofOrders = _context.Order1s.Count();
            ViewBag.numberofShops = _context.Shop1s.Count();
            ViewBag.numberofcategories = _context.Category1s.Count();
            var orderproduct = _context.Orderproducts.ToList();
            var useraccount = _context.Useraccounts.ToList();
            var order = _context.Order1s.ToList();
            var product = _context.Product1s.ToList();
            var shop = _context.Shop1s.ToList();
            var most= (from s in shop
             orderby s.Totalsales descending
             select s).Take(5);
            var join = from u in useraccount
                       join o in order
                       on u.Userid equals o.Userid
                       join op in orderproduct
                       on o.Orderid equals op.Orderid
                       join p in product
                       on op.Productid equals p.Productid

                       select new JoinUserOrder { useraccount = u, order = o, orderproduct = op, product = p };
            ViewBag.sales = join.Where(x => x.orderproduct.Status == "1").Sum(x => x.orderproduct.Totalamount);


            var testimonial = _context.Testimonials.ToList();
            var mainpage = _context.Mainpages.ToList();
            var contactus = _context.Contactus.ToList();
            var lastFiveRegister = (from u in useraccount
                                    orderby u.Userid descending
                                    select u).Take(5);
            var lastFivemsg = (from c in contactus
                               orderby c.Contid descending
                               select c).Take(5);
            var lastFivetest = (from t in testimonial
                                orderby t.Testmoninalid descending
                                select t).Take(4);
            var home = Tuple.Create<IEnumerable<Testimonial>, IEnumerable<Mainpage>, IEnumerable<Useraccount>, IEnumerable<Contactu>, IEnumerable<Shop1>>(lastFivetest, mainpage, lastFiveRegister, lastFivemsg, most);

            return View(home);
        }
        public IActionResult Reports()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var orderproduct = _context.Orderproducts.ToList();
            var useraccount = _context.Useraccounts.ToList();
            var order = _context.Order1s.ToList();
            var product = _context.Product1s.ToList();

            var join = from u in useraccount
                       join o in order
                       on u.Userid equals o.Userid
                       join op in orderproduct
                       on o.Orderid equals op.Orderid
                       join p in product
                       on op.Productid equals p.Productid

                       select new JoinUserOrder { useraccount = u, order = o, orderproduct = op, product = p };
            ViewBag.sales = join.Where(x => x.orderproduct.Status == "1").Sum(x => x.orderproduct.Totalamount);
            ViewBag.numberof = join.Where(x => x.orderproduct.Status == "1").Sum(x => x.orderproduct.Numberofpieces);
            ViewBag.orders = _context.Orderproducts.Count(x => x.Status=="1");
            ViewBag.rent = _context.Shop1s.Sum(x => x.Monthlyrent);
            ViewBag.profit = _context.Orderproducts.Sum(x => x.Product.Price - x.Product.Productvalue);
            ViewBag.total = ViewBag.profit- ViewBag.rent;
            var modelContext = _context.Productshops.Include(p => p.Product).Include(p => p.Shop);

            var shop = _context.Shop1s.ToList();
            var shops = new List<charts>();            
            //foreach (var s in shop)
            //{               
            //    var st = new charts();
            //    st.totalsales = (double)s.Totalsales;
            //    shops.Add(st);
            //}

            var model = Tuple.Create<IEnumerable<JoinUserOrder>, IEnumerable<Productshop>, IEnumerable<charts>, IEnumerable<Shop1>>(join, modelContext, shops, shop);
            return View(model);
        }
        [HttpPost]
        public IActionResult Reports(DateTime? startDate, DateTime? endDate)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var orderproduct = _context.Orderproducts.ToList();
            var useraccount = _context.Useraccounts.ToList();
            var order = _context.Order1s.ToList();
            var product = _context.Product1s.ToList();
            var join = from u in useraccount
                       join o in order
                       on u.Userid equals o.Userid
                       join op in orderproduct
                       on o.Orderid equals op.Orderid
                       join p in product
                       on op.Productid equals p.Productid
                       select new JoinUserOrder { useraccount = u, order = o, orderproduct = op, product = p };
            ViewBag.sales = join.Where(x => x.orderproduct.Status == "1").Sum(x => x.orderproduct.Totalamount);
            ViewBag.numberof = join.Where(x => x.orderproduct.Status == "1").Sum(x => x.orderproduct.Numberofpieces);
            ViewBag.orders = join.Count(x => x.orderproduct.Status == "1");
            ViewBag.rent = _context.Shop1s.Sum(x => x.Monthlyrent);
            ViewBag.val = join.Where(x => x.orderproduct.Status == "1").Sum(x => x.product.Productvalue);
            ViewBag.profit = ViewBag.sales- ViewBag.val;
            ViewBag.total = ViewBag.profit - ViewBag.rent;
            var modelContext = _context.Productshops.Include(p => p.Product).Include(p => p.Shop);

            if (startDate == null && endDate == null)
            {
                var model = Tuple.Create<IEnumerable<JoinUserOrder>, IEnumerable<Productshop>>(join, modelContext);
                return View(model);
            }
            else if (startDate != null && endDate == null)
            {
                var result1 = join.Where(x => x.order.Orderdate.Value.Date >= startDate).ToList();
                var model = Tuple.Create<IEnumerable<JoinUserOrder>, IEnumerable<Productshop>>(result1, modelContext);
                ViewBag.Sales = result1.Sum(x => x.orderproduct.Totalamount);
                ViewBag.numberof = result1.Sum(x => x.orderproduct.Numberofpieces);
                ViewBag.orders = result1.Count(x => x.orderproduct.Status == "1");
                return View(model);
            }
            else if (startDate == null && endDate != null)
            {
                var result = join.Where(x => x.order.Orderdate.Value.Date <= endDate).ToList();
                var model = Tuple.Create<IEnumerable<JoinUserOrder>, IEnumerable<Productshop>>(result, modelContext);
                ViewBag.Sales = result.Sum(x => x.orderproduct.Totalamount);
                ViewBag.numberof = result.Sum(x => x.orderproduct.Numberofpieces);
                ViewBag.orders = result.Count(x => x.orderproduct.Status == "1");
                return View(model);
            }
            else
            {
                var result = join.Where(x => x.order.Orderdate.Value.Date <= endDate && x.order.Orderdate.Value.Date >= startDate).ToList();
                var model = Tuple.Create<IEnumerable<JoinUserOrder>, IEnumerable<Productshop>>(result, modelContext);
                ViewBag.Sales = result.Sum(x => x.orderproduct.Totalamount);
                ViewBag.numberof = result.Sum(x => x.orderproduct.Numberofpieces);
                ViewBag.orders = result.Count(x => x.orderproduct.Status == "1");
                return View(model);
            }
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

            return View(useraccount);
        }

        // POST: Useraccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Userid,Fullname,Phonenumber,Image,Email,Password,ImageFile")] Useraccount useraccount)
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
                return RedirectToAction(nameof(Dashboard));
            }
            return View(useraccount);
        }
        //public IActionResult Search()
        //{
        //    ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
        //    ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
        //    ViewBag.Email = HttpContext.Session.GetString("Email");
        //    var modelContext = _context.Orderproducts.Include(o => o.Order).Include(o => o.Product);
        //    return View(modelContext);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Search(DateTime? startDate, DateTime? endDate)
        //{
        //    var modelContext = _context.Orderproducts
        //    .Include(p => p.Order)
        //    .Include(p => p.Product);
        //    if (startDate == null && endDate == null)
        //        return View(modelContext);
        //    else if (startDate != null && endDate == null)
        //    {
        //        var result1 = modelContext.Where(x => x.Order.Orderdate.Value.Date >= startDate).ToListAsync();
        //        return View(result1);
        //    }
        //    else if (startDate == null && endDate != null)
        //    {
        //        var result = await modelContext.Where(x => x.Order.Orderdate.Value.Date <= endDate).ToListAsync();
        //        return View(modelContext);
        //    }
        //    else
        //    {
        //        var result = await modelContext.Where(x => x.Order.Orderdate.Value.Date <= endDate && x.Order.Orderdate.Value.Date >= startDate).ToListAsync();
        //        return View(result);
        //    }
        //}



        private bool UseraccountExists(decimal id)
        {
            return _context.Useraccounts.Any(e => e.Userid == id);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
