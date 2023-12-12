using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.Attributes;

namespace ITMO_Rev
{
    [Transaction(TransactionMode.Manual)]
    public class Button3 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uIApplication = commandData.Application;
            UIDocument uIDocument = uIApplication.ActiveUIDocument;
            Document doc = uIDocument.Document;
            List<FamilySymbol> columns = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol))
                                                                        .OfCategory(BuiltInCategory.OST_StructuralColumns)
                                                                        .Cast<FamilySymbol>()
                                                                        .ToList();

            TaskDialog.Show("Задание 3", $"Количество колонн: {columns.Count}");
            return Result.Succeeded;
        }
    }
}
