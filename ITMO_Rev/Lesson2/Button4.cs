using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;

namespace ITMO_Rev
{
    [Transaction(TransactionMode.Manual)]
    public class Button4 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uIApplication = commandData.Application;
            UIDocument uIDocument = uIApplication.ActiveUIDocument;
            Document doc = uIDocument.Document;
            List<Level> levels = new FilteredElementCollector(doc).OfClass(typeof(Level))
                                                                  .Cast<Level>()
                                                                  .ToList();
            string dialogmessage = "Количество стен ";
            foreach (Level level in levels)
            {
                List<Wall> columns = new FilteredElementCollector(doc).WhereElementIsNotElementType()
                                                                  .OfClass(typeof(Wall))
                                                                  .Cast<Wall>()
                                                                  .Where(x => x.LevelId == level.Id)
                                                                  .ToList();
                dialogmessage = dialogmessage + $"на уровне {level.Name}: {columns.Count}\n";
            }
            

            TaskDialog.Show("Задание 4", dialogmessage);
            return Result.Succeeded;
        }
    }
}
