using GEPAMECore.LD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GEPAMECore.AD
{
    class AD_Posicion
    {
        private IDbConnection connection;

        public AD_Posicion(IDbConnection connection)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public Posicion GetUltimaPosicion(string matricula)
        {
            Posicion p = new Posicion();
            string idIncidencia = "", tipoIncidencia = "";

            string sql = "SELECT * FROM Posicion AS p JOIN Vehiculo AS v ON v.id = p.idVehiculo WHERE v.matricula = @matricula";

            try
            {
                IDbCommand command = this.connection.CreateCommand();

                command.CommandText = sql;
                command.Parameters.Add(new SqlParameter("@matricula", matricula));

                this.connection.Open();

                IDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    p.Fecha = reader.GetDateTime(1);
                    p.Utm = reader.GetString(2);
                    idIncidencia = reader.GetString(3);
                    tipoIncidencia = reader.GetString(4);
                }
                this.connection.Close();

                Vehiculo v = new AD_Vehiculo(this.connection).getVehiculo(matricula);
                Incidencia i = new AD_Incidencia(this.connection).getIncidencia(idIncidencia, tipoIncidencia);
                p.Vehiculo = v;
                p.Incidencia = i;
            }
            catch (Exception ex)
            {
                if (!this.connection.State.Equals(ConnectionState.Closed))
                    this.connection.Close();
            }

            return p;
        }

        public bool SetIncidencia(Posicion posicion)
        {
            string sql = "INSERT INTO Posicion VALUES(@idVehiculo,@fecha,@utm,@idIncidencia,@tipoIncidencia)";
            try
            {
                IDbCommand command = this.connection.CreateCommand();

                command.CommandText = sql;
                command.Parameters.Add(new SqlParameter("@idVehiculo", posicion.Vehiculo.Id));
                command.Parameters.Add(new SqlParameter("@fecha", posicion.Fecha));
                command.Parameters.Add(new SqlParameter("@utm", posicion.Utm));
                command.Parameters.Add(new SqlParameter("@idIncidencia", posicion.Incidencia.Id));
                command.Parameters.Add(new SqlParameter("@tipoIncidencia", posicion.Incidencia.Tipo));

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
