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
    public class MasterObatController : ApiController
    {
        // GET: MasterObat
        private string connString = ConfigurationManager.AppSettings["connString"].ToString();

        public async Task<IHttpActionResult> GetList(string search)
        {
            var status_code = 100;
            var message = "";
            IEnumerable<MasterObat> result = new List<MasterObat>() { };
            var conn = new SqlConnection(connString);

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var p = new DynamicParameters();
                p.Add("@search", search, DbType.String, ParameterDirection.Input);
                result = await SqlMapper.QueryAsync<MasterObat>(conn, "usp_list_master_obat", p, null, null, CommandType.StoredProcedure);

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

        public async Task<IHttpActionResult> PostInsert(string kode_obat, string nama_obat, DateTime expired_date, int jumlah, int harga)
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
                p.Add("@kode_obat", kode_obat, DbType.String, ParameterDirection.Input);
                p.Add("@nama_Obat", nama_obat, DbType.String, ParameterDirection.Input);
                p.Add("@expired_date", expired_date, DbType.Date, ParameterDirection.Input);
                p.Add("@jumlah", jumlah, DbType.Int64, ParameterDirection.Input);
                p.Add("@harga", harga, DbType.Int64, ParameterDirection.Input);
                result = await SqlMapper.ExecuteScalarAsync<String>(conn, "usp_insert_master_obat", p, null, null, CommandType.StoredProcedure);
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

        public async Task<IHttpActionResult> PostUpdate(int id_obat, string kode_obat, string nama_obat, DateTime expired_date, int jumlah, int harga)
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
                if (id_obat <= 0)
                {
                    message = "Id Obat tidak boleh kosong!";
                }
                else if (string.IsNullOrEmpty(kode_obat) && string.IsNullOrEmpty(nama_obat) && expired_date == null && jumlah == null && harga == null)
                {
                    message = "Data tidak boleh kosong!";
                }
                else
                {
                    var p = new DynamicParameters();
                    p.Add("@id_obat", id_obat, DbType.Int64, ParameterDirection.Input);
                    p.Add("@kode_obat", kode_obat, DbType.String, ParameterDirection.Input);
                    p.Add("@nama_obat", nama_obat, DbType.String, ParameterDirection.Input);
                    p.Add("@expired_date", expired_date, DbType.DateTime, ParameterDirection.Input);
                    p.Add("@jumlah", jumlah, DbType.Int64, ParameterDirection.Input);
                    p.Add("@harga", harga, DbType.Int64, ParameterDirection.Input);

                    result = await SqlMapper.ExecuteScalarAsync<string>(conn, "usp_update_master_obat", p, null, null, CommandType.StoredProcedure);

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

        public async Task<IHttpActionResult> PostDelete(string kode_obat, string nama_obat)
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

                if (string.IsNullOrEmpty(kode_obat))
                {
                    message = "Kode Obat tidak boleh kosong!";
                }
                else if (string.IsNullOrEmpty(nama_obat))
                {
                    message = "Nama Obat tidak boleh kosng!";
                }
                else
                {
                    var p = new DynamicParameters();
                    p.Add("@kode_obat", kode_obat, DbType.String, ParameterDirection.Input);
                    p.Add("@nama_obat", nama_obat, DbType.String, ParameterDirection.Input);
                    result = await SqlMapper.ExecuteScalarAsync<string>(conn, "usp_delete_master_obat", p, null, null, CommandType.StoredProcedure);

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