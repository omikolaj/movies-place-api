namespace MoviesPlaceAPI.Auth
{
    public static class Constants
    {
        public static class Strings
        {
           public const string TokenName ="jwt";
            public static class JwtClaimIdentifiers
            {
                public const string Role = "role", Id = "id", UserName="UserName", Permission = "permission";
            }            

            public static class JwtClaims
            {
                public const string ApiAccess = "Admin";
            }
        }        
    }
}