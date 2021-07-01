using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Метод_Пикара_для_решения_ДУ
{
    class Program
    {
        static void Main(string[] args)
        {
            float x0 = 0;
            float y10 = 1, y20 = 1;

            float a = 1, b = 1;

            //Максимум функций
            float M = 6;            
            //Максимум производных
            float N = 1;

            float h;

            if (a < b / M) { h = a; }
            else { h = b / M; }

            double eps = 1e-4;

            int n = itter(a, b, M, eps);

            float[] Y1 = new float[n + 1];
            float[] Y2 = new float[n + 1];

            Y1[0] = 3;
            Y2[0] = 3;

            Picar(Y1, Y2, y10, y20);

            for (int i = 0; i<n; i++)
            {
                Console.Write(Y1[i] + " * x^" + i + " + ");
            }
            Console.WriteLine("\nс точностью eps = " + eps);
            Console.WriteLine("\nна |x - x0| < " + h + "; |y1 - 1| < 1; |y2 - 1| < 1");
        }

        //Вычисляет необходимое число иттераций
        static int itter(float a, float b, float M, double eps)
        {
            int n = 0;
            int fact = 1;
            float h;

            if (a < b / M) { h = a; }
            else { h = b / M; }

            do
            {
                n++;
                fact *= n;

              //N^n пропущено, так как N = 1
            } while (eps < M * Math.Pow(h, n + 1) / (fact * (n + 1)));

            Console.WriteLine("Требуемое количество иттераций: n = " + n);

            return n;
        }

        //Реализация метода Пикара для данной системы ДУ
        static void Picar(float[] Y1, float[] Y2, float y10, float y20)
        {
            float[] pastY1 = new float[Y1.GetLength(0)];

            for (int i = 0; i < Y1.GetLength(0) - 1; i++)
            {
                for (int j = i; j >= 0; j--)
                {
                    pastY1[j] = Y1[j];
                    Y1[j] = Y1[j] + 2 * Y2[j];
                    Y1[j + 1] = Y1[j] / (j + 1);

                    Y2[j] = 2 * pastY1[j] + Y2[j];
                    Y2[j + 1] = Y2[j] / (j + 1);
                }
                Y1[0] = y10;
                Y2[0] = y20;
            }

        }
    }
}
