using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiApotek2.Models
{
    public class LogLogin
    {
        public int Id_Log { get; set; }

        public DateTime waktu { get; set; }

        public string aktifitas { get; set; }

        public int Id_User { get; set; }

        public int Is_Deleted { get; set; }
    }
}