using Business.DTOs;
using Helper;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public class UserService(ErrorLogger errorLogger, UserManager<UserEntity> userManager)
{ 
    private readonly ErrorLogger _errorLogger = errorLogger;
    private readonly UserManager<UserEntity> _userManager = userManager;

    public async Task<UserDetailsDTO> GetUserDetailsAsync(string userId)
    {
        try
        {
            var userEntity = await _userManager.FindByIdAsync(userId);
            if (userEntity != null) 
            {
                UserDetailsDTO userDetailsDTO = new UserDetailsDTO
                {
                    UserId = userEntity.Id,
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
            //var user = await _userManager.GetUserAsync();
            UserEntity userEntity = new UserEntity
            {
                Id = userDetailsDTO.UserId,
                FirstName = userDetailsDTO.FirstName,
                LastName = userDetailsDTO.LastName,
                Email = userDetailsDTO.Email,
                Phone = userDetailsDTO.Phone,
                Bio = userDetailsDTO.Bio
            };

            var result = await _userManager.UpdateAsync(userEntity);
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
