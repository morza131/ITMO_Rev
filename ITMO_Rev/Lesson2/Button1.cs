using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;

namespace ITMO_Rev
{
    [Transaction(TransactionMode.Manual)]
    public class Button1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uIApplication = commandData.Application;
            UIDocument uIDocument = uIApplication.ActiveUIDocument;
            Document doc = uIDocument.Document;
            List<Duct> ducts = new FilteredElementCollector(doc).WhereElementIsNotElementType().OfClass(typeof(Duct)).Cast<Duct>().ToList();
            TaskDialog.Show("Задание 1", $"Количество воздуховодов: {ducts.Count}");
            return Result.Succeeded;
        }
    }
}
