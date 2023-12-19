using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using ITMO_Rev.Filters;

namespace ITMO_Rev.Lesson5.WPF_Task2
{
    [Transaction(TransactionMode.Manual)]
    public class WPFButton2 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uIApplication = commandData.Application;
            UIDocument uIDocument = uIApplication.ActiveUIDocument;
            Document doc = uIDocument.Document;
            TaskDialog.Show("Приложение", "Выберите стены");
            IList<Reference> references = uIDocument.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element, new WallFilter(), "Выберите стены"); 
            List<WallType> wallTypes = new FilteredElementCollector(doc).WhereElementIsElementType().OfClass(typeof(WallType)).Cast<WallType>().ToList();
            MainView mainView = new MainView(commandData, references);
            mainView.ShowDialog(); // не понимаю, почему не работает с методом .Show(). Если использовать его, то возникает ошибка
            //"Starting a transaction from an external application running outside of API context is not allowed".
            return Result.Succeeded;
        }
    }
}

