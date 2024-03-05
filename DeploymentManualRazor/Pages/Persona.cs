using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace DeploymentManualRazor.Pages
{
    public class Persona
    {
        //INFORMACION CAMBIO
        public string requesType { get; set; }
        public string changeNumber { get; set; }
        public string changeType { get; set; }
        public string changeDescrip { get; set; }
        public string changeOwner { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string changeDeveloper { get; set; }
        //FIRMAS DE ACEPTACION
        public string signatureRolMain { get; set; }
        public string signatureNameMain { get; set; }
        public string signatureAreaMain { get; set; }
        public string signatureEmailMain { get; set; }
        public string signatureObserMain { get; set; }
        public string signatureRol0 { get; set; }
        public string signatureName0 { get; set; }
        public string signatureArea0 { get; set; }
        public string signatureEmail0 { get; set; }
        public string signatureObser0 { get; set; }
        public string signatureRol1 { get; set; }
        public string signatureName1 { get; set; }
        public string signatureArea1 { get; set; }
        public string signatureEmail1 { get; set; }
        public string signatureObser1 { get; set; }
        public string signatureRol2 { get; set; }
        public string signatureName2 { get; set; }
        public string signatureArea2 { get; set; }
        public string signatureEmail2 { get; set; }
        public string signatureObser2 { get; set; }
        public string signatureRol3 { get; set; }
        public string signatureName3 { get; set; }
        public string signatureArea3 { get; set; }
        public string signatureEmail4 { get; set; }
        public string signatureObser5 { get; set; }



        //FORMATO CAPACITACION
        public string startDateTraining { get; set; }
        public string developerNameMain { get; set; }
        public string dataTrainingMain { get; set; }
        public string comments { get; set; }
        public string type { get; set; }
        public string objetive { get; set; }
        public string issues { get; set; }



        //INFORMACION GENERAL DE CONTACTO



        public string contactRolMain { get; set; }
        public string contactNameMain { get; set; }
        public string contactPhoneMain { get; set; }
        public string contactEmailMain { get; set; }
        public string contactObserMain { get; set; }
        public string contactRolDev { get; set; }
        public string contactNameDev { get; set; }
        public string contactPhoneDev { get; set; }
        public string contactEmailDev { get; set; }
        public string contactObserDev { get; set; }



        //PRERQUISITOS
        public string prereSecuMain { get; set; }
        public string prereDescripMain { get; set; }
        public string startDatePre { get; set; }
        public string endDatePre { get; set; }
        public string timeExPre { get; set; }
        public string responsiblePre { get; set; }
        public string areaPre { get; set; }



        //PLAN DE IMPLEMENTACION
        public string planSecuMain { get; set; }
        public string planDescripMain { get; set; }
        public string startDatePlan { get; set; }
        public string endDatePlan { get; set; }
        public string timeEx { get; set; }
        public string responsiblePlan { get; set; }
        public string areaPlan { get; set; }
        //-------------------
        public string planSecu0 { get; set; }
        public string planDescrip0 { get; set; }
        public string startDatePlan0 { get; set; }
        public string endDatePlan0 { get; set; }
        public string timeEx0 { get; set; }
        public string responsiblePlan0 { get; set; }
        public string areaPlan0 { get; set; }
        //-------------------
        public string planSecu1 { get; set; }
        public string planDescrip1 { get; set; }
        public string startDatePlan1 { get; set; }
        public string endDatePlan1 { get; set; }
        public string timeEx1 { get; set; }
        public string responsiblePlan1 { get; set; }
        public string areaPlan1 { get; set; }
        //-------------------
        public string planSecu2 { get; set; }
        public string planDescrip2 { get; set; }
        public string startDatePlan2 { get; set; }
        public string endDatePlan2 { get; set; }
        public string timeEx2 { get; set; }
        public string responsiblePlan2 { get; set; }
        public string areaPlan2 { get; set; }
        //-------------------
        public string planSecu3 { get; set; }
        public string planDescrip3 { get; set; }
        public string startDatePlan3 { get; set; }
        public string endDatePlan3 { get; set; }
        public string timeEx3 { get; set; }
        public string responsiblePlan3 { get; set; }
        public string areaPlan3 { get; set; }
        //-------------------
        public string planSecu4 { get; set; }
        public string planDescrip4 { get; set; }
        public string startDatePlan4 { get; set; }
        public string endDatePlan4 { get; set; }
        public string timeEx4 { get; set; }
        public string responsiblePlan4 { get; set; }
        public string areaPlan4 { get; set; }



        //PRE POSTIMPLANTACION
        public string prerePostSecuMain { get; set; }
        public string prePostDescripMain { get; set; }
        public string prerePostAreaMain { get; set; }
        public string prerePostNameMain { get; set; }
        public string startDatePrePost { get; set; }
        public string endDatePrePost { get; set; }



        //DETALLES USUARION FUNCIONALES
        public string usuSecuMain { get; set; }
        public string usuFuncionarioMain { get; set; }
        public string usuAreaMain { get; set; }
        public string usuIngresoMain { get; set; }
        public string usuPhoneMain { get; set; }
        public string startDateUsu { get; set; }
        public string endDateUsu { get; set; }
        //blue y roll back
        public string rollBack { get; set; }
        public string versionBlue { get; set; }
        public string dateBlue { get; set; }
        public string observaciones { get; set; }
    }
}