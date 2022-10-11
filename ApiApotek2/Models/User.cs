using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiApotek2.Models
{
    public class User
    {
        public int Id_User { get; set; }
        public string Tipe_User { get; set; }
        public string Nama_User { get; set; }
        public string Alamat { get; set; }
        public string Telpon { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Is_Deleted { get; set; }
    }
}