using FitnessOnline.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessOnline.Models
{
    public class dbInitializer
    {

        public static void Seed(ApplicationDbContext context)
        {
           string NormalPackageImagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images", "package1.jpg");
           string PremiumPackageImagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images", "package2.jpg");

            Byte[] byteNormalPackageImage = File.ReadAllBytes(NormalPackageImagePath);
            Byte[] bytePremiumPackageImage = File.ReadAllBytes(PremiumPackageImagePath);

         if (!context.Subscriptions.Any())
            {
                context.AddRange(

                      new Subscription { Name = "Normal Package", Price = 250, SubscriptionImage = byteNormalPackageImage, Description = "Whether you are looking to gain muscle, lose fat, get lean and defined or all of the above, the 12 Week Body Transformation is for you. During his 25-plus years of experience as a Master Trainer, Gisselle Zapata has learned one indisputable truth: Every body is different. That’s why each Body Transformation Program is customized to help you reach your individual goal, taking all aspects of your life and level of fitness into consideration. When you sign up for a Body By O Transformation Program, Kim personally attends to your progress and specific needs every step of the way through phone updates or in-person appointments for local residents. If you are looking to improve your appearance and overall quality of life, the Body Transformation Program is the best investment you’ll ever make!" },
                      new Subscription { Name = "Premium Package", Price = 350, SubscriptionImage = bytePremiumPackageImage, Description = "Training for a marathon, triathlon, mud run or to raise your level of performance for your current team, the Performance Program combines Gisselle's expert instruction and cutting-edge training methodologies to help you achieve your peak performance level regardless of age, gender or skill." }

                    );

                context.SaveChanges();
            }
        }
    }
}
