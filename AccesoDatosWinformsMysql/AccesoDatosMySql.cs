using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MySql.Data.MySqlClient;

namespace AccesoDatosWinForm.data
{
    class AccesoDatosMySql 
    {
        MySqlConnection _connection ;
        MySqlCommand _command;
        MySqlDataReader _reader;
        List<MySqlParameter> _parametros;

        public int ejecutarSentencia(string sentenciaSQL,Categories c) {

            if (_connection == null)
            {
                throw new InvalidOperationException("La conexión no está inicializada.");
            }
            _command = new MySqlCommand(sentenciaSQL, _connection);
            _parametros = new List<MySqlParameter>
            {
                new MySqlParameter("@categoryname", c.CategoryName),
                new MySqlParameter("@description", c.Description)
            };

            foreach (var parametro in _parametros)
            {
                _command.Parameters.Add(parametro);
            }


            return _command.ExecuteNonQuery();
        }

        public DataTable ejecutarQuery(string consulta,string filtro) { 
            _command = new MySqlCommand(consulta, _connection);
            _parametros = new List<MySqlParameter>
            {
                new MySqlParameter("@filtro",filtro)
            };
            foreach(var parametro in _parametros)
            {
                _command.Parameters.Add(parametro);
            }
            MySqlDataReader lector = _command.ExecuteReader();
            using (DataTable tabla = new DataTable())
            {
                tabla.Load(lector);
                lector.Dispose();
                return tabla;
            }
            
        }

        public AccesoDatosMySql(string host, string db, 
            string user, string password, int port) {

            string connectionString = 
                $"Server={host};Database={db};User ID={user};" +
                $"Password={password};" +
                $"Port={port};";

            _connection = new MySqlConnection(connectionString);
            _connection.Open();

            MySqlConnectionStringBuilder builderConnString =
                new MySqlConnectionStringBuilder();

            builderConnString.Server = host;
            builderConnString.Database = db;
            builderConnString.UserID = user;
            builderConnString.Password = password;
            builderConnString.Port = (uint)port;

            var otraCAdena = 
                builderConnString.ConnectionString;

        }
    }
}
