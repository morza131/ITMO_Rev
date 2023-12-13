using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using ITMO_Rev.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITMO_Rev.Lesson3
{
    [Transaction(TransactionMode.Manual)]
    public class Button2 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uIApplication = commandData.Application;
            UIDocument uIDocument = uIApplication.ActiveUIDocument;
            Document doc = uIDocument.Document;

            IList<Reference> references = uIDocument.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element, new PipeFilter(), "Выберите трубы");
            double volume = 0;
            foreach (Reference reference in references)
            {
                Pipe pipe = doc.GetElement(reference) as Pipe;
                volume += pipe.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble();
            }
            TaskDialog.Show("Задание 2", $"Длины выбранных труб равна {UnitUtils.ConvertFromInternalUnits(volume, UnitTypeId.Meters).ToString("0.##")} м");

            return Result.Succeeded;
        }
    }
}
