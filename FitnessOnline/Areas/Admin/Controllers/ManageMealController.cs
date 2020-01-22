using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessOnline.Data;
using FitnessOnline.Models;
using FitnessOnline.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FitnessOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageMealController : Controller
    {

        private readonly ApplicationDbContext _dbContext;

        [TempData]
        public string StatusMessage { get; set; }


        public ManageMealController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
           
                var mealplans = _dbContext.MealPlans
                    .Include(mealplan => mealplan.ApplicationUser)
                    .ToList();
          
            return View(mealplans);
        }

        //Get - Create
        public async Task<IActionResult> Create()
        {
            MealPlanAppUserViewModel model = new MealPlanAppUserViewModel()
            {
                UserList = await _dbContext.ApplicationUsers.ToListAsync(),
                MealPlan = new Models.MealPlan(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MealPlanAppUserViewModel model)
        {
            if(ModelState.IsValid)
            {

                List<ApplicationUser> appUsers = new List<ApplicationUser>();

                appUsers = await (from _User in _dbContext.ApplicationUsers
                                   where _User.Id == model.MealPlan.ApplicationUserId
                                   select _User).ToListAsync();

                if (appUsers.Count > 0)
                {
                    model.MealPlan.ApplicationUser = appUsers[0];

                    _dbContext.MealPlans.Add(model.MealPlan);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                

            }

            MealPlanAppUserViewModel modelVM = new MealPlanAppUserViewModel()
            {
                UserList = await _dbContext.ApplicationUsers.ToListAsync(),
                MealPlan = model.MealPlan
               
            };

            return View(modelVM);

        }


        [ActionName("GetMealPlan")]
        public async Task<IActionResult> GetMealPlan(int id)
        {
            List<MealPlan> mealplans = new List<MealPlan>();

            mealplans = await (from meals in _dbContext.MealPlans
                               where meals.Id == id
                               select meals).ToListAsync();
            return Json(new SelectList(mealplans, "Id", "Name"));
        }


        //GET EDIT

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealplan = await _dbContext.MealPlans.FindAsync(id);
            if (mealplan == null)
            {
                return NotFound();
            }

            return View(mealplan);
        }



        //Update Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MealPlan meals)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Update(meals);

                await _dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(meals);
        }

        //GET - Delete

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealplan = await _dbContext.MealPlans.FindAsync(id);
            if (mealplan == null)
            {
                return NotFound();
            }

            return View(mealplan);
        }


        //Post - Delete  

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var mealplan = await _dbContext.MealPlans.FindAsync(id);

            if (mealplan == null)
            {
                return View();
            }

            _dbContext.MealPlans.Remove(mealplan);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

    }
}
