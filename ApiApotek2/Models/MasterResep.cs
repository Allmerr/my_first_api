using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiApotek2.Models
{
    public class MasterResep
    {
        public int Id_Resep { get; set; }
        public string No_Resep { get; set; }
        public DateTime Tgl_Resep { get; set; }
        public string Nama_Dokter { get; set; }
        public string Nama_Pasien { get; set; }
        public string Nama_ObatDibeli { get; set; }
        public string Jumlah_ObatDibeli { get; set; }
        public int Is_Deleted { get; set; }
    }
}