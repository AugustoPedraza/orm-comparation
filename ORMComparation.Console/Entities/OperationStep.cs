using System;

namespace ORMComparation.Entities
{
    public class OperationStep
    {
        public long Id { get; set; }
        public long OperationId { get; set; }
        public virtual Operation Operation { get; set; }
        public string ExitMessage { get; set; }
        public byte Status { get; set; }
        public string WorkerType { get; set; }
        public string WorkerUtility { get; set; }
        public string WorkerMethod { get; set; }
        public string Parameters { get; set; }
        public string PrerequisiteSteps { get; set; }
        public DateTime? ExecutedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public int? Order { get; set; }

        public OperationStep(long id, long operationId, string exitMessage, byte status, int? order, string workerType, string workerUtility, string workerMethod,
            string parameters, string prerequisiteSteps, DateTime? executedOn, DateTime? completedOn)
        {
            Id = id;
            OperationId = operationId;
            ExitMessage = exitMessage;
            Status = status;
            WorkerType = workerType;
            WorkerUtility = workerUtility;
            WorkerMethod = workerMethod;
            Parameters = parameters;
            PrerequisiteSteps = prerequisiteSteps;
            Order = order ?? 0;
            ExecutedOn = executedOn ?? DateTime.MinValue;
            CompletedOn = completedOn ?? DateTime.MinValue;
        }

        public OperationStep()
        {
        }
    }
}
