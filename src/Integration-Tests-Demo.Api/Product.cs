using System;

namespace Integration_Tests_Demo.Api.Controllers
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}