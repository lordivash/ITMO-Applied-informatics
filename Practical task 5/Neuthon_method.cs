using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Метод_Ньютона
{
    class Program
    {
        static void Main(string[] args)
        {
            float eps, x0;

            Console.Write("Введите допустимую погрешность: eps = ");
            eps = float.Parse(Console.ReadLine());
            Console.Write("Начальное приближение x0 = ");
            x0 = float.Parse(Console.ReadLine());

            Console.WriteLine("Значение корня x = " + Newton(eps, x0) + " +- " + eps);
        }

        public static float Newton(float eps, float x0)
        {
            int k = 0;
            float xk;

            do
            {
                xk = x0;
                x0 = x0 - F(x0) / dF(x0);
                k++;
            } while ((x0 - xk > eps) | (xk - x0 > eps));
            Console.WriteLine("Количество иттераций k = " + k);
            return x0;
        }

        public static float F(float x0)
        {
            return (x0 * x0) - (float)Math.Sin(x0);
        }

        public static float dF(float x0)
        {
            return 2 * x0 - (float)Math.Cos(x0);
        }
    }
}
