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
    public class MasterResepController : ApiController
    {
        // GET: MasterResep
        private string connString = ConfigurationManager.AppSettings["connString"].ToString();

        public async Task<IHttpActionResult> GetList(string search)
        {
            var status_code = 100;
            var message = "";
            IEnumerable<MasterResep> result = new List<MasterResep>() { };
            var conn = new SqlConnection(connString);

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var p = new DynamicParameters();
                p.Add("@search", search, DbType.String, ParameterDirection.Input);
                result = await SqlMapper.QueryAsync<MasterResep>(conn, "usp_list_resep", p, null, null, CommandType.StoredProcedure);

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

        public async Task<IHttpActionResult> PostInsert(string no_resep, DateTime tgl_resep, string nama_dokter, string nama_pasien, string nama_obat_dibeli, string jumlah_obat_dibeli)
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
                p.Add("@no_resep", no_resep, DbType.String, ParameterDirection.Input);
                p.Add("@tgl_resep", tgl_resep, DbType.Date, ParameterDirection.Input);
                p.Add("@nama_dokter", nama_dokter, DbType.String, ParameterDirection.Input);
                p.Add("@nama_pasien", nama_pasien, DbType.String, ParameterDirection.Input);
                p.Add("@nama_obat_dibeli", nama_obat_dibeli, DbType.String, ParameterDirection.Input);
                p.Add("@jumlah_obat_dibeli", jumlah_obat_dibeli, DbType.String, ParameterDirection.Input);
                result = await SqlMapper.ExecuteScalarAsync<String>(conn, "usp_insert_resep", p, null, null, CommandType.StoredProcedure);
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

        public async Task<IHttpActionResult> PostUpdate(int id_resep, string no_resep, DateTime tgl_resep, string nama_dokter, string nama_pasien, string nama_obat_dibeli, string jumlah_obat_dibeli)
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
                if (id_resep <= 0)
                {
                    message = "Id Resep tidak boleh kosong!";
                }
                else if (string.IsNullOrEmpty(no_resep) && tgl_resep == null && string.IsNullOrEmpty(nama_dokter) && string.IsNullOrEmpty(nama_pasien) && string.IsNullOrEmpty(nama_obat_dibeli) && string.IsNullOrEmpty(jumlah_obat_dibeli))
                {
                    message = "Data tidak boleh kosong!";
                }
                else
                {
                    var p = new DynamicParameters();
                    p.Add("@id_resep", id_resep, DbType.Int64, ParameterDirection.Input);
                    p.Add("@no_resep", no_resep, DbType.String, ParameterDirection.Input);
                    p.Add("@tgl_resep", tgl_resep, DbType.Date, ParameterDirection.Input);
                    p.Add("@nama_dokter", nama_dokter, DbType.String, ParameterDirection.Input);
                    p.Add("@nama_pasien", nama_pasien, DbType.String, ParameterDirection.Input);
                    p.Add("@nama_obat_dibeli", nama_obat_dibeli, DbType.String, ParameterDirection.Input);
                    p.Add("@jumlah_obat_dibeli", jumlah_obat_dibeli, DbType.String, ParameterDirection.Input);

                    result = await SqlMapper.ExecuteScalarAsync<string>(conn, "usp_update_resep", p, null, null, CommandType.StoredProcedure);

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

        public async Task<IHttpActionResult> PostDelete(int id_resep, string no_resep)
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

                if (id_resep <= 0)
                {
                    message = "Id Resep tidak boleh kosong!";
                }
                else if (string.IsNullOrEmpty(no_resep))
                {
                    message = "Nama Obat tidak boleh kosng!";
                }
                else
                {
                    var p = new DynamicParameters();
                    p.Add("@id_resep", id_resep, DbType.Int64, ParameterDirection.Input);
                    p.Add("@no_resep", no_resep, DbType.String, ParameterDirection.Input);
                    result = await SqlMapper.ExecuteScalarAsync<string>(conn, "usp_delete_resep", p, null, null, CommandType.StoredProcedure);

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