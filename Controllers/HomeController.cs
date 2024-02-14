using ElectronicsStore.Models;
using ElectronicsStore.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ElectronicsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeReposity;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeReposity)
        {
            _homeReposity = homeReposity;
            _logger = logger;
        }
        public async Task <IActionResult> Index(string sterm = "", int genreId = 0 )
        {
            IEnumerable<Electronic> electronics = await _homeReposity.GetElectronics(sterm, genreId);
            IEnumerable<Genre> genres = await _homeReposity.Genres();
            ElectronicDisplayModel electronicModel = new ElectronicDisplayModel
            {
                Electronics = electronics,
                Genres = genres,
                STerm = sterm,
                GenreId = genreId
            };
            return View(electronicModel);
        }
        //[Authorize(Roles = "Admin")]
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
