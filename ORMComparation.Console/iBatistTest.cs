using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using ORMComparation.Entities;

namespace ORMComparation.ConsoleApp
{
    public class iBatistTest
    {
        private ISqlMapper _mapper;
        
        public iBatistTest(string path)
        {
            _mapper = new DomSqlMapBuilder { ValidateSqlMapConfig = true }.Configure(path);
        }
        
        public List<Operation> GetAll(byte status)
        {
            var list = _mapper
                        .QueryForList<Operation>("FindAllOperations", status) as List<Operation>;

            list.ForEach(x => x.Steps = GetOperatioSteps(x.Id));
            return list;
        }

        private List<OperationStep> GetOperatioSteps(long operationId)
        {
            return _mapper.
                QueryForList<OperationStep>("FindStepsByOperationId", operationId) as List<OperationStep>;
        }
    }
}
