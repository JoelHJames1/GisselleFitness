using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FitnessOnline.Data;
using FitnessOnline.Models;
using FitnessOnline.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubscriptionController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public SubscriptionController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //GET
        public async Task<IActionResult> Index()
        {
            //
            return View(await _dbContext.Subscriptions.ToListAsync());
        }

        // GET CREATE
        public IActionResult Create()
        {
            return View();
        }

        // POST CREATE

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubscriptionViewModel subscriptionVM)
        {
            Subscription subscription = new Subscription();


            if (ModelState.IsValid)
            {
                using (MemoryStream memorystream = new MemoryStream())
                {
                    await subscriptionVM.SubscriptionImage.CopyToAsync(memorystream);
                    subscription.SubscriptionImage = memorystream.ToArray();
                    subscription.Name = subscriptionVM.Name;
                    subscription.Price = subscriptionVM.Price;
                    subscription.Description = subscriptionVM.Description;
                }


                //If valid save subscription data
                _dbContext.Add(subscription);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }

            return View(subscriptionVM);
        }






        //GET EDIT

        public async Task<IActionResult> Edit(int? id)
        {

           

            if(id == null)
            {
                return NotFound();
            }

            var subscription = await _dbContext.Subscriptions.FindAsync(id);

            if (subscription == null)
            {
                return NotFound();
            }

            SubscriptionViewModel _subscriptionViewModel = new SubscriptionViewModel()
            {
                Id = subscription.Id,
                Name = subscription.Name,
                Description = subscription.Description,
                Price = subscription.Price
            };

            return View(_subscriptionViewModel);
        }




        //Update Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SubscriptionViewModel subscriptionVM)
        {
            Subscription subscription = new Subscription();

            if(ModelState.IsValid)
            {

             //   using (MemoryStream memorystream = new MemoryStream())
             //   {
                    subscription.Id = subscriptionVM.Id;
                 //   await subscriptionVM.SubscriptionImage.CopyToAsync(memorystream);
                  //  subscription.SubscriptionImage = memorystream.ToArray();
                    subscription.Name = subscriptionVM.Name;
                    subscription.Price = subscriptionVM.Price;
                    subscription.Description = subscriptionVM.Description;
             //   }

                _dbContext.Update(subscription);

                await _dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(subscription);
        }


        //GET - Delete

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _dbContext.Subscriptions.FindAsync(id);
            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }


        //Post - Delete  

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var subscription = await _dbContext.Subscriptions.FindAsync(id);
           
             if(subscription == null)
            {
                return View();
            }

            _dbContext.Subscriptions.Remove(subscription);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }


    }
}