using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MoviesDomain.Converters;
using MoviesDomain.Models;
using MoviesDomain.ViewModels;

namespace MoviesDomain.Supervisor
{
    public partial class MoviesPlaceSupervisor : IMoviesPlaceSupervisor
    {
        public async Task<List<UserViewModel>> GetAllUsersAsync(CancellationToken ct = default(CancellationToken))
        {
            List<UserViewModel> usersViewModel = UserConverter.ConvertList(await _userRepository.GetAllAsync(ct));

            return usersViewModel;
        }

        public async Task<UserViewModel> GetUserByIDAsync(int ID, CancellationToken ct = default(CancellationToken))
        {
            UserViewModel userViewModel = UserConverter.Convert(await _userRepository.GetByIDAsync(ID, ct));

            userViewModel.Comments = await GetAllCommentsByUserIDAsync(userViewModel.UserID, ct);
            userViewModel.Posts = await GetAllPostsByUserIDAsync(userViewModel.UserID, ct);
            userViewModel.Favorites = await GetAllFavoritesByUserIDAsync(userViewModel.UserID, ct);

            return userViewModel;
        }

        public async Task<UserViewModel> GetUserByPostIDAsync(int ID, CancellationToken ct = default(CancellationToken))
        {
            UserViewModel userViewModel = UserConverter.Convert(await _userRepository.GetByPostIDAsync(ID, ct));

            userViewModel.Comments = await GetAllCommentsByUserIDAsync(userViewModel.UserID, ct);
            userViewModel.Posts = await GetAllPostsByUserIDAsync(userViewModel.UserID, ct);
            userViewModel.Favorites = await GetAllFavoritesByUserIDAsync(userViewModel.UserID, ct);
            
            return userViewModel;
        }

        public async Task<UserViewModel> AddUserAsync(UserViewModel userViewModel, CancellationToken ct = default(CancellationToken))
        {
            User user = new User()
            {
                Username = userViewModel.Username,
                Email = userViewModel.Email,
                Password = userViewModel.Password                
            };

            user = await _userRepository.AddAsync(user, ct);
            userViewModel.UserID = user.UserID;

            return userViewModel;
        }

        public async Task<bool> UpdateUserAsync(UserViewModel userViewModel, CancellationToken ct = default(CancellationToken))
        {
            User user = await _userRepository.GetByIDAsync(userViewModel.UserID, ct);

            user.Password = userViewModel.Password;
            user.Username = userViewModel.Username;
            user.Email = userViewModel.Email;

            return await _userRepository.UpdateAsync(user, ct);
        }

        public async Task<bool> DeleteUserAsync(int ID, CancellationToken ct = default(CancellationToken))
        {
            return await _userRepository.DeleteAsync(ID, ct);
        }

    }
}