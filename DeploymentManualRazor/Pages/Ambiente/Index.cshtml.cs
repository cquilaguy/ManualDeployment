using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Environment = Repository.Entities.Environment;
using DataTables.AspNet;

namespace DeploymentManualRazor.Pages.Ambiente
{
    public class IndexModel : PageModel
    {

        private readonly ManualDeploymentContext _context;

        public IndexModel(ManualDeploymentContext context)
        {
            _context = context;
        }

        public IEnumerable<Environment> Environments { get; set; }

        public IEnumerable<Applicative> Applicatives { get; set; }

        public IEnumerable<Change> Changes { get; set; }
        public int ChangeId { get; set; }

        public int ServerID { get; set; }
        public async Task OnGet()
        {
            Environments = await _context.Environment.ToListAsync();
            Applicatives = await _context.Applicative.ToListAsync();
            Changes = await _context.Change.ToListAsync();
        }


    }
}
