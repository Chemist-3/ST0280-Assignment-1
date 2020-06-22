using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Microsoft.AspNetCore.Cors;
using ProductStore.Models;

namespace ProductStore.Controllers
{
    public class TalentsController : ApiController
    {
        static readonly TalentRepository repository = new TalentRepository();
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/talents")]
        public IEnumerable<Talent> GetAllTalents()
        {
            return repository.GetAll();
        }

        [Route("api/talents/{id:int}")]
        public Talent GetTalent(int id)
        {
            Talent item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }
    }

}
