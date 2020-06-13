using System;
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

        //URI https://localhost:44310/api/V2/Products/GetProducts
        [HttpGet]
        [Route("GetProducts")]
        public IEnumerable<Product> GetAllProducts()
        {
            return repository.GetAll();
        }

        //URI https://localhost:44310/api/V2/Products/GetProducts/{id}
        [HttpGet]
        [Route("GetProducts/{id}", Name = "getProductId")]
        public Product GetAllProducts(int id)
        {
            Product item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        //URI https://localhost:44310/api/V2/Products/GetProductsCategory/{category}
        [HttpGet]
        [Route("GetProductsCategory/{category}")]
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return repository.GetAll().Where(
                p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        //URI https://localhost:44310/api/V2/products/itemPost
        [HttpPost]
        [Route("itemPost")]
        public HttpResponseMessage PostProduct(Product item)
        {
            item = repository.Add(item);    // Add item into 
            var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item); // Response 201 Created

            // Call HTTPGET getProductId
            string uri = Url.Link("getProductId", new { id = item.Id }); //generates a link to the new product and sets
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [HttpPut]
        [Route("GetProducts/{id:int}")]
        public HttpResponseMessage PutProduct(int id, Product product)
        {
            product.Id = id;
            HttpResponseMessage response = null;
            if (!repository.Update(product))
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "Invalid id. Invalid update request.");
                // throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                response = Request.CreateResponse<Product>(HttpStatusCode.OK, product);
            }
            return response;
        }


        /*//URI https://localhost:44310/api/V2/products/
        public void PutProduct(int id, Product product)
        {
            product.Id = id;
            if (!repository.Update(product))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

        }*/

        [HttpDelete]
        [Route("GetProducts/{id:int}")]
        public HttpResponseMessage DeleteProduct(int id)
        {
            Product item = repository.Get(id);
            HttpResponseMessage response = null;
            if (item == null)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, item.Id + " is not found. Invalid delete request.");
            }
            else
            {
                repository.Remove(id);

                response = Request.CreateResponse<Product>(HttpStatusCode.Accepted, item);
            }
            return response;
        }

    }

    
}
