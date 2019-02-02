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
    public async Task<List<FavoritesViewModel>> GetAllFavoritesAsync(CancellationToken ct = default(CancellationToken))
    {
      List<FavoritesViewModel> favorites = FavoritesConverter.ConvertList(await _favoritesRepository.GetAllAsync(ct));

      return favorites;
    }

    public async Task<List<FavoritesViewModel>> GetAllFavoritesByUserIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      List<FavoritesViewModel> favorites = FavoritesConverter.ConvertList(await _favoritesRepository.GetAllByUserIDAsync(ID, ct));

      return favorites;
    }

    public async Task<FavoritesViewModel> AddFavoriteAsync(FavoritesViewModel favoritesViewModel, CancellationToken ct = default(CancellationToken))
    {
      Favorites favorites = new Favorites()
      { };

      favorites = await _favoritesRepository.AddAsync(favorites, ct);

      favoritesViewModel.FavoritesID = favorites.FavoritesID;

      return favoritesViewModel;

    }
  }
}