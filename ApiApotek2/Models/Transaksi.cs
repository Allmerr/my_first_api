using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiApotek2.Models
{
    public class Transaksi
    {
        public int Id_Transaksi { get; set; }
        public string No_Transaksi { get; set; }
        public DateTime Tgl_Transaksi { get; set; }
        public int Total_Bayar { get; set; }
        public int Id_User { get; set; }
        public int Id_Obat { get; set; }
        public int Id_Resep { get; set; }
        public int Is_Deleted { get; set; }
    }
}