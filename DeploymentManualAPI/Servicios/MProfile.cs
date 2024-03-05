using AutoMapper;
using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    public class MProfile : AutoMapper.Profile
    {
        //Creación de mapeo entre las entidades y los DTO(JSON) para los casos necesarios
        public MProfile(){
            CreateMap<Change, ChangeDTO>().ReverseMap();
            CreateMap<Signature, SignatureDTO>().ReverseMap();
            CreateMap<Training, TrainingDTO>().ReverseMap();
            CreateMap<Contact, ContactDTO>().ReverseMap();
            CreateMap<Server, ServerDTO>().ReverseMap();
            CreateMap<Prerequisite, PrerequisiteDTO>().ReverseMap();
            CreateMap<Plan, PlanDTO>().ReverseMap();
            CreateMap<Postimplantation, PostImplantationDTO>().ReverseMap();
            CreateMap<FunctionalUser, FunctionalUserDTO>().ReverseMap();
            CreateMap<RollbackPre, RollbackPreDTO>().ReverseMap();
            CreateMap<RollbackPlan, RollbackPlanDTO>().ReverseMap();
            CreateMap<Blueprint, BlueprintDTO>().ReverseMap();
            CreateMap<ChangeApplicative, ChangeApplicativeDTO>().ReverseMap();


        }
        
    }
}
