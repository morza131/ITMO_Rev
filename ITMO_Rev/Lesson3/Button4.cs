using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using ITMO_Rev.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITMO_Rev.Lesson3
{
    [Transaction(TransactionMode.Manual)]
    public class Button4 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uIApplication = commandData.Application;
            UIDocument uIDocument = uIApplication.ActiveUIDocument;
            Document doc = uIDocument.Document;
            string paramName = "Наименование";
            CategorySet categorySet = new CategorySet();
            categorySet.Insert(Category.GetCategory(doc, BuiltInCategory.OST_PipeCurves));
            IList<Reference> references = uIDocument.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element, new PipeFilter());

            using (Transaction transaction = new Transaction(doc, "Button4"))
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
                    string paramValue = $"Труба {pipe.get_Parameter(BuiltInParameter.RBS_PIPE_OUTER_DIAMETER).AsValueString()}" +
                        $" / {pipe.get_Parameter(BuiltInParameter.RBS_PIPE_INNER_DIAM_PARAM).AsValueString()}";                    
                    pipe.LookupParameter(paramName).Set(paramValue);
                }
                transaction.Commit();
            }
            return Result.Succeeded;
        }
    }
}
