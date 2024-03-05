
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeploymentManualRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Repository.Entities;


namespace DeploymentManualRazor.Pages
{
    
    public class UserModel : PageModel

    {
        private readonly UserViewModel _userViewModel;
        
        public List<User> Users => _userViewModel.User;

        public UserModel(UserViewModel userViewModel)
        {
            _userViewModel = userViewModel;
        }

        public void OnGet()
        {
            _userViewModel.GetUsers();
        }
        
    }
}
