using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_Graph_both_ways
{
    class Program
    {
        static void Main(string[] args)
        {
            string s;
            int n = 0, m = 0;
            bool statement;

            Console.WriteLine("Как вы хотите задать граф?");
            Console.WriteLine("1: Матрица смежности");
            Console.WriteLine("2: Матрица инцидентности");
            Console.Write("Введите ваш выбор (1 или 2): ");

            for (int i = 0; i < 1; i++)
            {
                s = Console.ReadLine();
                n = Input_check_N(s);

                if (n == 1)
                {
                    Console.Write("\nВведите количество узлов графа: n = ");
                    n = Input_check_N(Console.ReadLine(), "Ошибка, введите натуральное число n =");

                    bool[,] adjacency_matrix = Create_adjacency_matrix(n);
                    Show_matrix(adjacency_matrix, n, n);
                }
                else if (n == 2)
                {
                    Console.Write("\nВведите количество узлов графа: n = ");
                    n = Input_check_N(Console.ReadLine());

                    for (int j = 0; j < 1; j++)
                    {
                        Console.Write("Введите количество ребер m = ");
                        m = Input_check_N(Console.ReadLine(), "Ошибка, введите натуральное число m = ");

                        //Условие на m
                        statement = m <= n * (n + 1) / 2;
                        if (statement == false)
                        {
                            Console.WriteLine("Число m должно быть меньше n * (n + 1) / 2");
                            j--;
                        }
                    }

                    Console.WriteLine();
                    bool[,] Incidency_matrix = Create_incidency_matrix(n, m);
                    Show_matrix(Incidency_matrix, n, m);
                }
                else
                {
                    Console.Write("Вы ошиблись, введите 1 или 2: ");
                    i--;
                }
            }
        }

        //Проверка ввода на натуральное число
        static int Input_check_N(string s, string message = "Ошибка, введите заного: ")
        {
            bool convertcheck;
            convertcheck = int.TryParse(s, out int n);
            while (convertcheck == false | n <= 0)
            {
                Console.Write(message);
                s = Console.ReadLine();
                convertcheck = int.TryParse(s, out n);
            }
            return n;
        }

        //Метод вывода матрицы
        static void Show_matrix(bool[,] A, int n, int m)
        {
            for(int i = 1; i <= m; i++)
            {
                Console.Write("{0,6}", i);
            }
            Console.WriteLine();
            for (int i = 1; i <= n; i++)
            {
                Console.Write("{0,-2}",i);
                for (int j = 1; j <= m; j++)
                {
                    Console.Write("{0,-6}", A[i - 1, j - 1]);
                }
                Console.WriteLine();
            }
        }

        //Cоздаем матрицу смежности (Обработка ввода предусмотрена)
        static bool[,] Create_adjacency_matrix(int n)
        {
            bool[,] Adjacency_matrix = new bool[n, n];
            string s;

            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    Console.Write("Узел " + (i + 1) + " Имеет связь с узлом " + (j + 1) + " ? (y/n)");
                    s = Console.ReadLine();
                    if (s == "y")
                    {
                        Adjacency_matrix[i, j] = true;
                        Adjacency_matrix[j, i] = true;
                    }
                    else if (s == "n")
                    {
                        Adjacency_matrix[i, j] = false;
                        Adjacency_matrix[j, i] = false;
                    }
                    else
                    {
                        Console.WriteLine("Вы ошиблись, попробуйте снова");
                        j--;
                    }
                }
            }
            return Adjacency_matrix;
        }

        //Создаем матрицу инцидентности
        static bool[,] Create_incidency_matrix(int n, int m)
        {
            bool[,] Incidency_matrix = new bool[n, m];
            int a;

            for (int i = 0; i < m; i++)
            {
                Console.Write("Укажите, каким узлам инцидентно ребро " + (i + 1) + ": \n");
                for (int j = 1; j < 3; j++)
                {
                    Console.Write("Узел № " + j + ": ");
                    do
                    {
                        a = Input_check_N(Console.ReadLine(), "Вы ошиблись с номером, узел № " + j + ": ");
                        if (a > n)
                        {
                            Console.Write("Узла с таким номером не существует, узел № " + j + ": ");
                        }
                    } while (a > n);

                    Incidency_matrix[a - 1, i] = true;
                }
                Console.WriteLine();
            }
            return Incidency_matrix;
        }
    }
}
