using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ITMO_Rev.Lesson5.WPF_Task2
{
    public class MainViewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private List<WallType> wallTypes;
        public List<WallType> WallTypes
        {
            get => wallTypes;
            set
            {
                wallTypes = value;
                OnPropertyChanged();
            }
        }

        private WallType selectedWallType;
        public WallType SelectedWallType
        {
            get => selectedWallType;
            set
            {
                selectedWallType = value;
                OnPropertyChanged();
            }
        }

        private IList<Reference> wallReferences;
        private IList<Reference> WallReferences
        {
            get => wallReferences;
            set
            {
                wallReferences = value;                
            }
        }

        public DelegateCommand ChangeTypeCommand { get; }
        public ExternalCommandData _commandData;       
        private Document Doc { get; set; }

        public MainViewViewModel(ExternalCommandData commandData, IList<Reference> references)
        {
            _commandData = commandData;
            WallReferences = references;
            Doc = _commandData.Application.ActiveUIDocument.Document;
            WallTypes = new FilteredElementCollector(Doc).WhereElementIsElementType().OfClass(typeof(WallType)).Cast<WallType>().ToList();
            SelectedWallType = WallTypes.FirstOrDefault();
            ChangeTypeCommand = new DelegateCommand(OnChangeTypeCommandExecute);
            
        }

        private void OnChangeTypeCommandExecute()
        {
            using (Transaction transaction = new Transaction(_commandData.Application.ActiveUIDocument.Document, "Изменение типа стен"))
            {
                transaction.Start();
                foreach (Reference reference in WallReferences)
                {
                    Wall wall = _commandData.Application.ActiveUIDocument.Document.GetElement(reference) as Wall;

                    wall.WallType = SelectedWallType;
                }
                transaction.Commit();   
            }
        }
    }
}
