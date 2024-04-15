using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiliconMVC.Models;
using SiliconMVC.ViewModels;
using System.Security.Claims;

namespace SiliconMVC.Controllers
{
    //ViewData - I en view
    //TempData - Över flera views

    [Authorize]
    public class UserController(UserManager<UserEntity> userManager, DataContext dataContext) : Controller
    {
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly DataContext _dataContext = dataContext;

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            ViewData["Title"] = "User details";

            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var user = await _dataContext.Users.Include(u => u.Address).SingleOrDefaultAsync(x => x.Id == nameIdentifier);

            var viewModel = new UserDetailsViewModel
            {
                //ANVÄNDER INJECTION I STÄLLET
                //ProfileInfo = new UserProfileInfoModel
                //{
                //    ProfileImg = user!.ProfileImg,
                //    FirstName = user!.FirstName,
                //    LastName = user!.LastName,
                //    Email = user.Email!
                //},
                BasicInfo = new UserBasicInfoModel
                {
                    FirstName = user!.FirstName,
                    LastName = user!.LastName,
                    Email = user.Email!,
                    Phone = user.PhoneNumber,
                    Bio = user.Bio,
                },
                Address = new UserAddressModel
                {
                    AddressLine1 = user.Address?.Address1, //?? "DENNA VAR NULL"
                    AddressLine2 = user.Address?.Address2,
                    PostalCode = user.Address?.PostalCode,
                    City = user.Address?.City
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DetailsSaveBasicInfo(UserDetailsViewModel model)
        {
            ViewData["Title"] = "User details";

            if (TryValidateModel(model.BasicInfo!))
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    user.FirstName = model.BasicInfo!.FirstName;
                    user.LastName = model.BasicInfo!.LastName;
                    user.Email = model.BasicInfo!.Email;
                    user.Phone = model.BasicInfo.Phone;
                    user.Bio = model.BasicInfo.Bio;
                    user.UserName = model.BasicInfo.Email;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        TempData["StatusMessage"] = "Basic info saved successfully!";
                    else
                        TempData["StatusMessage"] = "Error - Basic info not saved!";
                }
                
            }
            else
            {
                TempData["StatusMessage"] = "Error - Basic info not saved!";
            }
            
            return RedirectToAction("Details", "User");
        }

        [HttpPost]
        public async Task<IActionResult> DetailsSaveAddress(UserDetailsViewModel model)
        {
            ViewData["Title"] = "User details";

            try
            {
                if (TryValidateModel(model.Address!))
                {
                    var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                    var user = await _dataContext.Users.Include(u => u.Address).SingleOrDefaultAsync(x => x.Id == nameIdentifier);

                    if (user!.Address != null)
                    {
                        user.Address.Address1 = model.Address!.AddressLine1;
                        user.Address.Address2 = model.Address!.AddressLine2;
                        user.Address.PostalCode = model.Address!.PostalCode;
                        user.Address.City = model.Address!.City;

                    }
                    else
                    {
                        user.Address = new AddressEntity
                        {
                            Address1 = model.Address!.AddressLine1,
                            Address2 = model.Address!.AddressLine2,
                            PostalCode = model.Address!.PostalCode,
                            City = model.Address!.City
                        };
                    }

                    _dataContext.SaveChanges();
                    var result = await _dataContext.SaveChangesAsync();

                    if (result >= 1)
                        TempData["StatusMessage"] = "Address saved successfully!";
                }
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                TempData["StatusMessage"] = "Error - Address not saved!";
            }
            
            return RedirectToAction("Details", "User");
        }
    

        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null && file != null && file.Length != 0)
                {
                    var fileName = $"{user.Id}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img/uploads/profiles", fileName);

                    using var fs = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(fs);

                    user.ProfileImg = fileName;
                    await _userManager.UpdateAsync(user);
                }
                else
                {
                    TempData["StatusMessage"] = "Error - Profile image not uploaded!";
                }
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }

            return RedirectToAction("Details", "User");
        }
    }
}