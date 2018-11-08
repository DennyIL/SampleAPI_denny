using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using SampleAPI.DAL;
using SampleAPI.Models;

namespace SampleAPI.Controllers
{
    public class GajiController : ApiController
    {
        private GajiDAL gajiDAL;

        /// <summary>
        /// 
        /// </summary>
        public GajiController()
        {
            gajiDAL = new GajiDAL();
        }

        /// <summary>
        /// Get all data gaji
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Gaji> Get()
        {
            return gajiDAL.GetAll();
        }
        /// <summary>
        /// Get data gaji by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Gaji Get(String Id)
        {
            return gajiDAL.GetByID(Id);
        }

        /// <summary>
        /// Controller jumlah gaji > param
        /// </summary>
        /// <param name="jumlah"></param>
        /// <returns></returns>
        public IEnumerable<Gaji> Get(decimal jumlah)
        {
            return gajiDAL.GetByJumlah(jumlah);
        }

        /// <summary>
        /// INsert gaji baru
        /// </summary>
        /// <param name="objGaji"></param>
        /// <returns></returns>
        public IHttpActionResult Post (Gaji objGaji)
        {
            try
            {
                gajiDAL.Insert(objGaji);
                return Ok("Data Gaji : " + objGaji.NIK + " berhasil ditambahkan");
            }
            catch (Exception xx)
            {
                return BadRequest("Data Gaji : " + objGaji.NIK + " gagal ditambahkan : " + xx.Message);
            }
        }

        /// <summary>
        /// Edit data Gaji
        /// </summary>
        /// <param name="objGaji"></param>
        /// <returns></returns>
        public IHttpActionResult Put(Gaji objGaji)
        {
            try
            {
                gajiDAL.Update(objGaji);
                return Ok("Data Gaji : " + objGaji.NIK + " berhasil di edit ---");
            }
            catch (Exception xx)
            {
                return BadRequest("Data Gaji : " + objGaji.NIK + " gagal di edit eaeaaa : " + xx.Message);
            }
        }

        /// <summary>
        /// DELETE data GAJI by NIK
        /// </summary>
        /// <param name="NIK"></param>
        /// <returns></returns>
        public IHttpActionResult Put(String NIK)
        {
            try
            {
                gajiDAL.Delete(NIK);
                return Ok("Data Gaji : " + NIK + " berhasil di edit");
            }
            catch (Exception xx)
            {
                return BadRequest("Data Gaji : " + NIK + " gagal di edit : " + xx.Message);
            }
        }

        /// <summary>
        /// Get all gaji include pegawai
        /// </summary>
        /// <returns></returns>
        [Route("api/Gaji/GetGajiWithNama")]
        [HttpGet]//di inisiaisi di awal apakah get/post/put dll
        public IEnumerable<Gaji> GetGajiWithNama()
        {
            return gajiDAL.GetGajiWithNama();
        }

    }
    

}