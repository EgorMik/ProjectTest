using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Presentation_Layer.Model;

namespace Project_Qulix.Controllers
{
    public class CompanyController : Controller
    {
        public IConfiguration Configuration { get; }
        public CompanyController(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IActionResult Company()
        {
            List<Company> companyList = new List<Company>();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Вызываем SqlDataReader
                connection.Open();

                string sql = "Select * From Company";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Company company = new Company();
                        company.Id = Convert.ToInt32(dataReader["Id"]);
                        company.NameCompany = Convert.ToString(dataReader["NameCompany"]);
                        company.SizeCompany = Convert.ToInt32(dataReader["SizeCompany"]);
                        company.FormOfIncorporation = Convert.ToString(dataReader["FormOfIncorporation"]);

                        companyList.Add(company);
                    }
                }
                connection.Close();
            }
            return View(companyList);
        }

        [HttpGet]
        public IActionResult CreateCompany()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateCompany(Company company)
        {
            if (ModelState.IsValid)
            {
                string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = $"Insert Into Company (NameCompany, " +
                        $"SizeCompany," +
                        $" FormOfIncorporation)" +
                        $" Values (N'{company.NameCompany}', " +
                        $"'{company.SizeCompany}'," +
                        $"N'{company.FormOfIncorporation}')";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    return RedirectToAction("Company");
                }
            }
            else
                return View();
        }

        [HttpGet]
        public IActionResult UpdateCompany(int id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            Company company = new Company();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql =$"Select * From Company Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        company.Id = Convert.ToInt32(dataReader["Id"]);
                        company.NameCompany = Convert.ToString(dataReader["NameCompany"]);
                        company.SizeCompany = Convert.ToInt32(dataReader["SizeCompany"]);
                        company.FormOfIncorporation = Convert.ToString(dataReader["FormOfIncorporation"]);
                    }
                    connection.Close();
                }
                return View(company);
            }
        }
        [HttpPost]
        [ActionName("UpdateCompany")]
        public IActionResult UpdateCompany(Company company)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Update Company SET NameCompany =N'{company.NameCompany}', " +
                    $"SizeCompany='{company.SizeCompany}'," +
                    $"FormOfIncorporation=N'{company.FormOfIncorporation}'Where Id='{company.Id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Company");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete From Company Where Id='{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        ViewBag.Result = "Operation got error:" + ex.Message;
                    }
                    connection.Close();
                }
            }
            return RedirectToAction("Company");
        }

    }
}