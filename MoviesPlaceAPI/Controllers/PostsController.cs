using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using Newtonsoft.Json;

namespace MoviesPlaceAPI.Controllers
{
  [Route("[controller]")]
  [Produces("application/json")]
  [Authorize]
  public class PostsController : MoviesPlaceBaseController
  {
    private readonly ILogger _logger;
    private readonly CloudinaryService _cloudinary;
    public PostsController(IMoviesPlaceSupervisor supervisor, ILogger<PostsController> logger, CloudinaryService cloudinary) : base(supervisor)
    {
      _cloudinary = cloudinary;
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
    public async Task<ActionResult<PostViewModel>> Post() //[FromBody]PostViewModel post
    {
      // Retrieve post data values
      PostViewModel post = JsonConvert.DeserializeObject<PostViewModel>(Request.Form["postForm"]);
      
      // Check if we are uploading images
      if (Request.Form.Files.Count() > 0)
      {
        var file = Request.Form.Files[0];

        var result = await _cloudinary.UploadImage(file);
        if (result.StatusCode == HttpStatusCode.OK)
        {
          post.MoviePictureID = result.PublicId;
          post.MoviePictureURL = result.SecureUri.AbsoluteUri;
        }
      }      

      PostViewModel postViewModel = await _moviesPlaceSupervisor.AddPostAsync(post);

      return new JsonResult(postViewModel);
    }
    
    [HttpPatch("like/{id}")]
    public async Task<ActionResult<PostViewModel>> Like(int id)
    {
      PostViewModel postViewModel = await _moviesPlaceSupervisor.GetPostByIDAsync(id);
      if(postViewModel == null)
      {
        return BadRequest();
      }
      postViewModel.Likes += 1;     
      if(await _moviesPlaceSupervisor.UpdatePostAsync((postViewModel)))
      {
        return new OkObjectResult(postViewModel);
      }

      return BadRequest();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<PostViewModel>> Patch()
    {
      //Check if post exists
      PostViewModel postViewModel = JsonConvert.DeserializeObject<PostViewModel>(Request.Form["postForm"]);
      if(await _moviesPlaceSupervisor.GetPostByIDAsync((int) postViewModel.PostID) == null)
      {
        return BadRequest(Errors.AddErrorToModelState("post_update_failure", "The provided post could not be found", ModelState));
      }

      if(Request.Form.Files.Count() > 0)
      {
        var file = Request.Form.Files[0];

        var delResult = await _cloudinary.DeleteResource(postViewModel.MoviePictureID);
        if(delResult.StatusCode == HttpStatusCode.OK)
        {
          var result = await _cloudinary.UploadImage(file);
          if(result.StatusCode == HttpStatusCode.OK)
          {
            postViewModel.MoviePictureID = result.PublicId;
            postViewModel.MoviePictureURL = result.SecureUri.AbsoluteUri;
          }          
        }
      }      

      if (await _moviesPlaceSupervisor.UpdatePostAsync(postViewModel))
      {
        postViewModel = await _moviesPlaceSupervisor.GetPostByIDAsync((int)postViewModel.PostID);
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
      if (postViewModelToDelete == null)
      {
        return BadRequest(Errors.AddErrorToModelState("post_delete_failure", "The provided post for deletion could not be found", ModelState));
      }

      var result = await _cloudinary.DeleteResource(postViewModelToDelete.MoviePictureID);      

      if (await _moviesPlaceSupervisor.DeletePostAsync((int)postViewModelToDelete.PostID))
      {
        return new OkObjectResult(id);
      }

      return BadRequest();
    }

    [HttpPost("upload")]
    public async Task<ActionResult> Upload()
    {

      var file = Request.Form.Files[0];

      var result = await _cloudinary.UploadImage(file);

      return Ok(result.SecureUri);
    }
  }
}
