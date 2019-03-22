using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesDomain.Models
{
  public class RefreshToken
    {
        public string RefreshTokenID { get; set; }
        public string UserId {get; set;}
        public User User { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }        
        public bool Active => DateTime.UtcNow <= Expires;
        // public string RemoteIpAddress { get; private set; }

        public RefreshToken(string token, DateTime expires, string userId)
        {
            Token = token;
            Expires = expires;
            UserId = userId;
            //RemoteIpAddress = remoteIpAddress;
        }
    }
}
