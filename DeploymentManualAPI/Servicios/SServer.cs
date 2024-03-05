using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    //Hereda de la interfaz ISServer para así implementar los metodo necesarios
    public class SServer:ISServer
    {
        //Llamado al contexto para poder usar las entidades gracias a Entity Framework Core
        private readonly ManualDeploymentContext _context;
        private readonly IMapper _Mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<SServer> _logger;


        public SServer(ManualDeploymentContext context, IMapper mapper, IServiceScopeFactory serviceScopeFactory, ILogger<SServer> logger)
        {
            _context = context;
            _Mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;

        }
        /// <summary>
        /// Metodo que permite obtener los servidores teniendo en cuenta un EnvironmentID
        /// </summary>
        /// <param name="EnvironmentID"></param>
        /// <returns>Retorna un JSON con los servidores en especifico</returns>
        public async Task<IEnumerable<ServerDTO>> GetServer(int EnvironmentID)
        {
            try
            {
                //ToListAsync convierte todos los objetos de la entidad en una lista
                var data = await _context.Server.Where(s => s.EnvironmentID == EnvironmentID).ToListAsync();
                //Mapea de una entidad a un JSON
                IEnumerable<ServerDTO> response = _Mapper.Map<IEnumerable<ServerDTO>>(data);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los servidores con EnvironmentID: " + EnvironmentID);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite verificar si un servidor existe teniendo en cuenta su ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un booleano que indica si el servidor existe o no</returns>
        public async Task<bool> ServerExists(int id)
        {
            bool existe = false;
            try
            {
                //AnyAsync busca un registro en la base de datos teniendo en cuenta una condición
                existe = await _context.Server.AnyAsync(x => x.ServerID == id);
                return existe;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al verificar si existe un servidor con id" + id, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que permite ingresar un servidor
        /// </summary>
        /// <param name="server"></param>
        /// <returns>Retorna una JSON con el servidor registrado</returns>
        public async Task<Server> PostServer(ServerDTO server)
        {
            try
            {
                Server data = _Mapper.Map<Server>(server);
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<ManualDeploymentContext>();
                    //Add ingresa la información en la entidad correspondiente
                    dbContext.Add(data);
                    await dbContext.SaveChangesAsync();
                }
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el registro de servidor - Data: {@Server}", server);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un servidor
        /// </summary>
        /// <param name="server"></param>
        /// <returns>Retorna un JSON con el servidor actualizado</returns>
        public async Task<Server> PutServer(ServerDTO server)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<ManualDeploymentContext>();

                    // Buscar el registro específico por su EnvironmentID y ServerID
                    Server specificServer = await dbContext.Server
                        .FirstOrDefaultAsync(s => s.EnvironmentID == server.EnvironmentID && s.ServerID == server.ServerID);

                    if (specificServer == null)
                    {
                        // Manejar el caso en que no se encontró el registro con el EnvironmentID y ServerID proporcionados
                        throw new InvalidOperationException($"No se encontró el registro con EnvironmentID {server.EnvironmentID} y ServerID {server.ServerID}");
                    }

                    // Mapear las propiedades actualizadas del DTO al registro existente
                    _Mapper.Map(server, specificServer);

                    // Guardar los cambios en la base de datos
                    await dbContext.SaveChangesAsync();

                    return specificServer;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de servidor - Data: {@Server}", server);
                throw ex;
            }
        }
    }
}
