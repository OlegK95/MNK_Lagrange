using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace MNK_Lagrange
{
    public partial class MainWindow : Window
    {

        const int rowCount = 5;
        private double[,]  matrix;
        private double[] resultMNK;


        private double Lagrange(double t)
        {
            double sum = 0, prod;
            for (int j = 0; j < 5; j++)
            {
                prod = 1;
                for (int i = 0; i < 5; i++)
                {
                    if (i != j)
                    {
                        prod *= ((t - i - 1.0) / (j  - i));
                    }
                }
                sum += setPoints[j] * prod;
            }
            return sum;
        }


        private double[,] MakeMatrix(int polynomDegree)
        {
            polynomDegree++;
            double[,] matrix = new double[polynomDegree, polynomDegree + 1];
            for (int i = 0; i < polynomDegree; i++)
            {
                for (int j = 0; j < polynomDegree; j++)
                {
                    matrix[i, j] = 0;
                }
            }
            for (int i = 0; i < polynomDegree; i++)
            {
                for (int j = 0; j < polynomDegree; j++)
                {
                    double sumA = 0, sumB = 0;
                    for (int k = 0; k < rowCount; k++)
                    {
                        sumA += Math.Pow(k + 1.0, i) * Math.Pow(k + 1.0, j);
                        sumB += setPoints[k]         * Math.Pow(k + 1.0, i);
                    }
                    matrix[i, j] = sumA;
                    matrix[i, polynomDegree] = sumB;
                }
            }
            return matrix;
        }

        private double[] Gauss(double[,] matrix, int rowCount, int colCount)
        {
            int[] mask = new int[colCount - 1];
            for (int i = 0; i < colCount - 1; i++) mask[i] = i;
            if (GaussDirectPass(ref matrix, ref mask, colCount, rowCount) == true)
            {
                double[] answer = GaussReversePass(ref matrix, mask, colCount, rowCount);
                return answer;
            }
            else throw new MyExeption("Неможливо знайти однозначне рішення ціеї системи рівнянь\n");
        }

        private bool GaussDirectPass(ref double[,] matrix, ref int[] mask,
            int colCount, int rowCount)
        {
            int i, j, k, maxId, tmpInt;
            double maxVal, tempDouble;
            for (i = 0; i < rowCount; i++)
            {
                maxId = i;
                maxVal = matrix[i, i];
                for (j = i + 1; j < colCount - 1; j++)
                    if (Math.Abs(maxVal) < Math.Abs(matrix[i, j]))
                    {
                        maxVal = matrix[i, j];
                        maxId = j;
                    }
                if (maxVal == 0) return false;
                if (i != maxId)
                {
                    for (j = 0; j < rowCount; j++)
                    {
                        tempDouble = matrix[j, i];
                        matrix[j, i] = matrix[j, maxId];
                        matrix[j, maxId] = tempDouble;
                    }
                    tmpInt = mask[i];
                    mask[i] = mask[maxId];
                    mask[maxId] = tmpInt;
                }
                for (j = 0; j < colCount; j++) matrix[i, j] /= maxVal;
                for (j = i + 1; j < rowCount; j++)
                {
                    double tempMn = matrix[j, i];
                    for (k = 0; k < colCount; k++)
                        matrix[j, k] -= matrix[i, k] * tempMn;
                }
            }
            return true;
        }

        private double[] GaussReversePass(ref double[,] matrix, int[] mask,
            int colCount, int rowCount)
        {
            int i, j, k;
            for (i = rowCount - 1; i >= 0; i--)
                for (j = i - 1; j >= 0; j--)
                {
                    double tempMn = matrix[j, i];
                    for (k = 0; k < colCount; k++)
                        matrix[j, k] -= matrix[i, k] * tempMn;
                }
            double[] answer = new double[rowCount];
            for (i = 0; i < rowCount; i++) answer[mask[i]] = matrix[i, colCount - 1];
            return answer;
        }

        private double CalcMNK_Y(double x)
        {
            double y = 0;
            for (int i = 0; i < 3+1; i++)
            {
                y += resultMNK[i] * Math.Pow(x, i);
            }
            return y;
        }

    }
}
