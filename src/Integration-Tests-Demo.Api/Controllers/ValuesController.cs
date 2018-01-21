using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace Integration_Tests_Demo.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (var conn = await GetConnection())
            {
                var products = conn.Query<Product>("SELECT * FROM Products");
                return Ok(products);
            }
        }

        private async Task<IDbConnection> GetConnection()
        {
            var conn = new SqlConnection("Server=192.168.0.8,1401;Database=Products;User=sa;Password=Pa33WorD!;");
            await conn.OpenAsync();
            return conn;
        }
    }
}
