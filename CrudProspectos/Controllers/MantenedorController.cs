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

            //Metodo modifica el prospecto en la BD
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
            //Metodo para eliminar el prospecto en la BD
            var respuesta = _ProspectosDatos.Eliminar(oProspectos.IdProspecto);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult MotivoRechazo(int IdProspecto)
        {
            //Metodo solo devuelve la vista
            var oprospecto = _ProspectosDatos.Obtener(IdProspecto);
            return View(oprospecto);
        }

        [HttpPost]
        public IActionResult MotivoRechazo(ProspectosModel oProspectos)
        {
            //Metodo que guarde en base de datos el motivo

            var respuesta = _ProspectosDatos.MotivoRechazo(oProspectos);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        [HttpPost]
        public IActionResult Subir_Archivo(string idProspecto, IFormFile documento)
        {

            //Metodo para subir el archivo PDF del equipo al sistema
            if (documento == null)
            {
                var oprospecto = _ProspectosDatos.Obtener(int.Parse(idProspecto));
                return RedirectToAction("Editar", oprospecto); 
            }

            string documentoBase64 = "";
            if (documento.Length > 0)
            {

                if(documento.ContentType != "application/pdf")
                {
                    var oprospecto = _ProspectosDatos.Obtener(int.Parse(idProspecto));
                    return RedirectToAction("Listar");
                }
                using (var ms = new MemoryStream())
                {
                    documento.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    documentoBase64 = Convert.ToBase64String(fileBytes);
                    
                }
            }

            _ProspectosDatos.Subir_Archivo(int.Parse(idProspecto), documentoBase64);
            return RedirectToAction("Listar");
            
        }

         public IActionResult Aceptar(string IdProspecto)
        {
            //Metodo para aceptar al prospecto
            _ProspectosDatos.aceptarRechazarSolicitud(int.Parse(IdProspecto), 3);
            return RedirectToAction("Listar");
        }

        public IActionResult Rechazar(string IdProspecto)
        {
            //Metodo para rechazar al prospecto
            _ProspectosDatos.aceptarRechazarSolicitud(int.Parse(IdProspecto), 4);
            return RedirectToAction("Listar");
        }

        public string descargarArchivo(int IdProspecto)
        {
            //Metodo para ver y descargar el archivo PDF
            var oprospecto = _ProspectosDatos.Obtener(IdProspecto);
            return oprospecto.archivoBase64;            
        }
    }
}
