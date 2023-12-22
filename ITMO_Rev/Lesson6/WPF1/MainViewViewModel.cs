using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
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

namespace ITMO_Rev.Lesson6.WPF1
{
    public class MainViewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DuctType SelectedDuctType { get; set; }
        public Level SelectedLevel { get; set; }
        public List<DuctType> DuctTypes { get; set; }
        public List<Level> Levels { get; set; }
        public XYZ FirstPoint { get; set; }
        public XYZ SecondPoint { get; set; }
        public double Elevation { get; set; }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commanddata = commandData;
            CreateDuctCommand = new DelegateCommand(OnCreateDuctCommandExecute);
            DuctTypes = WPF2Model.GetDuctTypes(commandData);
            Levels = WPF2Model.GetLevels(commandData);
        }

        private void OnCreateDuctCommandExecute()
        {
            using (var tx = new Transaction(_commanddata.Application.ActiveUIDocument.Document))
            {
                UIApplication uiapp = _commanddata.Application;
                UIDocument uidoc = uiapp.ActiveUIDocument;
                Document doc = uidoc.Document;
                RaiseCloseRequest();
                FirstPoint = uidoc.Selection.PickPoint(ObjectSnapTypes.Endpoints, "Выберите первую точку");
                SecondPoint = uidoc.Selection.PickPoint(ObjectSnapTypes.Endpoints, "Выберите вторую точку");
                
                tx.Start("Создание воздуховода");
                MechanicalSystemType mechanicalSystemType = MechanicalSystemType.Create(doc, MEPSystemClassification.SupplyAir, "ПВ");
                Duct duct = Duct.Create(doc, mechanicalSystemType.Id, SelectedDuctType.Id, SelectedLevel.Id, FirstPoint, SecondPoint);
                duct.get_Parameter(BuiltInParameter.RBS_OFFSET_PARAM).Set(UnitUtils.ConvertToInternalUnits(Elevation, UnitTypeId.Millimeters));
                tx.Commit();
            }            
        }

        public event EventHandler CloseRequest;
        public void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }

        public ExternalCommandData _commanddata { get; set; }
        public DelegateCommand CreateDuctCommand { get; }
    }
}
