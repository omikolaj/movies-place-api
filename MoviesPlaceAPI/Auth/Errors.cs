using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MoviesPlaceAPI.Auth
    { 
        public static class Errors
        {
            public static ModelStateDictionary AddErrorsToModelState(IdentityResult identityResult, ModelStateDictionary modelState)
            {
                foreach (var e in identityResult.Errors)
                {
                    modelState.TryAddModelError(e.Code, e.Description);
                }

                return modelState;
            }

            public static ModelStateDictionary AddErrorToModelState(string code, string description, ModelStateDictionary modelState)
            {
                modelState.TryAddModelError(code, description);
                return modelState;
            }

    internal static object AddErrorToModelState(string v1, string v2)
    {
      throw new NotImplementedException();
    }
  }
    }