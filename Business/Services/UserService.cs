using Business.DTOs;
using Helper;
using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace Business.Services;

public class UserService(ErrorLogger errorLogger, UserRepo userRepo)
{
    private readonly ErrorLogger _errorLogger = errorLogger;
    private readonly UserRepo _userRepo = userRepo;

    public async Task<UserDetailsDTO> GetUserDetailsAsync(string authId)
    {
        try
        {
            var userEntity = await _userRepo.GetOneAsync(x => x.Id == authId);
            if (userEntity != null) 
            {
                UserDetailsDTO userDetailsDTO = new UserDetailsDTO
                {
                    Id = userEntity.Id,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    Email = userEntity.Email,
                    Phone = userEntity.Phone,
                    Bio = userEntity.Bio
                };
                return userDetailsDTO;
            }   
        }
        catch(Exception ex) { LogError(ex.Message); }

        return null;
    }

    public async Task<bool> SaveUserDetailsAsync(UserDetailsDTO userDetailsDTO)
    {
        try
        {
            UserEntity userEntity = new UserEntity
            {
                Id = userDetailsDTO.Id,
                FirstName = userDetailsDTO.FirstName,
                LastName = userDetailsDTO.LastName,
                Email = userDetailsDTO.Email,
                Phone = userDetailsDTO.Phone,
                Bio = userDetailsDTO.Bio
            };

            var result = await _userRepo.UpdateAsync(x => x.Id == userEntity.Id, userEntity);
            if (result != null)
            {
                return true;
            }
        }
        catch(Exception ex) { LogError(ex.Message); }

        return false;
    }

    /// <summary>
    /// Sends classname and error message to ErrorLogger in Helper
    /// </summary>
    /// <param name="errorMessage">String value for logging of error</param>
    private void LogError(string errorMessage)
    {
        string className = this.ToString() ?? "Unknown class";
        _errorLogger.Logger(className, errorMessage);
    }
}
