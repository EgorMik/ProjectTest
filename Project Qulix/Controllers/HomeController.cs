using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Presentation_Layer.Model;
using Project_Qulix.Models;

namespace Project_Qulix.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; }
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IActionResult Index()
        {
            List<Employee> employeeList = new List<Employee>();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Вызываем SqlDataReader
                connection.Open();

                string sql = "Select * From Employee";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Employee employee = new Employee();
                        employee.Id = Convert.ToInt32(dataReader["Id"]);
                        employee.FirstName = Convert.ToString(dataReader["FirstName"]);
                        employee.LastName = Convert.ToString(dataReader["LastName"]);
                        employee.MiddleName = Convert.ToString(dataReader["MiddleName"]);
                        employee.Position = Convert.ToString(dataReader["Position"]);
                        employee.Company = Convert.ToString(dataReader["Company"]);
                        employee.Date = Convert.ToDateTime(dataReader["Date"]);
                        employeeList.Add(employee);
                    }
                }
                connection.Close();
            }
            return View(employeeList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = $"Insert Into Employee (FirstName, " +
                        $"LastName, " +
                        $"MiddleName," +
                        $"Position, " +
                        $"Company," +
                        $" Date) Values (N'{employee.FirstName}', " +
                        $"N'{employee.LastName}'," +
                        $"N'{employee.MiddleName}'," +
                        $"N'{employee.Position}'," +
                        $"N'{employee.Company}'," +
                        $"'{employee.Date}')";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    return RedirectToAction("Index");
                }
            }
            else
                return View();
        }



        [HttpGet]
        public IActionResult Update(int id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            Employee employee = new Employee();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From Employee Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        employee.Id = Convert.ToInt32(dataReader["Id"]);
                        employee.FirstName = Convert.ToString(dataReader["FirstName"]);
                        employee.LastName = Convert.ToString(dataReader["LastName"]);
                        employee.MiddleName = Convert.ToString(dataReader["MiddleName"]);
                        employee.Position = Convert.ToString(dataReader["Position"]);
                        employee.Company = Convert.ToString(dataReader["Company"]);
                        employee.Date = Convert.ToDateTime(dataReader["Date"]);
                    }
                    connection.Close();
                }
                return View(employee);
            }
        }



        [HttpPost]
        [ActionName("Update")]
        public IActionResult Update(Employee employee)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Update Employee SET FirstName=N'{employee.FirstName}', " +
                    $"LastName=N'{employee.LastName}'," +
                    $"MiddleName=N'{employee.MiddleName}'," +
                    $"Position=N'{employee.Position}'," +
                    $"Company=N'{employee.Company}'," +
                    $"Date='{employee.Date}' Where Id='{employee.Id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Index");
        }



        [HttpPost]
        public IActionResult Delete(int id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete From Employee Where Id='{id}'";
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
            return RedirectToAction("Index");
        }

    }
}
