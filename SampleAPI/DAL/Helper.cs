using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace SampleAPI.DAL
{
    public class Helper
    {
        /// <summary>
        /// Membuat helper untuk koneksi DB nama DEFAULT. 
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }
    }
}