using System;
using System.Collections.Generic;

namespace ORMComparation.Entities
{
    public class Operation
    {
        public long Id { get; set; }
        public byte OperationType { get; set; }
        public byte Status { get; set; }
        public long? EnvironmentId { get; set; }
        public string ServerName { get; set; }
        public string PostedBy { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime? ExecutedOn { get; set; }
        public DateTime? CompletedOn { get; set; }

        public virtual List<OperationStep> Steps { get; set; }

        public Operation(long id, byte operationType, byte status, long? environmentId, string serverName,
                         string postedBy, DateTime postedOn, DateTime? executedOn, DateTime? completedOn)
        {
            Id = id;
            OperationType = operationType;
            Status = status;
            EnvironmentId = environmentId ?? 0;
            ServerName = serverName;
            PostedBy = postedBy;
            PostedOn = postedOn;
            Steps = new List<OperationStep>();
            ExecutedOn = executedOn ?? DateTime.MinValue;
            CompletedOn = completedOn ?? DateTime.MinValue;
        }

        public Operation(byte operationType, byte status, string serverName, string postedBy, DateTime postedOn)
        {
            OperationType = operationType;
            Status = status;
            ServerName = serverName;
            PostedBy = postedBy;
            PostedOn = postedOn;
            Steps = new List<OperationStep>();
        }

        public Operation()
        {
            //Steps = new List<OperationStep>();
        }
    }
}
