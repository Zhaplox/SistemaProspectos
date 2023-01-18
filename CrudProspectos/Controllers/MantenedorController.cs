using Microsoft.AspNetCore.Mvc;

using CrudProspectos.Datos;
using CrudProspectos.Models;


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



    }
}
