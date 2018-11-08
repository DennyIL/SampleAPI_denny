using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SampleAPI.Controllers
{
    public class ValuesController : ApiController
    {

        //bikin private funct buat umum
        private static List<string> arrNama = new List<string>
        {
            "Denny", "indra", "Lesmana"
        };
        

        /// <summary>
        /// Mengembalikan semua nama dalam array statis
        /// </summary>
        /// <returns>Pengembalian adalah list nama All</returns>
        // GET api/values
        public IEnumerable<string> Get()
        {
            string[] nama = { "denny", "indfra", "lesmana"};
            //return new string[] { "value1", "value2" };
            return nama;
        }


        /// <summary>
        /// Mengembalikan nama dengan comparasi berdasarkan inputan.
        /// </summary>
        /// <param name="nama">Di isi parameter nama</param>
        /// <returns>Pengembvalian list nama yang sesuai Parameter</returns>
        public IEnumerable<string> Get(String nama)
        {
            //comtoh pake LINQ
            var result = from n in arrNama
                         where n.ToLower().Contains(nama.ToLower())
                         select n;
            return result;
        }

        /// <summary>
        /// Mengembalikan nama berdasar ID inputan.
        /// </summary>
        /// <param name="id">ID transaksi</param>
        /// <returns>Mengembalikan 1 value berdasarkan ID inputan</returns>
        public string Get(int id)
        {

            return arrNama[id];
        }

        /// <summary>
        /// Insert nama ke ARRAY statis
        /// </summary>
        /// <param name="value">Parameter nama yang akan ditambahkan</param>
        public IHttpActionResult Post([FromBody]string nama)
        {
            arrNama.Add(nama);
            return Ok("Data berhasil ditambahkan : " + nama);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
