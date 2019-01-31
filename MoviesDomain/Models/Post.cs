using System;

namespace MoviesDomain
{
  public class Post
  {
    public int PostID { get; set; }
    public int UserID { get; set; }
    public int MoviePostID { get; set; }
    public string Description { get; set; }
    public DateTime PostDate { get; set; }    
  }
}