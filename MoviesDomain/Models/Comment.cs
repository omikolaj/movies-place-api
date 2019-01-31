namespace MoviesDomain.Models
{
  public class Comment
  {
    public int CommentID { get; set; }
    public int PostID { get; set; }
    public string Content { get; set; }  
  }
}