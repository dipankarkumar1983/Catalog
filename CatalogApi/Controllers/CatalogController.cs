using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CatalogApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {

        private static IList<Product> products = new List<Product>() {

            new Product{Id=1,Name="ABC",Price=80,Quantity=25},
            new Product{Id=2,Name="DEF",Price=70,Quantity=35},
            new Product{Id=3,Name="GHI",Price=60,Quantity=45},
            new Product{Id=4,Name="JKL",Price=50,Quantity=55}

        };

        [HttpGet("", Name = "GetAll")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IEnumerable<Product> GetItems()
        {
            return products;
        }

        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<Product> GetItemByID([FromRoute]int id)
        {
            var item = products.Single(s => s.Id == id);
            if (item == null)
                return NotFound();
            else
                return item;

        }


        [HttpPatch("", Name = "AddItem")]
        [ProducesResponseType((int)HttpStatusCode.Created)]

        public ActionResult<Product> AddItem([FromBody] Product product)
        {
            product.Id = products.Max(s => s.Id) + 1;
            products.Add(product);
            return Created("", product);

        }

    }
}