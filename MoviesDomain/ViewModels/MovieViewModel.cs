using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MoviesDomain.Models;

namespace MoviesDomain.ViewModels
{
  public class MovieViewModel
  {
    public int MovieID { get; set; }            
    public string Title { get; set; }
    public IEnumerable<FavoriteViewModel> Favorites { get; set; }
  }
}