using GEPAMECore.LD;
using System;
using System.Data;
using System.Data.SqlClient;

namespace GEPAMECore.AD
{
    class AD_Vehiculo
    {
        private IDbConnection connection;

        public AD_Vehiculo(IDbConnection connection)
        {
            this.connection = connection;
        }

        public Vehiculo getVehiculo(string matricula)
        {
            Vehiculo v = new Vehiculo();
            string sql = "SELECT * FROM Vehiculo AS v JOIN Tipo_Vehiculo AS tv ON v.tipoVehiculo = tv.codigo WHERE v.matricula = @matricula";

            try
            {

                IDbCommand command = this.connection.CreateCommand();

                command.CommandText = sql;
                command.Parameters.Add(new SqlParameter("@matricula", matricula));

                this.connection.Open();

                IDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    v.Id = reader.GetString(0);
                    v.Vin = reader.GetString(1);
                    v.Matricula = reader.GetString(2);
                    v.Anno = reader.GetInt32(3).ToString();
                    v.Desplegado = reader.GetBoolean(4);
                    v.EnServicio = reader.GetBoolean(5);
                    v.TipoVehiculo = new TipoVehiculo(reader.GetString(6), reader.GetString(8));
                }

                this.connection.Close();
            }
            catch (Exception ex)
            {
                if (!this.connection.State.Equals(ConnectionState.Closed))
                    this.connection.Close();
            }
            return v;
        }

        public bool setVehiculo(Vehiculo vehiculo)
        {
            string sql = "INSERT INTO Vehiculo VALUES(@id,@vin,@matricula,@anno,@desplegado,@servicio,@tipo)";
            try
            {
                IDbCommand command = this.connection.CreateCommand();

                command.CommandText = sql;
                command.Parameters.Add(new SqlParameter("@id", vehiculo.Id));
                command.Parameters.Add(new SqlParameter("@vin", vehiculo.Vin));
                command.Parameters.Add(new SqlParameter("@matricula", vehiculo.Matricula));
                command.Parameters.Add(new SqlParameter("@anno", vehiculo.Anno));
                command.Parameters.Add(new SqlParameter("@desplegado", vehiculo.Desplegado));
                command.Parameters.Add(new SqlParameter("@servicio", vehiculo.EnServicio));
                command.Parameters.Add(new SqlParameter("@tipo", vehiculo.TipoVehiculo.Codigo));

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

        public bool updateVehiculo(string matricula, Vehiculo vehiculo)
        {
            string sql = "UPDATE Vehiculo SET [id] = @id,[vin] = @vin,[matricula] = @matricula,[anyo] = @anno,[desplegado] = @desplegado,[enServicio] = @servicio,[tipoVehiculo] = @tipo WHERE matricula = @matricula";
            try
            {
                IDbCommand command = this.connection.CreateCommand();

                command.CommandText = sql;
                command.Parameters.Add(new SqlParameter("@id", vehiculo.Id));
                command.Parameters.Add(new SqlParameter("@vin", vehiculo.Vin));
                command.Parameters.Add(new SqlParameter("@matricula", vehiculo.Matricula));
                command.Parameters.Add(new SqlParameter("@anno", vehiculo.Anno));
                command.Parameters.Add(new SqlParameter("@desplegado", vehiculo.Desplegado));
                command.Parameters.Add(new SqlParameter("@servicio", vehiculo.EnServicio));
                command.Parameters.Add(new SqlParameter("@tipo", vehiculo.TipoVehiculo.Codigo));

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

        public bool deleteVehiculo(string matricula)
        {
            string sql = "DELETE FROM vehiculo WHERE matricula = @matricula";
            try
            {
                IDbCommand command = this.connection.CreateCommand();

                command.CommandText = sql;
                command.Parameters.Add(new SqlParameter("@matricula", matricula));

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

        public TipoVehiculo getTipoVehiculo(string codigo)
        {
            TipoIncidencia t = new TipoIncidencia();
            string sql = "SELECT * FROM TIPO_VEHICULO WHERE codigo = @codigo";

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

        public bool setTipoVehiculo(TipoVehiculo tipoVehiculo)
        {
            string sql = "INSERT INTO TIPO_VEHICULO VALUES(@codigo,@descripcion)";
            try
            {
                IDbCommand command = this.connection.CreateCommand();

                command.CommandText = sql;
                command.Parameters.Add(new SqlParameter("@codigo", tipoVehiculo.Codigo));
                command.Parameters.Add(new SqlParameter("@descripcion", tipoVehiculo.Descripcion));

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

        public bool updateTipoVehiculo(string codigo, TipoVehiculo tipoVehiculo)
        {
            string sql = "UPDATE TIPO_VEHICULO SET codigo=@codigo, descripcion=@descripcion WHERE codigo=@codigo";
            try
            {
                IDbCommand command = this.connection.CreateCommand();

                command.CommandText = sql;
                command.Parameters.Add(new SqlParameter("@codigo", tipoVehiculo.Codigo));
                command.Parameters.Add(new SqlParameter("@descripcion", tipoVehiculo.Descripcion));

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

        public bool deleteTipoVehiculo(string codigo)
        {
            string sql = "DELETE FROM TIPO_VEHICULO WHERE codigo=@codigo";
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
