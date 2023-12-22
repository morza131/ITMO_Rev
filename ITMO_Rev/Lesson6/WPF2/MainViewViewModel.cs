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

namespace ITMO_Rev.Lesson6.WPF2
{
    public class MainViewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public FamilySymbol SelectedFurnitureSymbol { get; set; }
        public Level SelectedLevel { get; set; }
        public List<FamilySymbol> FurnitureSymbols { get; set; }
        public List<Level> Levels { get; set; }
        public XYZ Point { get; set; }        
        public double Elevation { get; set; }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commanddata = commandData;
            CreateFurnitureCommand = new DelegateCommand(OnCreateFurnitureCommandExecute);
            FurnitureSymbols = WPF2Model.GetFurnitureSymbols(commandData);
            Levels = WPF2Model.GetLevels(commandData);
        }

        private void OnCreateFurnitureCommandExecute()
        {
            using (var tx = new Transaction(_commanddata.Application.ActiveUIDocument.Document))
            {
                UIApplication uiapp = _commanddata.Application;
                UIDocument uidoc = uiapp.ActiveUIDocument;
                Document doc = uidoc.Document;  
                RaiseCloseRequest();
                Point = uidoc.Selection.PickPoint(ObjectSnapTypes.Endpoints, "Выберите точку");  
                tx.Start("Создание мебели");
                FamilyInstance furniture = doc.Create.NewFamilyInstance(Point, SelectedFurnitureSymbol, SelectedLevel, StructuralType.NonStructural);
                furniture.get_Parameter(BuiltInParameter.INSTANCE_ELEVATION_PARAM).Set(UnitUtils.ConvertToInternalUnits(Elevation, UnitTypeId.Millimeters));
                tx.Commit();
            }            
        }

        public event EventHandler CloseRequest;
        public void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }        
        public ExternalCommandData _commanddata { get; set; }
        public DelegateCommand CreateFurnitureCommand { get; }
    }
}
