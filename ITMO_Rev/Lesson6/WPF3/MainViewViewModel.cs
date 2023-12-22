using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ITMO_Rev.Lesson6.WPF3
{
    public class MainViewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public FamilySymbol SelectedFurnitureSymbol { get; set; }
        //public Level SelectedLevel { get; set; }
        public List<FamilySymbol> FurnitureSymbols { get; set; }
        //public List<Level> Levels { get; set; }
        public XYZ FirstPoint { get; set; }        
        public XYZ SecondPoint { get; set; }  
        public int NumberOfInstances { get; set; }
        public double Elevation { get; set; }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public MainViewViewModel(ExternalCommandData commandData, XYZ firstPoint, XYZ secondPoint)
        {
            _commanddata = commandData;
            FirstPoint = firstPoint;
            SecondPoint = secondPoint;
            CreateFurnitureCommand = new DelegateCommand(OnCreateFurnitureCommandExecute);
            FurnitureSymbols = WPF3Model.GetFurnitureSymbols(commandData);
            //Levels = WPF3Model.GetLevels(commandData);
        }

        private void OnCreateFurnitureCommandExecute()
        {
            using (var tx = new Transaction(_commanddata.Application.ActiveUIDocument.Document))
            {
                UIApplication uiapp = _commanddata.Application;
                UIDocument uidoc = uiapp.ActiveUIDocument;
                Document doc = uidoc.Document;               
                List<XYZ> points = WPF3Model.GetPoints(FirstPoint, SecondPoint, NumberOfInstances);
                Level level = uidoc.ActiveView.GenLevel;
                tx.Start("Создание мебели");
                foreach (XYZ point in points)
                {
                    FamilyInstance furniture = doc.Create.NewFamilyInstance(point, SelectedFurnitureSymbol, level, StructuralType.NonStructural);
                    furniture.get_Parameter(BuiltInParameter.INSTANCE_ELEVATION_PARAM).Set(UnitUtils.ConvertToInternalUnits(Elevation, UnitTypeId.Millimeters));
                }                    
                
                tx.Commit();
            }            
        }

        public ExternalCommandData _commanddata { get; set; }
        public DelegateCommand CreateFurnitureCommand { get; }
    }
}
