using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Симплекс_метод
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 3, m = 3;
            float[,] A = { { 18,15,12,1,0,0},
                           { 6,4,8,0,1,0},
                           { 5,3,3,0,0,1}
            };

            float[,] A0 = { { 3, 360 },
                            { 4, 192 },
                            { 5, 180 }
            };

            float[] C_delta = new float[m];
            float[] delta = new float[n + m];

            float[] C = { 9, 10, 16, 0, 0, 0 };

            Show_ans(A0, n);

            float F0 = 0;
            for (int i = 0; i<m; i++)
            {
                F0 += C_delta[i] * A0[i, 1];
            }
            Console.WriteLine("F0 = " + 0);


            for (int i = 0; i< n + m; i++)
            {
                delta[i] -= C[i];
            }

            float a;
            int r;
            int k;
            float max;
            float min;
            bool delta_negative;

            do
            {
                //Определение номера k вводимого в базис вектора 
                max = 0;
                k = 0;
                for (int i = 0; i < n + m; i++)
                {
                    if (delta[i] < 0 && -delta[i] > max)
                    {
                        k = i;
                        max = -delta[i];
                    }
                }

                //Определение номера r исключаемого из базиса вектора
                min = float.MaxValue;
                r = 0;
                for (int i = 0; i < m; i++)
                {
                    if (A0[i, 1] / A[i, k] < min)
                    {
                        min = A0[i, 1] / A[i, k];
                        r = i;
                    }
                }

                C_delta[r] = C[k];

                //Вычисление всех столбцов, кроме k-ого и r строки
                a = A[r, k];
                for (int j = 0; j < m + n; j++)
                {
                    if (j == k) { continue; }
                    for (int i = 0; i < n; i++)
                    {
                        if (i == r) { continue; }
                        A[i, j] -= A[r, j] / a * A[i, k];
                        if (j == 0)
                        {
                            A0[i, 1] = A0[i, 1] - A0[r, 1] / a * A[i, k];
                        }
                    }
                }
                //Вычисление k-ого столбца
                for (int i = 0; i < n; i++)
                {
                    if (i == r) { continue; }
                    A[i, k] -= A[r, k] / a * A[i, k];
                }

                //Вычисление r строки таблицы
                A0[r, 1] /= a;
                for (int i = 0; i < n + m; i++)
                {
                    A[r, i] = A[r, i] / a;
                }
                A0[r, 0] = k;

                //Вычисление значения ЦФ
                F0 = 0;
                for (int i = 0; i < m; i++)
                {
                    F0 += C_delta[i] * A0[i, 1];
                }

                Showarray(A);

                //Вычисление значений новых дельт
                for (int i = 0; i < n + m; i++)
                {
                    delta[i] = 0;
                    for (int j = 0; j < n; j++)
                    {
                        delta[i] += C_delta[j] * A[j, i];
                    }
                    delta[i] -= C[i];
                }

                Show_ans(A0, n);
                Console.WriteLine("F0 = " + F0);

                //Условие конца (нет отрицательных дельт)
                delta_negative = false;
                for (int i = 0; i < n + m; i++)
                {
                    if (delta[i] < 0) { delta_negative = true; }
                }

            } while (delta_negative);
        }

        //Вывод массива [n x m]
        static void Showarray(float[,] A)
        {
            Console.WriteLine();
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    Console.Write("{0:f2}  ", A[i, j]);
                }
                Console.WriteLine();
            }
        }

        //Вывод массива [n]
        static void Showarray(float[] A)
        {
            Console.WriteLine();
            for (int i = 0; i < A.GetLength(0); i++)
            {
                Console.Write("{0:f2}  ", A[i]);
            }

            Console.WriteLine();
        }
        
        //Вывод вектора ответов X
        static void Show_ans(float[,] A0, int n)
        {
            int m = A0.GetLength(0);
            float[] X = new float[n + m];
            for (int i = 0; i < m; i++)
            {
                int temp = (int)A0[i, 0];
                X[temp] = A0[i, 1];
            }
            Showarray(X);
        }
    }
}
