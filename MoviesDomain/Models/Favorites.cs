namespace MoviesDomain.Models
{
  public class Favorites
  {
    public int FavoriteID { get; set; }
    public int MovieID { get; set; }
    public int UserID { get; set; }
    public string Note { get; set; }
  }

}