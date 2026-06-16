using Domain.Model.Entities;
using Domain.Model.Interface;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.DataAccess.Repositories
{
    public class SensoresRepository : ISensoresRepository
    {
        private readonly IConnectionFactory _iconnectionFactory;
        private readonly string _connectionString;

        public SensoresRepository(IConnectionFactory iConnectionFactory, IConfiguration configuration)
        {
            _iconnectionFactory = iConnectionFactory;
            _connectionString = configuration.GetConnectionString("DataBase_MySqlRasp");
        }

        public async Task<OperationResult<List<LHMSensorDBModel>>> BuscarSensorFullAsync()
        {
            using (MySqlConnection c = await _iconnectionFactory.ObtenerConexionMySqlAsync(_connectionString))
            {
                try
                {

                    var sqlString = "SELECT * FROM Sensores";

                    using (MySqlCommand Comando = new MySqlCommand(sqlString, c))
                    {
                        using (MySqlDataReader reader = await Comando.ExecuteReaderAsync())
                        {
                            List<LHMSensorDBModel> query = new List<LHMSensorDBModel>();
                            while (await reader.ReadAsync())
                            {
                                query.Add(new LHMSensorDBModel()
                                {
                                    SensorId = reader.GetString("SensorId"),
                                    Text = reader.GetString("Text"),
                                    Hidden = reader.GetBoolean("Hidden")
                                });

                            }
                            return OperationResult<List<LHMSensorDBModel>>.Ok(query);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    return OperationResult<List<LHMSensorDBModel>>.Fail("Error al consultar: " + ex.Message);
                }
                catch (Exception ex)
                {
                    return OperationResult<List<LHMSensorDBModel>>.Fail("Error al consultar: " + ex.Message);
                }
            }

        }

        public async Task<OperationResult<List<LHMSensorDBModel>>> BuscarSensorHiddenAsync(bool hidden)
        {
            using (MySqlConnection c = await _iconnectionFactory.ObtenerConexionMySqlAsync(_connectionString))
            {
                try
                {

                    var sqlString = "SELECT * FROM Sensores WHERE Hidden = @Hidden";

                    using (MySqlCommand Comando = new MySqlCommand(sqlString, c))
                    {
                        Comando.Parameters.AddWithValue("@Hidden", hidden);

                        using (MySqlDataReader reader = await Comando.ExecuteReaderAsync())
                        {
                            List<LHMSensorDBModel> query = new List<LHMSensorDBModel>();
                            while (await reader.ReadAsync())
                            {
                                query.Add(new LHMSensorDBModel()
                                {
                                    SensorId = reader.GetString("SensorId"),
                                    Text = reader.GetString("Text"),
                                    Hidden = reader.GetBoolean("Hidden")
                                });

                            }
                            return OperationResult<List<LHMSensorDBModel>>.Ok(query);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    return OperationResult<List<LHMSensorDBModel>>.Fail("Error al consultar: " + ex.Message);
                }
                catch (Exception ex)
                {
                    return OperationResult<List<LHMSensorDBModel>>.Fail("Error al consultar: " + ex.Message);
                }
            }

        }

        public async Task<OperationResult<int>> InsertarSensorAsync(LHMSensorDBModel lHMSensorDBModel)
        {

            using (MySqlConnection c = await _iconnectionFactory.ObtenerConexionMySqlAsync(_connectionString))
            {
                try
                {
                    string sqlString = @"INSERT INTO Sensores (                                          
                                          SensorId,
                                          Text,
                                          Hidden)
                                         VALUES (
                                          @SensorId,
                                          @Text,
                                          @Hidden)";


                    using (MySqlCommand comando = new MySqlCommand(sqlString, c))
                    {
                        comando.Parameters.AddWithValue("@SensorId", lHMSensorDBModel.SensorId);
                        comando.Parameters.AddWithValue("@Text", lHMSensorDBModel.Text);
                        comando.Parameters.AddWithValue("@Hidden", lHMSensorDBModel.Hidden);

                        var filasRta = await comando.ExecuteNonQueryAsync();

                        if (filasRta > 0)
                            return OperationResult<int>.Ok(filasRta, "Insertado correctamente");
                        else
                            return OperationResult<int>.Fail("No se insertó el registro");
                    }
                }
                catch (MySqlException ex)
                {
                    return OperationResult<int>.Fail("Error al insertar: " + ex.Message);
                }
            }
        }

        public async Task<OperationResult<int>> InsertarSensoresAsync(List<LHMSensorDBModel> listlHMSensor)
        {
            if (listlHMSensor == null || listlHMSensor.Count == 0)
                return OperationResult<int>.Fail("La lista de sensores está vacía");

            using (MySqlConnection c = await _iconnectionFactory.ObtenerConexionMySqlAsync(_connectionString))
            {
                try
                {
                    var sb = new StringBuilder();
                    sb.Append(@"INSERT INTO Sensores (SensorId, Text, Hidden) VALUES ");

                    for (int i = 0; i < listlHMSensor.Count; i++)
                    {
                        if (i > 0) sb.Append(",");
                        sb.Append($"(@SensorId{i}, @Text{i}, @Hidden{i})");
                    }

                    using (MySqlCommand comando = new MySqlCommand(sb.ToString(), c))
                    {
                        for (int i = 0; i < listlHMSensor.Count; i++)
                        {
                            comando.Parameters.AddWithValue($"@SensorId{i}", listlHMSensor[i].SensorId);
                            comando.Parameters.AddWithValue($"@Text{i}", listlHMSensor[i].Text);
                            comando.Parameters.AddWithValue($"@Hidden{i}", listlHMSensor[i].Hidden);
                        }

                        var filasRta = await comando.ExecuteNonQueryAsync();
                        if (filasRta > 0)
                            return OperationResult<int>.Ok(filasRta, "Insertados correctamente");
                        else
                            return OperationResult<int>.Fail("No se insertaron registros");
                    }
                }
                catch (MySqlException ex)
                {
                    return OperationResult<int>.Fail("Error al insertar: " + ex.Message);
                }
            }
        }

        public async Task<OperationResult<int>> EliminarFullSensorAsync()
        {

            using (MySqlConnection c = await _iconnectionFactory.ObtenerConexionMySqlAsync(_connectionString))
            {
                try
                {
                    string sqlString = @"DELETE FROM Sensores";

                    using (MySqlCommand comando = new MySqlCommand(sqlString, c))
                    {
                        int filasRta = await comando.ExecuteNonQueryAsync();

                        if (filasRta >= 0)
                            return OperationResult<int>.Ok(filasRta, "Eliminados correctamente");
                        else
                            return OperationResult<int>.Fail("No se encontraron los registros para eliminar");

                    }
                }
                catch (MySqlException ex)
                {
                    return OperationResult<int>.Fail("Error al eliminar: " + ex.Message);
                }
            }

        }

    }
}
