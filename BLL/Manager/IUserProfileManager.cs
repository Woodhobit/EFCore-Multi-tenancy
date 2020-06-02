using BLL.DTO;
using System.Threading.Tasks;

namespace BLL.Manager
{
    public interface IUserProfileManager
    {
        Task<long> CreateAsync(UserCreateDTO dto);
        Task<UserProfileDTO> GetByIdAsync(long id);
        Task UpdateAsync(UserUpdateDTO dto);
    }
}