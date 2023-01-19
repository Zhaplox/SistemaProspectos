using System.ComponentModel.DataAnnotations;



namespace CrudProspectos.Models

{
    public class Documento
    {
        public int IdDocumento { get; set; }

        public string NombreDocumento { get; set; }

        public string Ruta { get; set; }    

        public IFormFile Archivo { get; set; }


    }
}
