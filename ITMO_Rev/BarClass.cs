using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ITMO_Rev
{
    internal class BarClass : IExternalApplication
    {       
        public Result OnStartup(UIControlledApplication application)
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location,
                   iconDirectoryPath = Path.GetDirectoryName(assemblyLocation) + @"\icons\",
                   tabName = "ITMO";
            application.CreateRibbonTab(tabName);

            RibbonPanel ITMO_App = application.CreateRibbonPanel(tabName, "Урок 2");
            PushButtonData buttonData2_1 = new PushButtonData(nameof(Button1), "Задание 1", assemblyLocation, typeof(Button1).FullName);          
            ITMO_App.AddItem(buttonData2_1);            
            PushButtonData buttonData2_2 = new PushButtonData(nameof(Button2), "Задание 2", assemblyLocation, typeof(Button2).FullName);
            ITMO_App.AddItem(buttonData2_2);
            PushButtonData buttonData2_3 = new PushButtonData(nameof(Button3), "Задание 3", assemblyLocation, typeof(Button3).FullName);
            ITMO_App.AddItem(buttonData2_3);
            PushButtonData buttonData2_4 = new PushButtonData(nameof(Button4), "Задание 4", assemblyLocation, typeof(Button4).FullName);
            ITMO_App.AddItem(buttonData2_4);
            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
