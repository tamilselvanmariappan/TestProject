using Data.Test;
using Data.Test.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace WebExperience.Test.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AssetController : ApiController
    {
        // TODO
        // Create an API controller via REST to perform all CRUD operations on the asset objects created as part of the CSV processing test
        // Visualize the assets in a paged overview showing the title and created on field
        // Clicking an asset should navigate the user to a detail page showing all properties
        // Any data repository is permitted
        // Use a client MVVM framework

        private readonly DataRepository repo = new DataRepository();

        // GET api/asset
        public async Task<IHttpActionResult> Get(int? pageId = 1)
        {
            return Ok(await repo.Get(pageId.Value, 20));
        }

        // GET api/asset/100
        public IHttpActionResult Get(string id)
        {
            return Ok(repo.GetAssetById(id));
        }

        // POST api/asset
        public IHttpActionResult Post([FromBody]Asset asset)
        {
            return Ok(repo.SaveAsset(asset));
        }

        // PUT api/asset/5
        public IHttpActionResult Put(string id, [FromBody]Asset asset)
        {
            return Ok(repo.UpdateAsset(id, asset));
        }

        // DELETE api/asset/5
        public IHttpActionResult Delete(string id)
        {
            return Ok(repo.Delete(id));
        }
    }
}
