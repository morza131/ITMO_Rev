using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;

namespace ITMO_Rev.Lesson5.WPF_Task1
{
    [Transaction(TransactionMode.Manual)]
    public class WPFButton1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            MainView mainView = new MainView(commandData);
            mainView.Show();
            return Result.Succeeded;
        }
    }
}
