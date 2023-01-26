using System.ComponentModel.DataAnnotations;

namespace CrudProspectos.Models
{
    public class ProspectosModel
    {

        [Required(ErrorMessage = "El campo IdProspecto es obligatorio")]
        public int IdProspecto { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string nombredelProspecto { get; set; }

        [Required(ErrorMessage = "El campo Primer Apellido es obligatorio")]
        public string primerApellido { get; set; }

        public string? segundoApellido { get; set; }

        [Required(ErrorMessage = "El campo Calle es obligatorio")]
        public string calle { get; set; }

        [Required(ErrorMessage = "El campo Numero es obligatorio")]
        public int numero { get; set; }

        [Required(ErrorMessage = "El campo Colonia es obligatorio")]
        public string colonia { get; set; }

        [Required(ErrorMessage = "El campo Codigo Postal es obligatorio")]
        public int codigoPostal { get; set; }

        [Required(ErrorMessage = "El campo Telefono es obligatorio")]
        public string telefono { get; set; }

        [Required(ErrorMessage = "El campo RFC es obligatorio")]
        public string rfc { get; set; }

        public int estatus { get; set; }
        public string estatusTxt { get; set; }
        public string archivoBase64 { get; set; }
        public string motivoRechazo{ get; set; }

    }
}
