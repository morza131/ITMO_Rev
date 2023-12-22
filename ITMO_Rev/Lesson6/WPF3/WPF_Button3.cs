using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITMO_Rev.Lesson6.WPF3
{
    [Transaction(TransactionMode.Manual)]
    public class WPF_Button3 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            XYZ firstPoint = uidoc.Selection.PickPoint(ObjectSnapTypes.Endpoints, "Выберите первую точку");
            XYZ secondPoint = uidoc.Selection.PickPoint(ObjectSnapTypes.Endpoints, "Выберите вторую точку");
            MainView mw = new MainView(commandData, firstPoint, secondPoint);
            mw.ShowDialog();
            return Result.Succeeded;
        }
    }
}
