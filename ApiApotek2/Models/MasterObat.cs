using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiApotek2.Models
{
    public class MasterObat
    {
        public int Id_Obat { get; set; }
        public string Kode_Obat { get; set; }
        public string Nama_Obat { get; set; }
        public DateTime Expired_Date { get; set; }
        public int Jumlah { get; set; }
        public int Harga { get; set; }
        public int Is_Deleted { get; set; }
    }
}