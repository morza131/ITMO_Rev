using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITMO_Rev.Filters;

namespace ITMO_Rev.Lesson3
{
    [Transaction(TransactionMode.Manual)]
    public class Button1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uIApplication = commandData.Application;
            UIDocument uIDocument = uIApplication.ActiveUIDocument;
            Document doc = uIDocument.Document;

            IList<Reference> references = uIDocument.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element, new WallFilter(), "Выберите стены");
            // Почему-то не работает с выбором по грани (ObjectType.Edge)
            double volume = 0;
            foreach (Reference reference in references)
            {
                Wall wall = doc.GetElement(reference) as Wall;
                volume += wall.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble();
            }            
            TaskDialog.Show("Задание 1", $"Объём выбранных стен равен {UnitUtils.ConvertFromInternalUnits(volume, UnitTypeId.CubicMeters).ToString("0.##")} м3");

            return Result.Succeeded;
        }
    }
}
