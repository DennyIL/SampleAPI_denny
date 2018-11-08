using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using SampleAPI.Models;
using SampleAPI.DAL;

namespace SampleAPI.Controllers
{
    public class PegawaiController : ApiController
    {

        #region dinamis data

        //Load data
        private PegawaiDAL pegawaiDAL;

        public PegawaiController()
        {
            pegawaiDAL = new PegawaiDAL();
        }


        /// <summary>
        /// Menampilkan list pegawai
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Pegawai> Get()
        {
            return pegawaiDAL.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Route("api/Pegawai/GetByID")]
        //[HttpGet]//di inisiaisi di awal apakah get/post/put dll
        public Pegawai Get(string id)
        {
            return pegawaiDAL.GetByID(id);
        }

        /// <summary>
        /// Insert data Pegawai :POST:: co http://dennylabs.azurewebsites.net/api/Pegawai
        /// </summary>
        /// <param name="dataPegawai"></param>
        /// <returns>Message sukses / gagal</returns>
        public IHttpActionResult Post(Pegawai dataPegawai)
        {
            //lsPegawai.Add(dataPegawai);
            //return Ok("Data Pegawai : " + dataPegawai.Nama.ToUpper() + " berhasil ditambahkan");
            try
            {
                pegawaiDAL.Insert(dataPegawai);
                return Ok("Data Pegawai : " + dataPegawai.Nama.ToUpper() + " berhasil ditambahkan");
            }
            catch(Exception ee)
            {
                return BadRequest("Data Pegawai : " + dataPegawai.Nama.ToUpper() + " Gagal ditambahkan :: " + ee.Message);
            }

        }

        /// <summary>
        /// EDIT data Pegawai :: PUT : http://dennylabs.azurewebsites.net/api/Pegawai
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataPegawai"></param>
        /// <returns></returns>
        public IHttpActionResult Put(Pegawai dataPegawai)
        {
            try
                {
                    pegawaiDAL.Update(dataPegawai);
                    return Ok("Data Pegawai: " + dataPegawai.Nama.ToUpper() + " berhasil diedit");
                }
            catch (Exception ee)
                {
                    return BadRequest("Data Pegawai: " + dataPegawai.Nama.ToUpper() + " gagal diedit : " + ee.Message);
            }
        }

        /// <summary>
        /// Mengahpus data Pegawai
        /// </summary>
        /// <param name="id">NIK</param>
        /// <returns></returns>
        public IHttpActionResult Delete(string id)
        {
            try
            {
                pegawaiDAL.Delete(id);
                return Ok("Data Pegawai: " + id + " berhasil dihapus");
            }
            catch (Exception ee)
            {
                return BadRequest("Data Pegawai: " + id + " gagal dihapus : " + ee.Message);
            }
        }
        #endregion dinamis data


        #region LIST Static
        /*
        //Memakai list static
        private static List<Models.Pegawai> lsPegawai = new List<Models.Pegawai>
        {
            new Models.Pegawai { NIK="1234", Nama = "Denny 123", Email = "aaa@gmail.com", Alamat = "Ini alamatnya123"},
            new Models.Pegawai { NIK="456", Nama = "Denny 456", Email = "456@gmail.com", Alamat = "Ini alamatnya456"}
        };
        

        

        /// <summary>
        /// Menampilkan list pegawai
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Models.Pegawai> Get()
        {
            return lsPegawai;
        }

        /// <summary>
        /// Mengembalikan data yang sesuai dengan NIP
        /// </summary>
        /// <param name="id">Parameter NIK aja</param>
        /// <returns></returns>
        public Models.Pegawai Get(string id)
        {

            //Pake LINQ donk
            var result = (from p in lsPegawai
                          where p.NIK == id
                          select p).SingleOrDefault();
            return result;
        }

        /// <summary>
        /// POST : insert data pegawai
        /// </summary>
        /// <param name="value">ISI data sesaui class</param>
        public IHttpActionResult Post([FromBody]Models.Pegawai dataPegawai)
        {
            lsPegawai.Add(dataPegawai);
            return Ok("Data Pegawai : " + dataPegawai.Nama.ToUpper() + " berhasil ditambahkan");

        }

        /// <summary>
        /// Edit data Pegawai
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataPegawai"></param>
        public IHttpActionResult Put(string id, Models.Pegawai dataPegawai)
        {
            //Load list dulu
            var editPegawai = Get(id);

            if (editPegawai != null)
            {
                //editPegawai.NIK = dataPegawai.NIK;
                editPegawai.Nama = dataPegawai.Nama;
                editPegawai.Alamat = dataPegawai.Alamat;
                editPegawai.Email = dataPegawai.Email;

                return Ok("Data Pegawai: " + dataPegawai.Nama.ToUpper() + " berhasil diedit");
            }
            else
            {
                return BadRequest("Data Pegawai: " + dataPegawai.Nama.ToUpper() + " tidak ditemukan");
            }
        }

        /// <summary>
        /// delete pegawai, pake standart ngirim ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(string id)
        {
            //Load list dulu
            var deletePegawai = Get(id);

            if (deletePegawai != null)
            {
                lsPegawai.Remove(deletePegawai);

                return Ok("Data Pegawai: " + deletePegawai.Nama.ToUpper() + " berhasil dihapus");
            }
            else
            {
                return BadRequest("Data Pegawai: " + deletePegawai.Nama.ToUpper() + " tidak ditemukan");
            }
        }


        //CUSTOM Atribute
        /// <summary>
        /// Memakai custom route krn bs nyampur
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("api/Pegawai/GetByName")]
        [HttpGet]//di inisiaisi di awal apakah get/post/put dll
        public IEnumerable<Models.Pegawai> GetByName(string name)
        {
            //Pake LINQ donk
            var result = from p in lsPegawai where p.Nama == name
                          select p;
            return result;
        }*/


        #endregion static data
    }
}
