using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZooApp.Models;

namespace ZooApp.Controllers
{
    public class AnimalFoodsController : Controller
    {
        private ZooContext db = new ZooContext();

        // GET: AnimalFoods
        public async Task<ActionResult> Index()
        {
            var animalFoods = db.AnimalFoods.Include(a => a.Animal).Include(a => a.Food);

            List<AnimalFoodTotal> totals = new List<AnimalFoodTotal>();

            foreach (AnimalFood animalFood in animalFoods)
            {
                AnimalFoodTotal foodTotal = new AnimalFoodTotal(animalFood);
                totals.Add(foodTotal);
            }


            List<AnimalFoodTotal> result = new List<AnimalFoodTotal>();

            var groupBy =totals.GroupBy(x=>x.FoodName);

            foreach (IGrouping<string,  AnimalFoodTotal> foodTotals in groupBy)
            {
                double totalPrice =foodTotals.Sum(x => x.TotalPrice);

                double totalQuantity =  foodTotals.Sum(x => x.TotalQuantity);

                AnimalFoodTotal foodTotal = new AnimalFoodTotal()
                {
                    FoodName = foodTotals.Key,
                    FoodPrice = foodTotals.First().FoodPrice,


                    TotalPrice = totalPrice,
                    TotalQuantity = totalQuantity
                };
                result.Add(foodTotal);
            }

            return View(result);
        }

        // GET: AnimalFoods/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnimalFood animalFood = await db.AnimalFoods.FindAsync(id);
            if (animalFood == null)
            {
                return HttpNotFound();
            }
            return View(animalFood);
        }

        // GET: AnimalFoods/Create
        public ActionResult Create()
        {
            ViewBag.AnimalId = new SelectList(db.Animals, "Id", "Name");
            ViewBag.FoodId = new SelectList(db.Foods, "Id", "Name");
            return View();
        }

        // POST: AnimalFoods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,AnimalId,FoodId,Quantiy")] AnimalFood animalFood)
        {
            if (ModelState.IsValid)
            {
                db.AnimalFoods.Add(animalFood);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AnimalId = new SelectList(db.Animals, "Id", "Name", animalFood.AnimalId);
            ViewBag.FoodId = new SelectList(db.Foods, "Id", "Name", animalFood.FoodId);
            return View(animalFood);
        }

        // GET: AnimalFoods/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnimalFood animalFood = await db.AnimalFoods.FindAsync(id);
            if (animalFood == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnimalId = new SelectList(db.Animals, "Id", "Name", animalFood.AnimalId);
            ViewBag.FoodId = new SelectList(db.Foods, "Id", "Name", animalFood.FoodId);
            return View(animalFood);
        }

        // POST: AnimalFoods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,AnimalId,FoodId,Quantiy")] AnimalFood animalFood)
        {
            if (ModelState.IsValid)
            {
                db.Entry(animalFood).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AnimalId = new SelectList(db.Animals, "Id", "Name", animalFood.AnimalId);
            ViewBag.FoodId = new SelectList(db.Foods, "Id", "Name", animalFood.FoodId);
            return View(animalFood);
        }

        // GET: AnimalFoods/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnimalFood animalFood = await db.AnimalFoods.FindAsync(id);
            if (animalFood == null)
            {
                return HttpNotFound();
            }
            return View(animalFood);
        }

        // POST: AnimalFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AnimalFood animalFood = await db.AnimalFoods.FindAsync(id);
            db.AnimalFoods.Remove(animalFood);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
