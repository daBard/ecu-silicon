using SiliconMVC.Models;

namespace SiliconMVC.ViewModels;

public class UserDetailsViewModel()
{
    public UserBasicInfoModel? BasicInfo { get; set; }

    public UserAddressModel? Address { get; set; }
}
