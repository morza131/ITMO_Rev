using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITMO_Rev.Lesson6.WPF2
{
    public class WPF2Model
    {
       public static List<FamilySymbol> GetFurnitureSymbols(ExternalCommandData commandData)
        {
            UIApplication uIApplication = commandData.Application;
            UIDocument uIDoc = uIApplication.ActiveUIDocument;
            Document doc = uIDoc.Document;
            return new FilteredElementCollector(doc)
                .OfClass(typeof(FamilySymbol))
                .OfCategory(BuiltInCategory.OST_Furniture)
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
    }
}
