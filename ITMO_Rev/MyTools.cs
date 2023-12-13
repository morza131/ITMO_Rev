using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITMO_Rev
{
    public class MyTools
    {
        public static void CreateSharedParameter(Application application, Document doc, string parameterName, CategorySet categorySet, BuiltInParameterGroup builtInParameterGroup, bool isInstance)
        {
            DefinitionFile definitionFile = application.OpenSharedParameterFile();
            if (definitionFile == null)
            {
                TaskDialog.Show("Ошибка", "Не найден ФОП");
                return;
            }
            Definition definition = definitionFile.Groups.SelectMany(g => g.Definitions).FirstOrDefault( def => def.Name.Equals(parameterName));
            if (definition == null)
            {
                TaskDialog.Show("Ошибка", "В ФОП отсутствует указанный параметр");
                return;
            }
            Binding binding = application.Create.NewTypeBinding(categorySet);
            if (isInstance)
            {
                binding = application.Create.NewInstanceBinding(categorySet);
            }
            BindingMap map = doc.ParameterBindings;
            map.Insert(definition, binding, builtInParameterGroup);            
        }
    }
}
