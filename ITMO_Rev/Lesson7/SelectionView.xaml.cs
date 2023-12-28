using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ITMO_Rev.Lesson7
{
    /// <summary>
    /// Логика взаимодействия для SelectionView.xaml
    /// </summary>
    public partial class SelectionView : Window
    {
        public List<ViewPlan> SelectedViews { get; set; }
        public SelectionView(ExternalCommandData commandData, int numberOfTitles, FamilySymbol selectedTitle)
        {
            InitializeComponent();
            SelectionViewModel viewModel = new SelectionViewModel(commandData, selectedTitle);
            viewModel.CloseRequest += (s, e) => this.Close();
            DataContext = viewModel;
            for (int i = 0; i < numberOfTitles; i++)
            {
                SelectionGrid.RowDefinitions.Add(new RowDefinition());
                Label label = new Label()
                {
                    Content = $"Лист №{i+1}",
                    Margin = new Thickness(5),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Center,

                };
                SelectionGrid.Children.Add(label);
                int indexOfLabel = SelectionGrid.Children.IndexOf(label);
                System.Windows.Controls.Grid.SetRow(SelectionGrid.Children[indexOfLabel],i);
                System.Windows.Data.Binding bindingSelection = new System.Windows.Data.Binding("SelectedView");
                System.Windows.Data.Binding bindingSource = new System.Windows.Data.Binding("Views");

                System.Windows.Controls.ComboBox comboBox = new System.Windows.Controls.ComboBox()
                {
                    Margin = new Thickness(5),
                    DisplayMemberPath = "Name",
                    Name = $"ViewComboBox{i + 1}",
                    
                };
                bindingSelection.Source = viewModel;
                //bindingSelection.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                bindingSource.Source = viewModel;
                bindingSource.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                BindingOperations.SetBinding(comboBox, System.Windows.Controls.ComboBox.SelectedValueProperty, bindingSelection);
                BindingOperations.SetBinding(comboBox, System.Windows.Controls.ComboBox.ItemsSourceProperty, bindingSource);
               

                SelectionGrid.Children.Add(comboBox);
                int indexOfComboBox = SelectionGrid.Children.IndexOf(comboBox);
                System.Windows.Controls.Grid.SetRow(SelectionGrid.Children[indexOfComboBox], i);
                System.Windows.Controls.Grid.SetColumn(comboBox, 1);
            }
            SelectionGrid.RowDefinitions.Add(new RowDefinition());
            Button createButton = new Button()
            { Margin = new Thickness(5),
                Content = "Создать листы",                
            };
            System.Windows.Data.Binding binding = new System.Windows.Data.Binding("CreateTitlesCommand");
            binding.Source = viewModel;
            BindingOperations.SetBinding(createButton, Button.CommandProperty, binding);
            SelectionGrid.Children.Add(createButton);
            System.Windows.Controls.Grid.SetRow(createButton, numberOfTitles+1);
        }
    }
}
