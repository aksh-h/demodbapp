using demo1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace demo1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            List<Person> person = GetData();
            return View(person);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Udpate(Person p)
        {
            return View();
        }

        public List<Person> GetData()
        {
            List<Person> person = new List<Person>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[GetAllData]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataReader reader = null;
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Person p = new Person();
                            p.Id = reader.GetInt32(reader.GetOrdinal("ID"));
                            p.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                            p.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                            person.Add(p);
                        }
                    }
                }
                return person;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetDataToEdit(string id)
        {
            Person p = new Person();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[GetDataToEdit]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        con.Open();
                        SqlDataReader reader = null;
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            p.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                            p.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                        }
                    }
                }
                return Json(p, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult UpdateData(string id, string fname, string lname)
        {
            Person p = new Person();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[UpdatePersons]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@fname", fname);
                        cmd.Parameters.AddWithValue("@lname", lname);
                        con.Open();
                        cmd.ExecuteScalar();
                    }
                }
                return Json(p, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult InsertIntoPerson(string fname, string lname)
        {
            Person p = new Person();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[InsertIntoPerson]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@fname", fname);
                        cmd.Parameters.AddWithValue("@lname", lname);
                        con.Open();
                        cmd.ExecuteScalar();
                    }
                }
                return Json(p, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult Delete(string id)
        {
            Person p = new Person();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[DeletePerson]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        con.Open();
                        cmd.ExecuteScalar();
                    }
                }
                return Json(p, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}