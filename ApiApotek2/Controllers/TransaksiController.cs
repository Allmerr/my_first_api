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
    public class TransaksiController : ApiController
    {
        // GET: Transaksi
        private string connString = ConfigurationManager.AppSettings["connString"].ToString();

        public async Task<IHttpActionResult> GetList(string search)
        {
            var status_code = 100;
            var message = "";
            IEnumerable<Transaksi> result = new List<Transaksi>() { };
            var conn = new SqlConnection(connString);

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var p = new DynamicParameters();
                p.Add("@search", search, DbType.String, ParameterDirection.Input);
                result = await SqlMapper.QueryAsync<Transaksi>(conn, "usp_list_transaksi", p, null, null, CommandType.StoredProcedure);

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

        public async Task<IHttpActionResult> PostInsert(string no_transaksi, DateTime tgl_transaksi, int total_bayar, int id_user, int id_obat, int id_resep)
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
                p.Add("@no_transaksi", no_transaksi, DbType.String, ParameterDirection.Input);
                p.Add("@tgl_transaksi", tgl_transaksi, DbType.Date, ParameterDirection.Input);
                p.Add("@total_bayar", total_bayar, DbType.Int64, ParameterDirection.Input);
                p.Add("@id_user", id_user, DbType.Int64, ParameterDirection.Input);
                p.Add("@id_obat", id_obat, DbType.Int64, ParameterDirection.Input);
                p.Add("@id_resep", id_resep, DbType.Int64, ParameterDirection.Input);
                result = await SqlMapper.ExecuteScalarAsync<String>(conn, "usp_insert_transaksi", p, null, null, CommandType.StoredProcedure);
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

        public async Task<IHttpActionResult> PostUpdate(int id_transaksi, string no_transaksi, DateTime tgl_transaksi, int total_bayar, int id_user, int id_obat, int id_resep)
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
                if (id_transaksi <= 0)
                {
                    message = "Id transaksi tidak boleh kosong!";
                }
                else if (string.IsNullOrEmpty(no_transaksi) && tgl_transaksi == null && total_bayar <= 0 && id_user <= 0 && id_obat <= 0 && id_resep <= 0)
                {
                    message = "Data tidak boleh kosong!";
                }
                else
                {
                    var p = new DynamicParameters();
                    p.Add("@id_transaksi", id_transaksi, DbType.Int64, ParameterDirection.Input);
                    p.Add("@no_transaksi", no_transaksi, DbType.String, ParameterDirection.Input);
                    p.Add("@tgl_transaksi", tgl_transaksi, DbType.Date, ParameterDirection.Input);
                    p.Add("@total_bayar", total_bayar, DbType.Int64, ParameterDirection.Input);
                    p.Add("@id_user", id_user, DbType.Int64, ParameterDirection.Input);
                    p.Add("@id_obat", id_obat, DbType.Int64, ParameterDirection.Input);
                    p.Add("@id_resep", id_resep, DbType.Int64, ParameterDirection.Input);
                    result = await SqlMapper.ExecuteScalarAsync<string>(conn, "usp_update_transaksi", p, null, null, CommandType.StoredProcedure);

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

        public async Task<IHttpActionResult> PostDelete(int id_transaksi, string no_transaksi)
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

                if (id_transaksi <= 0)
                {
                    message = "Id Transaksi tidak boleh kosong!";
                }
                else if (string.IsNullOrEmpty(no_transaksi))
                {
                    message = "No transkasi tidak boleh kosng!";
                }
                else
                {
                    var p = new DynamicParameters();
                    p.Add("@id_transaksi", id_transaksi, DbType.Int64, ParameterDirection.Input);
                    p.Add("@no_transaksi", no_transaksi, DbType.String, ParameterDirection.Input);
                    result = await SqlMapper.ExecuteScalarAsync<string>(conn, "usp_delete_transaksi", p, null, null, CommandType.StoredProcedure);

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