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

namespace ITMO_Rev.Lesson7
{
    public class MainViewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public FamilySymbol SelectedTitle { get; set; }
        public bool IsSelectViews { get; set; }
        //public Level SelectedLevel { get; set; }
        public List<FamilySymbol> ListOfTitles { get; set; }
        //public List<Level> Levels { get; set; }
        public XYZ Point { get; set; }
        public int NumberOfTitles { get; set; }
        //public double Elevation { get; set; }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commanddata = commandData;
            CreateTitlesCommand = new DelegateCommand(OnCreateTitlesCommandExecute);
            ListOfTitles = WPFModel.GetTitles(commandData);
            //Levels = WPFModel.GetLevels(commandData);
        }

        private void OnCreateTitlesCommandExecute()
        {
            if (IsSelectViews)
            {

                SelectionView selectionView = new SelectionView(_commanddata, NumberOfTitles, SelectedTitle);
                selectionView.ShowDialog();
                RaiseCloseRequest();
            }
            else
            {
                using (var tx = new Transaction(_commanddata.Application.ActiveUIDocument.Document))
                {
                    UIApplication uiapp = _commanddata.Application;
                    UIDocument uidoc = uiapp.ActiveUIDocument;
                    Document doc = uidoc.Document;
                    RaiseCloseRequest();
                    tx.Start("Создание листов");
                    for (int i = 0; i < NumberOfTitles; i++)
                    {
                        ViewSheet viewSheet = ViewSheet.Create(doc, SelectedTitle.Id);
                    }
                    tx.Commit();
                }
            }
        }
        public event EventHandler CloseRequest;
        public void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }
        public ExternalCommandData _commanddata { get; set; }
        public DelegateCommand CreateTitlesCommand { get; }
    }
}
