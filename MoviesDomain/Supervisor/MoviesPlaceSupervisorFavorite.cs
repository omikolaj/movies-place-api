using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MoviesDomain.Converters;
using MoviesDomain.Models;
using MoviesDomain.ViewModels;

namespace MoviesDomain.Supervisor
{
  public partial class MoviesPlaceSupervisor : IMoviesPlaceSupervisor 
  {
    public async Task<List<FavoriteViewModel>> GetAllFavoritesByUserIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      List<FavoriteViewModel> favorites = FavoritesConverter.ConvertList(await _favoriteRepository.GetAllByUserIDAsync(ID, ct));

      return favorites;
    }

    public async Task<List<FavoriteViewModel>> GetAllFavoritesByMovieIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      List<FavoriteViewModel> favorites = FavoritesConverter.ConvertList(await _favoriteRepository.GetAllByMovieIDAsync(ID, ct));

      return favorites;
    }

    public async Task<FavoriteViewModel> GetFavoriteByIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      FavoriteViewModel favorite = FavoritesConverter.Convert(await _favoriteRepository.GetByIDAsync(ID, ct));

      return favorite;
    }

    public async Task<FavoriteViewModel> AddFavoriteAsync(FavoriteViewModel FavoriteViewModel, CancellationToken ct = default(CancellationToken))
    {
      Favorite favorites = new Favorite()
      {
        MovieID = FavoriteViewModel.MovieID,
        UserID = FavoriteViewModel.UserID,
        Note = FavoriteViewModel.Note
      };

      favorites = await _favoriteRepository.AddAsync(favorites, ct);

      FavoriteViewModel.FavoriteID = favorites.FavoriteID;

      return FavoriteViewModel;

    }

    public async Task<bool> DeleteFavorite(int ID, CancellationToken ct = default(CancellationToken))
    {
      return await _favoriteRepository.DeleteAsync(ID, ct);
    }
  }
}