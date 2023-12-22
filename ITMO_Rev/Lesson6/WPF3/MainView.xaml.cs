﻿using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ITMO_Rev.Lesson5.WPF_Task1;
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

namespace ITMO_Rev.Lesson6.WPF3
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(ExternalCommandData commandData, XYZ firstPoint, XYZ secondPoint)
        {
            InitializeComponent();
            MainViewViewModel viewModel = new MainViewViewModel(commandData, firstPoint, secondPoint);
            DataContext = viewModel;            
        }
    }
}
