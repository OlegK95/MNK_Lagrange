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
//using System.Windows.Vector;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;





namespace MNK_Lagrange
{
    public partial class MainWindow : Window
    {
        private void CheckBoxLagrange_Unchecked(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(canvasDrawingArea); i++)
            {
                // Retrieve child visual at specified index value.
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(canvasDrawingArea, i);
                Shape shp = childVisual as Shape;
                if (shp != null)
                {
                    string st = shp.Name;
                    if ((shp is Polyline) && (st.IndexOf("PolylineLagrange") > -1))
                    {
                        canvasDrawingArea.Children.Remove(shp);
                        i--;
                    }
                }
            }
        }
        //----------------------------
        private void CheckBoxLagrange_Checked(object sender, RoutedEventArgs e)
        {
            if (isDrawPolyline == true)
                DrawLagrange(Brushes.Maroon);
        }
        //-----------------------------
        private void CheckBoxMNK_Unchecked(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(canvasDrawingArea); i++)
            {
                // Retrieve child visual at specified index value.
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(canvasDrawingArea, i);
                Shape shp = childVisual as Shape;
                if (shp != null)
                {
                    string st = shp.Name;
                    if ((shp is Polyline) && (st.IndexOf("PolylineMNK") > -1))
                    {
                        canvasDrawingArea.Children.Remove(shp);
                        i--;
                    }
                }
            }
        }
        //----------------------------
        private void CheckBoxMNK_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isDrawPolyline == true)
                {
                    matrix = MakeMatrix(3); //для полинома третьей степени
                    resultMNK = Gauss(matrix, 3 + 1, 3 + 1 + 1);
                    DrawMNK(Brushes.Green);
                }
            }
            catch (MyExeption ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnDraw_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetPoints();
                if (CheckBoxMNK.IsChecked == true)
                {
                    matrix = MakeMatrix(3); //для полинома третьей степени
                    resultMNK = Gauss(matrix, 3 + 1, 3 + 1 + 1);
                    DrawMNK(Brushes.Green);
                }
                if (CheckBoxLagrange.IsChecked == true)
                {
                    DrawLagrange(Brushes.Maroon);
                }
                isDrawPolyline = true;
            }
            catch (MyExeption ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //---------------------------
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(canvasDrawingArea); i++)
            {
                // Retrieve child visual at specified index value.
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(canvasDrawingArea, i);
                Shape shp = childVisual as Shape;
                if (shp != null)
                {
                    string st = shp.Name;
                    if (((shp is Line) && (st.IndexOf("Line") > -1))  || ((shp is Ellipse) && (st.IndexOf("Ellipse") > -1)) || (shp is Polyline))
                    {
                            canvasDrawingArea.Children.Remove(shp);
                            i--;
                    }
                }
            }
            for (int i = 0; i < 5; i++)
                setPoints[i] = -1.0;
            isDraw = false;
            DrawLines();
            isDrawPolyline = false;
        }
        //----------------------------
        private void CanvasDrawingArea_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition((Canvas)sender);
            HitTestResult result = VisualTreeHelper.HitTest(canvasDrawingArea, pt);
            if (result != null)
            {
                var shp = result.VisualHit as Shape;
                if ((shp != null)  && (shp is Ellipse))
                {
                    string st = shp.Name;
                    if (st.IndexOf("Ellipse") > -1)
                    {
                        int nom;
                        if (Int32.TryParse(st.Substring(st.Length - 1), out nom))
                        {
                            double db = Canvas.GetLeft(shp);
                            DrawShapeLine(nom);
                            canvasDrawingArea.Children.Remove(shp);
                        }
                    }
                }
            }
        }
        //-------------------------------
        private void CanvasDrawingArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition((Canvas)sender);
            HitTestResult result = VisualTreeHelper.HitTest(canvasDrawingArea, pt);
            if (result != null)
            {
                var shp = result.VisualHit as Shape;
                if (shp != null  && (shp is Line))
                {
                    string st = shp.Name;
                    if (st.IndexOf("Line") > -1)
                    {
                        int nom;
                        if (Int32.TryParse(st.Substring(st.Length - 1), out nom))
                        {
                            DrawShapeEllipse(nom, pt.Y);
                            canvasDrawingArea.Children.Remove(shp);
                        }
                    }
                }
            }
        }
    }
}

