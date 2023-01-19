using Microsoft.AspNetCore.Mvc;

using CrudProspectos.Datos;
using CrudProspectos.Models;
using System.Net.Http.Headers;

namespace CrudProspectos.Controllers
{
    public class MantenedorController : Controller
    {
        ProspectosDatos _ProspectosDatos = new ProspectosDatos();
        public IActionResult Listar()
        {
            //La vista mostrara una lista de contactos
            var oLista = _ProspectosDatos.Listar();

            return View(oLista);
        }

        public IActionResult Guardar()
        {
            //Metodo solo devuelve la vista
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(ProspectosModel oProspectos)
        {
            //Metodo recibe el objeto para guardarlo en BD 
            if (!ModelState.IsValid)
                return View();

            var respuesta = _ProspectosDatos.Guardar(oProspectos);

            if (respuesta) 
                return RedirectToAction("Listar");
            else
                return View();

        }

        public IActionResult Editar(int IdProspecto)
        {
            //Metodo solo devuelve la vista
            var oprospecto = _ProspectosDatos.Obtener(IdProspecto);
            return View(oprospecto);
        }

        [HttpPost]
        public IActionResult Editar(ProspectosModel oProspectos)
        {
            if (!ModelState.IsValid)
                return View();

            var respuesta = _ProspectosDatos.Editar(oProspectos);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult Eliminar(int IdProspecto)
        {
            //Metodo solo devuelve la vista
            var oprospecto = _ProspectosDatos.Obtener(IdProspecto);
            return View(oprospecto);
        }

        [HttpPost]
        public IActionResult Eliminar(ProspectosModel oProspectos)
        {

            var respuesta = _ProspectosDatos.Eliminar(oProspectos.IdProspecto);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        [HttpPost]
        public IActionResult Subir_Archivo(string idProspecto, IFormFile documento)
        {
            if (documento == null)
            {
                var oprospecto = _ProspectosDatos.Obtener(int.Parse(idProspecto));
                return RedirectToAction("Editar", oprospecto); 
            }
                
            
            _ProspectosDatos.Subir_Archivo(int.Parse(idProspecto), documento.FileName);
            return RedirectToAction("Listar");
            
        }

         public IActionResult Aceptar(string IdProspecto)
        {
            _ProspectosDatos.aceptarRechazarSolicitud(int.Parse(IdProspecto), 3);
            return RedirectToAction("Listar");
        }

        public IActionResult Rechazar(string IdProspecto)
        {
            _ProspectosDatos.aceptarRechazarSolicitud(int.Parse(IdProspecto), 4);
            return RedirectToAction("Listar");
        }

    }
}
