using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using R_Exam.Repositories.Base;
using System.Data;

namespace R_Exam.Repositories
{
    public class LogDapperRepository(string connectionString) : ILogRepository
    {
        private readonly string connectionString = connectionString;

        public void CreateRequestLog(RequestLog log)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = @"INSERT INTO RequestLogs (RequestId, Url, RequestBody, RequestHeaders, MethodType, CreationDateTime, ClientIp) 
                            VALUES(@RequestId, @Url, @RequestBody, @RequestHeaders, @MethodType, @CreationDateTime, @ClientIp);";
            db.Query(sqlQuery, log);
        }
        public void CreateResponseLog(ResponseLog log)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = @"INSERT INTO ResponseLogs (StatusCode, ResponseBody, ResponseHeaders, EndDateTime) 
                            VALUES(@StatusCode, @ResponseBody, @ResponseHeaders, @EndDateTime);";
            db.Query(sqlQuery, log);
        }
    }
}
