using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Рунге_Куты_4_пор
{
    class Program
    {
        static void Main(string[] args)
        {
            float h0, x0;
            int m, n;

            Console.Write("Введите количество уравнений n = ");
            n = int.Parse(Console.ReadLine());
            float[,] A = new float[n, n];
            float[] Y = new float[n];
            float[] Cx = new float[n];

            Console.Write("x0 = ");
            x0 = float.Parse(Console.ReadLine());

            Console.Write("Шаг таблицы h0 (решение будет искаться на [x0; x0 + h0] ) = ");
            h0 = float.Parse(Console.ReadLine());

            Console.Write("Коэффициент дробления шага m = ");
            m = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                Console.Write("Введите y" + (i + 1) + "(" + x0 + ") = ");
                Y[i] = float.Parse(Console.ReadLine());
            }

            //Ввод уравнений
            for (int i = 0; i<n; i++)
            {
                Console.Write("Коэффициент перед x" + (i + 1) + " = ");
                Cx[i] = float.Parse(Console.ReadLine());

                for (int j = 0; j < n; j++)
                {
                    Console.Write("Коэфф перед y" + (j + 1) + " = ");
                    A[i, j] = float.Parse(Console.ReadLine());
                }
                Console.WriteLine();
            }

            RK4(x0, Cx, A, Y, h0, m);
        }

        static void RK4(float x0, float[] Cx, float[,] A, float[] Y, float h0, int m)
        {
            float h = h0 / m;
            int n = A.GetLength(0);
            float[,] K = new float[4, n];
            float xi = x0;

            Console.WriteLine("x,Y1,Y2,...,Yn\n");
            Console.WriteLine();
            Console.Write("{0:0.00}     ", x0);
            for (int i = 0; i<n; i++)
            {
                Console.Write("{0:0.000}     ",Y[i]);
            }
            Console.WriteLine();

            for (int i = 1; i <= m; i++)
            {
                
                for (int j = 0; j<n; j++)
                {
                    K[0, j] = h * Cx[j] * xi;
                    K[1, j] = h * Cx[j] * (xi + h / 2);
                    K[2, j] = h * Cx[j] * (xi + h / 2);
                    K[3, j] = h * Cx[j] * (xi + h);
                }

                //K1 для всех функций
                for(int k = 0; k < n; k++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        K[0, k] += h * A[k, j] * Y[j];
                    }
                }

                //K2 для всех функций
                for (int k = 0; k < n; k++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        K[1, k] += h * A[k, j] * (Y[j] + K[0, j] / 2);
                    }
                }
                //K3 для всех функций
                for (int k = 0; k < n; k++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        K[2, k] += h * A[k, j] * (Y[j] + K[1, j] / 2);
                    }
                }
                //K4 для всех функций
                for (int k = 0; k < n; k++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        K[3, k] += h * A[k, j] * (Y[j] + K[2, j]);
                    }
                }

                for(int j = 0; j<n; j++)
                {
                    Y[j] += (K[0, j] + 2 * K[1, j] + 2 * K[2, j] + K[3, j]) / 6;
                }
                xi += h;

                //Обеспечивает вывод
                Console.Write("{0:0.00}     ",xi);
                for (int j = 0; j < n; j++)
                {
                    Console.Write("{0:0.000}     ", Y[j]);
                }
                Console.WriteLine();
            }
        }
    }
}
