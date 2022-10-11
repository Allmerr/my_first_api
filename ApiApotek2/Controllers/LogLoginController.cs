using ApiApotek2.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ApiApotek2.Controllers
{
    public class LogLoginController : ApiController
    {
        // GET: LogLogin
        private string connString = ConfigurationManager.AppSettings["connString"].ToString();

        public async Task<IHttpActionResult> GetList(string search)
        {
            var status_code = 100;
            var message = "";
            IEnumerable<LogLogin> result = new List<LogLogin>() { };
            var conn = new SqlConnection(connString);

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var p = new DynamicParameters();
                p.Add("@search", search, DbType.String, ParameterDirection.Input);
                result = await SqlMapper.QueryAsync<LogLogin>(conn, "usp_list_log", p, null, null, CommandType.StoredProcedure);
                if (result != null)
                {
                    status_code = 200;
                    message = "Berhasil" ;
                }
                else
                {
                    status_code = 100;
                    message = "Data Tidak Ditemukan!";
                }
            }
            catch (Exception ex)
            {
                status_code = 500;
                message = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
            }
            return Json(new { status_code = status_code, message = message, data = result });
        }

    }
}