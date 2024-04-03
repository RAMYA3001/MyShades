using MySql.Data.MySqlClient;
using Photo_Match.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Photo_Match.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase imageFile)
        {
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                // Save the uploaded image and update the ImagePath property
                // You would need to implement the logic to save and retrieve image paths
                // For simplicity, let's assume a folder named "Uploads" in the project root

                string fileName = Path.GetFileName(imageFile.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Uploads"), fileName);

                imageFile.SaveAs(filePath);

                // Update the ImagePath property with the new image path
                var model = new ImageViewModel
                {
                    ImagePath = "~/Uploads/" + fileName
                };

                return Json(model);
            }

            return HttpNotFound();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ShadeCardView()
        {
            return View();
        }
        public ActionResult getcolourShade(string shadecode,string BrandName)
        {
            try
            {
                string constr = "";
                constr = ConfigurationManager.ConnectionStrings["Nerolacconstr_cw"].ConnectionString;
                MySqlConnection cn = new MySqlConnection(constr);
                //// MySqlCommand cmd = new MySqlCommand();
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT  CONCAT('#', red_h, green_h, blue_h) AS Colour,Concat(shadename,',',colorFamily) shadename FROM ancptable where shadecode='" + shadecode+ "' and brandname ='" + BrandName +"'", cn);
                cmd.CommandTimeout = 1600;
                cmd.CommandType = CommandType.Text;
                List<BrandName> projects = new List<BrandName>();
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        BrandName project = new BrandName
                        {

                            Colour = rdr["Colour"].ToString(),
                            shadename = rdr["shadename"].ToString(),

                        };

                        projects.Add(project);
                    }
                }
                cn.Close();
                return Json(projects, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return View();
            }
           
        }
        public ActionResult getcolourShadeData(string BrandName)
        {
            try
            {
                string constr = "";
                constr = ConfigurationManager.ConnectionStrings["Nerolacconstr_cw"].ConnectionString;
                MySqlConnection cn = new MySqlConnection(constr);
                // MySqlCommand cmd = new MySqlCommand();
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("select BrandName as PalletCode, shadecode,ShadeName,ColorFamily,Ifnull(ColourGroup1,' ') as ColourGroup1,CONCAT('#', red_h, green_h, blue_h) AS Colour from ancptable where brandname='" + BrandName + "' order by ColorFamily, ColourGroup1, shadecode", cn);
                cmd.CommandTimeout = 1600;
                cmd.CommandType = CommandType.Text;
                List<modelShadecodeList> projects = new List<modelShadecodeList>();
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        modelShadecodeList project = new modelShadecodeList
                        {
                            Pallet = rdr["PalletCode"].ToString(),
                            shadeCode = rdr["shadecode"].ToString(),                            
                            shadename = rdr["ShadeName"].ToString(),
                            ColourFamly = rdr["ColorFamily"].ToString(),
                            ColourGrp = rdr["ColourGroup1"].ToString(),
                            Colour = rdr["Colour"].ToString(),

                        };

                        projects.Add(project);
                    }
                }
                cn.Close();
                return Json(projects, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return View();
            }

        }
        public ActionResult getcolourShadeDataFM(string BrandName,string SearchText)
        {
            try
            {
                string constr = "";
                constr = ConfigurationManager.ConnectionStrings["Nerolacconstr_cw"].ConnectionString;
                MySqlConnection cn = new MySqlConnection(constr);
                // MySqlCommand cmd = new MySqlCommand();
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("select BrandName as PalletCode, shadecode,ShadeName,ColorFamily,Ifnull(ColourGroup1,'') as ColourGroup1,CONCAT('#', red_h, green_h, blue_h) AS Colour from ancptable where brandname='" + BrandName + "' and ColorFamily like '%" + SearchText + "%' order by ColorFamily, ColourGroup1, shadecode", cn);
                cmd.CommandTimeout = 1600;
                cmd.CommandType = CommandType.Text;
                List<modelShadecodeList> projects = new List<modelShadecodeList>();
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        modelShadecodeList project = new modelShadecodeList
                        {
                            Pallet = rdr["PalletCode"].ToString(),
                            shadeCode = rdr["shadecode"].ToString(),
                            shadename = rdr["ShadeName"].ToString(),
                            ColourFamly = rdr["ColorFamily"].ToString(),
                            ColourGrp = rdr["ColourGroup1"].ToString(),
                            Colour = rdr["Colour"].ToString(),

                        };

                        projects.Add(project);
                    }
                }
                cn.Close();
                return Json(projects, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return View();
            }

        }
        public ActionResult getcolourShadeDataGP(string BrandName, string SearchText)
        {
            try
            {
                string constr = "";
                constr = ConfigurationManager.ConnectionStrings["Nerolacconstr_cw"].ConnectionString;
                MySqlConnection cn = new MySqlConnection(constr);
                // MySqlCommand cmd = new MySqlCommand();
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("select BrandName as PalletCode, shadecode,ShadeName,ColorFamily,Ifnull(ColourGroup1,'') as ColourGroup1,CONCAT('#', red_h, green_h, blue_h) AS Colour from ancptable where brandname='" + BrandName + "' and Ifnull(ColourGroup1,'') like '%" + SearchText + "%' order by ColorFamily, ColourGroup1, shadecode", cn);
                cmd.CommandTimeout = 1600;
                cmd.CommandType = CommandType.Text;
                List<modelShadecodeList> projects = new List<modelShadecodeList>();
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        modelShadecodeList project = new modelShadecodeList
                        {
                            Pallet = rdr["PalletCode"].ToString(),
                            shadeCode = rdr["shadecode"].ToString(),
                            shadename = rdr["ShadeName"].ToString(),
                            ColourFamly = rdr["ColorFamily"].ToString(),
                            ColourGrp = rdr["ColourGroup1"].ToString(),
                            Colour = rdr["Colour"].ToString(),

                        };

                        projects.Add(project);
                    }
                }
                cn.Close();
                return Json(projects, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return View();
            }

        }
        public ActionResult getcolourShadeDataCD(string BrandName, string SearchText)
        {
            try
            {
                string constr = "";
                constr = ConfigurationManager.ConnectionStrings["Nerolacconstr_cw"].ConnectionString;
                MySqlConnection cn = new MySqlConnection(constr);
                // MySqlCommand cmd = new MySqlCommand();
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("select BrandName as PalletCode, shadecode,ShadeName,ColorFamily,Ifnull(ColourGroup1,'') as ColourGroup1,CONCAT('#', red_h, green_h, blue_h) AS Colour from ancptable where brandname='" + BrandName + "' and Ifnull(shadecode,'') like '%" + SearchText + "%' order by ColorFamily, ColourGroup1, shadecode", cn);
                cmd.CommandTimeout = 1600;
                cmd.CommandType = CommandType.Text;
                List<modelShadecodeList> projects = new List<modelShadecodeList>();
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        modelShadecodeList project = new modelShadecodeList
                        {
                            Pallet = rdr["PalletCode"].ToString(),
                            shadeCode = rdr["shadecode"].ToString(),
                            shadename = rdr["ShadeName"].ToString(),
                            ColourFamly = rdr["ColorFamily"].ToString(),
                            ColourGrp = rdr["ColourGroup1"].ToString(),
                            Colour = rdr["Colour"].ToString(),

                        };

                        projects.Add(project);
                    }
                }
                cn.Close();
                return Json(projects, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return View();
            }

        }

        public ActionResult getcolourShadeDataSN(string BrandName, string SearchText)
        {
            try
            {
                string constr = "";
                constr = ConfigurationManager.ConnectionStrings["Nerolacconstr_cw"].ConnectionString;
                MySqlConnection cn = new MySqlConnection(constr);
                // MySqlCommand cmd = new MySqlCommand();
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("select BrandName as PalletCode, shadecode,ShadeName,ColorFamily,Ifnull(ColourGroup1,'') as ColourGroup1,CONCAT('#', red_h, green_h, blue_h) AS Colour from ancptable where brandname='" + BrandName + "' and Ifnull(ShadeName,'') like '%" + SearchText + "%' order by ColorFamily, ColourGroup1, shadecode", cn);
                cmd.CommandTimeout = 1600;
                cmd.CommandType = CommandType.Text;
                List<modelShadecodeList> projects = new List<modelShadecodeList>();
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        modelShadecodeList project = new modelShadecodeList
                        {
                            Pallet = rdr["PalletCode"].ToString(),
                            shadeCode = rdr["shadecode"].ToString(),
                            shadename = rdr["ShadeName"].ToString(),
                            ColourFamly = rdr["ColorFamily"].ToString(),
                            ColourGrp = rdr["ColourGroup1"].ToString(),
                            Colour = rdr["Colour"].ToString(),

                        };

                        projects.Add(project);
                    }
                }
                cn.Close();
                return Json(projects, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return View();
            }

        }
        public ActionResult loadBrandname()
        {
            string constr = "";       
            constr = ConfigurationManager.ConnectionStrings["Nerolacconstr_cw"].ConnectionString;           

            MySqlConnection con = new MySqlConnection(constr);
            try
            {
                MySqlConnection cn = new MySqlConnection(constr);
                // MySqlCommand cmd = new MySqlCommand();
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT BrandName, FanDeckname FROM ( SELECT 'Palette' AS BrandName, 'Select' AS FanDeckname, 1 AS SortOrder UNION ALL SELECT BrandName, CONCAT(BrandName, ' - ', FanDeckname) AS FanDeckname, 2 AS SortOrder FROM (SELECT DISTINCT acp.BrandName, LFD.FanDeckname FROM ancptable ACP LEFT JOIN LkUpFanDeck LFD ON LFD.BrandName = ACP.BrandName) AS subquery) AS result ORDER BY SortOrder, BrandName ASC", cn);
                cmd.CommandTimeout = 1600;
                cmd.CommandType = CommandType.Text;

                List<BrandName> projects = new List<BrandName>();
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        BrandName project = new BrandName
                        {

                            Brand_Name = rdr["BrandName"].ToString(),
                            FandeckName= rdr["FanDeckname"].ToString(),
                        };

                        projects.Add(project);
                    }
                }
                cn.Close();
                return Json(projects, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View();
            }

        }
    }
}