using CrudProspectos.Models;
using System.Data.SqlClient;
using System.Data;


namespace CrudProspectos.Datos
{
    public class ProspectosDatos
    {
        public List<ProspectosModel> Listar() {
            
                var oLista = new List<ProspectosModel>();

                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Listar", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new ProspectosModel()
                            {
                                IdProspecto = Convert.ToInt32(dr["IdProspecto"]),
                                nombredelProspecto = dr["nombredelProspecto"].ToString(),
                                primerApellido = dr["primerApellido"].ToString(),
                                segundoApellido = dr["segundoApellido"].ToString(),
                                calle = dr["calle"].ToString(),
                                numero = Convert.ToInt32(dr["numero"]),
                                colonia = dr["colonia"].ToString(),
                                codigoPostal = Convert.ToInt32(dr["codigoPostal"]),
                                telefono = dr["telefono"].ToString(),
                                rfc = dr["rfc"].ToString(),
                                estatus = int.Parse(dr["estatus"].ToString()),
                                estatusTxt = dr["estatusTxt"].ToString()
                            });
                        }
                    }
                }
            return oLista;
        }
        public ProspectosModel Obtener(int IdProspecto)        {

            var oProspecto = new ProspectosModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener", conexion);
                cmd.Parameters.AddWithValue("IdProspecto", IdProspecto);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oProspecto.IdProspecto = Convert.ToInt32(dr["IdProspecto"]);
                        oProspecto.nombredelProspecto = dr["nombredelProspecto"].ToString();
                        oProspecto.primerApellido = dr["primerApellido"].ToString();
                        oProspecto.segundoApellido = dr["segundoApellido"].ToString();
                        oProspecto.calle = dr["calle"].ToString();
                        oProspecto.numero = Convert.ToInt32(dr["numero"]);
                        oProspecto.colonia = dr["colonia"].ToString();
                        oProspecto.codigoPostal = Convert.ToInt32(dr["codigoPostal"]);
                        oProspecto.telefono = dr["telefono"].ToString();
                        oProspecto.rfc = dr["rfc"].ToString();
                        oProspecto.archivoBase64 = dr["archivoBase64"].ToString();
                        oProspecto.motivoRechazo = dr["motivoRechazo"].ToString();
                        oProspecto.estatus = int.Parse(dr["estatus"].ToString());
                    }
                }
            }
            return oProspecto;
        }

        public bool Guardar(ProspectosModel oprospecto) {
            bool rpta;

            try
            {

                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Guardar", conexion);
                    cmd.Parameters.AddWithValue("nombredelProspecto", oprospecto.nombredelProspecto);
                    cmd.Parameters.AddWithValue("primerApellido", oprospecto.primerApellido);
                    cmd.Parameters.AddWithValue("segundoApellido", string.IsNullOrEmpty(oprospecto.segundoApellido) ? "" : oprospecto.segundoApellido);
                    cmd.Parameters.AddWithValue("calle", oprospecto.calle);
                    cmd.Parameters.AddWithValue("numero", oprospecto.numero);
                    cmd.Parameters.AddWithValue("colonia", oprospecto.colonia);
                    cmd.Parameters.AddWithValue("codigoPostal", oprospecto.codigoPostal);
                    cmd.Parameters.AddWithValue("telefono", oprospecto.telefono);
                    cmd.Parameters.AddWithValue("rfc", oprospecto.rfc);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;

            }
            catch (Exception e) {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }

        public bool Editar(ProspectosModel oprospecto)
        {
            bool rpta;

            try
            {

                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Editar", conexion);
                    cmd.Parameters.AddWithValue("IdProspecto", oprospecto.IdProspecto);
                    cmd.Parameters.AddWithValue("nombredelProspecto", oprospecto.nombredelProspecto);
                    cmd.Parameters.AddWithValue("primerApellido", oprospecto.primerApellido);
                    cmd.Parameters.AddWithValue("segundoApellido", oprospecto.segundoApellido);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;

            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }

        public bool Eliminar(int IdProspecto)
        {
            bool rpta;

            try
            {

                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Eliminar", conexion);
                    cmd.Parameters.AddWithValue("IdProspecto", IdProspecto);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;

            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }

        internal void Subir_Archivo(int idProspecto, string nombreArchivo)
        {
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_guardar_documento", conexion);
                cmd.Parameters.AddWithValue("idProspecto", idProspecto);
                cmd.Parameters.AddWithValue("archivoBase64", nombreArchivo);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
            }
        }

        internal void aceptarRechazarSolicitud(int idProspecto, int estatus)
        {
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_aceptarRechazarSolicitud", conexion);
                cmd.Parameters.AddWithValue("IdProspecto", idProspecto);
                cmd.Parameters.AddWithValue("estatus", estatus);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
            }
        }


        public bool MotivoRechazo(ProspectosModel oprospecto)
        {
            bool rpta;

            try
            {

                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_guardarMotivorechazo", conexion);
                    cmd.Parameters.AddWithValue("IdProspecto", oprospecto.IdProspecto);
                    cmd.Parameters.AddWithValue("motivoRechazo", string.IsNullOrEmpty(oprospecto.motivoRechazo) ? "" : oprospecto.motivoRechazo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;

            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }
    }
}



