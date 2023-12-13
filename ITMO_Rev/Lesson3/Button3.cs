using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using ITMO_Rev.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ITMO_Rev.Lesson3
{
    [Transaction(TransactionMode.Manual)]
    public class Button3 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uIApplication = commandData.Application;
            UIDocument uIDocument = uIApplication.ActiveUIDocument;
            Document doc = uIDocument.Document;
            string paramName = "Длина с запасом";
            CategorySet categorySet = new CategorySet();
            categorySet.Insert(Category.GetCategory(doc, BuiltInCategory.OST_PipeCurves));
            IList<Reference> references = uIDocument.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element, new PipeFilter());
            
            using(Transaction transaction = new Transaction(doc, "Button3"))
            {
                transaction.Start();
                if (!doc.ParameterBindings.Contains(uIApplication.Application
                    .OpenSharedParameterFile().Groups.SelectMany(g => g.Definitions)
                    .FirstOrDefault(def => def.Name.Equals(paramName))))
                {
                    MyTools.CreateSharedParameter(uIApplication.Application, doc, paramName, categorySet, BuiltInParameterGroup.PG_DATA, true);
                };               
                foreach (Reference reference in references)
                {
                    Pipe pipe = (Pipe)doc.GetElement(reference);
                    double length = UnitUtils.ConvertFromInternalUnits(pipe.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), UnitTypeId.Meters);
                    pipe.LookupParameter(paramName).Set((UnitUtils.ConvertToInternalUnits(length * 1.1, UnitTypeId.Meters)));
                }
                transaction.Commit();
            }    
            return Result.Succeeded;
        }
    }
}
