using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesDomain;
using MoviesDomain.Converters;
using MoviesDomain.Models;
using MoviesDomain.Supervisor;
using MoviesDomain.ViewModels;
using MoviesPlaceAPI.Auth;

namespace MoviesPlaceAPI.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class PostsController : MoviesPlaceBaseController
    {
      private readonly ILogger _logger;
      public PostsController(IMoviesPlaceSupervisor supervisor, ILogger<PostsController> logger) : base (supervisor)
      {
        _logger = logger;
      }

        // GET api/v1/posts
        [HttpGet]        
        [Produces(typeof(List<PostViewModel>))]
        public async Task<ActionResult<List<PostViewModel>>> Get(CancellationToken ct = default(CancellationToken))
        {
          _logger.LogDebug(LoggingEvents.ListItems, "Fetching all posts");          

          return new JsonResult(await _moviesPlaceSupervisor.GetAllPostsAsync(ct));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            throw new Exception("Invalid ID");
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<PostViewModel>> Post([FromBody]PostViewModel post)
        {          
          return new JsonResult(await _moviesPlaceSupervisor.AddPostAsync(post));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] PostViewModel post)
        {

        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<PostViewModel>> Patch(int id, [FromBody] PostViewModel post)
        {
          //Check if post exists
          PostViewModel postViewModel = await  _moviesPlaceSupervisor.GetPostByIDAsync(id);
          if(postViewModel == null)
          {
            return BadRequest(Errors.AddErrorToModelState("post_update_failure", "The provided post could not be found", ModelState));
          }

          if(await _moviesPlaceSupervisor.UpdatePostAsync(post))
          {
            postViewModel = await _moviesPlaceSupervisor.GetPostByIDAsync(id);
            return new OkObjectResult(postViewModel);
          }

          return BadRequest(Errors.AddErrorToModelState("post_update_failure", "Failed to successfully update the post", ModelState));

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
          //Check if post exists
          PostViewModel postViewModelToDelete = await _moviesPlaceSupervisor.GetPostByIDAsync(id);
          if(postViewModelToDelete == null)
          {
            return BadRequest(Errors.AddErrorToModelState("post_delete_failure", "The provided post for deletion could not be found", ModelState));
          }

          if(await _moviesPlaceSupervisor.DeletePostAsync((int)postViewModelToDelete.PostID))
          {
            return new OkObjectResult(id);
          }

          return BadRequest();
        }
    }
}
