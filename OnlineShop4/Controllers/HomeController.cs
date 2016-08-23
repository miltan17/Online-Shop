using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop4.Controllers
{
    public class HomeController : Controller
    {
        
        private int lowestPrice;
        private int heighestPrice;
        private MyDatabaseEntities db = new MyDatabaseEntities();
        public ActionResult Index(string brand,string price)
        {
            //setting viewbeg
            ViewBag.Message = "There are a lot's of collection of mobile in this shop. You can see or bye any of these mobiles from here.";
            ViewBag.brands = db.ProductGalaries.Select(r => r.Brand).Distinct();
            ViewBag.prices = new List<string>()
            {
                
                ">20000",
                "0-1000",
                "1001-3000",
                "3001-5000",
                "5001-10000",
                "10001-20000"
            };
            
            //do at first time
            if (price == null)
            {
                var models = from p in db.ProductGalaries
                             where (p.Brand == brand || (brand == null))
                             select p;

                models = models.OrderByDescending(p => p.Id);

                return View(models);
            }
                //do when search button is clicked
            else
            {
                switch (price)
                {
                    case "0-1000":
                        lowestPrice = 0;
                        heighestPrice = 1000;
                        break;
                    case "1001-3000":
                        lowestPrice = 1001;
                        heighestPrice = 3000;
                        break;
                    case "3001-5000":
                        lowestPrice = 3001;
                        heighestPrice = 5000;
                        break;
                    case "5001-10000":
                        lowestPrice = 5001;
                        heighestPrice = 10000;
                        break;
                    case "10001-20000":
                        lowestPrice = 10001;
                        heighestPrice = 20000;
                        break;
                    case ">20000":
                        lowestPrice = 20001;
                        heighestPrice = 1000000;
                        break;

                }
                 var models = from p in db.ProductGalaries
                             where (p.Brand == brand || (brand == null))
                             && (p.Price >= lowestPrice || (lowestPrice == null))
                             && (p.Price <= heighestPrice || (heighestPrice == null))
                             select p;
               
                 models = models.OrderByDescending(p => p.Id);

                 return View(models);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(ProductGalary IG)
        {
            if (IG.File.ContentLength > (2 * 1024 * 1024))
            {
                ModelState.AddModelError("CustomError", "File size must be less than 2MB");
                return View(IG);
            }

            if (!(IG.File.ContentType == "image/jpeg" || IG.File.ContentType == "image/gif"))
            {
                ModelState.AddModelError("CustomError", "File type must be jpg or gif");
                return View();
            }

            IG.Model = IG.Model;
            IG.Brand = IG.Brand;
            IG.Description = IG.Description;
            IG.Price = IG.Price;


            IG.FileName = IG.File.FileName;
            IG.ImageSize = IG.File.ContentLength;


            byte[] data = new byte[IG.File.ContentLength];
            IG.File.InputStream.Read(data, 0, IG.File.ContentLength);

            IG.ImageData = data;

            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                dc.ProductGalaries.Add(IG);
                dc.SaveChanges();
            }

           return  RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var models = db.ProductGalaries.Find(id);
            return View(models);
        }

        [Authorize]
        public ActionResult Bye()
        {
            return View();
        }
    }
}
