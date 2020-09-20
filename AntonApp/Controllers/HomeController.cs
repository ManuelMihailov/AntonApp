using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AntonApp.Models;
using Services.Contracts;
using Data.Contracts;
using Data.Models;

namespace AntonApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatusesService sService;
        private readonly IProductService pService;

        public HomeController(IStatusesService sService, IProductService pService)
        {
            this.sService = sService;
            this.pService = pService;
            //pService.CheckFirstRun();
            //sService.CheckFirstRun();
        }

        public async Task<IActionResult> Index()
        {
            var items = await pService.GetItemsAsync();
            var vm = new IndexViewModel(items);
            return View(vm);
        }

        public async Task<IActionResult> BuyProduct(string submitButton)
        {
            await pService.BuyItem(submitButton);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CompleteDelivery(string submitButton)
        {
            await pService.DeliverItem(int.Parse(submitButton));
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
