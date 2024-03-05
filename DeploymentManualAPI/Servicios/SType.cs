using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    public class SType:ISType
    {
        private readonly ManualDeploymentContext _context;
        private readonly IMapper _Mapper;
        public SType(ManualDeploymentContext context, IMapper mapper)
        {
            _context = context;
            _Mapper = mapper;

        }

        public async Task<IEnumerable<Repository.Entities.Type>> GetTypes()
        {
            try
            {
                IEnumerable<Repository.Entities.Type> data = await _context.Type.ToListAsync();
                IEnumerable<Repository.Entities.Type> response = _Mapper.Map<IEnumerable<Repository.Entities.Type>>(data);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> TypeExists(int id)
        {
            bool existe = false;
            try
            {
                existe = await _context.Type.AnyAsync(x => x.TypeID == id);
                return existe;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
