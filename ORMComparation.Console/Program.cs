using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Dapper;
using IBatisNet.DataMapper.Configuration;
using Oracle.DataAccess.Client;
using ORMComparation.ConsoleApp;
using ORMComparation.Entities;
using System.IO;

namespace ORMComparation
{
    class Program
    {
        static string ConnectionString
            = ConfigurationManager.ConnectionStrings["Oasis"].ToString();

        static string iBatisConfigPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + @"App_Data\iBatis\sqlMap.config";
            }
        }

        
        static void Main(string[] args)
        {                                   
            var testFixture = new TestFixture();

            var efTest = new EF5Test(connectionName: "Oasis");
            testFixture.Add(x => efTest.GetAll(x), "EF 5");

            var dapperTest = new DapperTest(ConnectionString);
            testFixture.Add(x => dapperTest.GetAll(x), "Dapper");

            var iBatistTest = new iBatistTest(iBatisConfigPath);
            testFixture.Add(x => iBatistTest.GetAll(x), "iBatis");

            var adoNetTest = new AdoNetTest(ConnectionString);
            testFixture.Add(x => adoNetTest.GetAll(x), "ADO.NET");


            testFixture.Run(10000);

            Console.ReadLine();
        }
    }
}
