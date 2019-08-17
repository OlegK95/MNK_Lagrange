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
    public partial class MainWindow : Window
    {
        bool isDraw = false;
        bool isDrawPolyline = false;
        private double getX(double value)
        {
            return 100.0 + value * 50.0;
        }
        //----------------------
        private double getY(double value)
        {
            return canvasDrawingArea.Height - 90.0 - value * 50.0;
        }
        //------------------------
        private void DrawMNK(Brush brush)
        {
            Rect myRect1 = new Rect();
            myRect1.X = 30;
            myRect1.Y = 30;
            myRect1.Width = 330;
            myRect1.Height = 500;
            RectangleGeometry myRectangleGeometry1 = new RectangleGeometry();
            myRectangleGeometry1.Rect = myRect1;

            Polyline shapeToRender = null;
            shapeToRender = new Polyline()
            {
                Name = "PolylineMNK",
                Stroke = brush,
                StrokeThickness = 3,
                StrokeStartLineCap = PenLineCap.Round,
                StrokeEndLineCap = PenLineCap.Round,
                Clip = myRectangleGeometry1
            };

            PointCollection myPointCollection = new PointCollection();

            double  y;
            for (double i = 0.0; i <= 6.0; i += 0.04)
            {
                System.Windows.Point Point = new System.Windows.Point(getX(i) - 50.0, getY(CalcMNK_Y(i)));
                myPointCollection.Add(Point);
            }

            shapeToRender.Points = myPointCollection;
            canvasDrawingArea.Children.Add(shapeToRender);
        }
        //-----------------------
        private void DrawLagrange(Brush brush)
        {
            Rect myRect1 = new Rect();
            myRect1.X = 30;
            myRect1.Y = 30;
            myRect1.Width = 330;
            myRect1.Height = 500;
            RectangleGeometry myRectangleGeometry1 = new RectangleGeometry();
            myRectangleGeometry1.Rect = myRect1;

            Polyline shapeToRender = null;
            shapeToRender = new Polyline()
            {
                Name = "PolylineLagrange",
                Stroke = brush,
                StrokeThickness = 3,
                StrokeStartLineCap = PenLineCap.Round,
                StrokeEndLineCap = PenLineCap.Round,
                Clip = myRectangleGeometry1
            };

            PointCollection myPointCollection = new PointCollection();

            for (double i = 0.0; i <= 6.0; i += 0.04)
            {
                System.Windows.Point Point = new System.Windows.Point(getX(i) - 50.0, getY(Lagrange(i)));
                myPointCollection.Add(Point);
            }

            shapeToRender.Points = myPointCollection;
            canvasDrawingArea.Children.Add(shapeToRender);
        }
        //---------------------------
        private void GetPoints()
        {
            bool er = false;
            string massege = "Вы ввели не всі змінні.\nПозначте лінії відмічені червоним,\nіз значенням: ";
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(canvasDrawingArea); i++)
            {
                // Retrieve child visual at specified index value.
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(canvasDrawingArea, i);
                Shape shp = childVisual as Shape;
                if (shp != null)
                {
                    string st = shp.Name;
                    if ((shp is Line) && (st.IndexOf("Line") > -1))
                    {
                        if(er == true)
                            massege += ", ";
                        er = true;
                        massege += 1.0 + double.Parse(st.Substring(st.Length - 1));
                        
                        (shp as Line).Stroke = Brushes.Red;
                        int j = Int32.Parse(st.Substring(st.Length - 1));
                        setPoints[j] = -1.0;
                    }

                    if ((shp is Ellipse) && (st.IndexOf("Ellipse") > -1))
                    {
                        double Y = canvasDrawingArea.Height - Canvas.GetTop(shp) + (shp as Ellipse).ActualHeight / 2.0 - 98.0;
                        Y /= 50.0;
                        int j = Int32.Parse(st.Substring(st.Length - 1));
                        setPoints[j] = Y;
                    }
                }
            }
            if (er == true)
                throw new MyExeption(massege + ".");
        }
        //------------------------------
        private void DrawLines()
        {
            if (isDraw == false)
            { 
                for (int i = 0; i < 5; i++)
                     DrawShapeLine(i);
                isDraw = true;
            }
        }
        //-----------------------------
        private void DrawShapeLine(int nom)
        {
            Shape shapeToRender = null;
            shapeToRender = new Line()
            {
                Name = "Line" + nom.ToString(),
                Stroke = Brushes.Blue,
                
                StrokeThickness = 5,
                X1 = getX((double)nom),
                X2 = getX((double)nom),
                Y1 = 0,
                Y2 = 500,
            };
            Canvas.SetLeft(shapeToRender, 0);
            Canvas.SetTop(shapeToRender, 30);
            canvasDrawingArea.Children.Add(shapeToRender);
        }
        //-----------------------------
        private void DrawShapeEllipse(int nom, double Y_)
        {
            Shape shapeToRender = null;
            shapeToRender = new Ellipse()
            {
                Name = "Ellipse" + nom.ToString(),
                Stroke = Brushes.Red,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 8,
                Height = 8,
                StrokeThickness = 5,
            };
            Canvas.SetLeft(shapeToRender, getX((double) nom) - 4.0);
            Canvas.SetTop(shapeToRender, Y_ - 4.0);
            canvasDrawingArea.Children.Add(shapeToRender);
        }
    }
}



