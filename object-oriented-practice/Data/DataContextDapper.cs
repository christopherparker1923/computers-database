using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using object_oriented_practice.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_oriented_practice.Data
{
    public class DataContextDapper
    {
        private IConfiguration _config;

        public DataContextDapper(IConfiguration config) 
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        private string _connectionString = "Server=localhost;Database=DotnetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";

        public IEnumerable<T> LoadData<T>(string sql )
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Query<T>(sql);
        }

        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.QuerySingle<T>(sql);
        }    

        public bool ExecuteSql(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return (dbConnection.Execute(sql)>0);
        }

        public int ExecuteSqlWithRowCount(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sql);
        }
    }
}
