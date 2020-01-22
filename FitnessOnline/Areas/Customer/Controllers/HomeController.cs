using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FitnessOnline.Models;
using Microsoft.AspNetCore.Identity;
using FitnessOnline.Data;
using System.IO;
using FitnessOnline.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace FitnessOnline.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {

       
        private readonly ApplicationDbContext _dbContext;
        private readonly SignInManager<ApplicationUser> _SigninManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(SignInManager<ApplicationUser> SignInManager, UserManager<ApplicationUser> UserManager, ApplicationDbContext dbContext)
        {
            _SigninManager = SignInManager;
            _dbContext = dbContext;
            _userManager = UserManager;
        }


       
        public async Task<IActionResult> Index()
        {
            IndexViewModel IndexVm = new IndexViewModel()
            {
                Subscription = await _dbContext.Subscriptions.ToListAsync()
            };

             return View(IndexVm);

        }


        public FileContentResult PackagePhotos(int? Id)
        {

            var _subscription = _dbContext.Subscriptions.Where(x => x.Id == Id).FirstOrDefault();

            return new FileContentResult(_subscription.SubscriptionImage, "image/jpeg");

        }


        //
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


       
        public FileContentResult UserPhotos()
        {
            if(User.Identity.IsAuthenticated)
            {
                string UserId = _SigninManager.UserManager.GetUserId(User);
                if(UserId == null)
                {
                    // Not logged in default Image
                    string imagePath= Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images", "noprofileimage.png");

                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(imagePath);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");

                }

                // Get user details to load Profile Image



                int Id = Int32.Parse(UserId);

               
                var userImage = _dbContext.ApplicationUsers.Where(x => x.Id == Id).FirstOrDefault();

                return new FileContentResult(userImage.ProfileImage, "image/jpeg");
                
            }
            else
            {

                string  imagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images", "noprofileimage.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(imagePath);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);

                return File(imageData, "image/png");
            }



        }
    }
}
