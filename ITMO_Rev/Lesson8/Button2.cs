using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITMO_Rev.Lesson8
{
    [Transaction(TransactionMode.Manual)]
    public class Button2 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            View3D view3D = new FilteredElementCollector(doc).WhereElementIsNotElementType().OfClass(typeof(View3D)).Where(x => x.Name.Contains("NWC")).Cast<View3D>().FirstOrDefault();
            NavisworksExportOptions options = new NavisworksExportOptions()
            {
                ExportScope = NavisworksExportScope.Model,
                ViewId = view3D.Id
            };
            
                doc.Export(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "exportNWC.nwc", options);
                
            

            return Result.Succeeded;
        }
    }
}
