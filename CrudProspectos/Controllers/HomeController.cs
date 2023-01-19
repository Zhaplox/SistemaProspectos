using CrudProspectos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace CrudProspectos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Subir_Archivo(string descripcion, IFormFile documento)
        {
            var client = new HttpClient();
            using (var multipartFormContent = new MultipartFormDataContent())
            {

               
                multipartFormContent.Add(new StringContent(descripcion), name: "Descripcion");

                
                var fileStreamContent = new StreamContent(documento.OpenReadStream());
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(documento.ContentType);
                multipartFormContent.Add(fileStreamContent, name: "Archivo", fileName: documento.FileName);

                
                var response = await client.PostAsync("http://localhost:5289/Mantenedor/Documentos?IdProspecto=3", multipartFormContent);
                var test = await response.Content.ReadAsStringAsync();
            }

            return View("Index");
        }

        public IActionResult Privacy => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}


