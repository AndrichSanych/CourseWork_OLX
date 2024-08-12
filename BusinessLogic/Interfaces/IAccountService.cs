using BusinessLogic.DTOs;
using BusinessLogic.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IAccountService
    {
        Task RegisterUserAsync(RegisterUserModel model);
        Task<AuthResponce> LoginAsync(AuthRequest model);
        Task ToggleFavoriteAdvert(int advertId, ClaimsPrincipal user);
        Task<UserDto> GetUserAsync(string id);

    }
}
