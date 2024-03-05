using DeploymentManualRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Entities;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using Environment = System.Environment;
using Environmen = Repository.Entities.Environment;

namespace DeploymentManualRazor.Pages
{
    public class ConsulExcelModel : PageModel
    {
        

        private readonly ManualDeploymentContext _dbContext;


        private int currentRowIndex;



        public List<Environmen> Environments { get; set; }
        public List<Blueprint> Blueprints { get; set; }
        public List<Applicative> Applicatives { get; set; }
        public List<int> ChangeIDs { get; set; }
        public List<Change> Changes { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Signature> Signatures { get; set; }
        public List<Training> Trainings { get; set; }
        public List<Prerequisite> Prerequisites { get; set; }
        public List<Plan> Plans { get; set;}
        public List<Postimplantation> Postimplantations { get; set;}
        public List<FunctionalUser> FunctionalUsers { get; set; }


        [BindProperty(SupportsGet = true)]
        public int ChangeId { get; set; }

        public ConsulExcelModel(ManualDeploymentContext dbContext)
        {
            _dbContext = dbContext;
            Changes = _dbContext.Change.ToList();
            Environments = _dbContext.Environment.ToList();
            Applicatives = _dbContext.Applicative.ToList();
            Blueprints = new List<Blueprint>();
            Contacts = new List<Contact>();
            Signatures = new List<Signature>();
            Trainings = new List<Training>();
            Prerequisites = new List<Prerequisite>();
            Plans = new List<Plan>();
            Postimplantations = new List<Postimplantation>();
            FunctionalUsers = new List<FunctionalUser>();

            // Crear una instancia de BlueprintViewModel pasando la lista de cambios
            BlueprintViewModel blueprintViewModel = new BlueprintViewModel(_dbContext);
            blueprintViewModel.Changes = Changes;
            blueprintViewModel.ChangeID = ChangeId;
            blueprintViewModel.GetBlueprints();

            // Asignar los blueprints obtenidos al modelo
            Blueprints = blueprintViewModel.Blueprints;
        }



        public IActionResult OnGetDownloadTemplate(int changeId)
        {

            byte[] fileContents = GenerateExcelBytes(changeId);

            return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PlantillaExcel.xlsx");
        }

        private byte[] GenerateExcelBytes(int changeId)
        {
            ChangeViewModel viewModel = new ChangeViewModel(_dbContext);
            viewModel.ChangeID = changeId;
            viewModel.GetChange();

            BlueprintViewModel viewModel1 = new BlueprintViewModel(_dbContext);
            viewModel1.ChangeID = changeId;
            viewModel1.GetBlueprints();

            ContactViewModel viewModel2 = new ContactViewModel(_dbContext);
            viewModel2.ChangeID = changeId;
            viewModel2.GetContact();

            SignatureViewModel viewModel3 = new SignatureViewModel(_dbContext);
            viewModel3.ChangeID = changeId;
            viewModel3.GetSignature();

            TrainingViewModel viewModel4 = new TrainingViewModel(_dbContext);
            viewModel4.ChangeID = changeId;
            viewModel4.GetTraining();

            PrerrequisitoViewModel viewModel5 = new PrerrequisitoViewModel(_dbContext);
            viewModel5.ChangeID = changeId;
            viewModel5.GetPrerrequisito();

            PlanViewModel viewModel6 = new PlanViewModel(_dbContext);
            viewModel6.ChangeID = changeId;
            viewModel6.GetPlan();

            PosinViewModel viewModel7 = new PosinViewModel(_dbContext);
            viewModel7.ChangeID = changeId;
            viewModel7.GetPosin();

            FuncionalViewModel viewModel8 = new FuncionalViewModel(_dbContext);
            viewModel8.ChangeID = changeId;
            viewModel8.GetFuncional();

            List<Change> changes = viewModel.Changes;
            List<User> users = _dbContext.User.ToList();
            List<Blueprint> blueprints = viewModel1.Blueprints;
            List<Contact> contacts = viewModel2.Contacts;
            List<Signature> signatures = viewModel3.Signatures;
            List<Training> trainings = viewModel4.Trainings;
            //List<Prerrequisitos> prerrequisitoss = viewModel5.Prerrequisitoss;
            List<Plan> plans = viewModel6.Planss;
            List<Postimplantation> postimplantacions = viewModel7.Postimplantations;
            List<FunctionalUser> functionalUsers = viewModel8.FunctionalUsers;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {




                var worksheet = package.Workbook.Worksheets.Add("Datos");


                // Establecer el ancho de columna para las columnas A a M
                for (int col = 1; col <= 10; col++)
                {
                    worksheet.Column(col).Width = 15;
                }



                // Insertar la imagen en la celda combinada ["A1:C3"]
                var imageCell = worksheet.Cells["A1:C3"];
                


                // Cambiar el tamaño de la celda combinada "A1:C3"
                imageCell.Merge = true;
                imageCell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                imageCell.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                imageCell.Style.WrapText = true;
                imageCell.Style.Font.Bold = true;
                imageCell.Style.Font.Size = 30;

                worksheet.Cells["A4:H4"].Merge = true;
                worksheet.Cells["A4:H4"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A4:H4"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A4:H4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A4:H4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                worksheet.Cells["A4:H4"].Merge = true;


                // Establecer el contorno negro de las celdas
                var ranges7 = worksheet.Cells["A4:H4"];
                var border7 = ranges7.Style.Border;
                border7.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border7.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border7.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border7.Left.Color.SetColor(System.Drawing.Color.Black);
                border7.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border7.Top.Color.SetColor(System.Drawing.Color.Black);
                border7.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border7.Right.Color.SetColor(System.Drawing.Color.White);

                // Establecer el contorno negro de las celdas
                var ranges9 = worksheet.Cells["A5:H5"];
                var border9 = ranges9.Style.Border;
                border9.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border9.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border9.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border9.Top.Color.SetColor(System.Drawing.Color.Black);
                border9.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border9.Right.Color.SetColor(System.Drawing.Color.White);
                border9.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border9.Left.Color.SetColor(System.Drawing.Color.White);

                worksheet.Cells["D1:H3"].Merge = true;
                worksheet.Cells["D1:H3"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["D1:H3"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["D1:H3"].Value = "MANUAL DE DESPLIEGUE";
                worksheet.Cells["D1:H3"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["D1:H3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["D1:H3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges4 = worksheet.Cells["D1:H3"];
                var border4 = ranges4.Style.Border;
                border4.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border4.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border4.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border4.Left.Color.SetColor(System.Drawing.Color.Black);
                border4.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border4.Top.Color.SetColor(System.Drawing.Color.Black);
                border4.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border4.Right.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["I1:J1"].Merge = true;
                worksheet.Cells["I1:J1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["I1:J1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["I1:J1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                worksheet.Cells["I1:J1"].Merge = true;
                worksheet.Cells["I1:J1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["I1:J1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["I1:J1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["I1:J1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                worksheet.Cells["I1:J1"].Value = "VERSION:0";
                worksheet.Cells["I1:J1"].Style.Font.Color.SetColor(System.Drawing.Color.White);

                // Establecer el contorno negro de las celdas
                var ranges5 = worksheet.Cells["I1:J1"];
                var border5 = ranges5.Style.Border;
                border5.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border5.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border5.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border5.Left.Color.SetColor(System.Drawing.Color.Black);
                border5.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border5.Top.Color.SetColor(System.Drawing.Color.Black);
                border5.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border5.Right.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["I2:J3"].Merge = true;
                worksheet.Cells["I2:J3"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["I2:J3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["I2:J3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                worksheet.Cells["I2:J3"].Merge = true;
                worksheet.Cells["I2:J3"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["I2:J3"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["I2:J3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["I2:J3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                worksheet.Cells["I2:J3"].Value = "CODIGO:";
                worksheet.Cells["I2:J3"].Style.Font.Color.SetColor(System.Drawing.Color.White);

                // Establecer el contorno negro de las celdas
                var ranges6 = worksheet.Cells["I2:J3"];
                var border6 = ranges6.Style.Border;
                border6.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border6.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border6.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border6.Left.Color.SetColor(System.Drawing.Color.Black);
                border6.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border6.Top.Color.SetColor(System.Drawing.Color.Black);
                border6.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border6.Right.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["I4:J4"].Merge = true;
                worksheet.Cells["I4:J4"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["I4:J4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["I4:J4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                worksheet.Cells["I4:J4"].Merge = true;
                worksheet.Cells["I4:J4"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["I4:J4"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["I4:J4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["I4:J4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                worksheet.Cells["I4:J4"].Value = "FECHA:";
                worksheet.Cells["I4:J4"].Style.Font.Color.SetColor(System.Drawing.Color.Black);

                // Establecer el contorno negro de las celdas
                var ranges8 = worksheet.Cells["I4:J4"];
                var border8 = ranges8.Style.Border;
                border8.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border8.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border8.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border8.Left.Color.SetColor(System.Drawing.Color.Black);
                border8.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border8.Top.Color.SetColor(System.Drawing.Color.Black);
                border8.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border8.Right.Color.SetColor(System.Drawing.Color.Black);



                // Establecer el contorno negro de las celdas
                var ranges10 = worksheet.Cells["I5"];
                var border10 = ranges10.Style.Border;
                border10.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border10.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border10.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border10.Left.Color.SetColor(System.Drawing.Color.Black);
                border10.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border10.Top.Color.SetColor(System.Drawing.Color.Black);
                border10.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border10.Right.Color.SetColor(System.Drawing.Color.White);

                // Establecer el contorno negro de las celdas
                var ranges11 = worksheet.Cells["J5"];
                var border11 = ranges11.Style.Border;
                border11.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border11.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border11.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border11.Top.Color.SetColor(System.Drawing.Color.Black);
                border11.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border11.Right.Color.SetColor(System.Drawing.Color.Black);
                border11.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border11.Left.Color.SetColor(System.Drawing.Color.White);

                worksheet.Cells["A6:J6"].Merge = true;
                worksheet.Cells["A6:J6"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A6:J6"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["A6:J6"].Value = "1.INFORMACION CAMBIO";
                worksheet.Cells["A6:J6"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A6:J6"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A6:J6"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges18 = worksheet.Cells["A6:J6"];
                var border18 = ranges18.Style.Border;
                border18.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border18.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border18.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border18.Top.Color.SetColor(System.Drawing.Color.Black);
                border18.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border18.Right.Color.SetColor(System.Drawing.Color.Black);
                border18.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border18.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["A7:B7"].Merge = true;
                worksheet.Cells["A7:B7"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A7:B7"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["A7:B7"].Value = "TIPO DE LA SOLICITUD";
                worksheet.Cells["A7:B7"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A7:B7"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A7:B7"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges19 = worksheet.Cells["A7:B7"];
                var border19 = ranges19.Style.Border;
                border19.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border19.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border19.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border19.Top.Color.SetColor(System.Drawing.Color.Black);
                border19.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border19.Right.Color.SetColor(System.Drawing.Color.Black);
                border19.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border19.Left.Color.SetColor(System.Drawing.Color.White);

                worksheet.Cells["C7:E7"].Merge = true;
                worksheet.Cells["C7:E7"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["C7:E7"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["C7:E7"].Value = "";
                worksheet.Cells["C7:E7"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["C7:E7"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["C7:E7"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges20 = worksheet.Cells["C7:E7"];
                var border20 = ranges20.Style.Border;
                border20.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border20.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border20.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border20.Top.Color.SetColor(System.Drawing.Color.Black);
                border20.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border20.Right.Color.SetColor(System.Drawing.Color.Black);
                border20.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border20.Left.Color.SetColor(System.Drawing.Color.White);

                worksheet.Cells["A8:B8"].Merge = true;
                worksheet.Cells["A8:B8"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A8:B8"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["A8:B8"].Value = "DESCRIPCION DEL CAMBIO";
                worksheet.Cells["A8:B8"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A8:B8"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A8:B8"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges25 = worksheet.Cells["A8:B8"];
                var border25 = ranges25.Style.Border;
                border25.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border25.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border25.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border25.Top.Color.SetColor(System.Drawing.Color.Black);
                border25.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border25.Right.Color.SetColor(System.Drawing.Color.Black);
                border25.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border25.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["C8:G8"].Merge = true;
                worksheet.Cells["C8:G8"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["C8:G8"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["C8:G8"].Value = "";
                worksheet.Cells["C8:G8"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["C8:G8"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["C8:G8"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges26 = worksheet.Cells["C8:G8"];
                var border26 = ranges26.Style.Border;
                border26.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border26.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border26.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border26.Top.Color.SetColor(System.Drawing.Color.Black);
                border26.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border26.Right.Color.SetColor(System.Drawing.Color.Black);
                border26.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border26.Left.Color.SetColor(System.Drawing.Color.White);


                worksheet.Cells["A9:B9"].Merge = true;
                worksheet.Cells["A9:B9"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A9:B9"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["A9:B9"].Value = "PROPIETARIO DEL CAMBIO";
                worksheet.Cells["A9:B9"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A9:B9"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A9:B9"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges29 = worksheet.Cells["A9:B9"];
                var border29 = ranges29.Style.Border;
                border29.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border29.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border29.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border29.Top.Color.SetColor(System.Drawing.Color.Black);
                border29.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border29.Right.Color.SetColor(System.Drawing.Color.Black);
                border29.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border29.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["C9:J9"].Merge = true;
                worksheet.Cells["C9:J9"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["C9:J9"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["C9:J9"].Value = "";
                worksheet.Cells["C9:J9"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["C9:J9"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["C9:J9"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges30 = worksheet.Cells["C9:J9"];
                var border30 = ranges30.Style.Border;
                border30.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border30.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border30.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border30.Top.Color.SetColor(System.Drawing.Color.Black);
                border30.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border30.Right.Color.SetColor(System.Drawing.Color.Black);
                border30.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border30.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["A10:B10"].Merge = true;
                worksheet.Cells["A10:B10"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A10:B10"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["A10:B10"].Value = "FECHA DE INICIO ESTABILIZACION";
                worksheet.Cells["A10:B10"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A10:B10"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A10:B10"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges31 = worksheet.Cells["A10:B10"];
                var border31 = ranges31.Style.Border;
                border31.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border31.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border31.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border31.Top.Color.SetColor(System.Drawing.Color.Black);
                border31.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border31.Right.Color.SetColor(System.Drawing.Color.Black);
                border31.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border31.Left.Color.SetColor(System.Drawing.Color.Black);


                // Establecer el contorno negro de las celdas
                var ranges32 = worksheet.Cells["C10"];
                var border32 = ranges32.Style.Border;
                border32.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border32.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border32.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border32.Top.Color.SetColor(System.Drawing.Color.Black);
                border32.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border32.Right.Color.SetColor(System.Drawing.Color.Black);
                border32.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border32.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["D10:E10"].Merge = true;
                worksheet.Cells["D10:E10"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["D10:E10"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["D10:E10"].Value = "FECHA DE FIN ESTABILIZACION";
                worksheet.Cells["D10:E10"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["D10:E10"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["D10:E10"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges33 = worksheet.Cells["D10:E10"];
                var border33 = ranges33.Style.Border;
                border33.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border33.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border33.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border33.Top.Color.SetColor(System.Drawing.Color.Black);
                border33.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border33.Right.Color.SetColor(System.Drawing.Color.Black);
                border33.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border33.Left.Color.SetColor(System.Drawing.Color.Black);



                worksheet.Cells["F7"].Merge = true;
                worksheet.Cells["F7"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["F7"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["F7"].Value = "NUMERO DE CAMBIO:";
                worksheet.Cells["F7"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["F7"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["F7"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges21 = worksheet.Cells["F7"];
                var border21 = ranges21.Style.Border;
                border21.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border21.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border21.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border21.Top.Color.SetColor(System.Drawing.Color.Black);
                border21.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border21.Right.Color.SetColor(System.Drawing.Color.Black);
                border21.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border21.Left.Color.SetColor(System.Drawing.Color.White);

                // Establecer el contorno negro de las celdas
                var ranges22 = worksheet.Cells["G7"];
                var border22 = ranges22.Style.Border;
                border22.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border22.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border22.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border22.Top.Color.SetColor(System.Drawing.Color.Black);
                border22.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border22.Right.Color.SetColor(System.Drawing.Color.Black);
                border22.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border22.Left.Color.SetColor(System.Drawing.Color.White);


                worksheet.Cells["G10:H10"].Merge = true;
                worksheet.Cells["G10:H10"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["G10:H10"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["G10:H10"].Value = "APLICATIVO";
                worksheet.Cells["G10:H10"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["G10:H10"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["G10:H10"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges35 = worksheet.Cells["G10:H10"];
                var border35 = ranges35.Style.Border;
                border35.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border35.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border35.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border35.Top.Color.SetColor(System.Drawing.Color.Black);
                border35.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border35.Right.Color.SetColor(System.Drawing.Color.Black);
                border35.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border35.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["I10:J10"].Merge = true;
                worksheet.Cells["I10:J10"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["I10:J10"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["I10:J10"].Value = "";
                worksheet.Cells["I10:J10"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["I10:J10"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["I10:J10"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges36 = worksheet.Cells["I10:J10"];
                var border36 = ranges36.Style.Border;
                border36.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border36.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border36.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border36.Top.Color.SetColor(System.Drawing.Color.Black);
                border36.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border36.Right.Color.SetColor(System.Drawing.Color.Black);
                border36.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border36.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["H7"].Merge = true;
                worksheet.Cells["H7"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["H7"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["H7"].Value = "TIPOLOGIA DEL" + Environment.NewLine + " CAMBIO:";
                worksheet.Cells["H7"].Style.WrapText = true;
                worksheet.Cells["H7"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["H7"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["H7"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges23 = worksheet.Cells["H7"];
                var border23 = ranges23.Style.Border;
                border23.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border23.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border23.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border23.Top.Color.SetColor(System.Drawing.Color.Black);
                border23.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border23.Right.Color.SetColor(System.Drawing.Color.Black);
                border23.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border23.Left.Color.SetColor(System.Drawing.Color.White);



                worksheet.Cells["I7:J7"].Merge = true;
                worksheet.Cells["I7:J7"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["I7:J7"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["I7:J7"].Value = "";
                worksheet.Cells["I7:J7"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["I7:J7"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["I7:J7"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges24 = worksheet.Cells["I7:J7"];
                var border24 = ranges24.Style.Border;
                border24.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border24.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border24.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border24.Top.Color.SetColor(System.Drawing.Color.Black);
                border24.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border24.Right.Color.SetColor(System.Drawing.Color.Black);
                border24.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border24.Left.Color.SetColor(System.Drawing.Color.White);


                worksheet.Cells["H8:I8"].Merge = true;
                worksheet.Cells["H8:I8"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["H8:I8"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["H8:I8"].Value = "¿SE REALIZÓ VALIDACIÓN CON LISTA DE CHEQUEO?";
                worksheet.Cells["H8:I8"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["H8:I8"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["H8:I8"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges27 = worksheet.Cells["H8:I8"];
                var border27 = ranges27.Style.Border;
                border27.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border27.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border27.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border27.Top.Color.SetColor(System.Drawing.Color.Black);
                border27.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border27.Right.Color.SetColor(System.Drawing.Color.White);
                border27.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border27.Left.Color.SetColor(System.Drawing.Color.White);


                // Establecer el contorno negro de las celdas
                var ranges28 = worksheet.Cells["J8"];
                var border28 = ranges28.Style.Border;
                border28.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border28.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border28.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border28.Top.Color.SetColor(System.Drawing.Color.Black);
                border28.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border28.Right.Color.SetColor(System.Drawing.Color.Black);
                border28.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border28.Left.Color.SetColor(System.Drawing.Color.White);



                // Establecer el contorno negro de las celdas
                var ranges34 = worksheet.Cells["F10"];
                var border34 = ranges34.Style.Border;
                border34.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border34.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border34.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border34.Top.Color.SetColor(System.Drawing.Color.Black);
                border34.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border34.Right.Color.SetColor(System.Drawing.Color.Black);
                border34.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border34.Left.Color.SetColor(System.Drawing.Color.Black);



                worksheet.Cells["A11:J11"].Merge = true;
                worksheet.Cells["A11:J11"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A11:J11"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A11:J11"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A11:J11"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                worksheet.Cells["A11:J11"].Merge = true;

                // Establecer el contorno negro de las celdas
                var ranges37 = worksheet.Cells["A11:J11"];
                var border37 = ranges37.Style.Border;
                border37.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border37.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border37.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border37.Top.Color.SetColor(System.Drawing.Color.Black);
                border37.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border37.Right.Color.SetColor(System.Drawing.Color.Black);
                border37.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border37.Left.Color.SetColor(System.Drawing.Color.White);

                worksheet.Cells["A12:J12"].Merge = true;
                worksheet.Cells["A12:J12"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A12:J12"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["A12:J12"].Value = "2.FIRMAS DE ACEPTACION  (requiere aprobaciones anexas)";
                worksheet.Cells["A12:J12"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A12:J12"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A12:J12"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges12 = worksheet.Cells["A12:J12"];
                var border12 = ranges12.Style.Border;
                border12.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border12.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border12.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border12.Top.Color.SetColor(System.Drawing.Color.Black);
                border12.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border12.Right.Color.SetColor(System.Drawing.Color.Black);
                border12.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border12.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["A13"].Merge = true;
                worksheet.Cells["A13"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A13"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["A13"].Value = "ROL";
                worksheet.Cells["A13"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A13"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A13"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges13 = worksheet.Cells["A13"];
                var border13 = ranges13.Style.Border;
                border13.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border13.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border13.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border13.Top.Color.SetColor(System.Drawing.Color.Black);
                border13.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border13.Right.Color.SetColor(System.Drawing.Color.Black);
                border13.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border13.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["B13:D13"].Merge = true;
                worksheet.Cells["B13:D13"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B13:D13"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["B13:D13"].Value = "NOMBRE Y APELLIDO";
                worksheet.Cells["B13:D13"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B13:D13"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B13:D13"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges14 = worksheet.Cells["B13:D13"];
                var border14 = ranges14.Style.Border;
                border14.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border14.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border14.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border14.Top.Color.SetColor(System.Drawing.Color.Black);
                border14.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border14.Right.Color.SetColor(System.Drawing.Color.Black);
                border14.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border14.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["E13"].Merge = true;
                worksheet.Cells["E13"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["E13"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["E13"].Value = "AREA";
                worksheet.Cells["E13"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["E13"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["E13"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges15 = worksheet.Cells["E13"];
                var border15 = ranges15.Style.Border;
                border15.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border15.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border15.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border15.Top.Color.SetColor(System.Drawing.Color.Black);
                border15.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border15.Right.Color.SetColor(System.Drawing.Color.Black);
                border15.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border15.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["F13"].Merge = true;
                worksheet.Cells["F13"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["F13"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["F13"].Value = "Nombre del EMAIL" + Environment.NewLine + "(# Aprobación )";
                worksheet.Cells["F13"].Style.WrapText = true;
                worksheet.Cells["F13"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["F13"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["F13"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges16 = worksheet.Cells["F13"];
                var border16 = ranges16.Style.Border;
                border16.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border16.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border16.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border16.Top.Color.SetColor(System.Drawing.Color.Black);
                border16.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border16.Right.Color.SetColor(System.Drawing.Color.Black);
                border16.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border16.Left.Color.SetColor(System.Drawing.Color.White);

                worksheet.Cells["G13:J13"].Merge = true;
                worksheet.Cells["G13:J13"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["G13:J13"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["G13:J13"].Value = "OBSERVACIONES";
                worksheet.Cells["G13:J13"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["G13:J13"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["G13:J13"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges17 = worksheet.Cells["G13:J13"];
                var border17 = ranges17.Style.Border;
                border17.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border17.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border17.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border17.Top.Color.SetColor(System.Drawing.Color.Black);
                border17.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border17.Right.Color.SetColor(System.Drawing.Color.Black);
                border17.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border17.Left.Color.SetColor(System.Drawing.Color.White);



                foreach (var change in changes)
                {
                    
                      var check = "NO";
                    if (change.CheckList ==1)
                    {
                        check = "SI";
                    }

                    worksheet.Cells["G7"].Value = change.ChangeNumber;
                    worksheet.Cells["C7:E7"].Value = change.RequestTypeID;
                    worksheet.Cells["C8:G8"].Value = change.ChangeDescription;
                    worksheet.Cells["J8"].Value = check;
                    worksheet.Cells["C10"].Value = change.StartDate;
                    worksheet.Cells["F10"].Value = change.EndDate;
                    worksheet.Cells["I7:J7"].Value = change.TypologyID;
                    worksheet.Cells["I10:J10"].Value = "DPW";
                  


                    // Agrega más datos según las propiedades de Change que desees mostrar en las celdas

                }

                //Rou
                worksheet.Row(1).Height = 21;
                worksheet.Row(2).Height = 21;
                worksheet.Row(3).Height = 21;
                worksheet.Row(4).Height = 12;
                worksheet.Row(5).Height = 12.6;
                worksheet.Row(6).Height = 12;
                worksheet.Row(7).Height = 35.25;
                worksheet.Row(8).Height = 34.5;
                worksheet.Row(9).Height = 11.4;
                worksheet.Row(10).Height = 25.5;
                worksheet.Row(11).Height = 12;
                worksheet.Row(12).Height = 12;
                worksheet.Row(13).Height = 23.4;
                worksheet.Row(14).Height = 34.2;

                //Column
                worksheet.Column(1).Width = 23.109375;
                worksheet.Column(2).Width = 20.109375;
                worksheet.Column(3).Width = 15.33203125;
                worksheet.Column(4).Width = 23.44140625;
                worksheet.Column(5).Width = 36;
                worksheet.Column(6).Width = 28.88671875;
                worksheet.Column(7).Width = 26.88671875;
                worksheet.Column(8).Width = 19.44140625;
                worksheet.Column(9).Width = 20.6640625;
                worksheet.Column(10).Width = 20.44140625;
                worksheet.Column(11).Width = 13.44140625;
                worksheet.Column(12).Width = 13.44140625;
                worksheet.Column(13).Width = 20.88671875;

                // Agregar datos en filas
               



                int newRowCount = 5; // Número de nuevas filas a agregar

                for (int i = 0; i < newRowCount; i++)
                {
                    int currentRowCount = worksheet.Dimension.Rows; // Obtener el número de filas actualmente en uso
                    int rowIndex = currentRowCount + i + 1;

                    // Verificar si la fila está vacía y aplicar estilos
                    if (string.IsNullOrWhiteSpace(worksheet.Cells[rowIndex, 1].Text))
                    {
                        // Agrega una nueva fila con estilos definidos
                        var newRow = worksheet.Cells[currentRowCount + 1, 1, currentRowCount + 1, 1];
                        newRow.Style.Font.Bold = true;
                        newRow.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow1 = worksheet.Cells[currentRowCount + 1, 2, currentRowCount + 1, 4];
                        newRow1.Style.Font.Bold = true;
                        newRow1.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow1.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow1.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow1.Merge = true;


                        // Agrega una nueva fila con estilos definidos
                        var newRow2 = worksheet.Cells[currentRowCount + 1, 5, currentRowCount + 1, 5];
                        newRow2.Style.Font.Bold = true;
                        newRow2.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow2.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow2.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow2.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow2.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow2.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow3 = worksheet.Cells[currentRowCount + 1, 6, currentRowCount + 1, 6];
                        newRow3.Style.Font.Bold = true;
                        newRow3.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow3.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow3.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow3.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow3.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow3.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow4 = worksheet.Cells[currentRowCount + 1, 7, currentRowCount + 1, 10];
                        newRow4.Style.Font.Bold = true;
                        newRow4.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow4.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow4.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow4.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow4.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow4.Merge = true; // Combina las celdas

                        var ranges38 = worksheet.Cells[currentRowCount + 1, 1, currentRowCount + 1, 10];
                        var border38 = ranges38.Style.Border;
                        border38.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border38.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border38.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border38.Top.Color.SetColor(System.Drawing.Color.Black);
                        border38.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border38.Right.Color.SetColor(System.Drawing.Color.Black);
                        border38.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border38.Left.Color.SetColor(System.Drawing.Color.Black);

                        if (i < signatures.Count)
                        {
                            var sig = signatures[i];
                            var User =  sig.User;

                            worksheet.Cells[currentRowCount + 1, 1, currentRowCount + 1, 1].Value = User.Position;
                            worksheet.Cells[currentRowCount + 1, 2, currentRowCount + 1, 4].Value = User.Name;
                            worksheet.Cells[currentRowCount + 1, 5, currentRowCount + 1, 5].Value = User.Area;
                            
                            worksheet.Cells[currentRowCount + 1, 7, currentRowCount + 1, 10].Value = sig.Observatins;
                            // Insertar más datos en celdas específicas según tus necesidades
                        }


                    }



                }



                int lastRowIndex = currentRowIndex;
                int newRowCount2 = 1;
                for (int j = 0; j < newRowCount2; j++)
                {
                    int currentRowCount2 = worksheet.Dimension.Rows; // Obtener el número de filas actualmente en uso
                    int rowIndex = currentRowCount2 + j + 1;
                    int currentCellsCount = worksheet.Dimension.End.Row;
                    int rowIndex1 = currentCellsCount + j + 1;
                    // Verificar si la fila está vacía y aplica estilos 
                    if (string.IsNullOrWhiteSpace(worksheet.Cells[rowIndex, 1].Text))
                    {
                        var rowStyle = worksheet.Cells[currentRowCount2, 1, currentRowCount2, 10];
                        rowStyle.Style.Font.Bold = true;
                        rowStyle.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        rowStyle.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                        rowStyle.Value = "3. FORMATO DE CAPACITACION  (requiere aprobaciones anexas Correo de Aprobación)";
                        rowStyle.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle.Merge = true;

                        // Establecer el contorno negro de las celdas
                        var ranges39 = worksheet.Cells[currentRowCount2, 1, currentRowCount2, 10];
                        var border39 = ranges39.Style.Border;
                        border39.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border39.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border39.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border39.Top.Color.SetColor(System.Drawing.Color.Black);
                        border39.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border39.Right.Color.SetColor(System.Drawing.Color.Black);
                        border39.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border39.Left.Color.SetColor(System.Drawing.Color.Black);

                        var rowStyle1 = worksheet.Cells[rowIndex, 1, rowIndex + 1, 1];
                        rowStyle1.Style.Font.Bold = true;
                        rowStyle1.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle1.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        rowStyle1.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                        rowStyle1.Value = "FECHA";
                        rowStyle1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle1.Merge = true;
                        worksheet.Row(rowIndex).Height = 26.25;

                        // Establecer el contorno negro de las celdas
                        var ranges40 = worksheet.Cells[rowIndex, 1, rowIndex + 1, 1];
                        var border40 = ranges40.Style.Border;
                        border40.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border40.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border40.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border40.Top.Color.SetColor(System.Drawing.Color.Black);
                        border40.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border40.Right.Color.SetColor(System.Drawing.Color.Black);
                        border40.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border40.Left.Color.SetColor(System.Drawing.Color.Black);


                        // Agrega una nueva fila con estilos definidos
                        var newRow2 = worksheet.Cells[currentCellsCount + 1, 2, currentCellsCount + 1, 4];
                        newRow2.Style.Font.Bold = true;
                        newRow2.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow2.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        newRow2.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                        newRow2.Value = "NOMBRE Y APELLIDO";
                        newRow2.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow2.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow2.Merge = true; // Combina las celdas


                        // Establecer el contorno negro de las celdas
                        var ranges41 = worksheet.Cells[currentCellsCount + 1, 2, currentCellsCount + 1, 4];
                        var border41 = ranges41.Style.Border;
                        border41.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border41.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border41.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border41.Top.Color.SetColor(System.Drawing.Color.Black);
                        border41.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border41.Right.Color.SetColor(System.Drawing.Color.Black);
                        border41.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border41.Left.Color.SetColor(System.Drawing.Color.Black);


                        // Agrega una nueva fila con estilos definidos
                        var newRow3 = worksheet.Cells[rowIndex1 + 1, 2, rowIndex1 + 1, 2];
                        newRow3.Style.Font.Bold = true;
                        newRow3.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow3.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        newRow3.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                        newRow3.Value = "(INSTRUCTOR)";
                        newRow3.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow3.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow3.Merge = true; // Combina las celdas

                        // Establecer el contorno negro de las celdas
                        var ranges42 = worksheet.Cells[rowIndex1 + 1, 2, rowIndex1 + 1, 2];
                        var border42 = ranges42.Style.Border;
                        border42.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border42.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border42.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border42.Top.Color.SetColor(System.Drawing.Color.Black);
                        border42.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border42.Right.Color.SetColor(System.Drawing.Color.Black);
                        border42.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border42.Left.Color.SetColor(System.Drawing.Color.Black);

                        var newRow4 = worksheet.Cells[rowIndex1 + 1, 3, rowIndex1 + 1, 4];
                        newRow4.Style.Font.Bold = true;
                        newRow4.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow4.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        newRow4.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                        newRow4.Value = "(RECURSO CAPACITADO)";
                        newRow4.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow4.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow4.Merge = true; // Combina las celdas

                        // Establecer el contorno negro de las celdas
                        var ranges43 = worksheet.Cells[rowIndex1 + 1, 3, rowIndex1 + 1, 4];
                        var border43 = ranges43.Style.Border;
                        border43.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border43.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border43.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border43.Top.Color.SetColor(System.Drawing.Color.Black);
                        border43.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border43.Right.Color.SetColor(System.Drawing.Color.Black);
                        border43.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border43.Left.Color.SetColor(System.Drawing.Color.Black);

                        var newRow5 = worksheet.Cells[rowIndex1, 5, rowIndex1 + 1, 6];
                        newRow5.Style.Font.Bold = true;
                        newRow5.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow5.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        newRow5.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                        newRow5.Value = "(COMENTARIOS)";
                        newRow5.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow5.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow5.Merge = true; // Combina las celdas

                        // Establecer el contorno negro de las celdas
                        var ranges44 = newRow5 = worksheet.Cells[rowIndex1, 5, rowIndex1 + 1, 6];
                        var border44 = ranges44.Style.Border;
                        border44.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border44.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border44.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border44.Top.Color.SetColor(System.Drawing.Color.Black);
                        border44.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border44.Right.Color.SetColor(System.Drawing.Color.Black);

                        var newRow6 = worksheet.Cells[rowIndex1, 7, rowIndex1 + 1, 7];
                        newRow6.Style.Font.Bold = true;
                        newRow6.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow6.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        newRow6.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                        newRow6.Value = "(TIPO)";
                        newRow6.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow6.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow6.Merge = true; // Combina las celdas

                        // Establecer el contorno negro de las celdas
                        var ranges45 = worksheet.Cells[rowIndex1, 7, rowIndex1 + 1, 7];
                        var border45 = ranges45.Style.Border;
                        border45.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border45.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border45.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border45.Top.Color.SetColor(System.Drawing.Color.Black);
                        border45.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border45.Right.Color.SetColor(System.Drawing.Color.Black);
                        border45.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border45.Left.Color.SetColor(System.Drawing.Color.Black);

                        var newRow7 = worksheet.Cells[rowIndex1, 8, rowIndex1 + 1, 8];
                        newRow7.Style.Font.Bold = true;
                        newRow7.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow7.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        newRow7.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                        newRow7.Value = "(OBJETIVO DE" + Environment.NewLine + "CAPACITACION)";
                        newRow7.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow7.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow7.Merge = true;

                        // Establecer el contorno negro de las celdas
                        var ranges46 = worksheet.Cells[rowIndex1, 8, rowIndex1 + 1, 8];
                        var border46 = ranges46.Style.Border;
                        border46.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border46.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border46.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border46.Top.Color.SetColor(System.Drawing.Color.Black);
                        border46.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border46.Right.Color.SetColor(System.Drawing.Color.Black);
                        border46.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border46.Left.Color.SetColor(System.Drawing.Color.Black);


                        var newRow8 = worksheet.Cells[rowIndex1, 9, rowIndex1 + 1, 10];
                        newRow8.Style.Font.Bold = true;
                        newRow8.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow8.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        newRow8.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                        newRow8.Value = "(TEMAS TRATADOS)";
                        newRow8.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow8.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow8.Merge = true;

                        // Establecer el contorno negro de las celdas
                        var ranges47 = worksheet.Cells[rowIndex1, 9, rowIndex1 + 1, 10];
                        var border47 = ranges47.Style.Border;
                        border47.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border47.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border47.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border47.Top.Color.SetColor(System.Drawing.Color.Black);
                        border47.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border47.Right.Color.SetColor(System.Drawing.Color.Black);
                        border47.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border47.Left.Color.SetColor(System.Drawing.Color.Black);
                    }

                    // Agregar datos en las celdas de la fila

                    // Agregar más datos en las celdas según sea necesario
                }



                ////
                ///

                // Agregar datos en filas

                int newRowCount3 = 4; // Número de nuevas filas a agregar

                for (int i = 0; i < newRowCount3; i++)
                {
                    int currentRowCount3 = worksheet.Dimension.Rows; // Obtener el número de filas actualmente en uso
                    int rowIndex2 = currentRowCount3 + i + 1;

                    // Verificar si la fila está vacía y aplicar estilos
                    if (string.IsNullOrWhiteSpace(worksheet.Cells[rowIndex2, 1].Text))
                    {
                        // Agrega una nueva fila con estilos definidos
                        var newRow = worksheet.Cells[currentRowCount3 + 1, 1, currentRowCount3 + 1, 1];
                        newRow.Style.Font.Bold = true;
                        newRow.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow1 = worksheet.Cells[currentRowCount3 + 1, 2, currentRowCount3 + 1, 2];
                        newRow1.Style.Font.Bold = true;
                        newRow1.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow1.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow1.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow1.Merge = true;


                        // Agrega una nueva fila con estilos definidos
                        var newRow2 = worksheet.Cells[currentRowCount3 + 1, 3, currentRowCount3 + 1, 4];
                        newRow2.Style.Font.Bold = true;
                        newRow2.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow2.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow2.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow2.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow2.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow2.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow3 = worksheet.Cells[currentRowCount3 + 1, 5, currentRowCount3 + 1, 6];
                        newRow3.Style.Font.Bold = true;
                        newRow3.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow3.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow3.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow3.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow3.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow3.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow4 = worksheet.Cells[currentRowCount3 + 1, 7, currentRowCount3 + 1, 7];
                        newRow4.Style.Font.Bold = true;
                        newRow4.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow4.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow4.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow4.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow4.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow4.Merge = true; // Combina las celdas


                        // Agrega una nueva fila con estilos definidos
                        var newRow5 = worksheet.Cells[currentRowCount3 + 1, 8, currentRowCount3 + 1, 8];
                        newRow5.Style.Font.Bold = true;
                        newRow5.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow5.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow5.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow5.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow5.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow5.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow6 = worksheet.Cells[currentRowCount3 + 1, 9, currentRowCount3 + 1, 10];
                        newRow6.Style.Font.Bold = true;
                        newRow6.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow6.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow6.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow6.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow6.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow6.Merge = true; // Combina las celdas

                        var ranges38 = worksheet.Cells[currentRowCount3 + 1, 1, currentRowCount3 + 1, 10];
                        var border38 = ranges38.Style.Border;
                        border38.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border38.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border38.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border38.Top.Color.SetColor(System.Drawing.Color.Black);
                        border38.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border38.Right.Color.SetColor(System.Drawing.Color.Black);
                        border38.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border38.Left.Color.SetColor(System.Drawing.Color.Black);
                    }

                    if (i < trainings.Count)
                    {
                        var training = trainings[i];
                        var user1 = training.User;
                        var instruc = signatures[i];
                        var instructor = instruc.User;

                        worksheet.Cells[currentRowCount3 + 1, 1, currentRowCount3 + 1, 1].Value = training.DataTraining;
                        worksheet.Cells[currentRowCount3 + 1, 2, currentRowCount3 + 1, 2].Value = instructor.Name;
                        worksheet.Cells[currentRowCount3 + 1, 3, currentRowCount3 + 1, 4].Value = user1.Name;
                        worksheet.Cells[currentRowCount3 + 1, 5, currentRowCount3 + 1, 6].Value = training.Comments;
                        worksheet.Cells[currentRowCount3 + 1, 7, currentRowCount3 + 1, 7].Value = training.Type;
                        worksheet.Cells[currentRowCount3 + 1, 8, currentRowCount3 + 1, 8].Value = training.Objective;
                        worksheet.Cells[currentRowCount3 + 1, 9, currentRowCount3 + 1, 10].Value = training.Issues;
                        // Insertar más datos en celdas específicas según tus necesidades
                    }


                }


                ////4.1

                int lastRowIndex4 = currentRowIndex;
                int newRowCount4 = 1;
                for (int j = 0; j < newRowCount4; j++)
                {

                    int currentRowCount2 = worksheet.Dimension.Rows; // Obtener el número de filas actualmente en uso
                    int rowIndex = currentRowCount2 + j + 1;
                    int currentCellsCount = worksheet.Dimension.End.Row;
                    int rowIndex1 = currentCellsCount + j + 1;
                    // Verificar si la fila está vacía y aplica estilos 
                    if (string.IsNullOrWhiteSpace(worksheet.Cells[rowIndex1, 1].Text))
                    {
                        var rowStyle = worksheet.Cells[currentRowCount2, 1, currentRowCount2, 10];
                        rowStyle.Style.Font.Bold = true;
                        rowStyle.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        rowStyle.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle.Value = "4.1. INFORMACION GENERAL DE CONTACTOS";
                        rowStyle.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle.Merge = true;
                        worksheet.Row(rowIndex).Height = 12;




                        // Establecer el contorno negro de las celdas
                        var ranges50 = worksheet.Cells[currentRowCount2, 1, currentRowCount2, 10];
                        var border50 = ranges50.Style.Border;
                        border50.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border50.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border50.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border50.Top.Color.SetColor(System.Drawing.Color.Black);
                        border50.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border50.Right.Color.SetColor(System.Drawing.Color.Black);
                        border50.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border50.Left.Color.SetColor(System.Drawing.Color.Black);

                        var rowStyle1 = worksheet.Cells[rowIndex, 1, rowIndex, 2];
                        rowStyle1.Style.Font.Bold = true;
                        rowStyle1.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle1.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle1.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        rowStyle1.Value = "Rol";
                        rowStyle1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle1.Merge = true;

                        worksheet.Row(rowIndex).Height = 13;

                        var rowStyle2 = worksheet.Cells[rowIndex, 3, rowIndex, 5];
                        rowStyle2.Style.Font.Bold = true;
                        rowStyle2.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle2.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle2.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        rowStyle2.Value = "Nombre y Apellido";
                        rowStyle2.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle2.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle2.Merge = true;
                        worksheet.Row(rowIndex).Height = 13;


                        var rowStyle3 = worksheet.Cells[rowIndex, 6, rowIndex, 6];
                        rowStyle3.Style.Font.Bold = true;
                        rowStyle3.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle3.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle3.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        rowStyle3.Value = "Teléfono";
                        rowStyle3.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle3.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle3.Merge = true;
                        worksheet.Row(rowIndex).Height = 13;

                        var rowStyle4 = worksheet.Cells[rowIndex, 7, rowIndex, 7];
                        rowStyle4.Style.Font.Bold = true;
                        rowStyle4.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle4.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle4.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        rowStyle4.Value = "Email";
                        rowStyle4.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle4.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle4.Merge = true;
                        worksheet.Row(rowIndex).Height = 13;

                        var rowStyle5 = worksheet.Cells[rowIndex, 8, rowIndex, 10];
                        rowStyle5.Style.Font.Bold = true;
                        rowStyle5.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle5.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle5.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        rowStyle5.Value = "Observaciones";
                        rowStyle5.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle5.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle5.Merge = true;
                        worksheet.Row(rowIndex).Height = 13;

                        var ranges38 = worksheet.Cells[rowIndex, 1, rowIndex, 10];
                        var border38 = ranges38.Style.Border;
                        border38.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border38.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border38.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border38.Top.Color.SetColor(System.Drawing.Color.Black);
                        border38.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border38.Right.Color.SetColor(System.Drawing.Color.Black);
                        border38.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border38.Left.Color.SetColor(System.Drawing.Color.Black);


                    }

                    // Agregar datos en las celdas de la fila

                    // Agregar más datos en las celdas según sea necesario
                }

                ///

                // Agregar datos en filas

                int newRowCount5 = 4; // Número de nuevas filas a agregar

                for (int i = 0; i < newRowCount5; i++)
                {
                    int currentRowCount4 = worksheet.Dimension.Rows; // Obtener el número de filas actualmente en uso
                    int rowIndex3 = currentRowCount4 + i + 1;

                    // Verificar si la fila está vacía y aplicar estilos
                    if (string.IsNullOrWhiteSpace(worksheet.Cells[rowIndex3, 1].Text))
                    {
                        // Agrega una nueva fila con estilos definidos
                        var newRow = worksheet.Cells[currentRowCount4 + 1, 1, currentRowCount4 + 1, 2];
                        newRow.Style.Font.Bold = true;
                        newRow.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow1 = worksheet.Cells[currentRowCount4 + 1, 3, currentRowCount4 + 1, 5];
                        newRow1.Style.Font.Bold = true;
                        newRow1.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow1.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow1.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow1.Merge = true;


                        // Agrega una nueva fila con estilos definidos
                        var newRow2 = worksheet.Cells[currentRowCount4 + 1, 6, currentRowCount4 + 1, 6];
                        newRow2.Style.Font.Bold = true;
                        newRow2.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow2.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow2.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow2.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow2.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow2.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow3 = worksheet.Cells[currentRowCount4 + 1, 7, currentRowCount4 + 1, 7];
                        newRow3.Style.Font.Bold = true;
                        newRow3.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow3.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow3.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow3.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow3.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow3.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow4 = worksheet.Cells[currentRowCount4 + 1, 8, currentRowCount4 + 1, 10];
                        newRow4.Style.Font.Bold = true;
                        newRow4.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow4.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow4.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow4.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow4.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow4.Merge = true; // Combina las celdas






                        var ranges38 = worksheet.Cells[currentRowCount4 + 1, 1, currentRowCount4 + 1, 10];
                        var border38 = ranges38.Style.Border;
                        border38.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border38.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border38.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border38.Top.Color.SetColor(System.Drawing.Color.Black);
                        border38.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border38.Right.Color.SetColor(System.Drawing.Color.Black);
                        border38.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border38.Left.Color.SetColor(System.Drawing.Color.Black);
                        /*

                        if (i < contacts.Count)
                        {
                            var contact = contacts[i];
                            var user = contact.User;

                            worksheet.Cells[currentRowCount4 + 1, 1, currentRowCount4 + 1, 2].Value = user.Position;
                            worksheet.Cells[currentRowCount4 + 1, 3, currentRowCount4 + 1, 5].Value = user.Name;
                            worksheet.Cells[currentRowCount4 + 1, 6, currentRowCount4 + 1, 6].Value = user.Phone;
                            worksheet.Cells[currentRowCount4 + 1, 7, currentRowCount4 + 1, 7].Value = user.Email;
                            worksheet.Cells[currentRowCount4 + 1, 8, currentRowCount4 + 1, 10].Value = contact.Observations;
                            // Insertar más datos en celdas específicas según tus necesidades
                        }

                          if (i < users.Count)
                          {
                              var user = users[i];

                              worksheet.Cells[currentRowCount4 + 1, 1, currentRowCount4 + 1, 2].Value = user.Position;
                              worksheet.Cells[currentRowCount4 + 1, 3, currentRowCount4 + 1, 5].Value = user.Name;

                              worksheet.Cells[currentRowCount4 + 1, 6, currentRowCount4 + 1, 6].Value = user.Phone;
                              worksheet.Cells[currentRowCount4 + 1, 7, currentRowCount4 + 1, 7].Value = user.Email;
                              // Insertar más datos en celdas específicas según tus necesidades

                          }
                          rowIndex3++; // Avanzar a la siguiente fila*/




                        rowIndex3++;
                    }


                    /*if (i < users.Count)
                    {
                        var user = users[i];

                        worksheet.Cells[currentRowCount4 + 1, 1, currentRowCount4 + 1, 2].Value = user.Position;
                        worksheet.Cells[currentRowCount4 + 1, 3, currentRowCount4 + 1, 5].Value = user.Name;

                        worksheet.Cells[currentRowCount4 + 1, 6, currentRowCount4 + 1, 6].Value = user.Phone;
                        worksheet.Cells[currentRowCount4 + 1, 7, currentRowCount4 + 1, 7].Value = user.Email;
                        // Insertar más datos en celdas específicas según tus necesidades

                    }
                    rowIndex3++; // Avanzar a la siguiente fila
                    */
                }


                //4.2

                int lastRowIndex6 = currentRowIndex;
                int newRowCount6 = 1;
                for (int j = 0; j < newRowCount6; j++)
                {

                    int currentRowCount6 = worksheet.Dimension.Rows; // Obtener el número de filas actualmente en uso
                    int rowIndex = currentRowCount6 + j + 1;
                    int currentCellsCount = worksheet.Dimension.End.Row;
                    int rowIndex1 = currentCellsCount + j + 1;
                    // Verificar si la fila está vacía y aplica estilos 
                    if (string.IsNullOrWhiteSpace(worksheet.Cells[rowIndex1, 1].Text))
                    {
                        var rowStyle = worksheet.Cells[currentRowCount6, 1, currentRowCount6, 10];
                        rowStyle.Style.Font.Bold = true;
                        rowStyle.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        rowStyle.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle.Value = "4.2. PRERREQUISITOS (son actividades que se pueden hacer antes de la ventana de implementación, sin afectación de servicio) .";
                        rowStyle.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        rowStyle.Merge = true;
                        worksheet.Row(rowIndex).Height = 36;


                        // Establecer el contorno negro de las celdas
                        var ranges52 = worksheet.Cells[currentRowCount6, 1, currentRowCount6, 10];
                        var border52 = ranges52.Style.Border;
                        border52.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border52.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border52.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border52.Top.Color.SetColor(System.Drawing.Color.Black);
                        border52.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border52.Right.Color.SetColor(System.Drawing.Color.Black);
                        border52.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border52.Left.Color.SetColor(System.Drawing.Color.Black);





                        var rowStyle1 = worksheet.Cells[currentRowCount6, 11, currentRowCount6, 13];
                        rowStyle1.Style.Font.Bold = true;
                        rowStyle1.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle1.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        rowStyle1.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                        rowStyle1.Value = "4.9. Resultado de Ejecución / Infraestructura";
                        rowStyle1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle1.Merge = true;
                        worksheet.Row(rowIndex).Height = 36;



                        // Establecer el contorno negro de las celdas
                        var ranges53 = worksheet.Cells[currentRowCount6, 11, currentRowCount6, 13];
                        var border53 = ranges52.Style.Border;
                        border53.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border53.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border53.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border53.Top.Color.SetColor(System.Drawing.Color.Black);
                        border53.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border53.Right.Color.SetColor(System.Drawing.Color.Black);
                        border53.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border53.Left.Color.SetColor(System.Drawing.Color.Black);



                        var rowStyle2 = worksheet.Cells[rowIndex, 1, rowIndex, 1];
                        rowStyle2.Style.Font.Bold = true;
                        rowStyle2.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle2.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle2.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle2.Value = "Secuencia";
                        rowStyle2.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle2.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle2.Merge = true;
                        worksheet.Row(rowIndex).Height = 45.75;


                        // Establecer el contorno negro de las celdas
                        var ranges54 = worksheet.Cells[rowIndex, 1, rowIndex, 1];
                        var border54 = ranges54.Style.Border;
                        border54.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border54.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border54.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border54.Top.Color.SetColor(System.Drawing.Color.Black);
                        border54.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border54.Right.Color.SetColor(System.Drawing.Color.Black);
                        border54.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border54.Left.Color.SetColor(System.Drawing.Color.Black);


                        var rowStyle3 = worksheet.Cells[rowIndex, 2, rowIndex, 5];
                        rowStyle3.Style.Font.Bold = true;
                        rowStyle3.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle3.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle3.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle3.Value = "Descripcíon";
                        rowStyle3.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle3.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle3.Merge = true;
                        worksheet.Row(rowIndex).Height = 45.75;

                        // Establecer el contorno negro de las celdas
                        var ranges55 = worksheet.Cells[rowIndex, 2, rowIndex, 5];
                        var border55 = ranges55.Style.Border;
                        border55.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border55.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border55.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border55.Top.Color.SetColor(System.Drawing.Color.Black);
                        border55.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border55.Right.Color.SetColor(System.Drawing.Color.Black);
                        border55.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border55.Left.Color.SetColor(System.Drawing.Color.Black);



                        var rowStyle4 = worksheet.Cells[rowIndex, 6, rowIndex, 6];
                        rowStyle4.Style.Font.Bold = true;
                        rowStyle4.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle4.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle4.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle4.Value = "Fecha y Hora inicio" + Environment.NewLine + "(dd / mm / aaaa - hh:mm)";
                        rowStyle4.Style.WrapText = true;
                        rowStyle4.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle4.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle4.Merge = true;
                        worksheet.Row(rowIndex).Height = 45.75;


                        // Establecer el contorno negro de las celdas
                        var ranges56 = worksheet.Cells[rowIndex, 6, rowIndex, 6];
                        var border56 = ranges56.Style.Border;
                        border56.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border56.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border56.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border56.Top.Color.SetColor(System.Drawing.Color.Black);
                        border56.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border56.Right.Color.SetColor(System.Drawing.Color.Black);
                        border56.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border56.Left.Color.SetColor(System.Drawing.Color.Black);



                        var rowStyle5 = worksheet.Cells[rowIndex, 7, rowIndex, 7];
                        rowStyle5.Style.Font.Bold = true;
                        rowStyle5.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle5.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle5.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle5.Value = "Fecha y Hora Final" + Environment.NewLine + "(dd / mm / aaaa - hh:mm)";
                        rowStyle5.Style.WrapText = true;
                        rowStyle5.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle5.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle5.Merge = true;
                        worksheet.Row(rowIndex).Height = 45.75;

                        // Establecer el contorno negro de las celdas
                        var ranges57 = worksheet.Cells[rowIndex, 7, rowIndex, 7];
                        var border57 = ranges57.Style.Border;
                        border57.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border57.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border57.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border57.Top.Color.SetColor(System.Drawing.Color.Black);
                        border57.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border57.Right.Color.SetColor(System.Drawing.Color.Black);
                        border57.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border57.Left.Color.SetColor(System.Drawing.Color.Black);

                        var rowStyle6 = worksheet.Cells[rowIndex, 8, rowIndex, 8];
                        rowStyle6.Style.Font.Bold = true;
                        rowStyle6.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle6.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle6.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle6.Value = "Tiempo Ejecucíon";
                        rowStyle6.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle6.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle6.Merge = true;
                        worksheet.Row(rowIndex).Height = 45.75;

                        // Establecer el contorno negro de las celdas
                        var ranges58 = worksheet.Cells[rowIndex, 8, rowIndex, 8];
                        var border58 = ranges58.Style.Border;
                        border58.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border58.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border58.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border58.Top.Color.SetColor(System.Drawing.Color.Black);
                        border58.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border58.Right.Color.SetColor(System.Drawing.Color.Black);
                        border58.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border58.Left.Color.SetColor(System.Drawing.Color.Black);

                        var rowStyle7 = worksheet.Cells[rowIndex, 9, rowIndex, 9];
                        rowStyle7.Style.Font.Bold = true;
                        rowStyle7.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle7.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle7.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle7.Value = "Responsable" + Environment.NewLine + "(Nombre y Apellido)";
                        rowStyle7.Style.WrapText = true;
                        rowStyle7.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle7.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle7.Merge = true;
                        worksheet.Row(rowIndex).Height = 45.75;

                        // Establecer el contorno negro de las celdas
                        var ranges59 = worksheet.Cells[rowIndex, 9, rowIndex, 9];
                        var border59 = ranges59.Style.Border;
                        border59.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border59.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border59.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border59.Top.Color.SetColor(System.Drawing.Color.Black);
                        border59.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border59.Right.Color.SetColor(System.Drawing.Color.Black);
                        border59.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border59.Left.Color.SetColor(System.Drawing.Color.Black);


                        var rowStyle8 = worksheet.Cells[rowIndex, 10, rowIndex, 10];
                        rowStyle8.Style.Font.Bold = true;
                        rowStyle8.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle8.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle8.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle8.Value = "Área ó Proveedor";
                        rowStyle8.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle8.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle8.Merge = true;
                        worksheet.Row(rowIndex).Height = 45.75;

                        // Establecer el contorno negro de las celdas
                        var ranges60 = worksheet.Cells[rowIndex, 10, rowIndex, 10];
                        var border60 = ranges60.Style.Border;
                        border60.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border60.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border60.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border60.Top.Color.SetColor(System.Drawing.Color.Black);
                        border60.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border60.Right.Color.SetColor(System.Drawing.Color.Black);
                        border60.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border60.Left.Color.SetColor(System.Drawing.Color.Black);




                        var rowStyle9 = worksheet.Cells[rowIndex, 11, rowIndex, 11];
                        rowStyle9.Style.Font.Bold = true;
                        rowStyle9.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle9.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle9.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle9.Value = "SI";
                        rowStyle9.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle9.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle9.Merge = true;
                        worksheet.Row(rowIndex).Height = 45.75;

                        // Establecer el contorno negro de las celdas
                        var ranges61 = worksheet.Cells[rowIndex, 11, rowIndex, 11];
                        var border61 = ranges61.Style.Border;
                        border61.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border61.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border61.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border61.Top.Color.SetColor(System.Drawing.Color.Black);
                        border61.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border61.Right.Color.SetColor(System.Drawing.Color.Black);
                        border61.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border61.Left.Color.SetColor(System.Drawing.Color.Black);


                        var rowStyle10 = worksheet.Cells[rowIndex, 12, rowIndex, 12];
                        rowStyle10.Style.Font.Bold = true;
                        rowStyle10.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle10.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle10.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle10.Value = "NO";
                        rowStyle10.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle10.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle10.Merge = true;
                        worksheet.Row(rowIndex).Height = 45.75;


                        // Establecer el contorno negro de las celdas
                        var ranges62 = worksheet.Cells[rowIndex, 12, rowIndex, 12];
                        var border62 = ranges62.Style.Border;
                        border62.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border62.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border62.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border62.Top.Color.SetColor(System.Drawing.Color.Black);
                        border62.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border62.Right.Color.SetColor(System.Drawing.Color.Black);
                        border62.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border62.Left.Color.SetColor(System.Drawing.Color.Black);



                        var rowStyle11 = worksheet.Cells[rowIndex, 13, rowIndex, 13];
                        rowStyle11.Style.Font.Bold = true;
                        rowStyle11.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle11.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle11.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle11.Value = "ERROR";
                        rowStyle11.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle11.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle11.Merge = true;
                        worksheet.Row(rowIndex).Height = 45.75;


                        // Establecer el contorno negro de las celdas
                        var ranges63 = worksheet.Cells[rowIndex, 13, rowIndex, 13];
                        var border63 = ranges63.Style.Border;
                        border63.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border63.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border63.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border63.Top.Color.SetColor(System.Drawing.Color.Black);
                        border63.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border63.Right.Color.SetColor(System.Drawing.Color.Black);
                        border63.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border63.Left.Color.SetColor(System.Drawing.Color.Black);

                    }
                }


                // Agregar datos en filas

                int newRowCount7 = 1; // Número de nuevas filas a agregar

                for (int i = 0; i < newRowCount7; i++)
                {
                    int currentRowCount5 = worksheet.Dimension.Rows; // Obtener el número de filas actualmente en uso
                    int rowIndex3 = currentRowCount5 + i + 1;

                    // Verificar si la fila está vacía y aplicar estilos
                    if (string.IsNullOrWhiteSpace(worksheet.Cells[rowIndex3, 1].Text))
                    {
                        // Agrega una nueva fila con estilos definidos
                        var newRow = worksheet.Cells[currentRowCount5 + 1, 1, currentRowCount5 + 1, 1];
                        newRow.Style.Font.Bold = true;
                        newRow.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow1 = worksheet.Cells[currentRowCount5 + 1, 2, currentRowCount5 + 1, 5];
                        newRow1.Style.Font.Bold = true;
                        newRow1.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow1.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow1.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow1.Merge = true;


                        // Agrega una nueva fila con estilos definidos
                        var newRow2 = worksheet.Cells[currentRowCount5 + 1, 6, currentRowCount5 + 1, 6];
                        newRow2.Style.Font.Bold = true;
                        newRow2.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow2.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow2.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow2.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow2.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow2.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow3 = worksheet.Cells[currentRowCount5 + 1, 7, currentRowCount5 + 1, 7];
                        newRow3.Style.Font.Bold = true;
                        newRow3.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow3.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow3.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow3.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow3.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow3.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow4 = worksheet.Cells[currentRowCount5 + 1, 8, currentRowCount5 + 1, 8];
                        newRow4.Style.Font.Bold = true;
                        newRow4.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow4.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow4.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow4.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow4.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow4.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow5 = worksheet.Cells[currentRowCount5 + 1, 9, currentRowCount5 + 1, 9];
                        newRow5.Style.Font.Bold = true;
                        newRow5.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow5.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow5.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow5.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow5.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow5.Merge = true; // Combina las celdas


                        // Agrega una nueva fila con estilos definidos
                        var newRow6 = worksheet.Cells[currentRowCount5 + 1, 10, currentRowCount5 + 1, 10];
                        newRow6.Style.Font.Bold = true;
                        newRow6.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow6.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow6.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow6.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow6.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow6.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow7 = worksheet.Cells[currentRowCount5 + 1, 11, currentRowCount5 + 1, 11];
                        newRow7.Style.Font.Bold = true;
                        newRow7.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow7.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow7.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow7.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow7.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow7.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow8 = worksheet.Cells[currentRowCount5 + 1, 12, currentRowCount5 + 1, 12];
                        newRow8.Style.Font.Bold = true;
                        newRow8.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow8.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow8.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow8.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow8.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow8.Merge = true; // Combina las celdas


                        // Agrega una nueva fila con estilos definidos
                        var newRow9 = worksheet.Cells[currentRowCount5 + 1, 13, currentRowCount5 + 1, 13];
                        newRow9.Style.Font.Bold = true;
                        newRow9.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow9.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow9.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow9.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow9.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow9.Merge = true; // Combina las celdas

                        // Establecer el contorno negro de las celdas
                        var ranges64 = worksheet.Cells[currentRowCount5 + 1, 1, currentRowCount5 + 1, 9];
                        var border64 = ranges64.Style.Border;
                        border64.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border64.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border64.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border64.Top.Color.SetColor(System.Drawing.Color.Black);
                        border64.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border64.Right.Color.SetColor(System.Drawing.Color.Black);
                        border64.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border64.Left.Color.SetColor(System.Drawing.Color.Black);

                        // Establecer el contorno negro de las celdas
                        var ranges65 = worksheet.Cells[currentRowCount5 + 1, 10, currentRowCount5 + 1, 10];
                        var border65 = ranges65.Style.Border;
                        border65.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border65.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border65.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border65.Top.Color.SetColor(System.Drawing.Color.Black);
                        border65.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border65.Right.Color.SetColor(System.Drawing.Color.Black);
                        border65.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border65.Left.Color.SetColor(System.Drawing.Color.Black);

                        var ranges66 = worksheet.Cells[currentRowCount5 + 1, 11, currentRowCount5 + 1, 12];
                        var border66 = ranges66.Style.Border;
                        border66.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border66.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border66.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border66.Top.Color.SetColor(System.Drawing.Color.Black);
                        border66.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border66.Right.Color.SetColor(System.Drawing.Color.Black);
                        border66.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border66.Left.Color.SetColor(System.Drawing.Color.Black);


                        // Establecer el contorno negro de las celdas
                        var ranges67 = worksheet.Cells[currentRowCount5 + 1, 13, currentRowCount5 + 1, 13];
                        var border67 = ranges67.Style.Border;
                        border67.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border67.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border67.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border67.Top.Color.SetColor(System.Drawing.Color.Black);
                        border67.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border67.Right.Color.SetColor(System.Drawing.Color.Black);
                        border67.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border67.Left.Color.SetColor(System.Drawing.Color.Black);

                        if (i < Prerequisites.Count)
                        {
                            var Pre = Prerequisites[i];
                            

                            worksheet.Cells[currentRowCount5 + 1, 1, currentRowCount5 + 1, 1].Value = Pre.Sequence;
                            worksheet.Cells[currentRowCount5 + 1, 2, currentRowCount5 + 1, 5].Value = Pre.Description;
                            worksheet.Cells[currentRowCount5 + 1, 6, currentRowCount5 + 1, 6].Value = Pre.DataStart;
                            worksheet.Cells[currentRowCount5 + 1, 7, currentRowCount5 + 1, 7].Value = Pre.DataEnd;
                            worksheet.Cells[currentRowCount5 + 1, 8, currentRowCount5 + 1, 8].Value = Pre.ExecutionTime;
                            worksheet.Cells[currentRowCount5 + 1, 9, currentRowCount5 + 1, 9].Value = Pre.ResponsibleAreaID;
                            
                            // Insertar más datos en celdas específicas según tus necesidades
                        }
                    }

                 
                    /*
                    if (i < users.Count)
                    {
                        var user = users[i];

                        worksheet.Cells[currentRowCount5 + 1, 1, currentRowCount5 + 1, 2].Value = user.Position;
                        worksheet.Cells[currentRowCount5 + 1, 3, currentRowCount5 + 1, 5].Value = user.Name;

                        worksheet.Cells[currentRowCount5 + 1, 6, currentRowCount5 + 1, 6].Value = user.Phone;
                        worksheet.Cells[currentRowCount5 + 1, 7, currentRowCount5 + 1, 7].Value = user.Email;
                        // Insertar más datos en celdas específicas según tus necesidades

                    }
                    rowIndex3++; // Avanzar a la siguiente fila
                    */
                }


                //4.3

                int lastRowIndex7 = currentRowIndex;
                int newRowCount8 = 1;
                for (int j = 0; j < newRowCount8; j++)
                {

                    int currentRowCount6 = worksheet.Dimension.Rows; // Obtener el número de filas actualmente en uso
                    int rowIndex = currentRowCount6 + j + 1;
                    int currentCellsCount = worksheet.Dimension.End.Row;
                    int rowIndex1 = currentCellsCount + j + 1;
                    // Verificar si la fila está vacía y aplica estilos 
                    if (string.IsNullOrWhiteSpace(worksheet.Cells[rowIndex1, 1].Text))
                    {
                        var rowStyle = worksheet.Cells[currentRowCount6, 1, currentRowCount6, 10];
                        rowStyle.Style.Font.Bold = true;
                        rowStyle.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        rowStyle.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle.Value = "4.3.PLAN DE IMPLEMENTACION (Actividades que se realizan durante la ventana de tiempo) ";
                        rowStyle.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        rowStyle.Merge = true;



                        // Establecer el contorno negro de las celdas
                        var ranges52 = worksheet.Cells[currentRowCount6, 1, currentRowCount6, 10];
                        var border52 = ranges52.Style.Border;
                        border52.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border52.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border52.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border52.Top.Color.SetColor(System.Drawing.Color.Black);
                        border52.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border52.Right.Color.SetColor(System.Drawing.Color.Black);
                        border52.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border52.Left.Color.SetColor(System.Drawing.Color.Black);

                        var rowStyle1 = worksheet.Cells[currentRowCount6, 11, currentRowCount6, 13];
                        rowStyle1.Style.Font.Bold = true;
                        rowStyle1.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle1.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        rowStyle1.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle1.Value = "";
                        rowStyle1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        rowStyle1.Merge = true;



                        // Establecer el contorno negro de las celdas
                        var ranges70 = worksheet.Cells[currentRowCount6, 11, currentRowCount6, 13];
                        var border70 = ranges70.Style.Border;
                        border70.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border70.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border70.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border70.Top.Color.SetColor(System.Drawing.Color.Black);
                        border70.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border70.Right.Color.SetColor(System.Drawing.Color.Black);
                        border70.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border70.Left.Color.SetColor(System.Drawing.Color.Black);






                    }
                }




                // Agregar datos en filas

                int newRowCount9 = 8; // Número de nuevas filas a agregar

                for (int i = 0; i < newRowCount9; i++)
                {
                    int currentRowCount5 = worksheet.Dimension.Rows; // Obtener el número de filas actualmente en uso
                    int rowIndex3 = currentRowCount5 + i + 1;

                    // Verificar si la fila está vacía y aplicar estilos
                    if (string.IsNullOrWhiteSpace(worksheet.Cells[rowIndex3, 1].Text))
                    {
                        // Agrega una nueva fila con estilos definidos
                        var newRow = worksheet.Cells[currentRowCount5 + 1, 1, currentRowCount5 + 1, 1];
                        newRow.Style.Font.Bold = true;
                        newRow.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow1 = worksheet.Cells[currentRowCount5 + 1, 2, currentRowCount5 + 1, 5];
                        newRow1.Style.Font.Bold = true;
                        newRow1.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow1.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow1.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow1.Merge = true;


                        // Agrega una nueva fila con estilos definidos
                        var newRow2 = worksheet.Cells[currentRowCount5 + 1, 6, currentRowCount5 + 1, 6];
                        newRow2.Style.Font.Bold = true;
                        newRow2.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow2.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow2.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow2.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow2.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow2.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow3 = worksheet.Cells[currentRowCount5 + 1, 7, currentRowCount5 + 1, 7];
                        newRow3.Style.Font.Bold = true;
                        newRow3.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow3.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow3.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow3.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow3.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow3.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow4 = worksheet.Cells[currentRowCount5 + 1, 8, currentRowCount5 + 1, 8];
                        newRow4.Style.Font.Bold = true;
                        newRow4.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow4.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow4.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow4.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow4.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow4.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow5 = worksheet.Cells[currentRowCount5 + 1, 9, currentRowCount5 + 1, 9];
                        newRow5.Style.Font.Bold = true;
                        newRow5.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow5.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow5.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow5.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow5.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow5.Merge = true; // Combina las celdas


                        // Agrega una nueva fila con estilos definidos
                        var newRow6 = worksheet.Cells[currentRowCount5 + 1, 10, currentRowCount5 + 1, 10];
                        newRow6.Style.Font.Bold = true;
                        newRow6.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow6.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow6.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow6.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow6.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow6.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow7 = worksheet.Cells[currentRowCount5 + 1, 11, currentRowCount5 + 1, 11];
                        newRow7.Style.Font.Bold = true;
                        newRow7.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow7.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow7.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow7.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow7.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow7.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow8 = worksheet.Cells[currentRowCount5 + 1, 12, currentRowCount5 + 1, 12];
                        newRow8.Style.Font.Bold = true;
                        newRow8.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow8.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow8.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow8.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow8.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow8.Merge = true; // Combina las celdas


                        // Agrega una nueva fila con estilos definidos
                        var newRow9 = worksheet.Cells[currentRowCount5 + 1, 13, currentRowCount5 + 1, 13];
                        newRow9.Style.Font.Bold = true;
                        newRow9.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow9.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow9.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow9.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow9.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow9.Merge = true; // Combina las celdas

                        // Establecer el contorno negro de las celdas
                        var ranges64 = worksheet.Cells[currentRowCount5 + 1, 1, currentRowCount5 + 1, 9];
                        var border64 = ranges64.Style.Border;
                        border64.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border64.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border64.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border64.Top.Color.SetColor(System.Drawing.Color.Black);
                        border64.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border64.Right.Color.SetColor(System.Drawing.Color.Black);
                        border64.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border64.Left.Color.SetColor(System.Drawing.Color.Black);

                        // Establecer el contorno negro de las celdas
                        var ranges65 = worksheet.Cells[currentRowCount5 + 1, 10, currentRowCount5 + 1, 10];
                        var border65 = ranges65.Style.Border;
                        border65.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border65.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border65.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border65.Top.Color.SetColor(System.Drawing.Color.Black);
                        border65.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border65.Right.Color.SetColor(System.Drawing.Color.Black);
                        border65.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border65.Left.Color.SetColor(System.Drawing.Color.Black);

                        var ranges66 = worksheet.Cells[currentRowCount5 + 1, 11, currentRowCount5 + 1, 12];
                        var border66 = ranges66.Style.Border;
                        border66.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border66.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border66.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border66.Top.Color.SetColor(System.Drawing.Color.Black);
                        border66.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border66.Right.Color.SetColor(System.Drawing.Color.Black);
                        border66.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border66.Left.Color.SetColor(System.Drawing.Color.Black);


                        // Establecer el contorno negro de las celdas
                        var ranges67 = worksheet.Cells[currentRowCount5 + 1, 13, currentRowCount5 + 1, 13];
                        var border67 = ranges67.Style.Border;
                        border67.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border67.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border67.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border67.Top.Color.SetColor(System.Drawing.Color.Black);
                        border67.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border67.Right.Color.SetColor(System.Drawing.Color.Black);
                        border67.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border67.Left.Color.SetColor(System.Drawing.Color.Black);


                    }

                    if (i < plans.Count)
                    {
                        var training = plans[i];
                 

                        worksheet.Cells[currentRowCount5 + 1, 1, currentRowCount5 + 1, 1].Value = training.Sequence;
                        worksheet.Cells[currentRowCount5 + 1, 2, currentRowCount5 + 1, 2].Value = training.Description;
                        worksheet.Cells[currentRowCount5 + 1, 3, currentRowCount5 + 1, 4].Value = training.DataStartTime;
                        worksheet.Cells[currentRowCount5 + 1, 5, currentRowCount5 + 1, 6].Value = training.DataEndTime;
                        worksheet.Cells[currentRowCount5 + 1, 7, currentRowCount5 + 1, 7].Value = training.ExecutionTime;
                        worksheet.Cells[currentRowCount5 + 1, 8, currentRowCount5 + 1, 8].Value = training.ResponsibleAreaID;
                        
                        // Insertar más datos en celdas específicas según tus necesidades
                    }
                    /*
                    if (i < users.Count)
                    {
                        var user = users[i];

                        worksheet.Cells[currentRowCount5 + 1, 1, currentRowCount5 + 1, 2].Value = user.Position;
                        worksheet.Cells[currentRowCount5 + 1, 3, currentRowCount5 + 1, 5].Value = user.Name;

                        worksheet.Cells[currentRowCount5 + 1, 6, currentRowCount5 + 1, 6].Value = user.Phone;
                        worksheet.Cells[currentRowCount5 + 1, 7, currentRowCount5 + 1, 7].Value = user.Email;
                        // Insertar más datos en celdas específicas según tus necesidades

                    }
                    rowIndex3++; // Avanzar a la siguiente fila
                    */
                }


                //postimplantacion
                int lastRowIndex8 = currentRowIndex;
                int newRowCount10 = 1;
                for (int j = 0; j < newRowCount10; j++)
                {

                    int currentRowCount6 = worksheet.Dimension.Rows; // Obtener el número de filas actualmente en uso
                    int rowIndex = currentRowCount6 + j + 1;
                    int currentCellsCount = worksheet.Dimension.End.Row;
                    int rowIndex1 = currentCellsCount + j + 1;
                    // Verificar si la fila está vacía y aplica estilos 
                    if (string.IsNullOrWhiteSpace(worksheet.Cells[rowIndex1, 1].Text))
                    {
                        var rowStyle = worksheet.Cells[currentRowCount6, 1, currentRowCount6, 10];
                        rowStyle.Style.Font.Bold = true;
                        rowStyle.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        rowStyle.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle.Value = "4.4.PRERREQUISITOS POSTIMPLANTACION (Actividades de seguimiento y/o de normalización) ";
                        rowStyle.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        rowStyle.Merge = true;
                        worksheet.Row(rowIndex).Height = 12;


                        // Establecer el contorno negro de las celdas
                        var ranges74 = worksheet.Cells[currentRowCount6, 1, currentRowCount6, 10];
                        var border74 = ranges74.Style.Border;
                        border74.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border74.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border74.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border74.Top.Color.SetColor(System.Drawing.Color.Black);
                        border74.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border74.Right.Color.SetColor(System.Drawing.Color.Black);
                        border74.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border74.Left.Color.SetColor(System.Drawing.Color.Black);


                        var rowStyle1 = worksheet.Cells[currentRowCount6, 11, currentRowCount6, 13];
                        rowStyle1.Style.Font.Bold = true;
                        rowStyle1.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle1.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        rowStyle1.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle1.Value = "";
                        rowStyle1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        rowStyle1.Merge = true;



                        // Establecer el contorno negro de las celdas
                        var ranges75 = worksheet.Cells[currentRowCount6, 11, currentRowCount6, 13];
                        var border75 = ranges75.Style.Border;
                        border75.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border75.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Top.Color.SetColor(System.Drawing.Color.Black);
                        border75.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Right.Color.SetColor(System.Drawing.Color.Black);
                        border75.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Left.Color.SetColor(System.Drawing.Color.Black);

                        var rowStyle2 = worksheet.Cells[rowIndex, 1, rowIndex, 1];
                        rowStyle2.Style.Font.Bold = true;
                        rowStyle2.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle2.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle2.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                        rowStyle2.Value = "Secuencia";
                        rowStyle2.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle2.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle2.Merge = true;
                        worksheet.Row(rowIndex).Height = 57.75;

                        // Establecer el contorno negro de las celdas
                        var ranges76 = worksheet.Cells[rowIndex, 1, rowIndex, 1];
                        var border76 = ranges76.Style.Border;
                        border76.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border76.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border76.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border76.Top.Color.SetColor(System.Drawing.Color.Black);
                        border76.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border76.Right.Color.SetColor(System.Drawing.Color.Black);
                        border76.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border76.Left.Color.SetColor(System.Drawing.Color.Black);

                        var rowStyle3 = worksheet.Cells[rowIndex, 2, rowIndex, 5];
                        rowStyle3.Style.Font.Bold = true;
                        rowStyle3.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle3.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle3.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                        rowStyle3.Value = "Descripcíon";
                        rowStyle3.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle3.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle3.Merge = true;
                        worksheet.Row(rowIndex).Height = 57.75;



                        // Establecer el contorno negro de las celdas
                        var ranges77 = worksheet.Cells[rowIndex, 2, rowIndex, 5];
                        var border77 = ranges77.Style.Border;
                        border77.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border77.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border77.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border77.Top.Color.SetColor(System.Drawing.Color.Black);
                        border77.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border77.Right.Color.SetColor(System.Drawing.Color.Black);
                        border77.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border77.Left.Color.SetColor(System.Drawing.Color.Black);


                        var rowStyle4 = worksheet.Cells[rowIndex, 6, rowIndex, 6];
                        rowStyle4.Style.Font.Bold = true;
                        rowStyle4.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle4.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle4.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                        rowStyle4.Value = "Área ó Proveedor";
                        rowStyle4.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle4.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle4.Merge = true;
                        worksheet.Row(rowIndex).Height = 57.75;

                        // Establecer el contorno negro de las celdas
                        var ranges78 = worksheet.Cells[rowIndex, 6, rowIndex, 6];
                        var border78 = ranges78.Style.Border;
                        border78.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border78.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border78.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border78.Top.Color.SetColor(System.Drawing.Color.Black);
                        border78.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border78.Right.Color.SetColor(System.Drawing.Color.Black);
                        border78.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border78.Left.Color.SetColor(System.Drawing.Color.Black);

                        var rowStyle5 = worksheet.Cells[rowIndex, 7, rowIndex, 8];
                        rowStyle5.Style.Font.Bold = true;
                        rowStyle5.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle5.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle5.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                        rowStyle5.Value = "Responsable (Nombre y Apellido) ";
                        rowStyle5.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle5.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle5.Merge = true;
                        worksheet.Row(rowIndex).Height = 57.75;


                        // Establecer el contorno negro de las celdas
                        var ranges79 = worksheet.Cells[rowIndex, 7, rowIndex, 8];
                        var border79 = ranges79.Style.Border;
                        border79.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border79.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border79.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border79.Top.Color.SetColor(System.Drawing.Color.Black);
                        border79.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border79.Right.Color.SetColor(System.Drawing.Color.Black);
                        border79.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border79.Left.Color.SetColor(System.Drawing.Color.Black);

                        var rowStyle6 = worksheet.Cells[rowIndex, 9, rowIndex, 9];
                        rowStyle6.Style.Font.Bold = true;
                        rowStyle6.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle6.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle6.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                        rowStyle6.Value = "Fecha y Hora inicio" + Environment.NewLine + "(dd / mm / aaaa - hh:mm)";
                        rowStyle6.Style.WrapText = true;
                        rowStyle6.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle6.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle6.Merge = true;
                        worksheet.Row(rowIndex).Height = 57.75;



                        // Establecer el contorno negro de las celdas
                        var ranges80 = worksheet.Cells[rowIndex, 9, rowIndex, 9];
                        var border80 = ranges80.Style.Border;
                        border80.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border80.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border80.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border80.Top.Color.SetColor(System.Drawing.Color.Black);
                        border80.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border80.Right.Color.SetColor(System.Drawing.Color.Black);
                        border80.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border80.Left.Color.SetColor(System.Drawing.Color.Black);


                        var rowStyle7 = worksheet.Cells[rowIndex, 10, rowIndex, 10];
                        rowStyle7.Style.Font.Bold = true;
                        rowStyle7.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle7.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle7.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                        rowStyle7.Value = "Fecha y Hora Final" + Environment.NewLine + "(dd / mm / aaaa - hh:mm)";
                        rowStyle7.Style.WrapText = true;
                        rowStyle7.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle7.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle7.Merge = true;
                        worksheet.Row(rowIndex).Height = 57.75;

                        // Establecer el contorno negro de las celdas
                        var ranges81 = worksheet.Cells[rowIndex, 10, rowIndex, 10];
                        var border81 = ranges81.Style.Border;
                        border81.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border81.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border81.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border81.Top.Color.SetColor(System.Drawing.Color.Black);
                        border81.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border81.Right.Color.SetColor(System.Drawing.Color.Black);
                        border81.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border81.Left.Color.SetColor(System.Drawing.Color.Black);



                        var rowStyle8 = worksheet.Cells[rowIndex, 11, rowIndex, 13];
                        rowStyle8.Style.Font.Bold = true;
                        rowStyle8.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle8.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        rowStyle8.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                        rowStyle8.Value = "";
                        rowStyle8.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle8.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        rowStyle8.Merge = true;

                        // Establecer el contorno negro de las celdas
                        var ranges98 = worksheet.Cells[rowIndex, 11, rowIndex, 13];
                        var border98 = ranges98.Style.Border;
                        border98.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border98.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border98.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border98.Top.Color.SetColor(System.Drawing.Color.Black);
                        border98.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border98.Right.Color.SetColor(System.Drawing.Color.Black);
                        border98.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border98.Left.Color.SetColor(System.Drawing.Color.Black);


                    }
                }

                // Agregar datos en filas

                int newRowCount11 = 3; // Número de nuevas filas a agregar

                for (int i = 0; i < newRowCount11; i++)
                {
                    int currentRowCount5 = worksheet.Dimension.Rows; // Obtener el número de filas actualmente en uso
                    int rowIndex3 = currentRowCount5 + i + 1;

                    // Verificar si la fila está vacía y aplicar estilos
                    if (string.IsNullOrWhiteSpace(worksheet.Cells[rowIndex3, 1].Text))
                    {
                        // Agrega una nueva fila con estilos definidos
                        var newRow = worksheet.Cells[currentRowCount5 + 1, 1, currentRowCount5 + 1, 1];
                        newRow.Style.Font.Bold = true;
                        newRow.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow1 = worksheet.Cells[currentRowCount5 + 1, 2, currentRowCount5 + 1, 5];
                        newRow1.Style.Font.Bold = true;
                        newRow1.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow1.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow1.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow1.Merge = true;


                        // Agrega una nueva fila con estilos definidos
                        var newRow2 = worksheet.Cells[currentRowCount5 + 1, 6, currentRowCount5 + 1, 6];
                        newRow2.Style.Font.Bold = true;
                        newRow2.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow2.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow2.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow2.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow2.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow2.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow3 = worksheet.Cells[currentRowCount5 + 1, 7, currentRowCount5 + 1, 8];
                        newRow3.Style.Font.Bold = true;
                        newRow3.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow3.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow3.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow3.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow3.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow3.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow4 = worksheet.Cells[currentRowCount5 + 1, 9, currentRowCount5 + 1, 9];
                        newRow4.Style.Font.Bold = true;
                        newRow4.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow4.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow4.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow4.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow4.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow4.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow5 = worksheet.Cells[currentRowCount5 + 1, 9, currentRowCount5 + 1, 9];
                        newRow5.Style.Font.Bold = true;
                        newRow5.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow5.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow5.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow5.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow5.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow5.Merge = true; // Combina las celdas


                        // Agrega una nueva fila con estilos definidos
                        var newRow6 = worksheet.Cells[currentRowCount5 + 1, 10, currentRowCount5 + 1, 10];
                        newRow6.Style.Font.Bold = true;
                        newRow6.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow6.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow6.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow6.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow6.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow6.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow7 = worksheet.Cells[currentRowCount5 + 1, 11, currentRowCount5 + 1, 11];
                        newRow7.Style.Font.Bold = true;
                        newRow7.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow7.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow7.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow7.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow7.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow7.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow8 = worksheet.Cells[currentRowCount5 + 1, 12, currentRowCount5 + 1, 12];
                        newRow8.Style.Font.Bold = true;
                        newRow8.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow8.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow8.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow8.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow8.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow8.Merge = true; // Combina las celdas


                        // Agrega una nueva fila con estilos definidos
                        var newRow9 = worksheet.Cells[currentRowCount5 + 1, 13, currentRowCount5 + 1, 13];
                        newRow9.Style.Font.Bold = true;
                        newRow9.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow9.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow9.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow9.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow9.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow9.Merge = true; // Combina las celdas

                        // Establecer el contorno negro de las celdas
                        var ranges64 = worksheet.Cells[currentRowCount5 + 1, 1, currentRowCount5 + 1, 9];
                        var border64 = ranges64.Style.Border;
                        border64.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border64.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border64.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border64.Top.Color.SetColor(System.Drawing.Color.Black);
                        border64.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border64.Right.Color.SetColor(System.Drawing.Color.Black);
                        border64.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border64.Left.Color.SetColor(System.Drawing.Color.Black);

                        // Establecer el contorno negro de las celdas
                        var ranges65 = worksheet.Cells[currentRowCount5 + 1, 10, currentRowCount5 + 1, 10];
                        var border65 = ranges65.Style.Border;
                        border65.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border65.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border65.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border65.Top.Color.SetColor(System.Drawing.Color.Black);
                        border65.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border65.Right.Color.SetColor(System.Drawing.Color.Black);
                        border65.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border65.Left.Color.SetColor(System.Drawing.Color.Black);

                        var ranges66 = worksheet.Cells[currentRowCount5 + 1, 11, currentRowCount5 + 1, 12];
                        var border66 = ranges66.Style.Border;
                        border66.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border66.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border66.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border66.Top.Color.SetColor(System.Drawing.Color.Black);
                        border66.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border66.Right.Color.SetColor(System.Drawing.Color.Black);
                        border66.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border66.Left.Color.SetColor(System.Drawing.Color.Black);


                        // Establecer el contorno negro de las celdas
                        var ranges67 = worksheet.Cells[currentRowCount5 + 1, 13, currentRowCount5 + 1, 13];
                        var border67 = ranges67.Style.Border;
                        border67.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border67.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border67.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border67.Top.Color.SetColor(System.Drawing.Color.Black);
                        border67.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border67.Right.Color.SetColor(System.Drawing.Color.Black);
                        border67.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border67.Left.Color.SetColor(System.Drawing.Color.Black);   


                        if (i < postimplantacions.Count)
                        {
                            var pos = postimplantacions[i];
                            var user1 = pos.User;
                            
                            worksheet.Cells[currentRowCount5 + 1, 1, currentRowCount5 + 1, 1].Value = pos.Sequence;
                            worksheet.Cells[currentRowCount5 + 1, 2, currentRowCount5 + 1, 5].Value = pos.Description;
                            worksheet.Cells[currentRowCount5 + 1, 6, currentRowCount5 + 1, 6].Value = user1.Area;
                            worksheet.Cells[currentRowCount5 + 1, 7, currentRowCount5 + 1, 8].Value = user1.Name;
                            worksheet.Cells[currentRowCount5 + 1, 9, currentRowCount5 + 1, 9].Value = pos.DataStartTime;
                            worksheet.Cells[currentRowCount5 + 1, 10, currentRowCount5 + 1, 10].Value = pos.DataEndTime;
                        
                            // Insertar más datos en celdas específicas según tus necesidades
                        }
                    }

                    /*
                    if (i < users.Count)
                    {
                        var user = users[i];

                        worksheet.Cells[currentRowCount5 + 1, 1, currentRowCount5 + 1, 2].Value = user.Position;
                        worksheet.Cells[currentRowCount5 + 1, 3, currentRowCount5 + 1, 5].Value = user.Name;

                        worksheet.Cells[currentRowCount5 + 1, 6, currentRowCount5 + 1, 6].Value = user.Phone;
                        worksheet.Cells[currentRowCount5 + 1, 7, currentRowCount5 + 1, 7].Value = user.Email;
                        // Insertar más datos en celdas específicas según tus necesidades

                    }
                    rowIndex3++; // Avanzar a la siguiente fila
                    */
                }


                //4.5
                //postimplantacion
                int lastRowIndex9 = currentRowIndex;
                int newRowCount12 = 1;
                for (int j = 0; j < newRowCount12; j++)
                {

                    int currentRowCount6 = worksheet.Dimension.Rows; // Obtener el número de filas actualmente en uso
                    int rowIndex = currentRowCount6 + j + 1;
                    int currentCellsCount = worksheet.Dimension.End.Row;
                    int rowIndex1 = currentCellsCount + j + 1;
                    // Verificar si la fila está vacía y aplica estilos 
                    if (string.IsNullOrWhiteSpace(worksheet.Cells[rowIndex1, 1].Text))
                    {
                        var rowStyle = worksheet.Cells[currentRowCount6, 1, currentRowCount6, 10];
                        rowStyle.Style.Font.Bold = true;
                        rowStyle.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        rowStyle.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle.Value = "4.5. DETALLE USUARIOS FUNCIONALES (PRUEBAS POSTIMPLANTACION)";
                        rowStyle.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        rowStyle.Merge = true;
                        worksheet.Row(rowIndex).Height = 12;


                        // Establecer el contorno negro de las celdas
                        var ranges74 = worksheet.Cells[currentRowCount6, 1, currentRowCount6, 10];
                        var border74 = ranges74.Style.Border;
                        border74.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border74.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border74.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border74.Top.Color.SetColor(System.Drawing.Color.Black);
                        border74.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border74.Right.Color.SetColor(System.Drawing.Color.Black);
                        border74.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border74.Left.Color.SetColor(System.Drawing.Color.Black);


                        var rowStyle1 = worksheet.Cells[currentRowCount6, 11, currentRowCount6, 13];
                        rowStyle1.Style.Font.Bold = true;
                        rowStyle1.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle1.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        rowStyle1.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle1.Value = "";
                        rowStyle1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        rowStyle1.Merge = true;



                        // Establecer el contorno negro de las celdas
                        var ranges75 = worksheet.Cells[currentRowCount6, 11, currentRowCount6, 13];
                        var border75 = ranges75.Style.Border;
                        border75.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border75.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Top.Color.SetColor(System.Drawing.Color.Black);
                        border75.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Right.Color.SetColor(System.Drawing.Color.Black);
                        border75.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Left.Color.SetColor(System.Drawing.Color.Black);

                        var rowStyle2 = worksheet.Cells[rowIndex, 1, rowIndex, 1];
                        rowStyle2.Style.Font.Bold = true;
                        rowStyle2.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle2.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle2.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                        rowStyle2.Value = "Secuencia";
                        rowStyle2.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle2.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle2.Merge = true;
                        worksheet.Row(rowIndex).Height = 57.75;

                        // Establecer el contorno negro de las celdas
                        var ranges76 = worksheet.Cells[rowIndex, 1, rowIndex, 1];
                        var border76 = ranges76.Style.Border;
                        border76.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border76.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border76.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border76.Top.Color.SetColor(System.Drawing.Color.Black);
                        border76.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border76.Right.Color.SetColor(System.Drawing.Color.Black);
                        border76.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border76.Left.Color.SetColor(System.Drawing.Color.Black);

                        var rowStyle3 = worksheet.Cells[rowIndex, 2, rowIndex, 5];
                        rowStyle3.Style.Font.Bold = true;
                        rowStyle3.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle3.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle3.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                        rowStyle3.Value = " Funcionario" + Environment.NewLine + "(Nombre y Apellido)";
                        rowStyle3.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle3.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle3.Merge = true;
                        worksheet.Row(rowIndex).Height = 57.75;



                        // Establecer el contorno negro de las celdas
                        var ranges77 = worksheet.Cells[rowIndex, 2, rowIndex, 5];
                        var border77 = ranges77.Style.Border;
                        border77.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border77.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border77.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border77.Top.Color.SetColor(System.Drawing.Color.Black);
                        border77.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border77.Right.Color.SetColor(System.Drawing.Color.Black);
                        border77.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border77.Left.Color.SetColor(System.Drawing.Color.Black);


                        var rowStyle4 = worksheet.Cells[rowIndex, 6, rowIndex, 6];
                        rowStyle4.Style.Font.Bold = true;
                        rowStyle4.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle4.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle4.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                        rowStyle4.Value = "Área";
                        rowStyle4.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle4.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle4.Merge = true;
                        worksheet.Row(rowIndex).Height = 57.75;

                        // Establecer el contorno negro de las celdas
                        var ranges78 = worksheet.Cells[rowIndex, 6, rowIndex, 6];
                        var border78 = ranges78.Style.Border;
                        border78.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border78.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border78.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border78.Top.Color.SetColor(System.Drawing.Color.Black);
                        border78.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border78.Right.Color.SetColor(System.Drawing.Color.Black);
                        border78.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border78.Left.Color.SetColor(System.Drawing.Color.Black);

                        var rowStyle5 = worksheet.Cells[rowIndex, 7, rowIndex, 7];
                        rowStyle5.Style.Font.Bold = true;
                        rowStyle5.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle5.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle5.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                        rowStyle5.Value = "Usuario de Ingreso ";
                        rowStyle5.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle5.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle5.Merge = true;
                        worksheet.Row(rowIndex).Height = 57.75;


                        // Establecer el contorno negro de las celdas
                        var ranges79 = worksheet.Cells[rowIndex, 7, rowIndex, 7];
                        var border79 = ranges79.Style.Border;
                        border79.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border79.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border79.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border79.Top.Color.SetColor(System.Drawing.Color.Black);
                        border79.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border79.Right.Color.SetColor(System.Drawing.Color.Black);
                        border79.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border79.Left.Color.SetColor(System.Drawing.Color.Black);

                        var rowStyle10 = worksheet.Cells[rowIndex, 8, rowIndex, 8];
                        rowStyle10.Style.Font.Bold = true;
                        rowStyle10.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle10.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle10.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                        rowStyle10.Value = "Teléfono" + Environment.NewLine + "Contacto  (Ext.)";
                        rowStyle10.Style.WrapText = true;
                        rowStyle10.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle10.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle10.Merge = true;
                        worksheet.Row(rowIndex).Height = 57.75;


                        // Establecer el contorno negro de las celdas
                        var ranges80 = worksheet.Cells[rowIndex, 8, rowIndex, 8];
                        var border80 = ranges79.Style.Border;
                        border80.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border80.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border80.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border80.Top.Color.SetColor(System.Drawing.Color.Black);
                        border80.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border80.Right.Color.SetColor(System.Drawing.Color.Black);
                        border80.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border80.Left.Color.SetColor(System.Drawing.Color.Black);


                        var rowStyle6 = worksheet.Cells[rowIndex, 9, rowIndex, 9];
                        rowStyle6.Style.Font.Bold = true;
                        rowStyle6.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle6.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle6.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                        rowStyle6.Value = "Fecha y Hora inicio" + Environment.NewLine + "(dd / mm / aaaa - hh:mm)";
                        rowStyle6.Style.WrapText = true;
                        rowStyle6.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle6.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle6.Merge = true;
                        worksheet.Row(rowIndex).Height = 57.75;



                        // Establecer el contorno negro de las celdas
                        var ranges82 = worksheet.Cells[rowIndex, 9, rowIndex, 9];
                        var border82 = ranges80.Style.Border;
                        border82.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border82.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border82.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border82.Top.Color.SetColor(System.Drawing.Color.Black);
                        border82.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border82.Right.Color.SetColor(System.Drawing.Color.Black);
                        border82.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border82.Left.Color.SetColor(System.Drawing.Color.Black);


                        var rowStyle7 = worksheet.Cells[rowIndex, 10, rowIndex, 10];
                        rowStyle7.Style.Font.Bold = true;
                        rowStyle7.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle7.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle7.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                        rowStyle7.Value = "Fecha y Hora Final" + Environment.NewLine + "(dd / mm / aaaa - hh:mm)";
                        rowStyle7.Style.WrapText = true;
                        rowStyle7.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle7.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rowStyle7.Merge = true;
                        worksheet.Row(rowIndex).Height = 57.75;

                        // Establecer el contorno negro de las celdas
                        var ranges81 = worksheet.Cells[rowIndex, 10, rowIndex, 10];
                        var border81 = ranges81.Style.Border;
                        border81.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border81.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border81.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border81.Top.Color.SetColor(System.Drawing.Color.Black);
                        border81.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border81.Right.Color.SetColor(System.Drawing.Color.Black);
                        border81.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border81.Left.Color.SetColor(System.Drawing.Color.Black);



                        var rowStyle8 = worksheet.Cells[rowIndex, 11, rowIndex, 13];
                        rowStyle8.Style.Font.Bold = true;
                        rowStyle8.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle8.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        rowStyle8.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                        rowStyle8.Value = "";
                        rowStyle8.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle8.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        rowStyle8.Merge = true;

                        // Establecer el contorno negro de las celdas
                        var ranges98 = worksheet.Cells[rowIndex, 11, rowIndex, 13];
                        var border98 = ranges98.Style.Border;
                        border98.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border98.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border98.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border98.Top.Color.SetColor(System.Drawing.Color.Black);
                        border98.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border98.Right.Color.SetColor(System.Drawing.Color.Black);
                        border98.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border98.Left.Color.SetColor(System.Drawing.Color.Black);


                    }
                }
                // Agregar datos en filas

                int newRowCount13 = 3; // Número de nuevas filas a agregar

                for (int i = 0; i < newRowCount13; i++)
                {
                    int currentRowCount5 = worksheet.Dimension.Rows; // Obtener el número de filas actualmente en uso
                    int rowIndex3 = currentRowCount5 + i + 1;

                    // Verificar si la fila está vacía y aplicar estilos
                    if (string.IsNullOrWhiteSpace(worksheet.Cells[rowIndex3, 1].Text))
                    {
                        // Agrega una nueva fila con estilos definidos
                        var newRow = worksheet.Cells[currentRowCount5 + 1, 1, currentRowCount5 + 1, 1];
                        newRow.Style.Font.Bold = true;
                        newRow.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow1 = worksheet.Cells[currentRowCount5 + 1, 2, currentRowCount5 + 1, 5];
                        newRow1.Style.Font.Bold = true;
                        newRow1.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow1.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow1.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow1.Merge = true;


                        // Agrega una nueva fila con estilos definidos
                        var newRow2 = worksheet.Cells[currentRowCount5 + 1, 6, currentRowCount5 + 1, 6];
                        newRow2.Style.Font.Bold = true;
                        newRow2.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow2.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow2.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow2.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow2.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow2.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow3 = worksheet.Cells[currentRowCount5 + 1, 7, currentRowCount5 + 1, 7];
                        newRow3.Style.Font.Bold = true;
                        newRow3.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow3.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow3.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow3.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow3.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow3.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRowI = worksheet.Cells[currentRowCount5 + 1, 8, currentRowCount5 + 1, 8];
                        newRowI.Style.Font.Bold = true;
                        newRowI.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRowI.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRowI.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRowI.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRowI.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRowI.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow4 = worksheet.Cells[currentRowCount5 + 1, 9, currentRowCount5 + 1, 9];
                        newRow4.Style.Font.Bold = true;
                        newRow4.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow4.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow4.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow4.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow4.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow4.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow5 = worksheet.Cells[currentRowCount5 + 1, 9, currentRowCount5 + 1, 9];
                        newRow5.Style.Font.Bold = true;
                        newRow5.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow5.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow5.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow5.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow5.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow5.Merge = true; // Combina las celdas


                        // Agrega una nueva fila con estilos definidos
                        var newRow6 = worksheet.Cells[currentRowCount5 + 1, 10, currentRowCount5 + 1, 10];
                        newRow6.Style.Font.Bold = true;
                        newRow6.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow6.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow6.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow6.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow6.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow6.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow7 = worksheet.Cells[currentRowCount5 + 1, 11, currentRowCount5 + 1, 11];
                        newRow7.Style.Font.Bold = true;
                        newRow7.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow7.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow7.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow7.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow7.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow7.Merge = true; // Combina las celdas

                        // Agrega una nueva fila con estilos definidos
                        var newRow8 = worksheet.Cells[currentRowCount5 + 1, 12, currentRowCount5 + 1, 12];
                        newRow8.Style.Font.Bold = true;
                        newRow8.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow8.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow8.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow8.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow8.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow8.Merge = true; // Combina las celdas


                        // Agrega una nueva fila con estilos definidos
                        var newRow9 = worksheet.Cells[currentRowCount5 + 1, 13, currentRowCount5 + 1, 13];
                        newRow9.Style.Font.Bold = true;
                        newRow9.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow9.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow9.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow9.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow9.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow9.Merge = true; // Combina las celdas

                        // Establecer el contorno negro de las celdas
                        var ranges64 = worksheet.Cells[currentRowCount5 + 1, 1, currentRowCount5 + 1, 9];
                        var border64 = ranges64.Style.Border;
                        border64.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border64.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border64.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border64.Top.Color.SetColor(System.Drawing.Color.Black);
                        border64.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border64.Right.Color.SetColor(System.Drawing.Color.Black);
                        border64.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border64.Left.Color.SetColor(System.Drawing.Color.Black);

                        // Establecer el contorno negro de las celdas
                        var ranges65 = worksheet.Cells[currentRowCount5 + 1, 10, currentRowCount5 + 1, 10];
                        var border65 = ranges65.Style.Border;
                        border65.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border65.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border65.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border65.Top.Color.SetColor(System.Drawing.Color.Black);
                        border65.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border65.Right.Color.SetColor(System.Drawing.Color.Black);
                        border65.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border65.Left.Color.SetColor(System.Drawing.Color.Black);

                        var ranges66 = worksheet.Cells[currentRowCount5 + 1, 11, currentRowCount5 + 1, 12];
                        var border66 = ranges66.Style.Border;
                        border66.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border66.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border66.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border66.Top.Color.SetColor(System.Drawing.Color.Black);
                        border66.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border66.Right.Color.SetColor(System.Drawing.Color.Black);
                        border66.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border66.Left.Color.SetColor(System.Drawing.Color.Black);


                        // Establecer el contorno negro de las celdas
                        var ranges67 = worksheet.Cells[currentRowCount5 + 1, 13, currentRowCount5 + 1, 13];
                        var border67 = ranges67.Style.Border;
                        border67.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border67.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border67.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border67.Top.Color.SetColor(System.Drawing.Color.Black);
                        border67.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border67.Right.Color.SetColor(System.Drawing.Color.Black);
                        border67.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border67.Left.Color.SetColor(System.Drawing.Color.Black);


                    }

                    if (i < functionalUsers.Count)
                    {
                        var Func = functionalUsers[i];
                        var user1 = Func.User;
                      

                        worksheet.Cells[currentRowCount5 + 1, 1, currentRowCount5 + 1, 1].Value = Func.Sequence;
                        worksheet.Cells[currentRowCount5 + 1, 2, currentRowCount5 + 1, 5].Value = user1.Name;
                        worksheet.Cells[currentRowCount5 + 1, 6, currentRowCount5 + 1, 6].Value = user1.Area;
                        worksheet.Cells[currentRowCount5 + 1, 7, currentRowCount5 + 1, 7].Value = user1.NetworkUser;
                        worksheet.Cells[currentRowCount5 + 1, 8, currentRowCount5 + 1, 8].Value = user1.Phone;
                        worksheet.Cells[currentRowCount5 + 1, 9, currentRowCount5 + 1, 9].Value = Func.DataStartTime;
                        worksheet.Cells[currentRowCount5 + 1, 10, currentRowCount5 + 1, 10].Value = Func.DataEndTime;
                        // Insertar más datos en celdas específicas según tus necesidades
                    }
                    /*
                    if (i < users.Count)
                    {
                        var user = users[i];

                        worksheet.Cells[currentRowCount5 + 1, 1, currentRowCount5 + 1, 2].Value = user.Position;
                        worksheet.Cells[currentRowCount5 + 1, 3, currentRowCount5 + 1, 5].Value = user.Name;

                        worksheet.Cells[currentRowCount5 + 1, 6, currentRowCount5 + 1, 6].Value = user.Phone;
                        worksheet.Cells[currentRowCount5 + 1, 7, currentRowCount5 + 1, 7].Value = user.Email;
                        // Insertar más datos en celdas específicas según tus necesidades

                    }
                    rowIndex3++; // Avanzar a la siguiente fila
                    */
                }

                //4.6
                int lastRowIndex10 = currentRowIndex;
                int newRowCount14 = 1;
                for (int j = 0; j < newRowCount14; j++)
                {

                    int currentRowCount6 = worksheet.Dimension.Rows; // Obtener el número de filas actualmente en uso
                    int rowIndex = currentRowCount6 + j + 1;
                    int currentCellsCount = worksheet.Dimension.End.Row;
                    int rowIndex1 = currentCellsCount + j + 1;
                    // Verificar si la fila está vacía y aplica estilos 
                    if (string.IsNullOrWhiteSpace(worksheet.Cells[rowIndex1, 1].Text))
                    {
                        var rowStyle = worksheet.Cells[currentRowCount6, 1, currentRowCount6, 10];
                        rowStyle.Style.Font.Bold = true;
                        rowStyle.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        rowStyle.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle.Value = "4.6. ROLLBACK  (Especificar qué asignaciones requiere y en qué momento)";
                        rowStyle.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        rowStyle.Merge = true;
                        worksheet.Row(rowIndex).Height = 12;


                        // Establecer el contorno negro de las celdas
                        var ranges74 = worksheet.Cells[currentRowCount6, 1, currentRowCount6, 10];
                        var border74 = ranges74.Style.Border;
                        border74.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border74.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border74.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border74.Top.Color.SetColor(System.Drawing.Color.Black);
                        border74.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border74.Right.Color.SetColor(System.Drawing.Color.Black);
                        border74.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border74.Left.Color.SetColor(System.Drawing.Color.Black);


                        var rowStyle1 = worksheet.Cells[currentRowCount6, 11, currentRowCount6, 13];
                        rowStyle1.Style.Font.Bold = true;
                        rowStyle1.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle1.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        rowStyle1.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle1.Value = "";
                        rowStyle1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        rowStyle1.Merge = true;



                        // Establecer el contorno negro de las celdas
                        var ranges75 = worksheet.Cells[currentRowCount6, 11, currentRowCount6, 13];
                        var border75 = ranges75.Style.Border;
                        border75.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border75.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Top.Color.SetColor(System.Drawing.Color.Black);
                        border75.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Right.Color.SetColor(System.Drawing.Color.Black);
                        border75.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Left.Color.SetColor(System.Drawing.Color.Black);




                    }
                }

                int newRowCount15 = 2; // Número de nuevas filas a agregar

                for (int i = 0; i < newRowCount15; i++)
                {
                    int currentRowCount5 = worksheet.Dimension.Rows; // Obtener el número de filas actualmente en uso
                    int rowIndex3 = currentRowCount5 + i + 1;

                    // Verificar si la fila está vacía y aplicar estilos
                    if (string.IsNullOrWhiteSpace(worksheet.Cells[rowIndex3, 1].Text))
                    {
                        // Agrega una nueva fila con estilos definidos
                        var newRow = worksheet.Cells[currentRowCount5 + 1, 1, currentRowCount5 + 1, 10];
                        newRow.Style.Font.Bold = true;
                        newRow.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow.Merge = true; // Combina las celdas
                        worksheet.Row(currentRowCount5).Height = 55;


                        // Establecer el contorno negro de las celdas
                        var ranges65 = worksheet.Cells[currentRowCount5 + 1, 1, currentRowCount5 + 1, 10];
                        var border65 = ranges65.Style.Border;
                        border65.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border65.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border65.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border65.Top.Color.SetColor(System.Drawing.Color.Black);
                        border65.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border65.Right.Color.SetColor(System.Drawing.Color.Black);
                        border65.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border65.Left.Color.SetColor(System.Drawing.Color.Black);

                        foreach (var change in changes)
                        {

                            //worksheet.Cells[currentRowCount5 + 1, 1, currentRowCount5 + 1, 10].Value = change.Rollback;
                            worksheet.Cells[currentRowCount5 + 1, 1, currentRowCount5 + 1, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            // Agrega más datos según las propiedades de Change que desees mostrar en las celdas

                        }

                        // Agrega una nueva fila con estilos definidos
                        var newRow2 = worksheet.Cells[currentRowCount5 + 1, 11, currentRowCount5 + 1, 13];
                        newRow2.Style.Font.Bold = true;
                        newRow2.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow2.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow2.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow2.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow2.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow2.Merge = true; // Combina las celdas
                        worksheet.Row(currentRowCount5).Height = 55;

                        // Establecer el contorno negro de las celdas
                        var ranges75 = worksheet.Cells[currentRowCount5, 11, currentRowCount5, 13];
                        var border75 = ranges75.Style.Border;
                        border75.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border75.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Top.Color.SetColor(System.Drawing.Color.Black);
                        border75.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Right.Color.SetColor(System.Drawing.Color.Black);
                        border75.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Left.Color.SetColor(System.Drawing.Color.Black);

                    }
                }
                //4.7



                int lastRowIndex11 = currentRowIndex;
                int newRowCount16 = 1;
                for (int j = 0; j < newRowCount16; j++)
                {

                    int currentRowCount6 = worksheet.Dimension.Rows; // Obtener el número de filas actualmente en uso
                    int rowIndex = currentRowCount6 + j + 1;
                    int currentCellsCount = worksheet.Dimension.End.Row;
                    int rowIndex1 = currentCellsCount + j + 1;
                    int rowIndex2 = rowIndex1 + j + 1;
                    // Verificar si la fila está vacía y aplica estilos 
                    if (string.IsNullOrWhiteSpace(worksheet.Cells[rowIndex1, 1].Text))
                    {
                        var rowStyle = worksheet.Cells[currentRowCount6, 1, currentRowCount6, 10];
                        rowStyle.Style.Font.Bold = true;
                        rowStyle.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        rowStyle.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle.Value = "4.7.BLUEPRINT  (Indicar Versión y Fecha de BluePrint que se utilizara como soporte para el paso)";
                        rowStyle.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        rowStyle.Merge = true;
                        worksheet.Row(currentRowCount6).Height = 12;


                        // Establecer el contorno negro de las celdas
                        var ranges74 = worksheet.Cells[currentRowCount6, 1, currentRowCount6, 10];
                        var border74 = ranges74.Style.Border;
                        border74.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border74.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border74.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border74.Top.Color.SetColor(System.Drawing.Color.Black);
                        border74.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border74.Right.Color.SetColor(System.Drawing.Color.Black);
                        border74.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border74.Left.Color.SetColor(System.Drawing.Color.Black);


                        var rowStyle1 = worksheet.Cells[currentRowCount6, 11, currentRowCount6, 13];
                        rowStyle1.Style.Font.Bold = true;
                        rowStyle1.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle1.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        rowStyle1.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle1.Value = "";
                        rowStyle1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        rowStyle1.Merge = true;



                        // Establecer el contorno negro de las celdas
                        var ranges75 = worksheet.Cells[currentRowCount6, 11, currentRowCount6, 13];
                        var border75 = ranges75.Style.Border;
                        border75.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border75.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Top.Color.SetColor(System.Drawing.Color.Black);
                        border75.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Right.Color.SetColor(System.Drawing.Color.Black);
                        border75.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Left.Color.SetColor(System.Drawing.Color.Black);

                        var rowStyle2 = worksheet.Cells[rowIndex, 1, rowIndex, 1];
                        rowStyle2.Style.Font.Bold = true;
                        rowStyle2.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle2.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle2.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                        rowStyle2.Value = "Versión";
                        rowStyle2.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle2.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        rowStyle2.Merge = true;
                        worksheet.Row(rowIndex).Height = 20;

                        // Establecer el contorno negro de las celdas
                        var ranges08 = worksheet.Cells[rowIndex, 1, rowIndex, 1];
                        var border08 = ranges08.Style.Border;
                        border08.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border08.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border08.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border08.Top.Color.SetColor(System.Drawing.Color.Black);
                        border08.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border08.Right.Color.SetColor(System.Drawing.Color.Black);
                        border08.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border08.Left.Color.SetColor(System.Drawing.Color.Black);


                        var rowStyle3 = worksheet.Cells[rowIndex2, 1, rowIndex2, 1];
                        rowStyle3.Style.Font.Bold = true;
                        rowStyle3.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle3.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        rowStyle3.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                        rowStyle3.Value = "Fecha";
                        rowStyle3.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle3.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        rowStyle3.Merge = true;
                        worksheet.Row(rowIndex2).Height = 20;

                        // Establecer el contorno negro de las celdas
                        var ranges09 = worksheet.Cells[rowIndex2, 1, rowIndex2, 1];
                        var border09 = ranges09.Style.Border;
                        border09.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border09.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border09.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border09.Top.Color.SetColor(System.Drawing.Color.Black);
                        border09.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border09.Right.Color.SetColor(System.Drawing.Color.Black);
                        border09.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border09.Left.Color.SetColor(System.Drawing.Color.Black);



                        // Agrega una nueva fila con estilos definidos
                        var newRow = worksheet.Cells[rowIndex1, 2, rowIndex1, 10];
                        newRow.Style.Font.Bold = true;
                        newRow.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow.Merge = true; // Combina las celdas

                        // Establecer el contorno negro de las celdas
                        var ranges65 = worksheet.Cells[rowIndex1, 2, rowIndex1, 10];
                        var border65 = ranges65.Style.Border;
                        border65.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border65.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border65.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border65.Top.Color.SetColor(System.Drawing.Color.Black);
                        border65.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border65.Right.Color.SetColor(System.Drawing.Color.Black);
                        border65.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border65.Left.Color.SetColor(System.Drawing.Color.Black);

                        // Agrega una nueva fila con estilos definidos
                        var newRow4 = worksheet.Cells[rowIndex2, 2, rowIndex2, 10];
                        newRow4.Style.Font.Bold = true;
                        newRow4.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow4.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow4.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow4.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow4.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow4.Merge = true; // Combina las celdas

                        // Establecer el contorno negro de las celdas
                        var ranges66 = worksheet.Cells[rowIndex2, 2, rowIndex2, 10];
                        var border66 = ranges66.Style.Border;
                        border66.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border66.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border66.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border66.Top.Color.SetColor(System.Drawing.Color.Black);
                        border66.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border66.Right.Color.SetColor(System.Drawing.Color.Black);
                        border66.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border66.Left.Color.SetColor(System.Drawing.Color.Black);


                        // Agrega una nueva fila con estilos definidos
                        var newRow5 = worksheet.Cells[rowIndex1, 11, rowIndex2, 13];
                        newRow5.Style.Font.Bold = true;
                        newRow5.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow5.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow5.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow5.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow5.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow5.Merge = true; // Combina las celdas

                        // Establecer el contorno negro de las celdas
                        var ranges67 = worksheet.Cells[rowIndex1, 11, rowIndex2, 13];
                        var border67 = ranges67.Style.Border;
                        border67.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border67.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border67.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border67.Top.Color.SetColor(System.Drawing.Color.Black);
                        border67.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border67.Right.Color.SetColor(System.Drawing.Color.Black);
                        border67.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border67.Left.Color.SetColor(System.Drawing.Color.Black);


                        foreach (var blue in blueprints)
                        {



                            worksheet.Cells[rowIndex1, 2, rowIndex1, 10].Value = blue.Version;
                            worksheet.Cells[rowIndex2, 2, rowIndex2, 10].Value = blue.Date;
                           


                            // Agrega más datos según las propiedades de Change que desees mostrar en las celdas

                        }

                    }
                }

                //4.8
                int lastRowIndex13 = currentRowIndex;
                int newRowCount17 = 1;
                for (int j = 0; j < newRowCount17; j++)
                {

                    int currentRowCount6 = worksheet.Dimension.Rows + 1; // Obtener el número de filas actualmente en uso
                    int rowIndex = currentRowCount6 + j + 1;
                    int currentCellsCount = worksheet.Dimension.End.Row;
                    int rowIndex1 = rowIndex + j + 1;
                    // Verificar si la fila está vacía y aplica estilos 
                    if (string.IsNullOrWhiteSpace(worksheet.Cells[rowIndex, 1].Text))
                    {
                        var rowStyle = worksheet.Cells[currentRowCount6, 1, currentRowCount6, 10];
                        rowStyle.Style.Font.Bold = true;
                        rowStyle.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        rowStyle.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle.Value = "4.8.OBSERVACIONES GENERALES Y ANEXOS (Descripción de nombres de archivos y ruta )";
                        rowStyle.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        rowStyle.Merge = true;
                        worksheet.Row(rowIndex).Height = 12;


                        // Establecer el contorno negro de las celdas
                        var ranges74 = worksheet.Cells[currentRowCount6, 1, currentRowCount6, 10];
                        var border74 = ranges74.Style.Border;
                        border74.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border74.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border74.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border74.Top.Color.SetColor(System.Drawing.Color.Black);
                        border74.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border74.Right.Color.SetColor(System.Drawing.Color.Black);
                        border74.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border74.Left.Color.SetColor(System.Drawing.Color.Black);


                        // Agrega una nueva fila con estilos definidos
                        var newRow = worksheet.Cells[rowIndex, 1, rowIndex, 10];
                        newRow.Style.Font.Bold = true;
                        newRow.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        newRow.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        newRow.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        newRow.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        newRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        newRow.Merge = true; // Combina las celdas
                        worksheet.Row(rowIndex).Height = 65;


                        // Establecer el contorno negro de las celdas
                        var ranges65 = worksheet.Cells[rowIndex, 1, rowIndex, 10];
                        var border65 = ranges65.Style.Border;
                        border65.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border65.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border65.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border65.Top.Color.SetColor(System.Drawing.Color.Black);
                        border65.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border65.Right.Color.SetColor(System.Drawing.Color.Black);
                        border65.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        border65.Left.Color.SetColor(System.Drawing.Color.Black);

                        var rowStyle1 = worksheet.Cells[rowIndex1, 1, rowIndex1, 10];
                        rowStyle1.Style.Font.Bold = true;
                        rowStyle1.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowStyle1.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        rowStyle1.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                        rowStyle1.Value = "";
                        rowStyle1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rowStyle1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        rowStyle1.Merge = true;
                        worksheet.Row(rowIndex1).Height = 12;


                        // Establecer el contorno negro de las celdas
                        var ranges75 = worksheet.Cells[rowIndex1, 1, rowIndex1, 10];
                        var border75 = ranges75.Style.Border;
                        border75.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        border75.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Top.Color.SetColor(System.Drawing.Color.Black);
                        border75.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Right.Color.SetColor(System.Drawing.Color.Black);
                        border75.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                        border75.Left.Color.SetColor(System.Drawing.Color.Black);


                        foreach (var blue in blueprints)
                        {



                            //worksheet.Cells[rowIndex, 1, rowIndex, 10].Value = blue.Route;
                            



                            // Agrega más datos según las propiedades de Change que desees mostrar en las celdas

                        }
                    }
                }


                // Guardar el archivo Excel en un MemoryStream
                byte[] fileBytes;
                using (var stream = new MemoryStream())
                {
                    package.SaveAs(stream);
                    fileBytes = stream.ToArray();
                }

                return fileBytes;


            }


        }
        private double CentimetersToPoints(double centimeters)
        {
            return centimeters * 28.3464567; // 1 centímetro equivale a 28.3464567 puntos en Excel
        }
        private int Pixel2MTU(int pixels)
        {
            const double mtuPerInch = 914400 / 96;
            return (int)(pixels * mtuPerInch / 96);
        }

        public List<int> GetChangeIDs()
        {
            List<int> changeIds = _dbContext.Change.Select(c => c.ChangeID).Distinct().ToList();
            return changeIds;
        }
    }
}
