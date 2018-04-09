using GEPAMECore.LD;
using System;
using System.Data;
using System.Data.SqlClient;

namespace GEPAMECore.AD
{
    class AD_Incidencia
    {
        private IDbConnection connection;

        public AD_Incidencia(IDbConnection connection)
        {
            this.connection = connection;
        }

        public Incidencia getIncidencia(string id)
        {
            Incidencia i = new Incidencia();
            string sql = "SELECT * FROM INCIDENCIA AS i JOIN TIPO_INCIDENCIA AS ti ON i.tipoIncidencia = ti.codigo WHERE i.idIncidencia = @id";

            try
            {

                IDbCommand command = this.connection.CreateCommand();

                command.CommandText = sql;
                command.Parameters.Add(new SqlParameter("@id", id));

                this.connection.Open();

                IDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    i.Id = reader.GetString(1);
                    i.Utm = reader.GetString(2);
                    i.Fecha = reader.GetDateTime(3);
                    i.Estado = reader.GetBoolean(4);
                    i.Descripcion = reader.GetString(5);
                    i.Tipo = new TipoIncidencia(reader.GetString(6), reader.GetString(7));
                }

                this.connection.Close();
            }
            catch (Exception ex)
            {
                if (!this.connection.State.Equals(ConnectionState.Closed))
                    this.connection.Close();
            }
            return i;
        }

        public bool setIncidencia(Incidencia incidencia)
        {
            string sql = "INSERT INTO INCIDENCIA VALUES(@tipoIncidencia,@idIncidencia,@utm,@fecha,@estado,@descripcion)";
            try
            {
                IDbCommand command = this.connection.CreateCommand();

                command.CommandText = sql;
                command.Parameters.Add(new SqlParameter("@tipoIncidencia", incidencia.Tipo.Codigo));
                command.Parameters.Add(new SqlParameter("@idIncidencia", incidencia.Id));
                command.Parameters.Add(new SqlParameter("@utm", incidencia.Utm));
                command.Parameters.Add(new SqlParameter("@fecha", incidencia.Fecha));
                command.Parameters.Add(new SqlParameter("@estado", incidencia.Estado));
                command.Parameters.Add(new SqlParameter("@descripcion", incidencia.Descripcion));

                this.connection.Open();

                int i = command.ExecuteNonQuery();

                this.connection.Close();

                if (i == 1)
                    return true;
            }
            catch (Exception ex)
            {
                if (!this.connection.State.Equals(ConnectionState.Closed))
                    this.connection.Close();
            }
            return false;
        }

        public bool updateIncidencia(string tipoIncidencia, string idIncidencia, Incidencia incidencia)
        {
            string sql = "UPDATE INCIDENCIA SET tipoIncidencia=@tipo, idIncidencia=@id, utm=@utm, fecha=@fecha, estado=@estado, descripcion=@descripcion WHERE tipoIncidencia=@tipo, idIncidencia=@id";
            try
            {
                IDbCommand command = this.connection.CreateCommand();

                command.CommandText = sql;
                command.Parameters.Add(new SqlParameter("@tipoIncidencia", incidencia.Tipo.Codigo));
                command.Parameters.Add(new SqlParameter("@idIncidencia", incidencia.Id));
                command.Parameters.Add(new SqlParameter("@utm", incidencia.Utm));
                command.Parameters.Add(new SqlParameter("@fecha", incidencia.Fecha));
                command.Parameters.Add(new SqlParameter("@estado", incidencia.Estado));
                command.Parameters.Add(new SqlParameter("@descripcion", incidencia.Descripcion));

                this.connection.Open();

                int i = command.ExecuteNonQuery();

                this.connection.Close();

                if (i == 1)
                    return true;
            }
            catch (Exception ex)
            {
                if (!this.connection.State.Equals(ConnectionState.Closed))
                    this.connection.Close();
            }
            return false;
        }

        public bool deleteIncidencia(string tipoIncidencia, string idIncidencia)
        {
            string sql = "DELETE FROM INCIDENCIA WHERE tipoIncidencia=@tipo, idIncidencia=@id";
            try
            {
                IDbCommand command = this.connection.CreateCommand();

                command.CommandText = sql;
                command.Parameters.Add(new SqlParameter("@tipo", tipoIncidencia));
                command.Parameters.Add(new SqlParameter("@id", idIncidencia));

                this.connection.Open();

                int i = command.ExecuteNonQuery();

                this.connection.Close();

                if (i == 1)
                    return true;
            }
            catch (Exception ex)
            {
                if (!this.connection.State.Equals(ConnectionState.Closed))
                    this.connection.Close();
            }
            return false;
        }

        public TipoIncidencia getTipoIncidencia(string codigo)
        {
            TipoIncidencia t = new TipoIncidencia();
            string sql = "SELECT * FROM TIPO_INCIDENCIA WHERE codigo = @codigo";

            try
            {

                IDbCommand command = this.connection.CreateCommand();

                command.CommandText = sql;
                command.Parameters.Add(new SqlParameter("@codigo", codigo));

                this.connection.Open();

                IDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    t.Codigo = reader.GetString(0);
                    t.Descripcion = reader.GetString(1);
                }

                this.connection.Close();
            }
            catch (Exception ex)
            {
                if (!this.connection.State.Equals(ConnectionState.Closed))
                    this.connection.Close();
            }
            return t;
        }

        public bool setTipoIncidencia(TipoIncidencia tipoIncidencia)
        {
            string sql = "INSERT INTO TIPO_INCIDENCIA VALUES(@codigo,@descripcion)";
            try
            {
                IDbCommand command = this.connection.CreateCommand();

                command.CommandText = sql;
                command.Parameters.Add(new SqlParameter("@codigo", tipoIncidencia.Codigo));
                command.Parameters.Add(new SqlParameter("@descripcion", tipoIncidencia.Descripcion));

                this.connection.Open();

                int i = command.ExecuteNonQuery();

                this.connection.Close();

                if (i == 1)
                    return true;
            }
            catch (Exception ex)
            {
                if (!this.connection.State.Equals(ConnectionState.Closed))
                    this.connection.Close();
            }
            return false;
        }

        public bool updateTipoIncidencia(string codigo, TipoIncidencia tipoIncidencia)
        {
            string sql = "UPDATE TIPO_INCIDENCIA SET codigo=@codigo, descripcion=@descripcion WHERE codigo=@codigo";
            try
            {
                IDbCommand command = this.connection.CreateCommand();

                command.CommandText = sql;
                command.Parameters.Add(new SqlParameter("@codigo", tipoIncidencia.Codigo));
                command.Parameters.Add(new SqlParameter("@descripcion", tipoIncidencia.Descripcion));

                this.connection.Open();

                int i = command.ExecuteNonQuery();

                this.connection.Close();

                if (i == 1)
                    return true;
            }
            catch (Exception ex)
            {
                if (!this.connection.State.Equals(ConnectionState.Closed))
                    this.connection.Close();
            }
            return false;
        }

        public bool deleteTipoIncidencia(string codigo)
        {
            string sql = "DELETE FROM TIPO_INCIDENCIA WHERE codigo=@codigo";
            try
            {
                IDbCommand command = this.connection.CreateCommand();

                command.CommandText = sql;
                command.Parameters.Add(new SqlParameter("@codigo", codigo));

                this.connection.Open();

                int i = command.ExecuteNonQuery();

                this.connection.Close();

                if (i == 1)
                    return true;
            }
            catch (Exception ex)
            {
                if (!this.connection.State.Equals(ConnectionState.Closed))
                    this.connection.Close();
            }
            return false;
        }
    }
}
