using Business.DTOs;
using Helper;
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Business.Services;

public class AddressService(ErrorLogger errorLogger, AddressRepo addressRepo)
{
    private readonly ErrorLogger _errorLogger = errorLogger;
    private readonly AddressRepo _addressRepo = addressRepo;

    public async Task<UserAddressDTO> GetUserAddressAsync(string userAddressId)
    {
        if (userAddressId != null)
        {
            try
            {
                var address = await _addressRepo.GetOneAsync(x => x.Id.ToString() == userAddressId);

                UserAddressDTO userAddress = new UserAddressDTO
                {
                    Id = address.Id,
                    Address1 = address.Address1,
                    Address2 = address.Address2,
                    PostalCode = address.PostalCode,
                    City = address.City
                };

                return userAddress;
            }
            catch(Exception ex) { LogError(ex.Message); }
        }

        return new UserAddressDTO();
    }

    public async Task<bool> SaveUserAddressAsync(UserAddressDTO userAddress)
    {
        AddressEntity addressEntity = new AddressEntity
        {
            Address1 = userAddress.Address1,
            Address2 = userAddress.Address2,
            PostalCode = userAddress.PostalCode,
            City = userAddress.City
        };

        if (addressEntity.Id == null)
        {  
            try
            {
                var result = await _addressRepo.CreateAsync(addressEntity);
                if (result.StatusCode == StatusCode.OK)
                {
                    return true;
                }
            }
            catch(Exception ex) { LogError(ex.Message); }
        }
        else
        {
            addressEntity.Id = userAddress.Id;
           
            try
            {
                var result = await _addressRepo.UpdateAsync(x => x.Id == addressEntity.Id, addressEntity);
                if (result != null)
                {
                    return true;
                }
            }
            catch(Exception ex) { LogError(ex.Message); }
        }

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
