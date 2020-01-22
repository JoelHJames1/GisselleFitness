using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FitnessOnline.Data;
using FitnessOnline.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FitnessOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageUserController : Controller
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly SignInManager<ApplicationUser> _SigninManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageUserController(SignInManager<ApplicationUser> SignInManager, UserManager<ApplicationUser> UserManager, ApplicationDbContext dbContext)
        {
            _SigninManager = SignInManager;
            _dbContext = dbContext;
            _userManager = UserManager;
        }


        //GET
        public async Task<IActionResult> Index()
        {
            //
            return View(await _dbContext.ApplicationUsers.ToListAsync());
        }

        // GET CREATE
        public IActionResult Create()
        {
            return View();
        }

        // POST CREATE

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUser appUser)
        {
            if (ModelState.IsValid)
            {
                //If valid save subscription data
                _dbContext.Add(appUser);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }

            return View(appUser);
        }

        //GET EDIT

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _dbContext.ApplicationUsers.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }


        [Area("Admin")]
        public FileContentResult UserPhotos(int? Id)
        {
        
                var userImage = _dbContext.ApplicationUsers.Where(x => x.Id == Id).FirstOrDefault();

                return new FileContentResult(userImage.ProfileImage, "image/jpeg");

        }


        //Update Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser AppUser)
        {

         

            if (ModelState.IsValid)
            {
                // _dbContext.Entry(AppUser).State = EntityState.Modified;

                var person = _dbContext.ApplicationUsers.Single(p => p.Id == AppUser.Id);
                
                using (MemoryStream memorystream = new MemoryStream())
                {
                    await AppUser.UserPhoto.CopyToAsync(memorystream);
                    person.ProfileImage = memorystream.ToArray();
                }

                person.FirstName = AppUser.FirstName;
                person.LastName = AppUser.LastName;
                person.Email = AppUser.Email;
                person.PhoneNumber = AppUser.PhoneNumber;
                person.StreetAddress = AppUser.StreetAddress;
                person.City = AppUser.City;
                person.State = AppUser.State;
                person.PostalCode = AppUser.PostalCode;
               
              //  _dbContext.ApplicationUsers.Attach(person);
               // _dbContext.Entry(person).Property("ProfileImage").IsModified = true;
                _dbContext.Update(person);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(AppUser);
        }





        //GET - Delete

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var AppUser = await _dbContext.ApplicationUsers.FindAsync(id);
            if (AppUser == null)
            {
                return NotFound();
            }

            return View(AppUser);
        }


        //Post - Delete  

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var AppUser = await _dbContext.ApplicationUsers.FindAsync(id);

            if (AppUser == null)
            {
                return View();
            }

            _dbContext.ApplicationUsers.Remove(AppUser);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }


    }
}