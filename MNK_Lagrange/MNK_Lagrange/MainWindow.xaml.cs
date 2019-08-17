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





namespace MNK_Lagrange
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
   //     double [] setPoints = new double[5] { -1, -1, -1, -1, -1};
        double[] setPoints = new double[5] { 1, 3, 5, 7, 9 };
        public MainWindow()
        {
            InitializeComponent();
            DrawLines();
        }
    }
}
