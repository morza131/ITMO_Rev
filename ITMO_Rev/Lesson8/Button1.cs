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
    public class Button1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            using (Transaction t = new Transaction(doc, "Export IFC"))
            {
                t.Start();
                doc.Export(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "exportIFC.ifc", new IFCExportOptions());
                t.Commit();
            }

            return Result.Succeeded;
        }
    }
}
