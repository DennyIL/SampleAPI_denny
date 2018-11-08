using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using SampleAPI.Models;

namespace SampleAPI.DAL
{
    /// <summary>
    /// Konsep DAL (DATA access layer) adalah membuat function logic
    /// </summary>
    public class PegawaiDAL : ICrud<Pegawai>
    {
        /// <summary>
        /// DELETE tabel Pegawai
        /// </summary>
        /// <param name="id">NIK pegawai</param>
        public void Delete(string id)
        {
            //throw new NotImplementedException();
            //Cel data dlu
            Pegawai objPegawaiExist = GetByID(id);
            if (objPegawaiExist == null)
            {
                throw new Exception("ERROR : data NIK " + id + " Tidak ditemukan");
            }

            using (SqlConnection conn = new SqlConnection(Helper.GetConnectionString()))
            {
                string strSql = @"DELETE FROM Pegawai WHERE NIK = @NIK";

                SqlCommand cmd = new SqlCommand(strSql, conn);
                //ISI parameter
                cmd.Parameters.AddWithValue("@NIK", id);


                //Ekse
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception sqlEx)
                {
                    throw new Exception(sqlEx.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }//end of using db
        }




        /// <summary>
        /// Select All di table PEGAWAI (http://localhost:55980/api/Pegawai)
        /// </summary>
        /// <returns>Semua list PEGAWAI</returns>
        public IEnumerable<Pegawai> GetAll()
        {
            //throw new NotImplementedException();

            //Bikin penampungan atau POCO
            List<Pegawai> lsPegawai = new List<Pegawai>();

            //Connect ke DB
            using (SqlConnection conn = new SqlConnection(Helper.GetConnectionString()))
            {
                string strSql = "SELECT * FROM Pegawai ORDER BY NAMA ASC";
                conn.Open();
                SqlCommand cmd = new SqlCommand(strSql, conn);

                //baca
                SqlDataReader dr = cmd.ExecuteReader();

                //isi object memakai LIST biar bs langsung make 
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lsPegawai.Add(new Pegawai
                        {
                            NIK = dr["NIK"].ToString(),
                            Nama = dr["Nama"].ToString(),
                            Alamat = dr["Alamat"].ToString(),
                            Email = dr["Email"].ToString()
                        }
                        );
                    }
                }

                //tutup koneksi
                dr.Close();
                cmd.Dispose();
                conn.Close();

            }
            return lsPegawai;
        }


        /// <summary>
        /// Get Pegawai berdasar ID (NIK) :: http://dennylabs.azurewebsites.net/api/Pegawai/1111
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Pegawai GetByID(string id)
        {
            //throw new NotImplementedException();

            //Bikin penampungan atau POCO
            Pegawai pegawai = new Pegawai();

            //Connect ke DB
            using (SqlConnection conn = new SqlConnection(Helper.GetConnectionString()))
            {
                string strSql = @"SELECT * FROM Pegawai 
                                WHERE NIK = @NIK";

                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@NIK", id);
                conn.Open();
                //baca
                SqlDataReader dr = cmd.ExecuteReader();

                //isi object memakai LIST biar bs langsung make 
                if (dr.HasRows)
                {
                    dr.Read();
                    pegawai.NIK = dr["NIK"].ToString();
                    pegawai.Nama = dr["Nama"].ToString();
                    pegawai.Alamat = dr["Alamat"].ToString();
                    pegawai.Email = dr["Email"].ToString();
                }
                else
                {
                    pegawai = null;
                }

                //tutup koneksi
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return pegawai;
        }


        /// <summary>
        /// INSERT to db dengan EXECUTE non Query :: 
        /// </summary>
        /// <param name="obj">object Pegawai</param>
        public void Insert(Pegawai obj)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(Helper.GetConnectionString()))
            {
                string strSql = @"INSERT INTO pegawai
                                (NIK, Nama, Alamat, Email) VALUES
                                (@NIK, @NAMA, @ALAMAT, @EMAIL)";
                SqlCommand cmd = new SqlCommand(strSql, conn);

                //ISI parameter
                cmd.Parameters.AddWithValue("@NIK", obj.NIK);
                cmd.Parameters.AddWithValue("@Nama", obj.Nama);
                cmd.Parameters.AddWithValue("@Alamat", obj.Alamat);
                cmd.Parameters.AddWithValue("@Email", obj.Email);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception sqlEx)
                {
                    throw new Exception(sqlEx.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();

                }
            }
        }

        /// <summary>
        /// EDIT data kirim objek Pegawai
        /// </summary>
        /// <param name="obj">Object pegawai</param>
        public void Update(Pegawai obj)
        {

            //Cel data dlu
            Pegawai objPegawaiExist = GetByID(obj.NIK);
            if (objPegawaiExist == null)
            {
                throw new Exception("ERROR : data NIK " + obj.NIK + "Tidak ditemukan");
            }
            else
            {

                using (SqlConnection conn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string strSql = @"UPDATE Pegawai
                                SET 
                                Nama = @NAMA,
                                Alamat = @ALAMAT,
                                Email = @EMAIL
                                WHERE NIK = @NIK";

                    SqlCommand cmd = new SqlCommand(strSql, conn);
                    //ISI parameter
                    cmd.Parameters.AddWithValue("@NIK", obj.NIK);
                    cmd.Parameters.AddWithValue("@NAMA", obj.Nama);
                    cmd.Parameters.AddWithValue("@ALAMAT", obj.Alamat);
                    cmd.Parameters.AddWithValue("@EMAIL", obj.Email);


                    //Ekse
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception sqlEx)
                    {
                        throw new Exception(sqlEx.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                        conn.Close();
                    }
                }//end of using db
            }
        }


    }
}