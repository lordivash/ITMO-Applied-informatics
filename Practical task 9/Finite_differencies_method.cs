using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Метод_прогноза_и_коррекции
{
    class Program
    {
        static void Main(string[] args)
        {
            float l = 6;    //Правая граница сетки
            float tk = 6;   //Верхняя граница сетки

            Console.Write("Введите количество значение сетки по t, n = ");
            int n = int.Parse(Console.ReadLine());
            Console.Write("Введите количество значений сетки по x, m = ");
            int m = int.Parse(Console.ReadLine());

            float[,] U = new float[n, m];

            MKR(U, l, tk);

            for (int i = n - 1; i >= 0; i--)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write("{0:f1} ",U[i, j]);
                }
                Console.WriteLine();
            }
        }

        //Правая граница сетки
        static float ult(float l, float t)
        {
            return (float)Math.Pow(l + 2 * t, 2);
        }

        //Нижняя граница сетки
        static float ux(float x)
        {
            return x * x;
        }

        //Производные функции по t в нижней границе сетки
        static float dux(float x)
        {
            return 4 * x;
        }

        //Левая граница сетка
        static float ut(float t)
        {
            return 4 * (float)Math.Pow(t, 2);
        }

        //Метод конечных разностей
        static void MKR(float[,] U, float l, float tk)
        {
            int n = U.GetLength(0);
            int m = U.GetLength(1);

            float dt = tk / (n - 1);
            float dx = l / (m - 1);

            //Заполнение правой и левой границы сетки
            for (int i = 0; i < n; i++)
            {
                U[i, 0] = ut(i * dt);
                U[i, m - 1] = ult(l, i * dt);
            }

            //Заполнение нижней границы сетки
            for (int i = 0; i < m; i++)
            {
                U[0, i] = ux(i * dx);
            }

            //Заполнение первого временного слоя сетки
            for (int i = 1; i < m - 1; i++)
            {
                U[1, i] = U[0, i] + dux(i * dx) * dt;
            }

            //Нахождение значений функции МКР
            for (int i = 1; i < n - 1; i++)
            {
                for (int j = 1; j < m - 1; j++)
                {
                    //Явное выражение для нового значения функции
                    U[i + 1, j] = 4 * (dt * dt) / (dx * dx) * 
                        (U[i, j + 1] - 2 * U[i, j] + U[i, j - 1]) + 2 * U[i, j] - U[i - 1, j];
                }
            }
        }
    }
}