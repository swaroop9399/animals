using Animal.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Animal.Controllers
{
    public class HomeController : Controller
    {


        string conString = "Data Source = LTP_RD_411; Initial Catalog = test; Integrated Security = True;";

        public IActionResult AllData()
        {
            string query = "select * from animals";
            SqlConnection con = new SqlConnection(conString);
            var list = con.Query<Class>(query).ToList();
            return View(list);
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Insert(Class a)
        {
            string insert = "insert into animals (Name, City) values(@name, @city)";
            Console.WriteLine(a.Name);
            IDbConnection con = new SqlConnection(conString);
            con.Execute(insert, a);
            return Redirect ("/");
        }

        public IActionResult DeleteProcess(int id)
        {
            string delete = "delete from animals where AID = @AID";
            IDbConnection con = new SqlConnection(conString);
            con.Execute(delete, new {AID = id});
            return Redirect ("/");
        }

        public IActionResult EditProcess(int id)
        {
            string query = "select * from animals where AID = @AID";
            SqlConnection con = new SqlConnection(conString);
            var a = con.Query<Class>(query, new { AID = id }).FirstOrDefault();
            
            return View(a);
        }


        public RedirectResult Edit(Class u)
        {
            string edit = "update animals set Name = @name, City = @city where AID = @AID";
            IDbConnection con = new SqlConnection(conString);
            con.Execute(edit, u);
            return Redirect("/");
        }
    }
}