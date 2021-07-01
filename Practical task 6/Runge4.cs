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
            float h0, x0, y0;
            int m;

            Console.Write("x0 = ");
            x0 = float.Parse(Console.ReadLine());

            Console.Write("y0 = ");
            y0 = float.Parse(Console.ReadLine());

            Console.Write("Шаг таблицы h0 (решение будет искаться на [x0; x0 + h0]) = ");
            h0 = float.Parse(Console.ReadLine());

            Console.Write("Коэффициент дробления шага m = ");
            m = int.Parse(Console.ReadLine());

            RK4(x0, y0, h0, m);
        }

        static void RK4(float x0, float y0, float h0, int m)
        {
            float h = h0 / m;
            float K1, K2, K3, K4;
            float xi = x0, yi = y0;

            Console.WriteLine(" X           Y");
            Console.WriteLine("{0,-10}{1}", x0, y0);

            for (int i = 1; i <= m; i++)
            {
                K1 = func(xi, yi);
                K2 = func(xi + h / 2, yi + h / 2 * K1);
                K3 = func(xi + h / 2, yi + h / 2 * K2);
                K4 = func(xi + h, yi + h * K3);

                yi = yi + h / 6 * (K1 + 2 * K2 + 2 * K3 + K4);
                xi = xi + h;

                Console.WriteLine("{0,-10}{1}",xi, yi);
            }
        }

        //Решение ДУ y = C * e^(-x^2/2)
        static float func(float x0, float y0)
        {
            return -x0 * y0;
        }
    }
}
