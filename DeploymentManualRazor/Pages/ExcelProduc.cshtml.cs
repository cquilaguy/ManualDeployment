using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using System.IO;
using Repository.Entities;
using Environment = System.Environment;

namespace DeploymentManualRazor.Pages
{

    public class ExcelProducModel : PageModel
  {

     public IActionResult OnGetDownloadTemplate()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


            // Retorna el archivo Excel para descargarlo utilizando un método de generación bajo demanda
        return new FileContentResult(GenerateExcelBytes(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
              FileDownloadName = "PasoaProduccion.xlsx"
         };



     }
        
        
        private readonly ManualDeploymentContext _dbContext;

        public ExcelProducModel(ManualDeploymentContext dbContext)
        {
            _dbContext = dbContext;
        }
        private byte[] GenerateExcelBytes()
        {
            List<User> users = _dbContext.User.ToList();

            const int ChunkSize = 1000; // Tamaño del fragmento de generación

            using (var stream = new MemoryStream())
            using (var package = new ExcelPackage(stream))
            {

                var worksheet = package.Workbook.Worksheets.Add("Plantilla");
                // Establecer el contorno negro de las celdas
                /* var ranges = worksheet.Cells["A1:J35"];
                 var border = ranges.Style.Border;
                 border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                 border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                 border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                 border.Left.Color.SetColor(System.Drawing.Color.Black);
                 border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                 border.Top.Color.SetColor(System.Drawing.Color.Black);
                 border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                 border.Right.Color.SetColor(System.Drawing.Color.Black);*/

                /* // Establecer el contorno negro de las celdas
                 var ranges1 = worksheet.Cells["A36:J85"];
                 var border1 = ranges1.Style.Border;
                 border1.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                 border1.Bottom.Color.SetColor(System.Drawing.Color.Black);
                 border1.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                 border1.Left.Color.SetColor(System.Drawing.Color.Black);
                 border1.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                 border1.Top.Color.SetColor(System.Drawing.Color.Black);
                 border1.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                 border1.Right.Color.SetColor(System.Drawing.Color.Black);
                */
                /* // Establecer el contorno negro de las celdas
                 var ranges2 = worksheet.Cells["K36:M75"];
                 var border2 = ranges2.Style.Border;
                 border2.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                 border2.Bottom.Color.SetColor(System.Drawing.Color.Black);
                 border2.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                 border2.Left.Color.SetColor(System.Drawing.Color.Black);
                 border2.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                 border2.Top.Color.SetColor(System.Drawing.Color.Black);
                 border2.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                 border2.Right.Color.SetColor(System.Drawing.Color.Black);
                */

                // Definir la fuente y tamaño de letra
                var fontName = "Verdana";
                var fontSize = 9;


                //  rango de celdas
                var ran = worksheet.Cells["A1:M100"];

                // Configurar la fuente y tamaño de letra para el rango de celdas
                ran.Style.Font.Name = fontName;
                ran.Style.Font.Size = fontSize;
                // Aplicar negrita a las letras del rango de celdas
                ran.Style.Font.Bold = true;
                // Insertar la imagen en la celda combinada ["A1:C3"]
                var imageCell = worksheet.Cells["A1:C3"];
                var picture = worksheet.Drawings.AddPicture("imagen", new FileInfo("C:\\Users\\jebellof\\Desktop\\png\\LOGO_AXA.png"));

                // Insertar la imagen en la celda combinada ["A1:C3"]
                picture.From.Column = imageCell.Start.Column;
                picture.From.Row = imageCell.Start.Row;
                picture.SetPosition(0, 0, 1, 1);
                picture.SetSize(100, 100);



                // Convertir el ancho y el alto a puntos
                double widthInCentimeters = 4.42;
                double heightInCentimeters = 2.11;
                double widthInPoints = CentimetersToPoints(widthInCentimeters);
                double heightInPoints = CentimetersToPoints(heightInCentimeters);

                // Establecer el tamaño de la imagen en puntos
                picture.SetSize((int)widthInPoints, (int)heightInPoints);

                // Establecer el color de la celda combinada ["A1:C3"] como blanco
                var range = worksheet.Cells["A1:C3"];
                var fill = range.Style.Fill;
                fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid; // Establecer el tipo de patrón de relleno como sólido
                fill.BackgroundColor.SetColor(System.Drawing.Color.White); // Establecer el color de fondo de la celda combinada como blanco

                worksheet.Cells["A1:C3"].Merge = true;
                worksheet.Cells["A1:C3"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A1:C3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A1:C3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                // Cambiar el tamaño de la celda combinada "A1:C3"
                imageCell.Merge = true;
                imageCell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                imageCell.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                imageCell.Style.WrapText = true;
                imageCell.Style.Font.Bold = true;
                imageCell.Style.Font.Size = 30;

                // Establecer el contorno negro de las celdas
                var ranges3 = worksheet.Cells["A1:C3"];
                var border3 = ranges3.Style.Border;
                border3.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border3.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border3.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border3.Left.Color.SetColor(System.Drawing.Color.Black);
                border3.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border3.Top.Color.SetColor(System.Drawing.Color.Black);
                border3.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border3.Right.Color.SetColor(System.Drawing.Color.Black);







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
                border7.Right.Color.SetColor(System.Drawing.Color.Black);

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
                worksheet.Cells["H7"].Value = "TIPOLOGIA DEL CAMBIO:";
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

                worksheet.Cells["A14"].Merge = true;
                worksheet.Cells["A14"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A14"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A14"].Value = "";
                worksheet.Cells["A14"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A14"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A14"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["A15"].Merge = true;
                worksheet.Cells["A15"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A15"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A15"].Value = "";
                worksheet.Cells["A15"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A15"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A15"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["A16"].Merge = true;
                worksheet.Cells["A16"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A16"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A16"].Value = "";
                worksheet.Cells["A16"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A16"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A16"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["A17"].Merge = true;
                worksheet.Cells["A17"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A17"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A17"].Value = "";
                worksheet.Cells["A17"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A17"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A17"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["A18"].Merge = true;
                worksheet.Cells["A18"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A18"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A18"].Value = "";
                worksheet.Cells["A18"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A18"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A18"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["A19"].Merge = true;
                worksheet.Cells["A19"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A19"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A19"].Value = "";
                worksheet.Cells["A19"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A19"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A19"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges38 = worksheet.Cells["A14:J20"];
                var border38 = ranges38.Style.Border;
                border38.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border38.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border38.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border38.Top.Color.SetColor(System.Drawing.Color.Black);
                border38.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border38.Right.Color.SetColor(System.Drawing.Color.Black);
                border38.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border38.Left.Color.SetColor(System.Drawing.Color.Black);


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


                worksheet.Cells["B14:D14"].Merge = true;
                worksheet.Cells["B14:D14"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B14:D14"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B14:D14"].Value = "";
                worksheet.Cells["B14:D14"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B14:D14"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B14:D14"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["B15:D15"].Merge = true;
                worksheet.Cells["B15:D15"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B15:D15"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B15:D15"].Value = "";
                worksheet.Cells["B15:D15"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B15:D15"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B15:D15"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["B16:D16"].Merge = true;
                worksheet.Cells["B16:D16"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B16:D16"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B16:D16"].Value = "";
                worksheet.Cells["B16:D16"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B16:D16"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B16:D16"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["B17:D17"].Merge = true;
                worksheet.Cells["B17:D17"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B17:D17"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B17:D17"].Value = "";
                worksheet.Cells["B17:D17"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B17:D17"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B17:D17"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["B18:D18"].Merge = true;
                worksheet.Cells["B18:D18"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B18:D18"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B18:D18"].Value = "";
                worksheet.Cells["B18:D18"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B18:D18"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B18:D18"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["B19:D19"].Merge = true;
                worksheet.Cells["B19:D19"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B19:D19"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B19:D19"].Value = "";
                worksheet.Cells["B19:D19"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B19:D19"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B19:D19"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

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

                worksheet.Cells["E14"].Merge = true;
                worksheet.Cells["E14"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["E14"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["E14"].Value = "";
                worksheet.Cells["E14"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["E14"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["E14"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["E15"].Merge = true;
                worksheet.Cells["E15"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["E15"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["E15"].Value = "";
                worksheet.Cells["E15"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["E15"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["E15"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["E16"].Merge = true;
                worksheet.Cells["E16"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["E16"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["E16"].Value = "";
                worksheet.Cells["E16"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["E16"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["E16"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["E17"].Merge = true;
                worksheet.Cells["E17"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["E17"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["E17"].Value = "";
                worksheet.Cells["E17"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["E17"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["E17"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["E18"].Merge = true;
                worksheet.Cells["E18"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["E18"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["E18"].Value = "";
                worksheet.Cells["E18"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["E18"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["E18"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["E19"].Merge = true;
                worksheet.Cells["E19"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["E19"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["E19"].Value = "";
                worksheet.Cells["E19"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["E19"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["E19"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

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

                worksheet.Cells["F14"].Merge = true;
                worksheet.Cells["F14"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["F14"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["F14"].Value = "";
                worksheet.Cells["F14"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["F14"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["F14"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["F15"].Merge = true;
                worksheet.Cells["F15"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["F15"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["F15"].Value = "";
                worksheet.Cells["F15"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["F15"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["F15"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["F16"].Merge = true;
                worksheet.Cells["F16"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["F16"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["F16"].Value = "";
                worksheet.Cells["F16"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["F16"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["F16"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["F17"].Merge = true;
                worksheet.Cells["F17"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["F17"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["F17"].Value = "";
                worksheet.Cells["F17"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["F17"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["F17"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["F18"].Merge = true;
                worksheet.Cells["F18"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["F18"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["F18"].Value = "";
                worksheet.Cells["F18"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["F18"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["F18"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["F19"].Merge = true;
                worksheet.Cells["F19"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["F19"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["F19"].Value = "";
                worksheet.Cells["F19"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["F19"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["F19"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


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

                worksheet.Cells["G14:J14"].Merge = true;
                worksheet.Cells["G14:J14"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["G14:J14"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["G14:J14"].Value = "";
                worksheet.Cells["G14:J14"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["G14:J14"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["G14:J14"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["G15:J15"].Merge = true;
                worksheet.Cells["G15:J15"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["G15:J15"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["G15:J15"].Value = "";
                worksheet.Cells["G15:J15"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["G15:J15"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["G15:J15"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["G16:J16"].Merge = true;
                worksheet.Cells["G16:J16"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["G16:J16"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["G16:J16"].Value = "";
                worksheet.Cells["G16:J16"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["G16:J16"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["G16:J16"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["G17:J17"].Merge = true;
                worksheet.Cells["G17:J17"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["G17:J17"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["G17:J17"].Value = "";
                worksheet.Cells["G17:J17"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["G17:J17"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["G17:J17"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["G18:J18"].Merge = true;
                worksheet.Cells["G18:J18"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["G18:J18"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["G18:J18"].Value = "";
                worksheet.Cells["G18:J18"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["G18:J18"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["G18:J18"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["G19:J19"].Merge = true;
                worksheet.Cells["G19:J19"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["G19:J19"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["G19:J19"].Value = "";
                worksheet.Cells["G19:J19"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["G19:J19"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["G19:J19"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["A22:J22"].Merge = true;
                worksheet.Cells["A22:J22"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A22:J22"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["A22:J22"].Value = "3. FORMATO DE CAPACITACION  (requiere aprobaciones anexas Correo de Aprobación)";
                worksheet.Cells["A22:J22"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A22:J22"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A22:J22"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges39 = worksheet.Cells["A22:J22"];
                var border39 = ranges39.Style.Border;
                border39.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border39.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border39.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border39.Top.Color.SetColor(System.Drawing.Color.Black);
                border39.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border39.Right.Color.SetColor(System.Drawing.Color.Black);
                border39.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border39.Left.Color.SetColor(System.Drawing.Color.Black);





                worksheet.Cells["A23:A24"].Merge = true;
                worksheet.Cells["A23:A24"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A23:A24"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["A23:A24"].Value = "FECHA";
                worksheet.Cells["A23:A24"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A23:A24"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A23:A24"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges40 = worksheet.Cells["A23:A24"];
                var border40 = ranges40.Style.Border;
                border40.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border40.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border40.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border40.Top.Color.SetColor(System.Drawing.Color.Black);
                border40.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border40.Right.Color.SetColor(System.Drawing.Color.Black);
                border40.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border40.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["B23:D23"].Merge = true;
                worksheet.Cells["B23:D23"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B23:D23"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["B23:D23"].Value = "NOMBRE Y APELLIDO";
                worksheet.Cells["B23:D23"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B23:D23"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B23:D23"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges41 = worksheet.Cells["B23:D23"];
                var border41 = ranges41.Style.Border;
                border41.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border41.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border41.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border41.Top.Color.SetColor(System.Drawing.Color.Black);
                border41.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border41.Right.Color.SetColor(System.Drawing.Color.Black);
                border41.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border41.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["B24"].Merge = true;
                worksheet.Cells["B24"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B24"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["B24"].Value = "(INSTRUCTOR)";
                worksheet.Cells["B24"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B24"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B24"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges42 = worksheet.Cells["B24"];
                var border42 = ranges42.Style.Border;
                border42.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border42.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border42.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border42.Top.Color.SetColor(System.Drawing.Color.Black);
                border42.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border42.Right.Color.SetColor(System.Drawing.Color.Black);
                border42.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border42.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["C24:D24"].Merge = true;
                worksheet.Cells["C24:D24"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["C24:D24"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["C24:D24"].Value = "(RECURSO CAPACITADO)";
                worksheet.Cells["C24:D24"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["C24:D24"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["C24:D24"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges43 = worksheet.Cells["C24:D24"];
                var border43 = ranges43.Style.Border;
                border43.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border43.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border43.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border43.Top.Color.SetColor(System.Drawing.Color.Black);
                border43.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border43.Right.Color.SetColor(System.Drawing.Color.Black);
                border43.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border43.Left.Color.SetColor(System.Drawing.Color.Black);


                // Establecer el contorno negro de las celdas
                var ranges48 = worksheet.Cells["A25:J28"];
                var border48 = ranges48.Style.Border;
                border48.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border48.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border48.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border48.Top.Color.SetColor(System.Drawing.Color.Black);
                border48.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border48.Right.Color.SetColor(System.Drawing.Color.Black);
                border48.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border48.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["C25:D25"].Merge = true;
                worksheet.Cells["C25:D25"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["C25:D25"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["C25:D25"].Value = "";
                worksheet.Cells["C25:D25"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["C25:D25"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["C25:D25"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["C26:D26"].Merge = true;
                worksheet.Cells["C26:D26"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["C26:D26"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["C26:D26"].Value = "";
                worksheet.Cells["C26:D26"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["C26:D26"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["C26:D26"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["C27:D27"].Merge = true;
                worksheet.Cells["C27:D27"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["C27:D27"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["C27:D27"].Value = "";
                worksheet.Cells["C27:D27"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["C27:D27"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["C27:D27"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["E23:F24"].Merge = true;
                worksheet.Cells["E23:F24"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["E23:F24"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["E23:F24"].Value = "(COMENTARIOS)";
                worksheet.Cells["E23:F24"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["E23:F24"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["E23:F24"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges44 = worksheet.Cells["E23:F24"];
                var border44 = ranges44.Style.Border;
                border44.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border44.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border44.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border44.Top.Color.SetColor(System.Drawing.Color.Black);
                border44.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border44.Right.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["E25:F25"].Merge = true;
                worksheet.Cells["E25:F25"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["E25:F25"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["E25:F25"].Value = "";
                worksheet.Cells["E25:F25"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["E25:F25"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["E25:F25"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["E26:F26"].Merge = true;
                worksheet.Cells["E26:F26"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["E26:F26"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["E26:F26"].Value = "";
                worksheet.Cells["E26:F26"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["E26:F26"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["E26:F26"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["E27:F27"].Merge = true;
                worksheet.Cells["E27:F27"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["E27:F27"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["E27:F27"].Value = "";
                worksheet.Cells["E27:F27"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["E27:F27"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["E27:F27"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["A29:J29"].Merge = true;
                worksheet.Cells["A29:J29"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A29:J29"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A29:J29"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A29:J29"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                worksheet.Cells["A29:J29"].Merge = true;

                // Establecer el contorno negro de las celdas
                var ranges49 = worksheet.Cells["A29:J29"];
                var border49 = ranges49.Style.Border;
                border49.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border49.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border49.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border49.Top.Color.SetColor(System.Drawing.Color.Black);
                border49.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border49.Right.Color.SetColor(System.Drawing.Color.White);
                border49.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border49.Left.Color.SetColor(System.Drawing.Color.White);


                worksheet.Cells["G23:G24"].Merge = true;
                worksheet.Cells["G23:G24"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["G23:G24"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["G23:G24"].Value = "(TIPO)";
                worksheet.Cells["G23:G24"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["G23:G24"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["G23:G24"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges45 = worksheet.Cells["G23:G24"];
                var border45 = ranges45.Style.Border;
                border45.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border45.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border45.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border45.Top.Color.SetColor(System.Drawing.Color.Black);
                border45.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border45.Right.Color.SetColor(System.Drawing.Color.Black);
                border45.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border45.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["H23:H24"].Merge = true;
                worksheet.Cells["H23:H24"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["H23:H24"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["H23:H24"].Value = "(OBJETIVO DE" + Environment.NewLine + "CAPACITACION)";
                worksheet.Cells["H23:H24"].Style.WrapText = true;
                worksheet.Cells["H23:H24"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["H23:H24"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["H23:H24"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges46 = worksheet.Cells["H23:H24"];
                var border46 = ranges46.Style.Border;
                border46.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border46.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border46.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border46.Top.Color.SetColor(System.Drawing.Color.Black);
                border46.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border46.Right.Color.SetColor(System.Drawing.Color.Black);
                border46.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border46.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["I23:J24"].Merge = true;
                worksheet.Cells["I23:J24"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["I23:J24"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["I23:J24"].Value = "(TEMAS TRATADOS)";
                worksheet.Cells["I23:J24"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["I23:J24"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["I23:J24"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges47 = worksheet.Cells["I23:J24"];
                var border47 = ranges47.Style.Border;
                border47.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border47.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border47.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border47.Top.Color.SetColor(System.Drawing.Color.Black);
                border47.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border47.Right.Color.SetColor(System.Drawing.Color.Black);
                border47.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border47.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["I25:J25"].Merge = true;
                worksheet.Cells["I25:J25"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["I25:J25"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["I25:J25"].Value = "";
                worksheet.Cells["I25:J25"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["I25:J25"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["I25:J25"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                worksheet.Cells["I26:J26"].Merge = true;
                worksheet.Cells["I26:J26"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["I26:J26"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["I26:J26"].Value = "";
                worksheet.Cells["I26:J26"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["I26:J26"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["I26:J26"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                worksheet.Cells["I27:J27"].Merge = true;
                worksheet.Cells["I27:J27"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["I27:J27"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["I27:J27"].Value = "";
                worksheet.Cells["I27:J27"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["I27:J27"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["I27:J27"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                worksheet.Cells["A30:J30"].Merge = true;
                worksheet.Cells["A30:J30"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A30:J30"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["A30:J30"].Value = "4.1. INFORMACION GENERAL DE CONTACTOS";
                worksheet.Cells["A30:J30"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A30:J30"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A30:J30"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges50 = worksheet.Cells["A30:J30"];
                var border50 = ranges50.Style.Border;
                border50.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border50.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border50.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border50.Top.Color.SetColor(System.Drawing.Color.Black);
                border50.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border50.Right.Color.SetColor(System.Drawing.Color.Black);
                border50.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border50.Left.Color.SetColor(System.Drawing.Color.Black);


                // Establecer el contorno negro de las celdas
                var ranges51 = worksheet.Cells["A31:J35"];
                var border51 = ranges51.Style.Border;
                border51.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border51.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border51.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border51.Top.Color.SetColor(System.Drawing.Color.Black);
                border51.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border51.Right.Color.SetColor(System.Drawing.Color.Black);
                border51.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border51.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["A31:B31"].Merge = true;
                worksheet.Cells["A31:B31"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A31:B31"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A31:B31"].Value = "Rol";
                worksheet.Cells["A31:B31"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A31:B31"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A31:B31"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["A32:B32"].Merge = true;
                worksheet.Cells["A32:B32"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A32:B32"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A32:B32"].Value = "";
                worksheet.Cells["A32:B32"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A32:B32"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A32:B32"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["A33:B33"].Merge = true;
                worksheet.Cells["A33:B33"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A33:B33"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A33:B33"].Value = "";
                worksheet.Cells["A33:B33"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A33:B33"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A33:B33"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["A34:B34"].Merge = true;
                worksheet.Cells["A34:B34"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A34:B34"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A34:B34"].Value = "";
                worksheet.Cells["A34:B34"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A34:B34"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A34:B34"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["A35:B35"].Merge = true;
                worksheet.Cells["A35:B35"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A35:B35"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A35:B35"].Value = "";
                worksheet.Cells["A35:B35"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A35:B325"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A35:B35"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;



                worksheet.Cells["C31:E31"].Merge = true;
                worksheet.Cells["C31:E31"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["C31:E31"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["C31:E31"].Value = "Nombre y Apellido";
                worksheet.Cells["C31:E31"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["C31:E31"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["C31:E31"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["C32:E32"].Merge = true;
                worksheet.Cells["C32:E32"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["C32:E32"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["C32:E32"].Value = "";
                worksheet.Cells["C32:E32"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["C32:E32"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["C32:E32"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["C33:E33"].Merge = true;
                worksheet.Cells["C33:E33"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["C33:E33"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["C33:E33"].Value = "";
                worksheet.Cells["C33:E33"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["C33:E33"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["C33:E33"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["C34:E34"].Merge = true;
                worksheet.Cells["C34:E34"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["C34:E34"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["C34:E34"].Value = "";
                worksheet.Cells["C34:E34"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["C34:E34"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["C34:E34"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["C35:E35"].Merge = true;
                worksheet.Cells["C35:E35"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["C35:E35"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["C35:E35"].Value = "";
                worksheet.Cells["C35:E35"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["C35:E35"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["C33:E35"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["F31"].Merge = true;
                worksheet.Cells["F31"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["F31"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["F31"].Value = "Teléfono";
                worksheet.Cells["F31"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["F31"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["F31"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;



                worksheet.Cells["G31"].Merge = true;
                worksheet.Cells["G31"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["G31"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["G31"].Value = "Email";
                worksheet.Cells["G31"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["G31"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["G31"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["H31:J31"].Merge = true;
                worksheet.Cells["H31:J31"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["H31:J31"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["H31:J31"].Value = "Observaciones";
                worksheet.Cells["H31:J31"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["H31:J31"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["H31:J31"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["H32:J32"].Merge = true;
                worksheet.Cells["H32:J32"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["H32:J32"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["H32:J32"].Value = "";
                worksheet.Cells["H32:J32"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["H32:J32"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["H32:J32"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["H33:J33"].Merge = true;
                worksheet.Cells["H33:J33"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["H33:J33"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["H33:J33"].Value = "";
                worksheet.Cells["H33:J33"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["H33:J33"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["H33:J33"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["H34:J34"].Merge = true;
                worksheet.Cells["H34:J34"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["H34:J34"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["H34:J34"].Value = "";
                worksheet.Cells["H34:J34"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["H34:J34"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["H34:J34"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["H35:J35"].Merge = true;
                worksheet.Cells["H35:J35"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["H35:J35"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["H35:J35"].Value = "";
                worksheet.Cells["H35:J35"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["H35:J35"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["H35:J35"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["A36:J36"].Merge = true;
                worksheet.Cells["A36:J36"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A36:J36"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["A36:J36"].Value = "4.2. PRERREQUISITOS (son actividades que se pueden hacer antes de la ventana de implementación, sin afectación de servicio) .";
                worksheet.Cells["A36:J36"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A36:J36"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A36:J36"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges52 = worksheet.Cells["A36:J36"];
                var border52 = ranges52.Style.Border;
                border52.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border52.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border52.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border52.Top.Color.SetColor(System.Drawing.Color.Black);
                border52.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border52.Right.Color.SetColor(System.Drawing.Color.Black);
                border52.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border52.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["K36:M36"].Merge = true;
                worksheet.Cells["K36:M36"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["K36:M36"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060"));
                worksheet.Cells["K36:M36"].Value = "4.9. Resultado de Ejecución / Infraestructura";
                worksheet.Cells["K36:M36"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells["K36:M36"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["K36:M36"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges53 = worksheet.Cells["K36:M36"];
                var border53 = ranges53.Style.Border;
                border53.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border53.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border53.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border53.Top.Color.SetColor(System.Drawing.Color.Black);
                border53.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border53.Right.Color.SetColor(System.Drawing.Color.Black);
                border53.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border53.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["A37"].Merge = true;
                worksheet.Cells["A37"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A37"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["A37"].Value = "Secuencia";
                worksheet.Cells["A37"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A37"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A37"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges54 = worksheet.Cells["A37"];
                var border54 = ranges54.Style.Border;
                border54.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border54.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border54.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border54.Top.Color.SetColor(System.Drawing.Color.Black);
                border54.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border54.Right.Color.SetColor(System.Drawing.Color.Black);
                border54.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border54.Left.Color.SetColor(System.Drawing.Color.Black);


                // Establecer el contorno negro de las celdas
                var ranges64 = worksheet.Cells["A38:I42"];
                var border64 = ranges64.Style.Border;
                border64.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border64.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border64.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border64.Top.Color.SetColor(System.Drawing.Color.Black);
                border64.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border64.Right.Color.SetColor(System.Drawing.Color.Black);
                border64.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border64.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["B37:E37"].Merge = true;
                worksheet.Cells["B37:E37"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B37:E37"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["B37:E37"].Value = "Descripcíon";
                worksheet.Cells["B37:E37"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B37:E37"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B37:E37"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges55 = worksheet.Cells["B37:E37"];
                var border55 = ranges55.Style.Border;
                border55.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border55.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border55.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border55.Top.Color.SetColor(System.Drawing.Color.Black);
                border55.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border55.Right.Color.SetColor(System.Drawing.Color.Black);
                border55.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border55.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["B38:E38"].Merge = true;
                worksheet.Cells["B38:E38"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B38:E38"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B38:E38"].Value = "";
                worksheet.Cells["B38:E38"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B38:E38"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B38:E38"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                worksheet.Cells["B39:E39"].Merge = true;
                worksheet.Cells["B39:E39"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B39:E39"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B39:E39"].Value = "";
                worksheet.Cells["B39:E39"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B39:E39"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B39:E39"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                worksheet.Cells["B40:E40"].Merge = true;
                worksheet.Cells["B40:E40"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B40:E40"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B40:E40"].Value = "";
                worksheet.Cells["B40:E40"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B40:E40"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B40:E40"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                worksheet.Cells["B41:E41"].Merge = true;
                worksheet.Cells["B41:E41"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B41:E41"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B41:E41"].Value = "";
                worksheet.Cells["B41:E41"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B41:E41"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B41:E41"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                worksheet.Cells["B42:E42"].Merge = true;
                worksheet.Cells["B42:E42"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B42:E42"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B42:E42"].Value = "";
                worksheet.Cells["B42:E42"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B42:E42"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B42:E42"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["F37"].Merge = true;
                worksheet.Cells["F37"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["F37"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["F37"].Value = "Fecha y Hora inicio" + Environment.NewLine + "(dd / mm / aaaa - hh:mm)";
                worksheet.Cells["F37"].Style.WrapText = true;
                worksheet.Cells["F37"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["F37"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["F37"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges56 = worksheet.Cells["F37"];
                var border56 = ranges56.Style.Border;
                border56.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border56.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border56.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border56.Top.Color.SetColor(System.Drawing.Color.Black);
                border56.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border56.Right.Color.SetColor(System.Drawing.Color.Black);
                border56.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border56.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["G37"].Merge = true;
                worksheet.Cells["G37"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["G37"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["G37"].Value = "Fecha y Hora Final" + Environment.NewLine + "(dd / mm / aaaa - hh:mm)";
                worksheet.Cells["G37"].Style.WrapText = true;
                worksheet.Cells["G37"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["G37"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["G37"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges57 = worksheet.Cells["G37"];
                var border57 = ranges57.Style.Border;
                border57.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border57.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border57.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border57.Top.Color.SetColor(System.Drawing.Color.Black);
                border57.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border57.Right.Color.SetColor(System.Drawing.Color.Black);
                border57.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border57.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["H37"].Merge = true;
                worksheet.Cells["H37"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["H37"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["H37"].Value = "Tiempo Ejecucíon";
                worksheet.Cells["H37"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["H37"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["H37"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges58 = worksheet.Cells["H37"];
                var border58 = ranges58.Style.Border;
                border58.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border58.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border58.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border58.Top.Color.SetColor(System.Drawing.Color.Black);
                border58.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border58.Right.Color.SetColor(System.Drawing.Color.Black);
                border58.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border58.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["I37"].Merge = true;
                worksheet.Cells["I37"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["I37"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["I37"].Value = "Responsable" + Environment.NewLine + "(Nombre y Apellido)";
                worksheet.Cells["I37"].Style.WrapText = true;
                worksheet.Cells["I37"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["I37"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["I37"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges59 = worksheet.Cells["I37"];
                var border59 = ranges59.Style.Border;
                border59.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border59.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border59.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border59.Top.Color.SetColor(System.Drawing.Color.Black);
                border59.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border59.Right.Color.SetColor(System.Drawing.Color.Black);
                border59.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border59.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["J37"].Merge = true;
                worksheet.Cells["J37"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["J37"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["J37"].Value = "Área ó Proveedor";
                worksheet.Cells["J37"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["J37"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["J37"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges60 = worksheet.Cells["J37"];
                var border60 = ranges60.Style.Border;
                border60.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border60.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border60.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border60.Top.Color.SetColor(System.Drawing.Color.Black);
                border60.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border60.Right.Color.SetColor(System.Drawing.Color.Black);
                border60.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border60.Left.Color.SetColor(System.Drawing.Color.Black);

                // Establecer el contorno negro de las celdas
                var ranges65 = worksheet.Cells["J38:J42"];
                var border65 = ranges65.Style.Border;
                border65.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border65.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border65.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border65.Top.Color.SetColor(System.Drawing.Color.Black);
                border65.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border65.Right.Color.SetColor(System.Drawing.Color.Black);
                border65.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border65.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["K37"].Merge = true;
                worksheet.Cells["K37"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["K37"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["K37"].Value = "SI";
                worksheet.Cells["K37"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["K37"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["K37"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges61 = worksheet.Cells["K37"];
                var border61 = ranges61.Style.Border;
                border61.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border61.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border61.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border61.Top.Color.SetColor(System.Drawing.Color.Black);
                border61.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border61.Right.Color.SetColor(System.Drawing.Color.Black);
                border61.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border61.Left.Color.SetColor(System.Drawing.Color.Black);
                // Establecer el contorno negro de las celdas
                var ranges66 = worksheet.Cells["K38:L42"];
                var border66 = ranges66.Style.Border;
                border66.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border66.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border66.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border66.Top.Color.SetColor(System.Drawing.Color.Black);
                border66.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border66.Right.Color.SetColor(System.Drawing.Color.Black);
                border66.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border66.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["L37"].Merge = true;
                worksheet.Cells["L37"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["L37"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["L37"].Value = "NO";
                worksheet.Cells["L37"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["L37"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["L37"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges62 = worksheet.Cells["L37"];
                var border62 = ranges62.Style.Border;
                border62.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border62.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border62.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border62.Top.Color.SetColor(System.Drawing.Color.Black);
                border62.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border62.Right.Color.SetColor(System.Drawing.Color.Black);
                border62.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border62.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["M37"].Merge = true;
                worksheet.Cells["M37"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["M37"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["M37"].Value = "ERROR";
                worksheet.Cells["M37"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["M37"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["M37"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges63 = worksheet.Cells["M37"];
                var border63 = ranges63.Style.Border;
                border63.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border63.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border63.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border63.Top.Color.SetColor(System.Drawing.Color.Black);
                border63.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border63.Right.Color.SetColor(System.Drawing.Color.Black);
                border63.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border63.Left.Color.SetColor(System.Drawing.Color.Black);

                // Establecer el contorno negro de las celdas
                var ranges67 = worksheet.Cells["M38:M42"];
                var border67 = ranges67.Style.Border;
                border67.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border67.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border67.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border67.Top.Color.SetColor(System.Drawing.Color.Black);
                border67.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border67.Right.Color.SetColor(System.Drawing.Color.Black);
                border67.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border67.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["A43:J43"].Merge = true;
                worksheet.Cells["A43:J43"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A43:J43"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["A43:J43"].Value = "4.3.PLAN DE IMPLEMENTACION (Actividades que se realizan durante la ventana de tiempo) ";
                worksheet.Cells["A43:J43"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A43:J43"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A43:J43"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges68 = worksheet.Cells["A43:J43"];
                var border68 = ranges68.Style.Border;
                border68.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border68.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border68.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border68.Top.Color.SetColor(System.Drawing.Color.Black);
                border68.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border68.Right.Color.SetColor(System.Drawing.Color.Black);
                border68.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border68.Left.Color.SetColor(System.Drawing.Color.Black);

                // Establecer el contorno negro de las celdas
                var ranges69 = worksheet.Cells["A44:J49"];
                var border69 = ranges69.Style.Border;
                border69.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border69.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border69.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border69.Top.Color.SetColor(System.Drawing.Color.Black);
                border69.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border69.Right.Color.SetColor(System.Drawing.Color.Black);
                border69.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border69.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["B44:E44"].Merge = true;
                worksheet.Cells["B44:E44"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B44:E44"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B44:E44"].Value = "";
                worksheet.Cells["B44:E44"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B44:E44"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B44:E44"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                worksheet.Cells["B45:E45"].Merge = true;
                worksheet.Cells["B45:E45"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B45:E45"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B45:E45"].Value = "";
                worksheet.Cells["B45:E45"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B45:E45"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B45:E45"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                worksheet.Cells["B46:E46"].Merge = true;
                worksheet.Cells["B46:E46"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B46:E46"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B46:E46"].Value = "";
                worksheet.Cells["B46:E46"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B46:E46"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B46:E46"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                worksheet.Cells["B47:E47"].Merge = true;
                worksheet.Cells["B47:E47"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B47:E47"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B47:E47"].Value = "";
                worksheet.Cells["B47:E47"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B47:E47"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B47:E47"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                worksheet.Cells["B48:E48"].Merge = true;
                worksheet.Cells["B48:E48"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B48:E48"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B48:E48"].Value = "";
                worksheet.Cells["B48:E48"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B48:E48"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B48:E48"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                worksheet.Cells["B49:E49"].Merge = true;
                worksheet.Cells["B49:E49"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B49:E49"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B49:E49"].Value = "";
                worksheet.Cells["B49:E49"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B49:E49"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B49:E49"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["K43:M43"].Merge = true;
                worksheet.Cells["K43:M43"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["K43:M43"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["K43:M43"].Value = "";
                worksheet.Cells["K43:M43"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["K43:M43"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["K43:M43"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges70 = worksheet.Cells["K43:M43"];
                var border70 = ranges70.Style.Border;
                border70.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border70.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border70.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border70.Top.Color.SetColor(System.Drawing.Color.Black);
                border70.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border70.Right.Color.SetColor(System.Drawing.Color.Black);
                border70.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border70.Left.Color.SetColor(System.Drawing.Color.Black);


                // Establecer el contorno negro de las celdas
                var ranges71 = worksheet.Cells["K44:K49"];
                var border71 = ranges71.Style.Border;
                border71.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border71.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border71.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border71.Top.Color.SetColor(System.Drawing.Color.Black);
                border71.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border71.Right.Color.SetColor(System.Drawing.Color.Black);
                border71.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border71.Left.Color.SetColor(System.Drawing.Color.Black);


                // Establecer el contorno negro de las celdas
                var ranges72 = worksheet.Cells["L44:L49"];
                var border72 = ranges72.Style.Border;
                border72.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border72.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border72.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border72.Top.Color.SetColor(System.Drawing.Color.Black);
                border72.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border72.Right.Color.SetColor(System.Drawing.Color.Black);
                border72.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border72.Left.Color.SetColor(System.Drawing.Color.Black);

                // Establecer el contorno negro de las celdas
                var ranges73 = worksheet.Cells["M44:M49"];
                var border73 = ranges73.Style.Border;
                border73.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border73.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border73.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border73.Top.Color.SetColor(System.Drawing.Color.Black);
                border73.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border73.Right.Color.SetColor(System.Drawing.Color.Black);
                border73.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border73.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["A50:J50"].Merge = true;
                worksheet.Cells["A50:J50"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A50:J50"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["A50:J50"].Value = "4.4.PRERREQUISITOS POSTIMPLANTACION (Actividades de seguimiento y/o de normalización) ";
                worksheet.Cells["A50:J50"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A50:J50"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A50:J50"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges74 = worksheet.Cells["A50:J50"];
                var border74 = ranges74.Style.Border;
                border74.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border74.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border74.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border74.Top.Color.SetColor(System.Drawing.Color.Black);
                border74.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border74.Right.Color.SetColor(System.Drawing.Color.Black);
                border74.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border74.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["K50:M50"].Merge = true;
                worksheet.Cells["K50:M50"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["K50:M50"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["K50:M50"].Value = "";
                worksheet.Cells["K50:M50"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["K50:M50"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["K50:M50"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges75 = worksheet.Cells["K50:M50"];
                var border75 = ranges75.Style.Border;
                border75.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border75.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border75.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border75.Top.Color.SetColor(System.Drawing.Color.Black);
                border75.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border75.Right.Color.SetColor(System.Drawing.Color.Black);
                border75.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border75.Left.Color.SetColor(System.Drawing.Color.Black);



                worksheet.Cells["A51"].Merge = true;
                worksheet.Cells["A51"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A51"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                worksheet.Cells["A51"].Value = "Secuencia";
                worksheet.Cells["A51"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A51"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A51"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges76 = worksheet.Cells["A51"];
                var border76 = ranges76.Style.Border;
                border76.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border76.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border76.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border76.Top.Color.SetColor(System.Drawing.Color.Black);
                border76.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border76.Right.Color.SetColor(System.Drawing.Color.Black);
                border76.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border76.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["B51:E51"].Merge = true;
                worksheet.Cells["B51:E51"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B51:E51"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                worksheet.Cells["B51:E51"].Value = "Descripcíon";
                worksheet.Cells["B51:E51"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B51:E51"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B51:E51"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges77 = worksheet.Cells["B51:E51"];
                var border77 = ranges77.Style.Border;
                border77.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border77.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border77.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border77.Top.Color.SetColor(System.Drawing.Color.Black);
                border77.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border77.Right.Color.SetColor(System.Drawing.Color.Black);
                border77.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border77.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["B52:E52"].Merge = true;
                worksheet.Cells["B52:E52"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B52:E52"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B52:E52"].Value = "";
                worksheet.Cells["B52:E52"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B52:E52"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B52:E52"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["B53:E53"].Merge = true;
                worksheet.Cells["B53:E53"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B53:E53"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B53:E53"].Value = "";
                worksheet.Cells["B53:E53"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B53:E53"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B53:E53"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["B54:E54"].Merge = true;
                worksheet.Cells["B54:E54"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B54:E54"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B54:E54"].Value = "";
                worksheet.Cells["B54:E54"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B54:E54"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B54:E54"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["B55:E55"].Merge = true;
                worksheet.Cells["B55:E55"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B55:E55"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B55:E55"].Value = "";
                worksheet.Cells["B55:E55"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B55:E55"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B55:E55"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["B56:E56"].Merge = true;
                worksheet.Cells["B56:E56"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B56:E56"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B56:E56"].Value = "";
                worksheet.Cells["B56:E56"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B56:E56"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B56:E56"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["B57:E57"].Merge = true;
                worksheet.Cells["B57:E57"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B57:E57"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B57:E57"].Value = "";
                worksheet.Cells["B57:E57"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B57:E57"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B57:E57"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                worksheet.Cells["F51"].Merge = true;
                worksheet.Cells["F51"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["F51"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                worksheet.Cells["F51"].Value = "Área ó Proveedor";
                worksheet.Cells["F51"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["F51"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["F51"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges78 = worksheet.Cells["F51"];
                var border78 = ranges78.Style.Border;
                border78.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border78.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border78.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border78.Top.Color.SetColor(System.Drawing.Color.Black);
                border78.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border78.Right.Color.SetColor(System.Drawing.Color.Black);
                border78.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border78.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["G51:H51"].Merge = true;
                worksheet.Cells["G51:H51"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["G51:H51"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                worksheet.Cells["G51:H51"].Value = "Responsable (Nombre y Apellido) ";
                worksheet.Cells["G51:H51"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["G51:H51"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["G51:H51"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges79 = worksheet.Cells["G51:H51"];
                var border79 = ranges79.Style.Border;
                border79.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border79.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border79.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border79.Top.Color.SetColor(System.Drawing.Color.Black);
                border79.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border79.Right.Color.SetColor(System.Drawing.Color.Black);
                border79.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border79.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["G52:H52"].Merge = true;
                worksheet.Cells["G52:H52"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["G52:H52"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["G52:H52"].Value = "";
                worksheet.Cells["G52:H52"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["G52:H52"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["G52:H52"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["G53:H53"].Merge = true;
                worksheet.Cells["G53:H53"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["G53:H53"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["G53:H53"].Value = "";
                worksheet.Cells["G53:H53"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["G53:H53"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["G53:H53"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["G54:H54"].Merge = true;
                worksheet.Cells["G54:H54"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["G54:H54"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["G54:H54"].Value = "";
                worksheet.Cells["G54:H54"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["G54:H54"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["G54:H54"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["G55:H55"].Merge = true;
                worksheet.Cells["G55:H55"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["G55:H55"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["G55:H55"].Value = "";
                worksheet.Cells["G55:H55"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["G55:H55"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["G55:H55"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["G56:H56"].Merge = true;
                worksheet.Cells["G56:H56"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["G56:H56"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["G56:H56"].Value = "";
                worksheet.Cells["G56:H56"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["G56:H56"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["G56:H56"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["G57:H57"].Merge = true;
                worksheet.Cells["G57:H57"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["G57:H57"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["G57:H57"].Value = "";
                worksheet.Cells["G57:H57"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["G57:H57"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["G57:H57"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["I51"].Merge = true;
                worksheet.Cells["I51"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["I51"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                worksheet.Cells["I51"].Value = "Fecha y Hora inicio" + Environment.NewLine + "(dd / mm / aaaa - hh:mm)";
                worksheet.Cells["I51"].Style.WrapText = true;
                worksheet.Cells["I51"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["I51"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["I51"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;



                // Establecer el contorno negro de las celdas
                var ranges80 = worksheet.Cells["I51"];
                var border80 = ranges80.Style.Border;
                border80.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border80.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border80.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border80.Top.Color.SetColor(System.Drawing.Color.Black);
                border80.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border80.Right.Color.SetColor(System.Drawing.Color.Black);
                border80.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border80.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["J51"].Merge = true;
                worksheet.Cells["J51"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["J51"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                worksheet.Cells["J51"].Value = "Fecha y Hora Final" + Environment.NewLine + "(dd / mm / aaaa - hh:mm)";
                worksheet.Cells["J51"].Style.WrapText = true;
                worksheet.Cells["J51"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["J51"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["J51"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges81 = worksheet.Cells["J51"];
                var border81 = ranges81.Style.Border;
                border81.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border81.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border81.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border81.Top.Color.SetColor(System.Drawing.Color.Black);
                border81.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border81.Right.Color.SetColor(System.Drawing.Color.Black);
                border81.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border81.Left.Color.SetColor(System.Drawing.Color.Black);


                // Establecer el contorno negro de las celdas
                var ranges82 = worksheet.Cells["K51:M51"];
                var border82 = ranges82.Style.Border;
                border82.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border82.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border82.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border82.Top.Color.SetColor(System.Drawing.Color.Black);
                border82.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border82.Right.Color.SetColor(System.Drawing.Color.Black);
                border82.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border82.Left.Color.SetColor(System.Drawing.Color.Black);


                // Establecer el contorno negro de las celdas
                var ranges83 = worksheet.Cells["A52:I58"];
                var border83 = ranges83.Style.Border;
                border83.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border83.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border83.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border83.Top.Color.SetColor(System.Drawing.Color.Black);
                border83.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border83.Right.Color.SetColor(System.Drawing.Color.Black);
                border83.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border83.Left.Color.SetColor(System.Drawing.Color.Black);

                // Establecer el contorno negro de las celdas
                var ranges84 = worksheet.Cells["J52:J58"];
                var border84 = ranges84.Style.Border;
                border84.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border84.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border84.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border84.Top.Color.SetColor(System.Drawing.Color.Black);
                border84.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border84.Right.Color.SetColor(System.Drawing.Color.Black);
                border84.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border84.Left.Color.SetColor(System.Drawing.Color.Black);

                // Establecer el contorno negro de las celdas
                var ranges85 = worksheet.Cells["K52:L58"];
                var border85 = ranges85.Style.Border;
                border85.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border85.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border85.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border85.Top.Color.SetColor(System.Drawing.Color.Black);
                border85.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border85.Right.Color.SetColor(System.Drawing.Color.Black);
                border85.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border85.Left.Color.SetColor(System.Drawing.Color.Black);

                // Establecer el contorno negro de las celdas
                var ranges86 = worksheet.Cells["M52:M58"];
                var border86 = ranges86.Style.Border;
                border86.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border86.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border86.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border86.Top.Color.SetColor(System.Drawing.Color.Black);
                border86.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border86.Right.Color.SetColor(System.Drawing.Color.Black);
                border86.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border86.Left.Color.SetColor(System.Drawing.Color.Black);
                //4.5 USUARIOS 

                //
                worksheet.Cells["A59:J59"].Merge = true;
                worksheet.Cells["A59:J59"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A59:J59"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["A59:J59"].Value = "4.5. DETALLE USUARIOS FUNCIONALES (PRUEBAS POSTIMPLANTACION)";
                worksheet.Cells["A59:J59"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A59:J59"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A59:J59"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges87 = worksheet.Cells["A59:J59"];
                var border87 = ranges87.Style.Border;
                border87.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border87.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border87.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border87.Top.Color.SetColor(System.Drawing.Color.Black);
                border87.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border87.Right.Color.SetColor(System.Drawing.Color.Black);
                border87.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border87.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["K59:M59"].Merge = true;
                worksheet.Cells["K59:M59"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["K59:M59"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["K59:M59"].Value = "";
                worksheet.Cells["K59:M59"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["K59:M59"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["K59:M59"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges88 = worksheet.Cells["K59:M59"];
                var border88 = ranges88.Style.Border;
                border88.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border88.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border88.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border88.Top.Color.SetColor(System.Drawing.Color.Black);
                border88.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border88.Right.Color.SetColor(System.Drawing.Color.Black);
                border88.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border88.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["A60"].Merge = true;
                worksheet.Cells["A60"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A60"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                worksheet.Cells["A60"].Value = "Secuencia";
                worksheet.Cells["A60"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A60"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A60"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges89 = worksheet.Cells["A60"];
                var border89 = ranges89.Style.Border;
                border89.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border89.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border89.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border89.Top.Color.SetColor(System.Drawing.Color.Black);
                border89.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border89.Right.Color.SetColor(System.Drawing.Color.Black);
                border89.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border89.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["B60:E60"].Merge = true;
                worksheet.Cells["B60:E60"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B60:E60"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                worksheet.Cells["B60:E60"].Value = " Funcionario" + Environment.NewLine + "(Nombre y Apellido)";
                worksheet.Cells["B60:E60"].Style.WrapText = true;
                worksheet.Cells["B60:E60"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B60:E60"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B60:E60"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges90 = worksheet.Cells["B60:E60"];
                var border90 = ranges90.Style.Border;
                border90.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border90.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border90.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border90.Top.Color.SetColor(System.Drawing.Color.Black);
                border90.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border90.Right.Color.SetColor(System.Drawing.Color.Black);
                border90.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border90.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["B61:E61"].Merge = true;
                worksheet.Cells["B61:E61"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B61:E61"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B61:E61"].Value = "";
                worksheet.Cells["B61:E61"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B61:E61"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B61:E61"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                worksheet.Cells["B62:E62"].Merge = true;
                worksheet.Cells["B62:E62"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B62:E62"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B62:E62"].Value = "";
                worksheet.Cells["B62:E62"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B62:E62"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B62:E62"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["B63:E63"].Merge = true;
                worksheet.Cells["B63:E63"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B63:E63"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B63:E63"].Value = "";
                worksheet.Cells["B63:E63"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B63:E63"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B63:E63"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["B64:E64"].Merge = true;
                worksheet.Cells["B64:E64"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B64:E64"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B64:E64"].Value = "";
                worksheet.Cells["B64:E64"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B64:E64"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B64:E64"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                worksheet.Cells["B65:E65"].Merge = true;
                worksheet.Cells["B65:E65"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B65:E65"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B65:E65"].Value = "";
                worksheet.Cells["B65:E65"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B65:E65"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B65:E65"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["B66:E66"].Merge = true;
                worksheet.Cells["B66:E66"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B66:E66"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B66:E66"].Value = "";
                worksheet.Cells["B66:E66"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B66:E66"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B66:E66"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells["B67:E67"].Merge = true;
                worksheet.Cells["B67:E67"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B67:E67"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B67:E67"].Value = "";
                worksheet.Cells["B67:E67"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B67:E67"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B67:E67"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;



                worksheet.Cells["F60"].Merge = true;
                worksheet.Cells["F60"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["F60"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                worksheet.Cells["F60"].Value = "Área";
                worksheet.Cells["F60"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["F60"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["F60"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges91 = worksheet.Cells["F60"];
                var border91 = ranges91.Style.Border;
                border91.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border91.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border91.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border91.Top.Color.SetColor(System.Drawing.Color.Black);
                border91.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border91.Right.Color.SetColor(System.Drawing.Color.Black);
                border91.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border91.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["G60"].Merge = true;
                worksheet.Cells["G60"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["G60"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                worksheet.Cells["G60"].Value = "Usuario de Ingreso ";
                worksheet.Cells["G60"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["G60"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["G60"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges92 = worksheet.Cells["G60"];
                var border92 = ranges92.Style.Border;
                border92.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border92.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border92.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border92.Top.Color.SetColor(System.Drawing.Color.Black);
                border92.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border92.Right.Color.SetColor(System.Drawing.Color.Black);
                border92.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border92.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["H60"].Merge = true;
                worksheet.Cells["H60"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["H60"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                worksheet.Cells["H60"].Value = "Teléfono" + Environment.NewLine + "Contacto  (Ext.)";
                worksheet.Cells["H60"].Style.WrapText = true;
                worksheet.Cells["H60"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["H60"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["H60"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;



                // Establecer el contorno negro de las celdas
                var ranges93 = worksheet.Cells["H60"];
                var border93 = ranges93.Style.Border;
                border93.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border93.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border93.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border93.Top.Color.SetColor(System.Drawing.Color.Black);
                border93.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border93.Right.Color.SetColor(System.Drawing.Color.Black);
                border93.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border93.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["I60"].Merge = true;
                worksheet.Cells["I60"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["I60"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                worksheet.Cells["I60"].Value = "Fecha y Hora inicio" + Environment.NewLine + "(dd / mm / aaaa - hh:mm)";
                worksheet.Cells["I60"].Style.WrapText = true;
                worksheet.Cells["I60"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["I60"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["I60"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;



                // Establecer el contorno negro de las celdas
                var ranges94 = worksheet.Cells["I60"];
                var border94 = ranges94.Style.Border;
                border94.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border94.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border94.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border94.Top.Color.SetColor(System.Drawing.Color.Black);
                border94.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border94.Right.Color.SetColor(System.Drawing.Color.Black);
                border94.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border94.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["J60"].Merge = true;
                worksheet.Cells["J60"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["J60"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                worksheet.Cells["J60"].Value = "Fecha y Hora Final" + Environment.NewLine + "(dd / mm / aaaa - hh:mm)";
                worksheet.Cells["J60"].Style.WrapText = true;
                worksheet.Cells["J60"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["J60"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["J60"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges95 = worksheet.Cells["J60"];
                var border95 = ranges95.Style.Border;
                border95.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border95.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border95.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border95.Top.Color.SetColor(System.Drawing.Color.Black);
                border95.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border95.Right.Color.SetColor(System.Drawing.Color.Black);
                border95.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border95.Left.Color.SetColor(System.Drawing.Color.Black);


                // Establecer el contorno negro de las celdas
                var ranges96 = worksheet.Cells["A61:I67"];
                var border96 = ranges96.Style.Border;
                border96.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border96.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border96.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border96.Top.Color.SetColor(System.Drawing.Color.Black);
                border96.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border96.Right.Color.SetColor(System.Drawing.Color.Black);
                border96.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border96.Left.Color.SetColor(System.Drawing.Color.Black);

                // Establecer el contorno negro de las celdas
                var ranges97 = worksheet.Cells["J61:J67"];
                var border97 = ranges97.Style.Border;
                border97.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border97.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border97.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border97.Top.Color.SetColor(System.Drawing.Color.Black);
                border97.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border97.Right.Color.SetColor(System.Drawing.Color.Black);
                border97.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border97.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["K60:M60"].Merge = true;
                worksheet.Cells["K60:M60"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["K60:M60"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                worksheet.Cells["K60:M60"].Value = "";
                worksheet.Cells["K60:M60"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["K60:M60"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["K60:M60"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges98 = worksheet.Cells["K60:M60"];
                var border98 = ranges98.Style.Border;
                border98.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border98.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border98.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border98.Top.Color.SetColor(System.Drawing.Color.Black);
                border98.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border98.Right.Color.SetColor(System.Drawing.Color.Black);
                border98.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border98.Left.Color.SetColor(System.Drawing.Color.Black);

                // Establecer el contorno negro de las celdas
                var ranges99 = worksheet.Cells["K61:L67"];
                var border99 = ranges99.Style.Border;
                border99.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border99.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border99.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border99.Top.Color.SetColor(System.Drawing.Color.Black);
                border99.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border99.Right.Color.SetColor(System.Drawing.Color.Black);
                border99.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border99.Left.Color.SetColor(System.Drawing.Color.Black);



                // Establecer el contorno negro de las celdas
                var ranges01 = worksheet.Cells["M61:M67"];
                var border01 = ranges01.Style.Border;
                border01.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border01.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border01.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border01.Top.Color.SetColor(System.Drawing.Color.Black);
                border01.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border01.Right.Color.SetColor(System.Drawing.Color.Black);
                border01.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border01.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["A68:J68"].Merge = true;
                worksheet.Cells["A68:J68"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A68:J68"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["A68:J68"].Value = "4.6. ROLLBACK  (Especificar qué asignaciones requiere y en qué momento)";
                worksheet.Cells["A68:J68"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A68:J68"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A68:J68"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges02 = worksheet.Cells["A68:J68"];
                var border02 = ranges02.Style.Border;
                border02.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border02.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border02.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border02.Top.Color.SetColor(System.Drawing.Color.Black);
                border02.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border02.Right.Color.SetColor(System.Drawing.Color.Black);
                border02.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border02.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["K68:M68"].Merge = true;
                worksheet.Cells["K68:M68"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["K68:M68"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                worksheet.Cells["K68:M68"].Value = "";
                worksheet.Cells["K68:M68"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["K68:M68"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["K68:M68"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;



                // Establecer el contorno negro de las celdas
                var ranges03 = worksheet.Cells["K68:M68"];
                var border03 = ranges03.Style.Border;
                border03.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border03.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border03.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border03.Top.Color.SetColor(System.Drawing.Color.Black);
                border03.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border03.Right.Color.SetColor(System.Drawing.Color.Black);
                border03.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border03.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["A69:J74"].Merge = true;
                worksheet.Cells["A69:J74"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A69:J74"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A69:J74"].Value = "";
                worksheet.Cells["A69:J74"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A69:J74"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A69:J74"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;



                // Establecer el contorno negro de las celdas
                var ranges04 = worksheet.Cells["A69:J74"];
                var border04 = ranges04.Style.Border;
                border04.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border04.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border04.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border04.Top.Color.SetColor(System.Drawing.Color.Black);
                border04.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border04.Right.Color.SetColor(System.Drawing.Color.Black);
                border04.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border04.Left.Color.SetColor(System.Drawing.Color.Black);



                worksheet.Cells["K69:M74"].Merge = true;
                worksheet.Cells["K69:M74"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["K69:M74"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["K69:M74"].Value = "";
                worksheet.Cells["K69:M74"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["K69:M74"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["K69:M74"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges05 = worksheet.Cells["K69:M74"];
                var border05 = ranges05.Style.Border;
                border05.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border05.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border05.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border05.Top.Color.SetColor(System.Drawing.Color.Black);
                border05.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border05.Right.Color.SetColor(System.Drawing.Color.Black);
                border05.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border05.Left.Color.SetColor(System.Drawing.Color.Black);



                worksheet.Cells["A75:J75"].Merge = true;
                worksheet.Cells["A75:J75"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A75:J75"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["A75:J75"].Value = "4.7.BLUEPRINT  (Indicar Versión y Fecha de BluePrint que se utilizara como soporte para el paso)";
                worksheet.Cells["A75:J75"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A75:J75"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A75:J75"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges06 = worksheet.Cells["A75:J75"];
                var border06 = ranges06.Style.Border;
                border06.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border06.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border06.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border06.Top.Color.SetColor(System.Drawing.Color.Black);
                border06.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border06.Right.Color.SetColor(System.Drawing.Color.Black);
                border06.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border06.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["A76"].Merge = true;
                worksheet.Cells["A76"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A76"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                worksheet.Cells["A76"].Value = "Versión";
                worksheet.Cells["A76"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A76"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A76"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges08 = worksheet.Cells["A76"];
                var border08 = ranges08.Style.Border;
                border08.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border08.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border08.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border08.Top.Color.SetColor(System.Drawing.Color.Black);
                border08.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border08.Right.Color.SetColor(System.Drawing.Color.Black);
                border08.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border08.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["B76:J76"].Merge = true;
                worksheet.Cells["B76:J76"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B76:J76"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B76:J76"].Value = "";
                worksheet.Cells["B76:J76"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B76:J76"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["B76:J76"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges010 = worksheet.Cells["B76:J76"];
                var border010 = ranges010.Style.Border;
                border010.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border010.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border010.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border010.Top.Color.SetColor(System.Drawing.Color.Black);
                border010.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border010.Right.Color.SetColor(System.Drawing.Color.Black);
                border010.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border010.Left.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["A77"].Merge = true;
                worksheet.Cells["A77"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A77"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                worksheet.Cells["A77"].Value = "Fecha";
                worksheet.Cells["A77"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A77"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A77"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges09 = worksheet.Cells["A77"];
                var border09 = ranges09.Style.Border;
                border09.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border09.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border09.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border09.Top.Color.SetColor(System.Drawing.Color.Black);
                border09.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border09.Right.Color.SetColor(System.Drawing.Color.Black);
                border09.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border09.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["B77:J77"].Merge = true;
                worksheet.Cells["B77:J77"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B77:J77"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["B77:J77"].Value = "";
                worksheet.Cells["B77:J77"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["B77:J77"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["B77:J77"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges011 = worksheet.Cells["B77:J77"];
                var border011 = ranges011.Style.Border;
                border011.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border011.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border011.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border011.Top.Color.SetColor(System.Drawing.Color.Black);
                border011.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border011.Right.Color.SetColor(System.Drawing.Color.Black);
                border011.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                border011.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["K75:M75"].Merge = true;
                worksheet.Cells["K75:M75"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["K75:M75"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
                worksheet.Cells["K75:M75"].Value = "";
                worksheet.Cells["K75:M75"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["K75:M75"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["K75:M75"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges07 = worksheet.Cells["K75:M75"];
                var border07 = ranges07.Style.Border;
                border07.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border07.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border07.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border07.Top.Color.SetColor(System.Drawing.Color.Black);
                border07.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border07.Right.Color.SetColor(System.Drawing.Color.Black);
                border07.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border07.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["K76:M77"].Merge = true;
                worksheet.Cells["K76:M77"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["K76:M77"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["K76:M77"].Value = "";
                worksheet.Cells["K76:M77"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["K76:M77"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["K76:M77"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges012 = worksheet.Cells["K76:M77"];
                var border012 = ranges012.Style.Border;
                border012.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border012.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border012.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border012.Top.Color.SetColor(System.Drawing.Color.Black);
                border012.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border012.Right.Color.SetColor(System.Drawing.Color.Black);
                border012.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border012.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["A78:J78"].Merge = true;
                worksheet.Cells["A78:J78"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A78:J78"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["A78:J78"].Value = "4.8.OBSERVACIONES GENERALES Y ANEXOS (Descripción de nombres de archivos y ruta )";
                worksheet.Cells["A78:J78"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A78:J78"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A78:J78"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges013 = worksheet.Cells["A78:J78"];
                var border013 = ranges013.Style.Border;
                border013.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border013.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border013.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border013.Top.Color.SetColor(System.Drawing.Color.Black);
                border013.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border013.Right.Color.SetColor(System.Drawing.Color.Black);
                border013.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border013.Left.Color.SetColor(System.Drawing.Color.Black);

                worksheet.Cells["A79:J84"].Merge = true;
                worksheet.Cells["A79:J84"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A79:J84"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["A79:J84"].Value = "";
                worksheet.Cells["A79:J84"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A79:J84"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A79:J84"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                // Establecer el contorno negro de las celdas
                var ranges014 = worksheet.Cells["A79:J84"];
                var border014 = ranges014.Style.Border;
                border014.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border014.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border014.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border014.Top.Color.SetColor(System.Drawing.Color.Black);


                worksheet.Cells["A85:J85"].Merge = true;
                worksheet.Cells["A85:J85"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A85:J85"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#808080"));
                worksheet.Cells["A85:J85"].Value = "";
                worksheet.Cells["A85:J85"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells["A85:J85"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["A85:J85"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                // Establecer el contorno negro de las celdas
                var ranges015 = worksheet.Cells["A85:J85"];
                var border015 = ranges015.Style.Border;
                border015.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border015.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border015.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border015.Top.Color.SetColor(System.Drawing.Color.Black);
                border015.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border015.Right.Color.SetColor(System.Drawing.Color.Black);
                border015.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                border015.Left.Color.SetColor(System.Drawing.Color.Black);

                //RoW
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
                worksheet.Row(15).Height = 11.4;
                worksheet.Row(16).Height = 22.8;
                worksheet.Row(17).Height = 11.4;
                worksheet.Row(18).Height = 11.4;
                worksheet.Row(19).Height = 11.4;
                worksheet.Row(20).Height = 11.4;
                worksheet.Row(21).Height = 11.4;
                worksheet.Row(22).Height = 12;
                worksheet.Row(23).Height = 26.25;
                worksheet.Row(24).Height = 12;
                worksheet.Row(25).Height = 54;
                worksheet.Row(26).Height = 11.4;
                worksheet.Row(27).Height = 11.4;
                worksheet.Row(28).Height = 12;
                worksheet.Row(29).Height = 12;
                worksheet.Row(30).Height = 12;
                worksheet.Row(31).Height = 11.4;
                worksheet.Row(32).Height = 14.4;
                worksheet.Row(33).Height = 14.4;
                worksheet.Row(34).Height = 14.4;
                worksheet.Row(35).Height = 15.75;
                worksheet.Row(36).Height = 36;
                worksheet.Row(37).Height = 45.75;
                worksheet.Row(38).Height = 11.4;
                worksheet.Row(39).Height = 15;
                worksheet.Row(40).Height = 15;
                worksheet.Row(41).Height = 15;
                worksheet.Row(42).Height = 15;
                worksheet.Row(43).Height = 12;
                worksheet.Row(44).Height = 24.75;
                worksheet.Row(45).Height = 25.5;
                worksheet.Row(46).Height = 48.75;
                worksheet.Row(47).Height = 276;
                worksheet.Row(48).Height = 31.5;
                worksheet.Row(49).Height = 12;
                worksheet.Row(50).Height = 12;
                worksheet.Row(51).Height = 57.75;
                worksheet.Row(52).Height = 11.25;
                worksheet.Row(53).Height = 15;
                worksheet.Row(54).Height = 11.4;
                worksheet.Row(55).Height = 11.4;
                worksheet.Row(56).Height = 11.4;
                worksheet.Row(57).Height = 11.4;
                worksheet.Row(58).Height = 15;
                worksheet.Row(59).Height = 12;
                worksheet.Row(60).Height = 57.75;
                worksheet.Row(61).Height = 15;
                worksheet.Row(62).Height = 15.75;
                worksheet.Row(63).Height = 11.4;
                worksheet.Row(64).Height = 11.4;
                worksheet.Row(65).Height = 15;
                worksheet.Row(66).Height = 11.4;
                worksheet.Row(67).Height = 12;
                worksheet.Row(68).Height = 12;
                worksheet.Row(69).Height = 11.25;
                worksheet.Row(70).Height = 11.4;
                worksheet.Row(71).Height = 11.4;
                worksheet.Row(72).Height = 11.4;
                worksheet.Row(73).Height = 11.4;
                worksheet.Row(74).Height = 12;
                worksheet.Row(75).Height = 12;
                worksheet.Row(76).Height = 11.4;
                worksheet.Row(77).Height = 12;
                worksheet.Row(78).Height = 12;
                worksheet.Row(79).Height = 11.25;
                worksheet.Row(80).Height = 11.25;
                worksheet.Row(81).Height = 11.25;
                worksheet.Row(82).Height = 11.25;
                worksheet.Row(83).Height = 11.25;
                worksheet.Row(84).Height = 22.5;
                worksheet.Row(85).Height = 12;
                worksheet.Row(131).Height = 11.4;
                worksheet.Row(132).Height = 11.4;
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
                worksheet.Column(11).Width = 20.88671875;
                worksheet.Column(12).Width = 13.44140625;
                int rowCount = 43615; // Número total de filas a generar
                int currentRow = 1; // Fila actual

                while (currentRow <= rowCount)
                {
                    int rowsToAdd = Math.Min(ChunkSize, rowCount - currentRow + 1);

                    // Genera y agrega filas al worksheet en fragmentos
                    GenerateRows(worksheet, currentRow, currentRow + rowsToAdd - 1);

                    currentRow += rowsToAdd;
                }





             
                // Insertar los datos en celdas específicas
                int row = 32; // Fila inicial para los datos
       
                foreach (var user in users)
                {
                    worksheet.Cells[row, 1].Value = user.Position;
                    worksheet.Cells[row, 3].Value = user.Name;
                    
                    worksheet.Cells[row, 6].Value = user.Phone;
                    worksheet.Cells[row, 7].Value = user.Email;
                    // Insertar más datos en celdas específicas según tus necesidades
               
                    row++; // Avanzar a la siguiente fila


                }

                // rango de celdas está en las columnas A a D y las filas 2 a 10
                int startRow1 = 32;
                int endRow1 = 35;
                int startColumn1 = 1;
                int endColumn1 = 10;

                // Bucle para recorrer las filas dentro del rango
                for (int row1 = startRow1; row1 <= endRow1; row1++)
                {
                    bool isRowEmpty = true;

                    // Bucle para recorrer las columnas dentro de cada fila
                    for (int column = startColumn1; column <= endColumn1; column++)
                    {
                        var cellValue = worksheet.Cells[row1, column].Value;
                        if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
                        {
                            // La celda no está vacía
                            isRowEmpty = false;
                            break;
                        }
                    }

                    if (isRowEmpty)
                    {
                        // Eliminar fila vacía
                        worksheet.DeleteRow(row1);
                        // Ajustar el valor de endRow después de eliminar una fila
                        endRow1--;
                        // Volver a verificar la misma fila en la siguiente iteración
                        row1--;
                    }
                }


                package.Save(); // Guarda el archivo completo en el stream
                stream.Position = 0; // Restablece la posición del stream a 0

                return stream.ToArray(); // Retorna el contenido del MemoryStream como un array de bytes
            }
        }

        private void GenerateRows(ExcelWorksheet worksheet, int startRow, int endRow)
        {
            for (int row = startRow; row <= endRow; row++)
            {
              
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

    }
}

