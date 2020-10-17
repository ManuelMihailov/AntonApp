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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace AntonApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatusesService sService;
        private readonly IProductService pService;
        private readonly IAccountService aService;

        public HomeController(IStatusesService sService, IProductService pService, IAccountService aService)
        {
            this.sService = sService;
            this.pService = pService;
            this.aService = aService;
        }

        public async Task<IActionResult> Index()
        {
            //await sService.CheckFirstRun();
            //await pService.CheckFirstRun();
            //await aService.CreateTestAdmin();
            var items = await pService.GetItemsAsync();
            var vm = new IndexViewModel(items);
            var requests = await pService.GetRequestsAsync();
            foreach (var item in requests)
            {
                var requestVM = new RequestViewModel(item, await pService.CheckWarehousesAsync(item.Product.Id));
                vm.Requests.Add(requestVM);
            }
            return View(vm);
        }

        public IActionResult AddProducts()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProducts(AddProductsViewModel vm)
        {
            await pService.AddProduct(vm.Name, vm.Price, vm.Count, vm.WarehouseCity);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BuyProduct(string submitButton)
        {
            var userId = int.Parse(this.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            await pService.BuyItem(submitButton, userId);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CompleteDelivery(string submitButton)
        {
            await pService.DeliverItem(int.Parse(submitButton));
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmRequest(IndexViewModel vm)
        {
            await pService.ConfirmRequest(int.Parse(vm.RequestId), vm.Warehouse);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ViewLogin()
        {
            var vm = new LoginViewModel();
            return View("Login", vm);
        }

        public IActionResult ViewRegister()
        {
            var vm = new RegisterViewModel();
            return View("Register", vm);
        }

        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!this.ModelState.IsValid)
                return View("Register", vm);
            try
            {
                if (vm.Password == vm.ConfirmP && vm.Password.Length >= 6)
                {
                    await this.aService.AddAccountAsync(vm.Username, vm.Password, vm.City);
                    var user = await aService.AttemptUserLoginAsync(vm.Username, vm.Password);
                    SignInUser(user);
                }
                else
                {
                    return View("Register", vm);
                }
                return ReturnHome();
            }
            catch (Exception)
            {
                return View("Register", vm);
            }
        }

        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!this.ModelState.IsValid)
                return View("Login", vm);

            try
            {
                var user = await this.aService.AttemptUserLoginAsync(vm.Email, vm.Password);
                SignInUser(user);
            }
            catch (Exception ex) { }
            return ReturnHome();
        }

        public async Task<IActionResult> Logout()
        {
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return ReturnHome();
        }

        private async void SignInUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.AccountType)

            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24),
                IssuedUtc = DateTimeOffset.UtcNow,
                IsPersistent = true,
            };

            await this.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
        private RedirectToActionResult ReturnHome()
          => RedirectToAction("Index", "Home");

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
