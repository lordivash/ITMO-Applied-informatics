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
            Console.Write("Размерность матрицы n = ");
            string s = Console.ReadLine();
            int n = Input_check(s);
            Console.WriteLine();

            //Creating matrices
            double[,] a = CM(n);
            double[,] b = CM(n);

            //Matrices sum
            double[,] c = SM(a, b, n);
            Showmatrix(c, n);

            Showmatrix(Productmatrix(a, a, n, n, n), n);

            Showmatrix(SM(Productmatrix(a, a, n, n, n), a, n), n);

            Showmatrix(c, n);

            double[,] c1 = Productmatrix(a, b, n, n, n);
            Showmatrix(c1, n);

            //Create inverse matrix
            double[,] Inva = Inversematrix(a, n);
            Showmatrix(Inva, n);
			
            Showmatrix(Productmatrix(Inva, a, n, n, n), n);
        }

        //Create Matrix
        static double[,] CM(int n)
        {
            double[,] a = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write("mas [" + i + "," + j + "] = ");
                    string s = Console.ReadLine();
                    a[i, j] = Input_check_double(s);
                }
            }
            Console.WriteLine();
            return a;
        }

        //Sum Matrix
        static double[,] SM(double[,] a, double[,] b, int n)
        {
            double[,] c = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    c[i, j] = a[i, j] + b[i, j];
                }
            }
            Console.WriteLine();
            return c;
        }
		
        static void Showmatrix(double[,] a, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.WriteLine("mas [" + i + "," + j + "] = " + a[i, j]);
                }
            }
            Console.WriteLine();
        }

        static double[,] Productmatrix(double[,] a, double[,] b, int n, int n1, int n2)
            double[,] c = new double[n, n2];
            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n2; i++)
                {
                    for (int j = 0; j < n1; j++)
                    {
                        c[k, i] += a[k, j] * b[j, i];
                    }
                }
            }
            return c;
        }
		
        static double[,] Inversematrix(double[,] a, int n)
        {
            //h - Матрица отражения(с её помощью матрица А будет преобразовываться в треугольную)
            double[,] h = new double[n, n];

            double[,] U = new double[n, 1];
            double[,] V = new double[1, n];
            double[,] e = new double[n, 1];
            double[,] Inversemat = new double[n, n];
            double[,] Q_T = new double[n, n];
            double[,] c = new double[n, n];
            double modU = 0;
            double Gamma;

            //Делаем матрицу Q_T единичной матрицей
            for (int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        Q_T[i, j] = 1;
                    }
                    else
                    {
                        Q_T[i, j] = 0;
                    }
                }
            }

            //присваиваем вектору значения i-ого столбца матрицы A начиная с i-ого эл-та
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j < i) { U[j, 0] = 0; }
                    else
                    {
                        U[j, 0] = a[j, i];
                    }
                }

                //Находим норму вектора
                modU = 0;
                for (int j = 0; j < n; j++)
                {
                    modU += U[j, 0] * U[j, 0];
                }
                modU = Math.Sqrt(modU);

                //Находим матрицу отражений
                if (modU == 0)
                {
                    for (int j = 0; j < n; j++)
                    {
                        U[j, 0] = 0;
                    }
                    U[i, 0] = 1;
                    Gamma = 1 / 2;
                }
                else
                {
                    for (int j = 0; j < n; j++)
                    {
                        U[j, 0] = U[j, 0] / modU;
                    }
                    if (U[i, 0] == 0)
                    {
                        U[i, 0] = 1;
                    }
                    else
                    {
                        U[i, 0] = U[i, 0] / Math.Abs(U[i, 0]) * (1 + Math.Abs(U[i, 0]));
                    }
                    Gamma = Math.Abs(U[i, 0]);
                }
                for (int j = 0; j < n; j++)
                {
                    V[0, j] = -U[j, 0] / Gamma;
                }
                h = Productmatrix(U, V, n, 1, n);
                for (int j = 0; j < n; j++)
                {
                    h[j, j] += 1;
                }

                //Произведение матриц h_n
                Q_T = Productmatrix(h, Q_T, n, n, n);

                //После преобразований матрица а - треугольная
                a = Productmatrix(h, a, n, n, n);
            }

            //Создаем столбец единичной матрицы (E_i в уравнении RX_i = Q^T * E_i)
            for (int i = 0; i<n; i++)
            {
                for(int j = 0; j<n; j++)
                {
                    if(i == j)
                    {
                        e[j, 0] = 1;
                    }
                    else                       
                    {
                        e[j, 0] = 0;
                    }
                }

                //Умножаем E_i на Q^T
                e = Productmatrix(Q_T, e, n, n, 1);

                //Осуществляем обратный ход метода Гаусса
                for (int k = 0; k < n - 1; k++)
                {
                    for(int j = 0; j < n - 1 - k; j++)
                    {
                        e[n - 2 - j - k, 0] = e[n - 2 - j - k, 0] - a[n - 2 - k - j, n - 1 - k] / a[n - 1 - k, n - 1 - k] * e[n - 1 - k, 0];
                    }
                }

                //Присваиваем столбцу X_i обратной матрицы его значения
                for (int j = 0; j<n; j++)
                {
                    c[n - 1 - j, i] = e[n - 1 - j, 0] / a[n - 1 - j, n - 1 - j];
                }
            }
            return c;
        }

        //Проверка ввода на целое положительное число
        static int Input_check(string s)
        {
            bool convertcheck;
            convertcheck = int.TryParse(s, out int n);
            while (convertcheck == false | n <= 0)
            {
                Console.Write("Вы ошиблись, попробуйте снова: ");
                s = Console.ReadLine();
                convertcheck = int.TryParse(s, out n);
            }
            return n;
        }

        //Проверка ввода на дробное число
        static double Input_check_double(string s)
        {
            bool convertcheck;
            convertcheck = double.TryParse(s, out double n);
            while (convertcheck == false)
            {
                Console.Write("Вы ошиблись, попробуйте снова: ");
                s = Console.ReadLine();
                convertcheck = double.TryParse(s, out n);
            }
            return n;
        }
    }
}