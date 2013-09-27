using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORMComparation.Entities;

namespace ORMComparation.ConsoleApp
{
    public class EF5Test : DbContext, IDisposable
    {
        public EF5Test(string connectionName)
            : base(connectionName)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Operation> Operations { get; set; }

        public DbSet<OperationStep> OperationSteps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new OperationMap());
            modelBuilder.Configurations.Add(new OperationStepMap());
        }

        public void GetAll(int status)
        {
            Operations
                .Where(op => op.Status == status)
                .Include(x => x.Steps)
                .ToList();
        }

        void IDisposable.Dispose()
        {
            base.Dispose();
        }
    }

    public class OperationMap : EntityTypeConfiguration<Operation>
    {
        public OperationMap()
        {
            ToTable("OASIS_OPERATIONS", "OASIS");

            HasKey(p => p.Id);

            Property(p => p.CompletedOn)
            .HasColumnName("COMPLETED_ON");

            Property(p => p.EnvironmentId)
                .HasColumnName("ENVIRONMENT_ID");

            Property(p => p.ExecutedOn)
                .HasColumnName("EXECUTED_ON");

            Property(p => p.Id)
                .HasColumnName("ID");

            Property(p => p.OperationType)
                .HasColumnName("OPERATION_TYPE");

            Property(p => p.PostedBy)
                .HasColumnName("POSTED_BY");

            Property(p => p.PostedOn)
                .HasColumnName("POSTED_ON");

            Property(p => p.ServerName)
                .HasColumnName("SERVER_NAME");

            Property(p => p.Status)
                .HasColumnName("STATUS");

            //Relationships
            HasMany(op => op.Steps)
                .WithRequired()
                .HasForeignKey(x => x.OperationId);
        }
    }

    public class OperationStepMap : EntityTypeConfiguration<OperationStep>
    {
        public OperationStepMap()
        {
            ToTable("OASIS_OPERATION_STEPS", "OASIS");

            HasKey(p => p.Id);

            Property(p => p.CompletedOn).HasColumnName("COMPLETED_ON");
            Property(p => p.ExecutedOn).HasColumnName("EXECUTED_ON");
            Property(p => p.ExitMessage).HasColumnName("EXIT_MESSAGE");
            Property(p => p.Id).HasColumnName("ID");
            Property(p => p.OperationId).HasColumnName("OPERATION_ID");
            Property(p => p.Order).HasColumnName("STEP_ORDER");
            Property(p => p.Parameters).HasColumnName("PARAMETERS");
            Property(p => p.PrerequisiteSteps).HasColumnName("PREREQUISITE_STEPS");
            Property(p => p.Status).HasColumnName("STATUS");
            Property(p => p.WorkerMethod).HasColumnName("WORKER_METHOD");
            Property(p => p.WorkerType).HasColumnName("WORKER_TYPE");
            Property(p => p.WorkerUtility).HasColumnName("WORKER_UTILITY");
        }
    }
}
