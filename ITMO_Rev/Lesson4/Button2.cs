using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Diagnostics;

namespace ITMO_Rev.Lesson4
{
    [Transaction(TransactionMode.Manual)]
    public class Button2 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uIApplication = commandData.Application;
            UIDocument uIDocument = uIApplication.ActiveUIDocument;
            Document doc = uIDocument.Document;

            List<Pipe> pipes = new FilteredElementCollector(doc).WhereElementIsNotElementType().OfClass(typeof(Pipe)).Cast<Pipe>().ToList();

            string excelPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "pipes.xlsx");

            using (FileStream stream = new FileStream(excelPath, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("Лист1");
                int rowIndex = 0;
                sheet.SetCellValue(rowIndex, columnIndex: 0, "Имя типа");
                sheet.SetCellValue(rowIndex, columnIndex: 1, "Внешний диаметр");
                sheet.SetCellValue(rowIndex, columnIndex: 2, "Внутренний диаметр");
                sheet.SetCellValue(rowIndex, columnIndex: 3, "Длина, м");
                rowIndex++;
                foreach (Pipe pipe in pipes)
                {
                    sheet.SetCellValue(rowIndex, columnIndex: 0, pipe.PipeType.Name);
                    sheet.SetCellValue(rowIndex, columnIndex: 1, pipe.get_Parameter(BuiltInParameter.RBS_PIPE_OUTER_DIAMETER).AsDouble());
                    sheet.SetCellValue(rowIndex, columnIndex: 2, pipe.get_Parameter(BuiltInParameter.RBS_PIPE_INNER_DIAM_PARAM).AsDouble());
                    sheet.SetCellValue(rowIndex, columnIndex: 3, UnitUtils.ConvertFromInternalUnits(pipe.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), UnitTypeId.Meters));
                    rowIndex++;
                }

                workbook.Write(stream);
                workbook.Close();
            }
            Process.Start(excelPath);

            return Result.Succeeded;
        }
    }
}
