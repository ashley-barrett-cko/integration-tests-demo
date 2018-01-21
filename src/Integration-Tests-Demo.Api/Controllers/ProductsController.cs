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
    public class ProductsController : Controller
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            using (var conn = await GetConnection())
            {
                var param = new { id = product.Id, name = product.Name, isActive = product.IsActive }; 

                var sql = @"INSERT INTO dbo.Products(Id, Name, IsActive) VALUES(@id, @name, @isActive)";
                
                await conn.ExecuteAsync(sql, param);

                return Accepted();
            }
        }

        private async Task<IDbConnection> GetConnection()
        {
            var conn = new SqlConnection("Server=demo.db;Database=Products;User=sa;Password=Pa33WorD!;");
            await conn.OpenAsync();
            return conn;
        }
    }
}
