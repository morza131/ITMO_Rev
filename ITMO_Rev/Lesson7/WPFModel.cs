using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITMO_Rev.Lesson7
{
    public class WPFModel
    {
       public static List<FamilySymbol> GetTitles(ExternalCommandData commandData)
        {
            UIApplication uIApplication = commandData.Application;
            UIDocument uIDoc = uIApplication.ActiveUIDocument;
            Document doc = uIDoc.Document;
            return new FilteredElementCollector(doc)
                .OfClass(typeof(FamilySymbol))
                .OfCategory(BuiltInCategory.OST_TitleBlocks)
                .Cast<FamilySymbol>()
                .ToList();
        }
        public static List<Level> GetLevels(ExternalCommandData commandData)
        {
            UIApplication uIApplication = commandData.Application;
            UIDocument uIDoc = uIApplication.ActiveUIDocument;
            Document doc = uIDoc.Document;
            return new FilteredElementCollector(doc)                
                .OfClass(typeof(Level))
                .Cast<Level>()
                .ToList();
        }
        public static List<ViewPlan> GetViews(ExternalCommandData commandData)
        {
            UIApplication uIApplication = commandData.Application;
            UIDocument uIDoc = uIApplication.ActiveUIDocument;
            Document doc = uIDoc.Document;
            return new FilteredElementCollector(doc)
                .WhereElementIsNotElementType()
                .OfClass(typeof(ViewPlan))
                .Cast<ViewPlan>()
                .ToList();
        }
       
    }
}
