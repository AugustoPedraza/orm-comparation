using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using ORMComparation.Entities;

namespace ORMComparation.ConsoleApp
{
    public class AdoNetTest : IDisposable
    {
        private OracleConnection _connection;

        public AdoNetTest(string connectionString)
        {
            _connection = new OracleConnection(connectionString);
        }

        internal List<Operation> GetAll(byte status)
        {
            var sqlCmd = @"SELECT   COMPLETED_ON,
                                    ENVIRONMENT_ID,
                                    EXECUTED_ON,
                                    ID,
                                    OPERATION_TYPE,
                                    POSTED_BY,
                                    POSTED_ON,
                                    SERVER_NAME,
                                    STATUS 
                                    FROM OASIS.OASIS_OPERATIONS where STATUS = :status";

            var command = new OracleCommand(sqlCmd, _connection);
            command.Parameters.Add("status", status);
            _connection.Open();
                
            var dataReader = command.ExecuteReader();
            
            var list = new List<Operation>();
            var counter = 0;
            while (dataReader.Read())
            {
                counter++;
                list.Add(new Operation
                {
                    CompletedOn =    dataReader.IsDBNull(1) ? default(DateTime) : dataReader.GetDateTime(0),
                    EnvironmentId = dataReader.IsDBNull(1) ? 0 : dataReader.GetInt64(1),
                    ExecutedOn =    dataReader.IsDBNull(2) ? default(DateTime) : dataReader.GetDateTime(2),
                    Id =            dataReader.IsDBNull(3) ? 0 : dataReader.GetInt64(3),
                    OperationType = dataReader.IsDBNull(4) ? (byte)0 : (byte)dataReader.GetInt32(4),
                    PostedBy =      dataReader.IsDBNull(5) ? string.Empty : dataReader.GetString(5),
                    PostedOn =      dataReader.IsDBNull(6) ? default(DateTime) : dataReader.GetDateTime(6),
                    ServerName =    dataReader.IsDBNull(7) ? string.Empty : dataReader.GetString(7),
                    Status =        dataReader.IsDBNull(8) ? (byte)0 : (byte)dataReader.GetInt32(8),
                    Steps =         GetSteps(dataReader.GetInt64(3))
                });
            }
            _connection.Close();
            return list;
        }


        private List<OperationStep> GetSteps(long operationId)
        {
            var sqlCmd = @"SELECT 
                        COMPLETED_ON,
                        EXECUTED_ON,
                        EXIT_MESSAGE,
                        ID,
                        OPERATION_ID,
                        STEP_ORDER,
                        PARAMETERS,
                        PREREQUISITE_STEPS,
                        STATUS,
                        WORKER_METHOD,
                        WORKER_TYPE,
                        WORKER_UTILITY
                            FROM OASIS_OPERATION_STEPS WHERE OPERATION_ID = :Id";

            var command = new OracleCommand(sqlCmd, _connection);
            command.Parameters.Add("Id", operationId);

            var dataReader = command.ExecuteReader();

            var list = new List<OperationStep>();
            var counter = 0;
            while (dataReader.Read())
            {
                counter++;
                list.Add(new OperationStep
                {
                    CompletedOn = dataReader.IsDBNull(0) ? default(DateTime) : dataReader.GetDateTime(0),
                    ExecutedOn = dataReader.IsDBNull(1) ? default(DateTime) : dataReader.GetDateTime(1),
                    ExitMessage = dataReader.IsDBNull(2) ? string.Empty : dataReader.GetString(2),
                    Id = dataReader.IsDBNull(3) ? 0 : dataReader.GetInt64(3),
                    OperationId = dataReader.IsDBNull(4) ? 0 : dataReader.GetInt64(4),
                    Order = dataReader.IsDBNull(5) ? (byte)0 : (byte)dataReader.GetInt32(5),
                    Parameters = dataReader.IsDBNull(6) ? string.Empty : dataReader.GetString(6),
                    PrerequisiteSteps = dataReader.IsDBNull(7) ? string.Empty : dataReader.GetString(7),
                    Status = dataReader.IsDBNull(8) ? (byte)0 : (byte)dataReader.GetInt32(8),
                    WorkerMethod = dataReader.IsDBNull(9) ? string.Empty : dataReader.GetString(9),
                    WorkerType = dataReader.IsDBNull(10) ? string.Empty : dataReader.GetString(10),
                    WorkerUtility = dataReader.IsDBNull(11) ? string.Empty : dataReader.GetString(11)
                });
            }
            return list;
        }
        
        public void Dispose()
        {
            _connection.Clone();
        }
    }
}
