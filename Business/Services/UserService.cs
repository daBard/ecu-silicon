using Business.DTOs;
using Helper;
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace Business.Services;

public class UserService
{
    private readonly ErrorLogger _errorLogger;
    private readonly UserRepo _userRepo;
    private readonly AuthRepo _authRepo;

    public UserService(ErrorLogger errorLogger, UserRepo userRepo, AuthRepo authRepo)
    {
        _errorLogger = errorLogger;
        _userRepo = userRepo;
        _authRepo = authRepo;
    }

    public async Task<bool> CreateUserAsync(UserCreateDTO newUser)
    {
        try
        {
            GenerateSecurePassword(newUser.Password, out string password, out string securityKey);

            UserEntity userEntity = new UserEntity();

            userEntity.Id = newUser.Id.ToString();
            userEntity.FirstName = newUser.FirstName;
            userEntity.LastName = newUser.LastName;
            userEntity.Email = newUser.Email;

            userEntity.Auth = new AuthEntity();

            userEntity.Auth.Password = password;
            userEntity.Auth.SecurityKey = securityKey;

            userEntity.RegistrationDate = DateTime.Now;

            var result = await _userRepo.CreateAsync(userEntity);
            if (result.StatusCode == StatusCode.OK)
            {
                return true;
            }
        }
        catch(Exception ex) { LogError(ex.Message); }
        return false;
    }

    public async Task<bool> CheckPassword(UserAuthDTO userAuth)
    {
        var user = await _userRepo.GetOneAsync(x => x.Email == userAuth.Email);

        var storedAuth = await _authRepo.ExistsAsync(x => x.Id == user.AuthId);

        var result = ValidateSecurePassword(userAuth.Password, storedAuth.Password, storedAuth.SecurityKey);
        
        return result;
    }


    private void GenerateSecurePassword(string password, out string generatedPassword, out string securityKey)
    {
        using var hmac = new HMACSHA256();
        securityKey = Convert.ToBase64String(hmac.Key);
        generatedPassword = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }

    public bool ValidateSecurePassword(string password, string storedPassword, string securityKey)
    {
        try
        {
            using var hmac = new HMACSHA256(Convert.FromBase64String(securityKey));
            var newHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var storedHash = Convert.FromBase64String(storedPassword);

            for(var i=0; i<newHash.Length; i++)
            {
                if (newHash[i] != storedHash[i])
                    return false;
            }
            return true;
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
