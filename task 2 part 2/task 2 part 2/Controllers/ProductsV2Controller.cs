﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using task_2_part_2.Models;

namespace task_2_part_2.Controllers
{
    [RoutePrefix("api/V2/products")]

    public class ProductsV2Controller : ApiController
    {
        static readonly IProductRepository repository = new ProductRepository();

        [Route("GetAllProducts")]
        public IEnumerable<Product> GetAllProducts()
        {
            return repository.GetAll();
        }

        [Route("GetProduct/{id}", Name = "getProductId")]
        public Product GetProduct(int id)
        {
            Product item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return repository.GetAll().Where(
                p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        // https://localhost:44310/api/V2/products/itemPost
        [Route("itemPost")]
        public HttpResponseMessage PostProduct(Product item)
        {
            item = repository.Add(item);    // Add item into repo
            var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item); // Response 201 Created

            // Call HTTPGET getProductId
            string uri = Url.Link("getProductId", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutProduct(int id, Product product)
        {
            product.Id = id;
            if (!repository.Update(product))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

        }
    }

    
}
