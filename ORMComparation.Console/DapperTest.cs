using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using ORMComparation.Entities;
using Dapper;

namespace ORMComparation.ConsoleApp
{
    public class DapperTest : IDisposable
    {
        private OracleConnection _oracleConection;

        public DapperTest(string connectionString)
        {
            _oracleConection = new OracleConnection(connectionString);
        }

        private List<OperationStep> GetSteps(long id)
        {
            return _oracleConection.Query(@"SELECT *FROM OASIS_OPERATION_STEPS WHERE OPERATION_ID = :Id", new { Id = id })
                .Select(obj => new OperationStep
                {
                    CompletedOn = obj.COMPLETED_ON,
                    ExecutedOn = obj.EXECUTED_ON,
                    ExitMessage = obj.EXIT_MESSAGE,
                    Id = obj.ID,
                    OperationId = obj.OPERATION_ID,
                    Order = obj.STEP_ORDER,
                    Parameters = obj.PARAMETERS,
                    PrerequisiteSteps = obj.PREREQUISITE_STEPS,
                    Status = (byte)obj.STATUS,
                    WorkerMethod = obj.WORKER_METHOD,
                    WorkerType = obj.WORKER_TYPE,
                    WorkerUtility = obj.WORKER_UTILITY
                }).ToList();
        }

        internal List<Operation> GetAll(byte status)
        {
            _oracleConection.Open();
            var list = _oracleConection.Query("SELECT *FROM OASIS.OASIS_OPERATIONS WHERE Status = :Status", new { Status = status }).Select(obj => new Operation
            {
                CompletedOn = obj.COMPLETED_ON,
                EnvironmentId = obj.ENVIRONMENT_ID,
                ExecutedOn = obj.EXECUTED_ON,
                Id = obj.ID,
                OperationType = (byte)obj.OPERATION_TYPE,
                PostedBy = obj.POSTED_BY,
                PostedOn = obj.POSTED_ON,
                ServerName = obj.SERVER_NAME,
                Status = (byte)obj.STATUS,
                Steps = GetSteps(obj.ID)
            }).ToList();

            _oracleConection.Close();
            return list;
        }

        public void Dispose()
        {
            _oracleConection.Close();
        }
    }
}
