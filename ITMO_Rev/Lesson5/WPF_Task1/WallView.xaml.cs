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

namespace ITMO_Rev.Lesson5.WPF_Task1
{
    /// <summary>
    /// Логика взаимодействия для WallView.xaml
    /// </summary>
    public partial class WallView : Window
    {
        public WallView(double volumeWalls)
        {
            InitializeComponent();
            volumeWallsText.Text = volumeWalls.ToString();
        }
    }
}
