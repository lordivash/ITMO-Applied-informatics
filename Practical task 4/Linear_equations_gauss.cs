using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;
            Console.Write("Введите количество неизвестных n = ");
            n = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите элементы массива А = ");
            double[,] A = readmatrix(n, n);
            Console.WriteLine("Введите элементы массива B = ");
            double[] B = readmatrix(n);

            double[,] AB = new double[n, n + 1];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    AB[i, j] = A[i, j];
                }
                AB[i, n] = B[i];
            }
            showmatrix(AB);

            TriangleMatrix(AB);
            showmatrix(AB);

            int r = trianglematrixrang(AB);
            Console.WriteLine("Rang = " + r + "\n");

            double[,] X = Solve(AB);
        }

        //Создание и ввод матрицы
        static double[,] readmatrix(int n, int m)
        {
            double[,] Arr = new double[n, m];

            for (int i = 0; i<n; i++)
            {
                for(int j = 0; j<m; j++)
                {
                    Console.Write("Arr[" + i + "," + j + "] = ");
                    Arr[i, j] = double.Parse(Console.ReadLine());
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            return Arr;
        }

        //Ввод вектора-столбца
        static double[] readmatrix(int n)
        {
            double[] Arr = new double[n];

            for (int i = 0; i < n; i++)
            {
                Console.Write("Arr[" + i + "] = ");
                Arr[i] = double.Parse(Console.ReadLine());
            }
            Console.WriteLine();
            return Arr;
        }

        //Вычитание к-ой строки, умноженной на коэффициент, из всех строк ниже к-ой
        static void subtractrow(double[,] M, int k)
        {
            double m = M[k, k];
            int s = k + 1;

            // В случае нулевого элемента меняются строки
            while (m == 0 && s < M.GetLength(0))
            {
                m = M[s, k];
                if (m != 0)
                {
                    swaprows(M, s, k);
                }
                s++;

                // Если ненулевого элемента нет, заставляет следующий алгоритм пропустить работу
                if (s == M.GetLength(0))
                {
                    k = s;
                }
            }

            //Вычитание строк
            for (int i = k + 1; i<M.GetLength(0); i++)
            {

                double t = M[i, k] / m;
                for (int j = k; j < M.GetLength(1); j++)
                {
                    M[i, j] = M[i, j] - M[k, j] * t;
                }
            }
        }

        //Приведение матрицы к верхнетреугольной
        static void TriangleMatrix(double[,] M)
        {
            for (int i = 1; i < M.GetLength(0); i++)
                subtractrow(M, i - 1);
        }

        //Вывод матрицы
        static void showmatrix(double[,] M)
        {
            for (int i = 0; i<M.GetLength(0); i++)
            {
                for (int j = 0; j<M.GetLength(1); j++)
                {
                    Console.Write("{0,3} ", M[i,j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        //Решение системы линейных уравнений
        public static double[,] Solve(double[,] M)
        {
            int n = M.GetLength(0);
            TriangleMatrix(M);
            int r = trianglematrixrang(M);
            double[,] X = new double[r, n - r + 1];

            //Проверка неравенства нулевой строки числу
            for (int i = n - 1; i >= r; i--)
            {
                if(M[i, n] != 0)
                {
                    Console.WriteLine("Система не имеет решения");
                    return X;
                }
            }

            for (int i = r - 1; i >= 0; i--)
            {
                //Заполняем i строку массива ответа
                X[i, n - r] = M[i, n] / M[i, i];
                for (int j = n - r - 1; j >= 0; j--)
                {
                        
                    X[i, j] = -M[i, r + j] / M[i, i];
                }

                //Находим i-ый зависимый x
                for (int j = r - 1; j > i; j--)
                {
                    for (int k = n - r; k >= 0; k--)
                    {
                        X[i, k] -= M[i, i + 1 + (r - j - 1)] / M[i, i] * X[i + 1 + (r - j - 1), k];
                    }
                }
            }
            showanswer(X, n, r);
            return X;
        }

        //Нахождение ранга треугольной матрицы
        public static int trianglematrixrang(double[,] M)
        {
            int r;

            for (r = 0; r < M.GetLength(0); r++)
            {
                if (M[r, M.GetLength(1) - 2] == 0)
                {
                    return r;
                }
            }
            return r;
        }

        //Перемена строк местами
        public static void swaprows(double [,] M, int k1, int k2)
        {
            for (int j = 0; j<M.GetLength(1); j++)
            {
                double temp = M[k1,j];
                M[k1, j] = M[k2, j];
                M[k2, j] = temp;
            }
        }

        //Форматированный вывод ответа
        public static void showanswer(double[,] X, int n, int r)
        {
            for (int i = 0; i < r; i++)
            {
                string answer;
                answer = "X" + (i + 1) + " = ";
                for (int j = 0; j < n - r; j++)
                {
                    answer += X[i, j] + "C" + (j + 1) + " + ";
                }
                answer += X[i, n - r];
                Console.WriteLine(answer);
            }
            for (int i = r; i < n; i++)
            {
                Console.WriteLine("X" + (i + 1) + " = C" + (i - r + 1));
            }
        }
    }
}
