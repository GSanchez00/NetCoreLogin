using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace IdentityDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            roleManager = _roleManager;
        }

        public async Task<IActionResult> Index()
        {
           if (User.Identity.IsAuthenticated)
           {
                //userManager.AddClaimAsync();
                //Agregar un usuario a un rol
                //userManager.AddToRoleAsync();

                //userManager.ChangeEmailAsync();
                //userManager.ChangePhoneNumberAsync();
                //Te pide que vuelvas a colocar tu password para confirmarlo, por ejemplo al cambiar el password. 
                //userManager.CheckPasswordAsync();
                //userManager.CreateAsync() //Crea un usuario
                //Tambien podemos buscar un usuario por email, id, nombre, etc. 
                //userManager.FindByEmailAsync
                //Podemos traer todos los claims de un usario. 
                //userManager.GetClaimsAsync(user);
                //Trae todos los usuarios que estan en un role especifico. 
                //userManager.GetUsersInRoleAsync(role);
                //Tenemos funciones como remover claim, remover usuario de un role, etc



                await roleManager.CreateAsync(new IdentityRole("Admin"));
                var user = await userManager.GetUserAsync(HttpContext.User);

                await userManager.AddToRoleAsync(user, "Admin");
           }

           return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Superman")]
        public IActionResult About()
        {
            ViewData["Message"] = "Sobre nosotros.";
            return View();
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Tu pagina de contacto.";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
