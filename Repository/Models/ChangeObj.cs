using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;

public class ChangeObj
{
    public int ChangeID { get; set; }
    public string ChangeDescription { get; set; }
    public string ChangeNumber { get; set; }
    public int CheckList { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
    public DateTime ApplicationDate { get; set; }
    public DateTime DeploymentDate { get; set; }
    public int Version { get; set; }
    public int IsTemplate { get; set; }
    public string Observations { get; set; }
    public int UserID { get; set; }
    public int StatusID { get; set; }
    public int EnvironmentID { get; set; }
    public int RequestTypeID { get; set; }
    public int TypologyID { get; set; }
    public List<SignatureDTO> Firmas { get; set; }
    public List<TrainingDTO> Capacitaciones { get; set; }
    public List<ContactDTO> InfoContactos { get; set; }
    public List<PrerequisiteDTO> Prerrequisitos { get; set; }
    public List<PlanDTO> PlanImplementacion { get; set; }
    public List<PostImplantationDTO> PostImplantaciones { get; set; }
    public List<FunctionalUserDTO> UsuariosFuncionales { get; set; }
    
    public List<ChangeApplicativeDTO> Aplicativos { get; set; }



    public ChangeObj()
    {
        Firmas = new List<SignatureDTO>();
        Capacitaciones = new List<TrainingDTO>();
        InfoContactos = new List<ContactDTO>();
        Prerrequisitos = new List<PrerequisiteDTO>();
        PlanImplementacion = new List<PlanDTO>();
        PostImplantaciones = new List<PostImplantationDTO>();
        UsuariosFuncionales = new List<FunctionalUserDTO>();
        Aplicativos = new List<ChangeApplicativeDTO>();

    }
}

