using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    //Hereda de la interfaz ISChange para así implementar los metodo necesarios
    public class SChange : ISChange
    {

        private readonly ManualDeploymentContext _context;
        private readonly IMapper _Mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ChangeObj _changeObj;
        private readonly FilterObj _filterObj;
        private readonly ILogger<SChange> _logger;
        private readonly ISSignature _SignatureRepository;
        private readonly ISContact _ContactRepository;
        private readonly ISTraining _TrainingRepository;
        private readonly ISPrerequisite _PrerequisiteRepository;
        private readonly ISPlan _PlanRepository;
        private readonly ISPostImplantation _PostImplantationRepository;
        private readonly ISFunctionalUser _FunctionalUserRepository;
        private readonly ISChangeApplicative _ChangeApplicativeRepository;



        public SChange(ManualDeploymentContext context, IMapper mapper, IServiceScopeFactory serviceScopeFactory, ChangeObj changeObj,
            FilterObj filterObj, ILogger<SChange> logger, ISSignature signatureRepository, ISContact contactRepository, ISTraining trainingRepository,
            ISPrerequisite prerequisiteRepository, ISPlan planRepository, ISPostImplantation postImplantationRepository, ISFunctionalUser functionalUserRepository,
            ISChangeApplicative changeApplicativeRepository)
        {
            _context = context;
            _Mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
            _changeObj = changeObj;
            _filterObj = filterObj;
            _logger = logger;
            _SignatureRepository = signatureRepository;
            _ContactRepository = contactRepository;
            _TrainingRepository = trainingRepository;
            _PrerequisiteRepository = prerequisiteRepository;
            _PlanRepository = planRepository;
            _PostImplantationRepository = postImplantationRepository;
            _FunctionalUserRepository = functionalUserRepository;
            _ChangeApplicativeRepository = changeApplicativeRepository;


        }
        /// <summary>
        /// Metodo que permite obtener un cambio en especifico teniendo en cuenta el ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un JSON con la información del cambio en especifico</returns>
        public async Task<ChangeDTO> GetChange(int id)
        {
            //FindAsync permite buscar en la base de datos un registro teniendo en un cuenta un id en especifico
            try
            {
                //FindAsync permite buscar en la base de datos un registro teniendo en un cuenta un id en especifico
                Change data = await _context.Change.FindAsync(id);
                //Mapea de una entidad a un JSON
                ChangeDTO response = _Mapper.Map<ChangeDTO>(data);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener el cambio con el id" + id, ex);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite obtener un cambio en especifico teniendo en cuenta el changeNumber
        /// </summary>
        /// <param name="changeNumber"></param>
        /// <returns>Retorna un JSON con la información del cambio en especifico</returns>
        public async Task<ChangeObj> GetChangeS(string changeNumber)
        {
            try
            {
                var data = await _context.Change.FirstOrDefaultAsync(s => s.ChangeNumber == changeNumber);

                if (data == null)
                {
                    return null;
                }
                _changeObj.ChangeID = data.ChangeID;
                _changeObj.ChangeDescription = data.ChangeDescription;
                _changeObj.ChangeNumber = data.ChangeNumber;
                _changeObj.CheckList = data.CheckList;
                _changeObj.StartDate = data.StartDate;
                _changeObj.EndDate = data.EndDate;
                _changeObj.CreationDate = data.ModificationDate;
                _changeObj.ApplicationDate = data.ApplicationDate;
                _changeObj.Version = data.Version;
                _changeObj.IsTemplate = data.IsTemplate;
                _changeObj.Observations = data.Observations;
                _changeObj.UserID = data.UserID;
                _changeObj.StatusID = data.StatusID;
                _changeObj.EnvironmentID = data.EnvironmentID;
                _changeObj.RequestTypeID = data.RequestTypeID;
                _changeObj.TypologyID = data.TypologyID;
                _changeObj.Firmas = (await _SignatureRepository.GetSignature(data.ChangeID)).ToList();
                _changeObj.Capacitaciones = (await _TrainingRepository.GetTraining(data.ChangeID)).ToList();
                _changeObj.InfoContactos = (await _ContactRepository.GetContact(data.ChangeID)).ToList();
                _changeObj.Prerrequisitos = (await _PrerequisiteRepository.GetPrerequisite(data.ChangeID)).ToList();
                _changeObj.PlanImplementacion = (await _PlanRepository.GetPlan(data.ChangeID)).ToList();
                _changeObj.PostImplantaciones = (await _PostImplantationRepository.GetPostImplantation(data.ChangeID)).ToList();
                _changeObj.UsuariosFuncionales = (await _FunctionalUserRepository.GetFunctionalUser(data.ChangeID)).ToList();
                _changeObj.Aplicativos = (await _ChangeApplicativeRepository.GetChangeApplicative(data.ChangeID)).ToList();


                return _changeObj;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener toda los cambios con ChangeNumber: " + changeNumber);
                throw ex;
            }
        }
        /// <summary>
        ///Metodo que permite verificar si un cambio existe en la base de datos teniendo en cuenta su ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<bool> ChangeExists(int id)
        {
            bool existe = false;
            try
            {
                //AnyAsync busca un registro en la base de datos teniendo en cuenta una condición
                existe = await _context.Change.AnyAsync(x => x.ChangeID == id);
                return existe;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al verificar si existe un cambio con id" + id, ex);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite ingresar un cambio
        /// </summary>
        /// <param name="change"></param>
        /// <returns>Retorna una JSON con el cambio registrado </returns>
        public async Task<Change> PostChange(ChangeDTO change)
        {
            try
            {
                Change data = _Mapper.Map<Change>(change);
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
                _logger.LogError(ex, "Error al guardar el registro de cambio - Data: {@Change}", change);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un cambio
        /// </summary>
        /// <param name="change"></param>
        /// <returns>Retorna un JSON con el cambio actualizado</returns>
        public async Task<Change> PutChange(ChangeDTO change)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<ManualDeploymentContext>();

                    // Buscar el registro específico por su ChangeID
                    Change specificChange = await dbContext.Change
                        .FirstOrDefaultAsync(s => s.ChangeID == change.ChangeID);

                    if (specificChange == null)
                    {
                        // Manejar el caso en que no se encontró el registro con el ChangeID proporcionado
                        throw new InvalidOperationException($"No se encontró el registro con ChangeID {change.ChangeID}");
                    }

                    // Mapear las propiedades actualizadas del DTO al registro existente
                    _Mapper.Map(change, specificChange);

                    // Guardar los cambios en la base de datos
                    await dbContext.SaveChangesAsync();

                    return specificChange;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de cambio - Data: {@Change}", change);
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que confirma que los cambios fueron realizados
        /// </summary>
        /// <returns>Retorna un booleano que indica si el cambio se guardó o no</returns>
        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                int resp = await _context.SaveChangesAsync();
                return (resp >= 0);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al confirmar el cambio guardado", ex);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que retorna los cambios teniendo un filtro
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<FilterObj> GetFilteredChanges(FilterDTO filter)
        {
            try
            {
                // Verificar si al menos un parámetro tiene datos
                if (filter == null)
                {
                    return new FilterObj(); // Devolver objeto vacío si no hay filtros aplicados
                }

                // Construir la consulta LINQ
                var change = await (from c in _context.Change
                                    join e in _context.Environment on c.EnvironmentID equals e.EnvironmentID
                                    join ca in _context.ChangeApplicative on c.ChangeID equals ca.ChangeID
                                    join s in _context.Signature on c.ChangeID equals s.ChangeID
                                    where
                                        1 == 1 &&// Añadir filtro 1=1
                                        (filter.ChangeNumber == null || c.ChangeNumber == filter.ChangeNumber) ||
                                        (filter.EnvironmentID == null || c.EnvironmentID == filter.EnvironmentID) ||
                                        (filter.ApplicativeID == null || _context.ChangeApplicative.Any(x => x.ChangeID == c.ChangeID && x.ApplicativeID == filter.ApplicativeID)) ||
                                        (filter.StartDate == null || c.StartDate >= filter.StartDate) ||
                                        (filter.EndDate == null || c.EndDate <= filter.EndDate) ||
                                        (filter.UserID == null || _context.Signature.Any(x => x.ChangeID == c.ChangeID && x.UserID == filter.UserID)) ||
                                        (filter.StatusID == null || c.StatusID == filter.StatusID)
                                        
                                    select new FilterObj
                                    {
                                        ChangeNumber = c.ChangeNumber,
                                        EnvironmentID = e.EnvironmentID,
                                        ApplicativeIDs = _context.ChangeApplicative
                                                             .Where(x => x.ChangeID == c.ChangeID)
                                                             .Select(x => x.ApplicativeID)
                                                             .ToList(),
                                        StartDate = c.StartDate,
                                        EndDate = c.EndDate,
                                        UserIDs = _context.Signature
                                                      .Where(x => x.ChangeID == c.ChangeID)
                                                      .Select(x => x.UserID)
                                                      .ToList(),
                                        StatusID = c.StatusID
                                    }).FirstOrDefaultAsync();

                if (change != null)
                {
                    _filterObj.ChangeNumber = change.ChangeNumber;
                    _filterObj.EnvironmentID = change.EnvironmentID;
                    _filterObj.ApplicativeIDs = change.ApplicativeIDs;
                    _filterObj.StartDate = change.StartDate;
                    _filterObj.EndDate = change.EndDate;
                    _filterObj.UserIDs = change.UserIDs;
                    _filterObj.StatusID = change.StatusID;
                    return _filterObj;
                }
                else {
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción si es necesario
                throw;
            }
        }


    }


}

