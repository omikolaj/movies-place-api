using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesDomain.Supervisor;
using MoviesDomain.ViewModels;

namespace MoviesPlaceAPI.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    public class PostsController : MoviesPlaceBaseController
    {
      private readonly ILogger _logger;
      public PostsController(IMoviesPlaceSupervisor supervisor, ILogger<PostsController> logger) : base (supervisor)
      {
        _logger = logger;
      }

        // GET api/posts
        [HttpGet]
        [Produces(typeof(List<PostViewModel>))]
        public async Task<ActionResult<List<PostViewModel>>> Get(CancellationToken ct = default(CancellationToken))
        {
          return new JsonResult(await _moviesPlaceSupervisor.GetAllPostsAsync(ct));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
