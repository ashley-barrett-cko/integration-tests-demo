using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Respawn;

namespace Integration_Tests_Demo.Tests
{
    [TestFixture]
    public class MyTestFixture
    {
        private static Checkpoint checkpoint;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            checkpoint = new Checkpoint(); 
        }

        [TearDown]
        public async Task TearDown()
        {
            await checkpoint.Reset("Server=demo.db;Database=Products;User=sa;Password=Pa33WorD!;");
        }

        [Test]
        public async Task MyTFirsTestMethod()
        {
            await AddNewProduct();
            await AddNewProduct();
            await AddNewProduct();
            await AddNewProduct();
            await AddNewProduct();
            var num = await GetNumberOfProducts();
            Assert.That(num, Is.EqualTo(5));
        }

        [Test]
        public async Task MySecondTestMethod()
        {
            await AddNewProduct();
            await AddNewProduct();
            var num = await GetNumberOfProducts();
            Assert.That(num, Is.EqualTo(2));
        }

        private async Task AddNewProduct()
        {
            using(var c = new HttpClient())
            {
                c.BaseAddress = new Uri("http://demo.api/");
                var product = new Product { Id = 10, Name = "Milk", IsActive = true };
                var json = JsonConvert.SerializeObject(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await c.PostAsync("/api/products", content);
                response.EnsureSuccessStatusCode();
            }
        }

        private async Task<int> GetNumberOfProducts()
        {
            using(var c = new HttpClient())
            {
                c.BaseAddress = new Uri("http://demo.api/");
                var response = await c.GetAsync("/api/products");
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<Product>>(responseString);
                return products.Count;
            }
        }
    }
}
