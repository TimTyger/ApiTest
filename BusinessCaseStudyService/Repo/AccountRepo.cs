using BusinessCaseStudyService.Models;
using BusinessCaseStudyService.Models.Script;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BusinessCaseStudyService.Repo
{
    public class AccountRepo :IAccountRepo
    {
        private readonly IConfiguration _configuration;
        private static string conString;
        public AccountRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            conString = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }
        public async Task<bool> LogPostedTxnAsync(TxnModel model, string processId)
        {
            int result = 0;
            try
            {
                var query =  await Queries.LogPostedTxn(model);
                if (query.status)
                    result = await RunScriptAsync(query.Script, query.Parameters, processId);

            }
            catch (Exception)
            {

                throw ;
            }
            return result is 1; 
        }
        public async Task<StatusCheckerRes> GetTxnStatusAsync(string refNo, string processId)
        {
            var result =new StatusCheckerRes();
            try
            {
                var query = await Queries.GetTxnStatus(refNo);
                if (query.status)
                    result = await GetSingleAsync<StatusCheckerRes>(query.Script, query.Parameters, processId);

            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        private async Task<int> RunScriptAsync(string str, object param, string processId = null)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    await conn.OpenAsync();
                    result = await SqlMapper.ExecuteAsync(conn, str, param, commandType: CommandType.Text);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        private async Task<T> GetSingleAsync<T>(string script, object param, string requestId = null) where T : new()
        {
            T response = new T();
            try
            {
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    await conn.OpenAsync();
                    response = await SqlMapper.QueryFirstAsync<T>(conn, script, param, commandType: CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return (response);
        }
    }

    public interface IAccountRepo
    {
        Task<bool> LogPostedTxnAsync(TxnModel model, string processId);
        Task<StatusCheckerRes> GetTxnStatusAsync(string refNo, string processId);
    }
}
