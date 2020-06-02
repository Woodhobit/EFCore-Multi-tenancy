using BLL.DTO;
using DAL.Model;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Manager
{
    public class UserProfileManager : IUserProfileManager
    {
        private readonly IRepository<UserProfile> repository;

        public UserProfileManager(IRepository<UserProfile> repository)
        {
            this.repository = repository;
        }

        public async Task UpdateAsync(UserUpdateDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException();
            }

            var entityDb = await this.repository.GetAsync(dto.Id);

            if (entityDb == null)
            {
                throw new ArgumentNullException();
            }

            entityDb.FirstName = dto.FirstName;
            entityDb.SecondName = dto.SecondName;
            entityDb.Email = dto.Email;

            await this.repository.AddOrUpdateAsync(entityDb);
        }

        public async Task<long> CreateAsync(UserCreateDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException();
            }

            var entity = new UserProfile
            {
                FirstName = dto.FirstName,
                SecondName = dto.SecondName,
                Email = dto.Email
            };

            var result = await this.repository.AddOrUpdateAsync(entity);

            return result.Id;
        }

        public async Task<UserProfileDTO> GetByIdAsync(long id)
        {
            return await this.repository
                .Where(x => x.Id == id)
                .Select(x => new UserProfileDTO { Id = x.Id, Name = $"{x.FirstName} {x.SecondName}", Email = x.Email })
                .FirstOrDefaultAsync();
        }
    }
}
