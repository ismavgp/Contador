using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinContador.Entity;
using System.Reflection;

namespace WinContador.Utils
{
    public static class Exportar
    {
        public static void Excel(List<JuegoResultEntity> lst, string fecha)
        {
            string fechaName = fecha.Replace("/", "");

            // Obtener carpeta donde se ejecuta el programa
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string rutaPlantilla = Path.Combine(basePath, "template.xlsx");

            // Validar que exista la plantilla
            if (!File.Exists(rutaPlantilla))
            {
                MessageBox.Show("No se encontró template.xlsx en la carpeta del programa.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            // Calcular totales
            decimal totalMonto = lst.Sum(x => decimal.TryParse(x.Monto, out var monto) ? monto : 0);
            decimal porcentaje = lst.Sum(x => decimal.TryParse(x.PorcentajeUtilidad, out var porce) ? porce : 0);
            decimal utilidad = lst.Sum(x => decimal.TryParse(x.Utilidad, out var util) ? util : 0);

            string carpetaDescargas =
            Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
               "Downloads"
            );

            if (!Directory.Exists(carpetaDescargas))
            {
                carpetaDescargas = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }



            // Seleccionar ruta de guardado
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Guardar archivo Excel";
                saveFileDialog.Filter = "Archivo Excel (*.xlsx)|*.xlsx";
                saveFileDialog.FileName = fechaName + ".xlsx";

                saveFileDialog.InitialDirectory = carpetaDescargas;

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                FileInfo archivo = new FileInfo(rutaPlantilla);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (ExcelPackage package = new ExcelPackage(archivo))
                {
                    ExcelWorksheet hoja = package.Workbook.Worksheets[0];

                    hoja.Cells[5, 4].Value = fecha;

                    int filaInicio = 8;
                    int fila = filaInicio;

                    foreach (JuegoResultEntity item in lst)
                    {
                        hoja.Cells[fila, 2].Value = item.Id;
                        hoja.Cells[fila, 3].Value = item.Fecha;
                        hoja.Cells[fila, 4].Value = item.Hora;

                        hoja.Cells[fila, 5].Value = decimal.TryParse(item.Monto, out var monto) ? monto : 0;
                        hoja.Cells[fila, 6].Value = decimal.TryParse(item.PorcentajeUtilidad, out var porce) ? porce : 0;
                        hoja.Cells[fila, 7].Value = decimal.TryParse(item.Utilidad, out var util) ? util : 0;

                        fila++;
                    }

                    // Fila totales
                    hoja.Cells[fila, 4].Value = "SUMA TOTAL";
                    hoja.Cells[fila, 5].Value = totalMonto;
                    hoja.Cells[fila, 6].Value = porcentaje;
                    hoja.Cells[fila, 7].Value = utilidad;
                    hoja.Cells[fila, 4, fila, 7].Style.Font.Bold = true;

                    // Bordes datos
                    var rango = hoja.Cells[filaInicio, 2, fila - 1, 7];
                    rango.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    rango.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    rango.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    rango.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    // Bordes totales
                    var rangoTotal = hoja.Cells[fila, 4, fila, 7];
                    rangoTotal.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    rangoTotal.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    rangoTotal.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    rangoTotal.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    // Ajustar columnas automáticamente
                    //hoja.Cells.AutoFitColumns();





                    package.SaveAs(new FileInfo(saveFileDialog.FileName));
                }
            }
        }

    }
}
