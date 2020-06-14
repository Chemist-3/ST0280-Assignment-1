using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using task_2_part_2.Models;

namespace task_2_part_2.Controllers
{

    [RoutePrefix("api/V3/Products")]


    //product v3 controller is created to check the model state and respond accordingly if there are errors when validation fails
    public class ProductV3Controller : ApiController
    {

        static readonly IProductRepository repository = new ProductRepository();

        //Third iteration
        //https://localhost:44310/api/V3/Products/GetAllProducts
        
        [Route("GetAllProducts")]
        public IEnumerable<Product> GetAllProducts()
        {
            return repository.GetAll();
        }


        //https://localhost:44310/api/V3/Products/GetAllProducts/{id}
        [HttpGet]
        [Route("GetAllProducts/{id:int:min(1)}", Name ="getProductByIdV3")]
        public HttpResponseMessage GetProduct(int id)
        {
            Product item = repository.Get(id);

            HttpResponseMessage response = null;
            if (item == null)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "Item not found. Invalid get request.");
            }
            else
            {
                response = Request.CreateResponse<Product>(HttpStatusCode.OK, item);
            }
            return response;
        }


        //https://localhost:44310/api/V3/Products/GetAllProducts?category=
        [HttpGet]
        [Route("GetAllProducts", Name ="getAllPRoductsByCategory")]

        public IEnumerable<Product> GetProductByCategory(string category)
        {
            return repository.GetAll().Where(p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase)); //return a cases that matches the category ignoring capitalisation
        }


        //https://localhost:44310/api/V3/Products/GetAllProducts
        [HttpPost]
        [Route("GetAllProducts")]
        public HttpResponseMessage CreateProduct(Product item)
        {
            if (ModelState.IsValid)
            {
                item = repository.Add(item);
                var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item); //reponse will be 201 created 

                //now to generate a link to the new product
                string uri = Url.Link("getProductByIdV3", new { id = item.Id });
                response.Headers.Location = new Uri(uri);
                return response;

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        //https://localhost:44310/api/V3/Products/GetAllProducts/{id}
        [HttpPut]
        [Route("GetAllProducts/{id:int}")]
        public void updateProduct(int id, Product product)
        {
            product.Id = id;
            if (!repository.Update(product))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        //https://localhost:44310/api/V3/Products/GetAllProducts/{id}
        [HttpDelete]
        [Route("GetAllProducts/{id:int}")]
        public void DeleteProduct(int id)
        {
            repository.Remove(id);
        }
    }
}
