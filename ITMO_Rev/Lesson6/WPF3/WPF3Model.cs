using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITMO_Rev.Lesson6.WPF3
{
    public class WPF3Model
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
        public static List<XYZ> GetPoints(XYZ firstPoint, XYZ secondPoint, int numberOfInstances)
        {      
            List<XYZ> points = new List<XYZ>();
            XYZ delta = (secondPoint - firstPoint) / numberOfInstances;
            for (int i = 0; i < numberOfInstances; i++)
            {
                points.Add(firstPoint + delta * i);
            }
            return points;
        }
    }
}
