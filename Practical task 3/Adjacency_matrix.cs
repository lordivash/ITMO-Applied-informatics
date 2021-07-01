using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract_3_Graphs
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;
            string s;

            Console.Write("Введите количество узлов n = ");
            s = Console.ReadLine();

            //Проверка и ввод
            n = Input_check(s);

            Console.Write("Граф ориентированный? (y/n)");
            s = Console.ReadLine();

            while (s != "y" & s != "n")
            {
                Console.Write("Вы ошиблись, попробуйте снова: ");
                s = Console.ReadLine();
            }

            Console.WriteLine();

            //Создание и заполнение необходимой матрицы
            if (s == "y")
            {
                bool[,] Adjacency_matrix_oriented = Create_adjacency_matrix_oriented(n);
                Console.WriteLine();
                Show_matrix(Adjacency_matrix_oriented, n);
            }
            else
            {
                bool[,] Adjacency_matrix = Create_adjacency_matrix(n);
                Console.WriteLine();
                Show_matrix(Adjacency_matrix, n);
            }
        }

        //Метод вывода матрицы
        static void Show_matrix(bool[,] A, int n)
        {
            for (int i = 1; i <= n; i++)
            {
                Console.Write("{0,6}", i);
            }
            Console.WriteLine();
            for (int i = 1; i <= n; i++)
            {
                Console.Write("{0,-2}", i);
                for (int j = 1; j <= n; j++)
                {
                    Console.Write("{0,-6}", A[i - 1, j - 1]);
                }
                Console.WriteLine();
            }
        }

        //Проверка ввода на натуральное число
        static int Input_check(string s)
        {
            bool convertcheck;
            convertcheck = int.TryParse(s, out int n);
            while (convertcheck == false | n <= 0)
            {
                Console.Write("Вы ошиблись, попробуйте снова, n = ");
                s = Console.ReadLine();
                convertcheck = int.TryParse(s, out n);
            }
            return n;
        }

        //Cоздаем матрицу смежности (Обработка ввода предусмотрена)
        static bool[,] Create_adjacency_matrix(int n)
        {
            bool[,] Adjacency_matrix = new bool [n, n];
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

        //Матрица смежности для ориентированного графа
        static bool[,] Create_adjacency_matrix_oriented(int n)
        {
            bool[,] Adjacency_matrix = new bool[n, n];
            string s;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (Adjacency_matrix[i, j] == true)
                    {
                        Adjacency_matrix[i, j] = false;
                        continue;
                    }

                    Console.Write("Узел " + (i + 1) + " Имеет связь с узлом " + (j + 1) + " ? (y/n)");
                    s = Console.ReadLine();
                    if (s == "y")
                    {
                        if (i < j)
                        {
                            Adjacency_matrix[j, i] = true;
                        }
                        Adjacency_matrix[i, j] = true;
                    }
                    else if (s == "n")
                    {
                        Adjacency_matrix[i, j] = false;
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
    }
}