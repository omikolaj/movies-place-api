using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesDomain.ViewModels
{
  public class PostViewModel
  {
    public int PostID { get; set; }   
    public int UserID { get; set; }
    public int MoviePostID { get; set; }
    [StringLength(300)]
    public string Description { get; set; }
    public DateTime PostDate { get; set; }
    //Has_many user_comments
    public IList<CommentViewModel> Comments { get; set; }
    public UserViewModel User { get; set; }    
    public MovieViewModel Movie { get; set; }    
  }
}