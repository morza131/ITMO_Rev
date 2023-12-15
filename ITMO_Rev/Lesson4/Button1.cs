using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITMO_Rev.Lesson4
{
    [Transaction(TransactionMode.Manual)]
    public class Button1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uIApplication = commandData.Application;
            UIDocument uIDocument = uIApplication.ActiveUIDocument;
            Document doc = uIDocument.Document;
            List<Wall> walls = new FilteredElementCollector(doc).WhereElementIsNotElementType().OfClass(typeof(Wall)).Cast<Wall>().ToList();
            string output = string.Empty;
            foreach (Wall wall in walls)
            {
                output += $"{wall.WallType.Name}\t{UnitUtils.ConvertFromInternalUnits(wall.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble(), UnitTypeId.CubicMeters)}{Environment.NewLine}";
            }
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string csvPath = Path.Combine(desktopPath, "walls.csv");
            File.WriteAllText(csvPath, output);
            return Result.Succeeded;
        }
    }
}
