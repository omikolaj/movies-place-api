using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MoviesPlaceAPI.Utilities
{
  public class CookieFilter : IActionFilter
  {
    public void OnActionExecuted(ActionExecutedContext context)
    {
           
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
      var cookieList = context.HttpContext.Request.Cookies;
      if((cookieList.Count() == 0) && (!cookieList.ContainsKey("token")))
      {
        context.Result = new BadRequestObjectResult(context.ModelState);
      }       
    }
  }
}