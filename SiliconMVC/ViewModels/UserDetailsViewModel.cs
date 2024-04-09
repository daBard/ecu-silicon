using SiliconMVC.Models;

namespace SiliconMVC.ViewModels;

public class UserDetailsViewModel()
{
    public UserBasicInfoModel BasicInfo = new UserBasicInfoModel() { 
        FirstName = "Kalle",
        LastName = "Anka",
        Email = "karl.ankselius@example.com"
    };

    public UserAddressModel Address = new UserAddressModel();
}
