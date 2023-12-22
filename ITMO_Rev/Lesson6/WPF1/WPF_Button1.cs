using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITMO_Rev.Lesson6.WPF1
{
    [Transaction(TransactionMode.Manual)]
    public class WPF_Button1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            MainView mv = new MainView(commandData);
            mv.ShowDialog();
            return Result.Succeeded;
        }
    }
}
