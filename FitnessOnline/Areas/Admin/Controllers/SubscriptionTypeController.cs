using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessOnline.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubscriptionTypeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public SubscriptionTypeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //GET
        public async Task<IActionResult> Index()
        {
            //
            return View(await _dbContext.SubscriptionType.ToListAsync());
        }

        // GET CREATE
        public IActionResult Create()
        {
            return View();
        }
    }
}