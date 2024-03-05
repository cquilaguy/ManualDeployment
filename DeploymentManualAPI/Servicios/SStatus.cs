using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    public class SStatus:ISStatus
    {
        private readonly ManualDeploymentContext _context;
        private readonly ILogger<SStatus> _logger;

        public SStatus(ManualDeploymentContext context, ILogger<SStatus> logger)
        {
            _context = context;
            _logger = logger;

        }
        public async Task<bool> StatusExists(int id)
        {
            bool existe = false;
            try
            {
                //AnyAsync busca un registro en la base de datos teniendo en cuenta una condición
                existe = await _context.Status.AnyAsync(x => x.StatusID == id);
                return existe;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al verificar si existe un cambio con id" + id, ex);
                throw ex;
            }
        }
    }
}
