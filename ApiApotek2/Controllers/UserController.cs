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
    public class UserController : ApiController
    {
        // GET: User
        private string connString = ConfigurationManager.AppSettings["connString"].ToString();

        public async Task<IHttpActionResult> GetList(string search)
        {
            var status_code = 100;
            var message = "";
            IEnumerable<User> result = new List<User>() { };
            var conn = new SqlConnection(connString);

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var p = new DynamicParameters();
                p.Add("@search", search, DbType.String, ParameterDirection.Input);
                result = await SqlMapper.QueryAsync<User>(conn, "usp_list_user", p, null, null, CommandType.StoredProcedure);

                if (result != null)
                {
                    status_code = 200;
                    message = "";
                }
                else
                {
                    status_code = 100;
                    message = "Data tidak ditemukan!";
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

        public async Task<IHttpActionResult> PostInsert(string type_user, string nama_user, string alamat, string telepon, string username, string password)
        {
            var status_code = 100;
            var message = "";
            var result = "";
            var conn = new SqlConnection(connString);

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                var p = new DynamicParameters();
                p.Add("@type_user", type_user, DbType.String, ParameterDirection.Input);
                p.Add("@nama_user", nama_user, DbType.String, ParameterDirection.Input);
                p.Add("@alamat", alamat, DbType.String, ParameterDirection.Input);
                p.Add("@telepon", telepon, DbType.String, ParameterDirection.Input);
                p.Add("@username", username, DbType.String, ParameterDirection.Input);
                p.Add("@password", password, DbType.String, ParameterDirection.Input);
                result = await SqlMapper.ExecuteScalarAsync<String>(conn, "usp_insert_user", p, null, null, CommandType.StoredProcedure);
                if (result.Contains("success"))
                {
                    status_code = 200;
                    message = result;
                }
                else
                {
                    status_code = 100;
                    message = result;
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

            return Json(new { status_code = status_code, message = message });
        }

        public async Task<IHttpActionResult> PostUpdate(int id_user, string type_user, string nama_user, string alamat, string telepon, string username, string password)
        {
            var status_code = 100;
            var message = "";
            var result = "";
            var conn = new SqlConnection(connString);

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (id_user <= 0)
                {
                    message = "Id User tidak boleh kosong!";
                }
                else if (string.IsNullOrEmpty(type_user) && string.IsNullOrEmpty(nama_user) && string.IsNullOrEmpty(alamat) && string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
                {
                    message = "Data tidak boleh kosong!";
                }
                else
                {
                    var p = new DynamicParameters();
                    p.Add("@id_user", id_user, DbType.Int64, ParameterDirection.Input);
                    p.Add("@tipe_user", type_user, DbType.String, ParameterDirection.Input);
                    p.Add("@nama_user", nama_user, DbType.String, ParameterDirection.Input);
                    p.Add("@alamat", alamat, DbType.String, ParameterDirection.Input);
                    p.Add("@telepon", telepon, DbType.String, ParameterDirection.Input);
                    p.Add("@username", username, DbType.String, ParameterDirection.Input);
                    p.Add("@password", password, DbType.String, ParameterDirection.Input);

                    result = await SqlMapper.ExecuteScalarAsync<string>(conn, "usp_update_user", p, null, null, CommandType.StoredProcedure);

                    if (result.Contains("success"))
                    {
                        status_code = 200;
                        message = result;
                    }
                    else
                    {
                        message = result;
                    }
                }
            }
            catch (Exception ex)
            {
                status_code = 500;
                message = result;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return Json(new { status_code = status_code, message = message });
        }

        public async Task<IHttpActionResult> PostDelete(int id_user, string username)
        {
            var status_code = 100;
            var message = "";
            var result = "";
            var conn = new SqlConnection(connString);

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (id_user <= 0)
                {
                    message = "Id user tidak boleh kosong!";
                }
                else if (string.IsNullOrEmpty(username))
                {
                    message = "Username tidak boleh kosng!";
                }
                else
                {
                    var p = new DynamicParameters();
                    p.Add("@id_user", id_user, DbType.Int64, ParameterDirection.Input);
                    p.Add("@username", username, DbType.String, ParameterDirection.Input);
                    result = await SqlMapper.ExecuteScalarAsync<string>(conn, "usp_delete_user", p, null, null, CommandType.StoredProcedure);

                    if (result.Contains("success"))
                    {
                        status_code = 200;
                        message = result;
                    }
                    else
                    {
                        message = result;
                    }
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
            }

            return Json(new { status_code = status_code, message = message });
        }
    }
}