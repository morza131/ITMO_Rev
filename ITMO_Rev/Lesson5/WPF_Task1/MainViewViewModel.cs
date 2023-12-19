using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Prism.Commands;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITMO_Rev.Lesson5.WPF_Task1
{
    public class MainViewViewModel
    {
        private ExternalCommandData _commanddata;
        public ExternalCommandData CommandData { get { return _commanddata; } set { _commanddata = value; } }

        public DelegateCommand PipeCommand { get; }
        public DelegateCommand WallCommand { get; }
        public DelegateCommand DoorCommand { get; }

        private int pipeCount;
        private int doorCount;
        private double volumeWalls;
        public int PipeCount { get { return pipeCount; } set { pipeCount = value; } }
        public int DoorCount { get { return doorCount; } set { doorCount = value; } }
        public double VolumeWalls { get { return volumeWalls; } set { volumeWalls = value; } }

        public Document Doc { get; set; }

        public MainViewViewModel(ExternalCommandData externalCommandData)
        {
            _commanddata = externalCommandData;
            Doc = externalCommandData.Application.ActiveUIDocument.Document;
            PipeCommand = new DelegateCommand(OnPipeCommand);
            WallCommand = new DelegateCommand(OnWallCommand);
            DoorCommand = new DelegateCommand(OnDoorCommand);
        }

        private void OnPipeCommand()
        {
            PipeCount = new FilteredElementCollector(Doc)
                .WhereElementIsNotElementType()
                .OfClass(typeof(Pipe))
                .Cast<Pipe>()
                .Count();
            PipeView pipeView = new PipeView(PipeCount);
            pipeView.Show();
        }

        private void OnDoorCommand()
        {
            DoorCount = new FilteredElementCollector(Doc)
                .WhereElementIsNotElementType()
                .OfClass(typeof(FamilyInstance))
                .OfCategory(BuiltInCategory.OST_Doors)
                .Cast<FamilyInstance>()
                .Count();
            DoorView doorView = new DoorView(DoorCount);
            doorView.Show();
        }

        private void OnWallCommand()
        {
            List<Wall> walls = new FilteredElementCollector(Doc)
                .WhereElementIsNotElementType()
                .OfClass(typeof(Wall))
                .Cast<Wall>()
                .ToList();
            foreach (Wall wall in walls)
            {
                VolumeWalls += UnitUtils.ConvertFromInternalUnits(wall.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble(), UnitTypeId.CubicMeters);
            }
            WallView wallView = new WallView(VolumeWalls);
            wallView.Show();
            VolumeWalls = 0;
        }

        public event EventHandler CloseRequest;
        private void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }
        
    }
}
