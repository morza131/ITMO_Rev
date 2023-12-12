using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITMO_Rev
{
    [Transaction(TransactionMode.Manual)]
    public class Button2 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uIApplication = commandData.Application;
            UIDocument uIDocument = uIApplication.ActiveUIDocument;
            Document doc = uIDocument.Document;
            List<Pipe> pipes = new FilteredElementCollector(doc, doc.ActiveView.Id).WhereElementIsNotElementType().OfClass(typeof(Pipe)).Cast<Pipe>().ToList();
            TaskDialog.Show("Задание 2", $"Количество труб на активном виде: {pipes.Count}");
            return Result.Succeeded;
        }
    }
}
