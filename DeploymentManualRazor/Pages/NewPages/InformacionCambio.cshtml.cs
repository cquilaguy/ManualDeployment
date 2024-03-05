using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Entities;
using Environment = System.Environment;

namespace DeploymentManualRazor.Pages.NewPages
{
    public class InformacionCambioModel : PageModel
    {
        public IEnumerable<User> Users { get; set; }
        public int ServerID { get; set; }
        public int EnvironmentApplicativeID { get; set; }
        public IEnumerable<Server> Servers { get; set; }
        public IEnumerable<Blueprint> Blueprints { get; set; }
        //public IEnumerable<Rol> Rols { get; set; }
        private readonly ManualDeploymentContext _context;
        //private readonly ILogger<Pdc> _logger;
        [BindProperty]
        public Persona Persona { get; set; }
        private int currentRowIndex;
        public void OnGet()
        {
        }
    }
}
