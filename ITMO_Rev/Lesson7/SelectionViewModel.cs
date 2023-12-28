using Autodesk.Revit.DB;
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
    public class SelectionViewModel : INotifyPropertyChanged
    {
        public SelectionViewModel(ExternalCommandData commandData, FamilySymbol selectedTitle)
        {
            CreateTitlesCommand = new DelegateCommand(OnCreateTitlesCommandExecute);
            _commanddata = commandData;
            Views = WPFModel.GetViews(commandData);
            SelectedTitleBlock = selectedTitle;
            ListOfViews = new List<ViewPlan>();
        }
        private List<ViewPlan> listOfViews;
        public List<ViewPlan> ListOfViews
        { 
            get { return listOfViews; }
            set { listOfViews = value; }
        }
        private ViewPlan _selectedView;
        public ExternalCommandData _commanddata { get; set; }
        public DelegateCommand CreateTitlesCommand { get; }
        public ViewPlan SelectedView {
            get => _selectedView;
            set
            {
                _selectedView = value;                
                if (!ListOfViews.Contains(value))
                {
                    ListOfViews.Add(value);
                }
            }
        }
        public List<ViewPlan> Views { get; set; }
        public FamilySymbol SelectedTitleBlock { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void OnCreateTitlesCommandExecute()
        {
            using (var tx = new Transaction(_commanddata.Application.ActiveUIDocument.Document))
            {
                UIApplication uiapp = _commanddata.Application;
                UIDocument uidoc = uiapp.ActiveUIDocument;
                Document doc = uidoc.Document;
                RaiseCloseRequest();                
                tx.Start("Создание листов");
                foreach (ViewPlan view in ListOfViews)
                {
                    ViewSheet viewSheet = ViewSheet.Create(doc, SelectedTitleBlock.Id);
                    FamilyInstance title = new FilteredElementCollector(doc).OwnedByView(viewSheet.Id).OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_TitleBlocks).Cast<FamilyInstance>().First();
                   XYZ location = (title.get_BoundingBox(viewSheet).Max - title.get_BoundingBox(viewSheet).Min) / 2;
                  
                    Viewport viewport = Viewport.Create(doc, viewSheet.Id, view.Id, location);               
                }
                tx.Commit();
            }
        }
        public event EventHandler CloseRequest;
        public void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }
    }
}
    

