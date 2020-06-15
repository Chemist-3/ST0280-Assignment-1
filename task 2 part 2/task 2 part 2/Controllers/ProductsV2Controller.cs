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
        public HttpResponseMessage GetProducts(int id) //change from Product to HttpResponseMessage to include error messages
        {
            Product item = repository.Get(id);

            HttpResponseMessage response = null;
            if (item == null)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "Invalid id. Item not found.");
            }
            else
            {
                response = Request.CreateResponse<Product>(HttpStatusCode.OK, item);
            }
            return response;

        }

        //URI https://localhost:44310/api/V2/Products/GetProductsCategory/{category}
        [HttpGet]
        [Route("GetProducts")]
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return repository.GetAll().Where(
                p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        //URI https://localhost:44310/api/V2/products/PostProduct
        [HttpPost]
        [Route("PostProduct")]
        public HttpResponseMessage PostProduct(Product item)
        {

            if (ModelState.IsValid)
            {
                item = repository.Add(item);
                var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item);
                string uri = Url.Link("getProductId", new { id = item.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
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
