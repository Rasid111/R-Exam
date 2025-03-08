using Dapper;
using Microsoft.Data.SqlClient;
using R_Exam.Domain.Models;
using R_Exam.Domain.Repositories;
using System.Data;

namespace R_Exam.Infrastructre.Repositories
{
    public class LogDapperRepository(string connectionString) : ILogRepository
    {
        private readonly string connectionString = connectionString;

        public async Task CreateRequestLog(RequestLog log)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = @"INSERT INTO RequestLogs (RequestId, Url, RequestBody, RequestHeaders, MethodType, CreationDateTime, ClientIp) 
                            VALUES(@RequestId, @Url, @RequestBody, @RequestHeaders, @MethodType, @CreationDateTime, @ClientIp);";
            await db.QueryAsync(sqlQuery, log);
        }
        public async Task CreateResponseLog(ResponseLog log)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = @"INSERT INTO ResponseLogs (StatusCode, ResponseBody, ResponseHeaders, EndDateTime) 
                            VALUES(@StatusCode, @ResponseBody, @ResponseHeaders, @EndDateTime);";
            await db.QueryAsync(sqlQuery, log);
        }
    }
}
