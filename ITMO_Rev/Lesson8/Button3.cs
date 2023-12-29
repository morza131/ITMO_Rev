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
    public class Button3 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            List<ElementId> viewIds = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Views)
                .WhereElementIsNotElementType()
                .Where(x => x.Name == "С1_Этаж_01")
                .Select(x => x.Id)
                .ToList();

            ImageExportOptions options = new ImageExportOptions()
            {
                ZoomType = ZoomFitType.FitToPage,
                PixelSize = 2048,
                FilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+ "\\exportPNG.png",
                FitDirection = FitDirectionType.Horizontal,
                HLRandWFViewsFileType = ImageFileType.PNG,
                ExportRange = ExportRange.SetOfViews
            };
            
            options.SetViewsAndSheets(viewIds);
            using (Transaction t = new Transaction(doc))
            {
                t.Start("Export PNG");
                doc.ExportImage(options);
                t.Commit();
            }

            return Result.Succeeded;
        }
    }
}
