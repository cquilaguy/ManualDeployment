using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeploymentManualRazor.Models
{
    public class ChangeService : PageModel
    {
        [BindProperty]
        public string Nombre { get; set; }

        [BindProperty]
        public string Email { get; set; }

        public void OnGet()
        {
            // Recupera los datos del TempData y asígnalos a las propiedades del ViewModel.
            Nombre = TempData["Nombre"] as string;
            Email = TempData["Email"] as string;

    }





    }
}
