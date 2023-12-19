using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ITMO_Rev.Lesson3;

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

            RibbonPanel ITMO_App2 = application.CreateRibbonPanel(tabName, "Урок 2");
            PushButtonData buttonData2_1 = new PushButtonData(nameof(Button1), "Задание 1", assemblyLocation, typeof(Button1).FullName);          
            ITMO_App2.AddItem(buttonData2_1);            
            PushButtonData buttonData2_2 = new PushButtonData(nameof(Button2), "Задание 2", assemblyLocation, typeof(Button2).FullName);
            ITMO_App2.AddItem(buttonData2_2);
            PushButtonData buttonData2_3 = new PushButtonData(nameof(Button3), "Задание 3", assemblyLocation, typeof(Button3).FullName);
            ITMO_App2.AddItem(buttonData2_3);
            PushButtonData buttonData2_4 = new PushButtonData(nameof(Button4), "Задание 4", assemblyLocation, typeof(Button4).FullName);
            ITMO_App2.AddItem(buttonData2_4);

            RibbonPanel ITMO_App3 = application.CreateRibbonPanel(tabName, "Урок 3");
            PushButtonData buttonData3_1 = new PushButtonData(nameof(Lesson3.Button1), "Задание 1", assemblyLocation, typeof(Lesson3.Button1).FullName);
            ITMO_App3.AddItem(buttonData3_1);
            PushButtonData buttonData3_2 = new PushButtonData(nameof(Lesson3.Button2), "Задание 2", assemblyLocation, typeof(Lesson3.Button2).FullName);
            ITMO_App3.AddItem(buttonData3_2);
            PushButtonData buttonData3_3 = new PushButtonData(nameof(Lesson3.Button3), "Задание 3", assemblyLocation, typeof(Lesson3.Button3).FullName);
            ITMO_App3.AddItem(buttonData3_3);
            PushButtonData buttonData3_4 = new PushButtonData(nameof(Lesson3.Button4), "Задание 4", assemblyLocation, typeof(Lesson3.Button4).FullName);
            ITMO_App3.AddItem(buttonData3_4);

            RibbonPanel ITMO_App4 = application.CreateRibbonPanel(tabName, "Урок 4");
            PushButtonData buttonData4_1 = new PushButtonData(nameof(Lesson4.Button1), "Задание 1", assemblyLocation, typeof(Lesson4.Button1).FullName);
            ITMO_App4.AddItem(buttonData4_1);
            PushButtonData buttonData4_2 = new PushButtonData(nameof(Lesson4.Button2), "Задание 2", assemblyLocation, typeof(Lesson4.Button2).FullName);
            ITMO_App4.AddItem(buttonData4_2);

            RibbonPanel ITMO_App5 = application.CreateRibbonPanel(tabName, "Урок 5");
            PushButtonData buttonData5_1 = new PushButtonData(nameof(Lesson5.WPF_Task1.WPFButton1), "Задание 1", assemblyLocation, typeof(Lesson5.WPF_Task1.WPFButton1).FullName);
            ITMO_App5.AddItem(buttonData5_1);
            PushButtonData buttonData5_2 = new PushButtonData(nameof(Lesson5.WPF_Task2.WPFButton2), "Задание 2", assemblyLocation, typeof(Lesson5.WPF_Task2.WPFButton2).FullName);
            ITMO_App5.AddItem(buttonData5_2);

            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
