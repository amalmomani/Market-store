using MailKit.Net.Smtp;
using Market_store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Market_store.Controllers
{
    public class UserDashboardController : Controller
    {
        private readonly ModelContext _context;
        public UserDashboardController(ModelContext context)
        {
            _context = context;
            //assign initial value variable
        }
        public IActionResult SendEmail()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View();
        }
        [HttpPost]
        public IActionResult SendEmail(string to, decimal amount)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            to = ViewBag.Email;
            MimeMessage obj = new MimeMessage();
            MailboxAddress emailfrom = new MailboxAddress("Hope Shop", "shophope17@gmail.com");
            MailboxAddress emailto = new MailboxAddress(ViewBag.Fullname, to);
            obj.From.Add(emailfrom);
            obj.To.Add(emailto);
            obj.Subject = "Success Checkout! "+ ViewBag.Fullname;
            BodyBuilder msgbody = new BodyBuilder();
            // bb.TextBody = body;
            msgbody.HtmlBody = "<html>" + "<h1>" + " Greetings from Hope Shop! " + ViewBag.Fullname + "</h1>" + "</br>" + " Your bill has been paid successfully with " + "</br>" + " Total : " + amount + "$" + "</br>" + "</html>";
            obj.Body = msgbody.ToMessageBody();
            MailKit.Net.Smtp.SmtpClient emailclient = new MailKit.Net.Smtp.SmtpClient();
            emailclient.Connect("smtp.gmail.com", 465, true);
            emailclient.Authenticate("shophope17@gmail.com", "icntjjvnvveooroy");
            emailclient.Send(obj);
            emailclient.Disconnect(true);
            emailclient.Dispose();


            // string e = ViewBag.Email;


            // using System.Net.Mail.SmtpClient mySmtpClient = new System.Net.Mail.SmtpClient("smtp.outlook.com", 587);
            // mySmtpClient.EnableSsl = true;

            // mySmtpClient.UseDefaultCredentials = false;
            // NetworkCredential basicAuthenticationInfo = new
            //NetworkCredential("hopeshop99@outlook.com", "hopeshop78");

            // mySmtpClient.Credentials = basicAuthenticationInfo;
            // MailMessage message = new MailMessage("hopeshop99@outlook.com", e);
            // string body = " Greetings from Hope Shop! " + ViewBag.Fullname + " Your bill has been paid successfully with" + " Total amount of " + amount + "$";
            // message.Subject = "Success Checkout";
            // message.Body = body;
            // mySmtpClient.Send(message);

            return View();
        }


        public IActionResult JoinTable()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var orderproduct = _context.Orderproducts.ToList();
            var useraccount = _context.Useraccounts.ToList();
            var order = _context.Order1s.ToList();
            var product = _context.Product1s.ToList();
            var productshop = _context.Productshops.ToList();
            var shop = _context.Shop1s.ToList();

            var join = from u in useraccount
                       join o in order
                       on u.Userid equals o.Userid
                       join op in orderproduct
                       on o.Orderid equals op.Orderid
                       join p in product
                       on op.Productid equals p.Productid                      
                       select new JoinUserOrder { useraccount = u, order = o, orderproduct = op, product = p};
            ViewBag.total = join.Where(x => x.orderproduct.Status == "0" && x.order.Userid == ViewBag.Userid).Sum(x => x.orderproduct.Totalamount);
            ViewBag.amount = join.Where(x => x.orderproduct.Status == "0" && x.order.Userid == ViewBag.Userid).Sum(x => x.orderproduct.Numberofpieces);

            return View(join);
        }
      
       
        public async Task<IActionResult> Delete(decimal id)
        {
            var orderproduct = await _context.Orderproducts.FindAsync(id);
            _context.Orderproducts.Remove(orderproduct);
            await _context.SaveChangesAsync();
            return RedirectToAction("JoinTable","UserDashboard");
        }
        public IActionResult Paied(DateTime? startDate, DateTime? endDate)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var orderproduct = _context.Orderproducts.ToList();
            var useraccount = _context.Useraccounts.ToList();
            var order = _context.Order1s.ToList();
            var product = _context.Product1s.ToList();
            var productshop = _context.Productshops.ToList();

            var join = from u in useraccount
                       join o in order
                       on u.Userid equals o.Userid
                       join op in orderproduct
                       on o.Orderid equals op.Orderid
                       join p in product
                       on op.Productid equals p.Productid
                       select new JoinUserOrder { useraccount = u, order = o, orderproduct = op, product = p };
            ViewBag.total = join.Where(x=>x.orderproduct.Status=="1" && x.order.Userid== ViewBag.Userid).Sum(x => x.orderproduct.Totalamount);
            ViewBag.amount = join.Where(x => x.orderproduct.Status == "1" && x.order.Userid == ViewBag.Userid).Sum(x => x.orderproduct.Numberofpieces);

            if (startDate == null && endDate == null)
                return View(join);
            else if (startDate != null && endDate == null)
            {
                var result1 = join.Where(x => x.order.Orderdate.Value.Date >= startDate).ToList();
                ViewBag.total = result1.Where(x => x.orderproduct.Status == "1" && x.order.Userid == ViewBag.Userid).Sum(x => x.orderproduct.Totalamount);
                ViewBag.amount = result1.Where(x => x.orderproduct.Status == "1" && x.order.Userid == ViewBag.Userid).Sum(x => x.orderproduct.Numberofpieces);
                return View(result1);
            }
            else if (startDate == null && endDate != null)
            {
                var result = join.Where(x => x.order.Orderdate.Value.Date <= endDate).ToList();
                ViewBag.total = result.Where(x => x.orderproduct.Status == "1" && x.order.Userid == ViewBag.Userid).Sum(x => x.orderproduct.Totalamount);
                ViewBag.amount = result.Where(x => x.orderproduct.Status == "1" && x.order.Userid == ViewBag.Userid).Sum(x => x.orderproduct.Numberofpieces);

                return View(join);
            }
            else
            {
                var result = join.Where(x => x.order.Orderdate.Value.Date <= endDate && x.order.Orderdate.Value.Date >= startDate).ToList();
                ViewBag.total = result.Where(x => x.orderproduct.Status == "1" && x.order.Userid == ViewBag.Userid).Sum(x => x.orderproduct.Totalamount);
                ViewBag.amount = result.Where(x => x.orderproduct.Status == "1" && x.order.Userid == ViewBag.Userid).Sum(x => x.orderproduct.Numberofpieces);

                return View(result);
            }
        }
        public IActionResult Payment()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View();
        }

        public async Task<IActionResult> StroeSale(List<Orderproduct> orderproduct)
        {
            decimal total = 0;

            var stores = _context.Shop1s.ToList();
            foreach (var s in stores)
            {
                foreach (var op in orderproduct)
                {
                    if (s.Shopid == op.Shopid)
                    {
                        total = total + (decimal)op.Totalamount;

                    }

                }
                s.Totalsales = s.Totalsales + total;
                _context.Shop1s.Update(s);
                total = 0;

            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Paied", "UserDashboard");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Payment([Bind()] Bank bank, Payment payment)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.flag = 1;
            ViewBag.emptyorder = 0;
            ViewBag.amount = 1;


            var userid = ViewBag.Userid;
            var auth = _context.Banks.Where(data => data.Cardnumber == bank.Cardnumber && data.Cvv == bank.Cvv).SingleOrDefault();
            if (auth != null)
            {
                var Order = _context.Order1s.ToList();
                var OrderProduct = _context.Orderproducts.ToList();
                List<decimal> orderid = new List<decimal>();
                decimal total = 0;
                foreach (var item in Order)
                {
                    if (userid == item.Userid && item.Status == "0")
                    {
                        orderid.Add(item.Orderid);
                        item.Status = "1";
                        _context.Order1s.Update(item);
                    }
                }
                if (orderid.Count != 0)
                {
                    List<Orderproduct> orderproduct = new List<Orderproduct>();
                    foreach (var item in _context.Orderproducts)
                    {
                        foreach (var o in orderid)
                        {
                            if (item.Orderid == o)
                            {
                                var order = item;
                                orderproduct.Add(order);
                                item.Status = "1";
                                _context.Update(item);
                            }
                        }
                    }
                    foreach (var p in orderproduct)
                    {
                        total = (decimal)(total + p.Totalamount);
                        _context.Orderproducts.Update(p);
                    }


                    if (auth.Amount >= total)
                    {
                        auth.Amount = auth.Amount - total;

                        DateTime now = DateTime.Now;
                        payment.Paydate = now;
                        payment.Cardnumber = auth.Cardnumber;
                        payment.Amount = total;
                        payment.Userid = userid;

                        _context.Payments.Add(payment);
                        await _context.SaveChangesAsync();
                        SendEmail(ViewBag.Email, total);
                        await StroeSale(orderproduct);
                        return RedirectToAction("Paied", "UserDashboard");
                    }
                    else
                        ViewBag.amount = 0;
                }
                else
                {
                    ViewBag.emptyorder = 1;
                }
            }
            else if (auth == null)
            {
                ViewBag.flag = 0;
            }
            return View();

        }

    }
}
