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
    /// Логика взаимодействия для PipeView.xaml
    /// </summary>
    public partial class PipeView : Window
    {
        public PipeView(int pipeCount)
        {
            InitializeComponent();
            pipeCountText.Text = pipeCount.ToString();
        }
    }
}
