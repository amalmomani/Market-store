using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Market_store.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Market_store.Controllers
{
    public class Product1Controller : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webhostEnvironment;
        public Product1Controller(ModelContext context, IWebHostEnvironment _webHostEnvironment)
        {
            _context = context;
            //assign initial value variable
            webhostEnvironment = _webHostEnvironment;
        }
        // GET: Product1
        public async Task<IActionResult> Index()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(await _context.Product1s.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string? product)
        {
            var modelContext = _context.Product1s;
            if (product != null)
            {
                var result = await modelContext.Where(x => x.Productname.ToUpper().Contains(product.ToUpper())).ToListAsync();
                return View(result);
            }
            return View(modelContext);
        }
        public IActionResult JoinShopProduct(int id)
        {

            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            HttpContext.Session.SetInt32("shopid", (int)id);
            var product = _context.Product1s.ToList();
            var orderproducts = _context.Orderproducts.ToList();
            var productshop = _context.Productshops.Where(x => x.Shopid == id);
            var shop = _context.Shop1s.ToList();
            var join = from p in product
                       join ps in productshop
                       on p.Productid equals ps.Productid
                       join s in shop
                       on ps.Shopid equals s.Shopid
                       select new JoinShopProduct { product = p, productshop = ps, shop = s };
            return View(join);
        }
        //[HttpPost]
        //public async Task<IActionResult> JoinShopProduct(string? productt)
        //{
        //    ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
        //    ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
        //    ViewBag.Email = HttpContext.Session.GetString("Email");
        //    var product = _context.Product1s.ToList();
        //    var orderproducts = _context.Orderproducts.ToList();
        //    var productshop = _context.Productshops.ToList();
        //    var shop = _context.Shop1s.ToList();
        //    var join = from p in product
        //               join ps in productshop
        //               on p.Productid equals ps.Productid
        //               join s in shop
        //               on ps.Shopid equals s.Shopid
        //               select new JoinShopProduct { product = p, productshop = ps, shop = s };
        //    if (productt != null)
        //    {
        //        var result = join.Where(x => x.product.Productname.ToUpper().Contains(productt.ToUpper())).ToList();
        //        return View(result);
        //    }
        //    return View(join);
        //}
        public IActionResult Products()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var product = _context.Product1s.ToList();
            var orderproducts = _context.Orderproducts.ToList();
            var productshop = _context.Productshops.ToList();
            var shop = _context.Shop1s.ToList();
            var join = from p in product
                       join ps in productshop
                       on p.Productid equals ps.Productid
                       join s in shop
                       on ps.Shopid equals s.Shopid
                       select new JoinShopProduct { product = p, productshop = ps, shop = s};
            return View(join);
        }
        [HttpPost]
        public async Task<IActionResult> Products(string? productt)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var product = _context.Product1s.ToList();
            var orderproducts = _context.Orderproducts.ToList();
            var productshop = _context.Productshops.ToList();
            var shop = _context.Shop1s.ToList();
            var join = from p in product
                       join ps in productshop
                       on p.Productid equals ps.Productid
                       join s in shop
                       on ps.Shopid equals s.Shopid
                       select new JoinShopProduct { product = p, productshop = ps, shop = s };
            if (productt != null)
            {
                var result = join.Where(x => x.product.Productname.ToUpper().Contains(productt.ToUpper())).ToList();
                return View(result);
            }
            return View(join);
        }
        public IActionResult AddToCart(int id)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var product = _context.Product1s.Where(x => x.Productid == id);

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int id, int Numberofpieces)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.shopid = HttpContext.Session.GetInt32("shopid");

            var product = _context.Product1s.Where(x => x.Productid == id).FirstOrDefault();
            Order1 order = new Order1();
            order.Userid = ViewBag.Userid;
            DateTime now = DateTime.Now;
            order.Orderdate = now;
            order.Status = "0";
            _context.Order1s.Add(order);
            await _context.SaveChangesAsync();
            Orderproduct orderproduct = new Orderproduct();
            orderproduct.Orderid = order.Orderid;
            orderproduct.Numberofpieces = Numberofpieces;
            orderproduct.Totalamount = Numberofpieces * product.Price;
            orderproduct.Status = "0";
            orderproduct.Shopid = ViewBag.shopid;
            orderproduct.Productid = product.Productid;
            _context.Orderproducts.Add(orderproduct);
            await _context.SaveChangesAsync();
            return RedirectToAction("Products", "Product1");
        }


        // GET: Product1/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product1 = await _context.Product1s
                .FirstOrDefaultAsync(m => m.Productid == id);
            if (product1 == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(product1);
        }

        // GET: Product1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Productid,Productname,Price,Productvalue,Image,Shopid,Productsize,ImageFile")] Product1 product1)
        {
            if (ModelState.IsValid)
            {
                if (product1.ImageFile != null)
                {
                    //1- get w3rootpath
                    string w3rootpath = webhostEnvironment.WebRootPath;
                    //Guid.NewGuid : generate unique string before image name ;
                    ////2- generate image name and add unique string
                    string fileName = Guid.NewGuid().ToString() + "_" + product1.ImageFile.FileName;
                    string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                    //4-create Image inside image file in w3root folder
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await product1.ImageFile.CopyToAsync(fileStream);
                    }

                    product1.Image = fileName;
                }
                _context.Add(product1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(product1);
        }

        // GET: Product1/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product1 = await _context.Product1s.FindAsync(id);
            if (product1 == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(product1);
        }

        // POST: Product1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Productid,Productname,Price,Productvalue,Image,Shopid,Productsize,ImageFile")] Product1 product1)
        {
            if (id != product1.Productid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (product1.ImageFile != null)
                    {
                        //1- get w3rootpath
                        string w3rootpath = webhostEnvironment.WebRootPath;
                        //Guid.NewGuid : generate unique string before image name ;
                        ////2- generate image name and add unique string
                        string fileName = Guid.NewGuid().ToString() + "_" + product1.ImageFile.FileName;
                        string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                        //4-create Image inside image file in w3root folder
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await product1.ImageFile.CopyToAsync(fileStream);
                        }

                        product1.Image = fileName;
                    }
                    _context.Update(product1);
                    await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Product1Exists(product1.Productid))
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
            return View(product1);
        }

        // GET: Product1/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product1 = await _context.Product1s
                .FirstOrDefaultAsync(m => m.Productid == id); 
            var productsh = _context.Productshops.Where(x => x.Productid == id).ToList();
            if (product1 == null)
            {
                return NotFound();
            }
           
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(product1);
        }

        // POST: Product1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var product1 = await _context.Product1s.FindAsync(id);           
            var productsh = _context.Productshops.Where(x=> x.Productid==id).ToList();

            foreach (var item in productsh)
            {
                _context.Productshops.Remove(item);
            }
            _context.Product1s.Remove(product1);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool Product1Exists(decimal id)
        {
            return _context.Product1s.Any(e => e.Productid == id);
        }
    }
}
