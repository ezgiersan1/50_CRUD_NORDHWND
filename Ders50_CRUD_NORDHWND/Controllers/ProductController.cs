using Microsoft.AspNetCore.Mvc;
using Ders50_CRUD_NORDHWND.Models;
using Microsoft.EntityFrameworkCore;

namespace Ders50_CRUD_NORDHWND.Controllers
{
    public class ProductController : Controller
    {

        NorthwindContext context = new NorthwindContext();

        public async Task<IActionResult> Index()
        {
            //select* from Products order by ProductID desc--ado.net 

            //efcore
            return View(await context.Products.OrderByDescending(p => p.ProductID).ToListAsync());
            //linq, dapper
        }

        //metod overload: aynı metodu aynı parametre sırasıyla yazamayız


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductName,UnitPrice")] Product product)
        {
            Product p = new Product();
            p.UnitPrice = 500;
            if (ModelState.IsValid) //zorunlu alanlar konrolü
            {
                context.Add(product);
                await context.SaveChangesAsync();
                //context.SaveChanges();
                //return RedirectToAction("Index"); //Create.cshtml
                //return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index");
            //return View(product);          
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || context.Products == null)
            {
                return NotFound();
            }

            var product = await context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductName,UnitPrice, ProductID")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid) //zorunlu alanlar konrolü
            {
                try
                {
                    context.Update(product);
                    await context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    if (ProductExists(product.ProductID))
                    {
                        return NotFound();
                    }
                }


            }
            return View(product);
        }


        private bool ProductExists(int id)
        {
            return context.Products.Any(p => p.ProductID == id);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.Products == null)
            {
                return NotFound();
            }

            var product = await context.Products.FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")] //routing
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (context.Products == null)
            {
                return NotFound();
            }

            var product = await context.Products.FindAsync(id);

            if (product != null)
            {
                context.Products.Remove(product);
            }
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id==null || context.Products == null)
            {
                return NotFound();
            }

            var product = await context.Products.FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }


            return View(product);
        }

    }
    

}

