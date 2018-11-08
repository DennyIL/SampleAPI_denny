using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SampleAPI.Models;
using Dapper;
using System.Data.SqlClient;

namespace SampleAPI.DAL
{
    public class GajiDAL : ICrud<Gaji>
    {
        /// <summary>
        /// Query Get ALl pake Dupper
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Gaji> GetAll()
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(Helper.GetConnectionString()))
            {
                string strsql = @"SELECT * FROM Gaji order by NIK";
                var results = conn.Query<Gaji>(strsql);
                return results;

            }
        }
        /// <summary>
        /// Query get By ID, 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single Result</returns>
        public Gaji GetByID(String id)
        {
            using (SqlConnection conn = new SqlConnection(Helper.GetConnectionString()))
            {
                string strsql = @"SELECT * FROM Gaji WHERE id = @id";
                var param = new { id = id};
                var results = conn.QuerySingle<Gaji>(strsql, param);
                return results;

            }
        }

        /// <summary>
        /// get gaji, yang dg jumlah tertentu
        /// </summary>
        /// <param name="jumlah"></param>
        /// <returns>JUmlah >= parameter</returns>
        public IEnumerable<Gaji> GetByJumlah(decimal jumlah)
        {
            using (SqlConnection conn = new SqlConnection(Helper.GetConnectionString()))
            {
                string strsql = @"SELECT * FROM Gaji WHERE gaji >= @JUMLAH";
                var param = new { JUMLAH = jumlah };
                var result = conn.Query<Gaji>(strsql, param);
                return result;
            }
        }

        /// <summary>
        /// insert data gaji
        /// </summary>
        /// <param name="obj">object gaji, selain ID</param>
        public void Insert(Gaji obj)
        {
            using (SqlConnection conn = new SqlConnection(Helper.GetConnectionString()))
            {
                string strsql = @"INSERT INTO gaji (NIK, Norek, Gaji) VALUES (@NIK, @Norek, @Gaji)";
                var param = new { NIK = obj.NIK, Norek = obj.norek, Gaji = obj.gaji };

                try
                {
                    conn.Execute(strsql, param);
                }
                catch (SqlException ee)
                {
                    throw new Exception($"Number : {ee.Number} EROOR : {ee.Message}");
                }
            }
        }

        /// <summary>
        /// Update jumlah gaji
        /// </summary>
        /// <param name="obj"></param>
        public void Update(Gaji obj)
        {
            using (SqlConnection conn = new SqlConnection(Helper.GetConnectionString()))
            {
                string strsql = @"UPDATE gaji set norek = @Norek, gaji = @Gaji) WHERE NIK = @NIK";
                var param = new { NIK = obj.NIK, Norek = obj.norek, Gaji = obj.gaji };

                try
                {
                    conn.Execute(strsql, param);
                }
                catch (SqlException ee)
                {
                    throw new Exception($"Number : {ee.Number} ERROR : {ee.Message}");
                }
            }
        }

        /// <summary>
        /// Delete berdasarkan NIK aja
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            using (SqlConnection conn = new SqlConnection(Helper.GetConnectionString()))
            {
                string strsql = @"DELETE FROM gaji WHERE NIK = @NIK";
                var param = new { NIK = id };

                try
                {
                    conn.Execute(strsql, param);
                }
                catch (SqlException ee)
                {
                    throw new Exception($"Number : {ee.Number} ERROR : {ee.Message}");
                }
            }
        }
    }
}