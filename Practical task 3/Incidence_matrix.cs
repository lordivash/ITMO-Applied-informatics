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
            int n, m;
            string s;

            //Проверка и ввод
            Console.Write("Введите количество узлов n = ");
            s = Console.ReadLine();
            n = Input_check_N(s);

            Console.Write("Введите количество ребер m = ");
            s = Console.ReadLine();
            m = Input_check_N(s);

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
                int[,] Adjacency_matrix_oriented = Create_incidency_matrix_oriented(n, m);
                Console.WriteLine();
                Show_matrix(Adjacency_matrix_oriented, n, m);
            }
            else
            {
                bool[,] Adjacency_matrix = Create_incidency_matrix(n, m);
                Console.WriteLine();
                Show_matrix(Adjacency_matrix, n, m);
            }
        }

        //Метод вывода матрицы
        static void Show_matrix(bool[,] A, int n, int m)
        {
            for (int i = 1; i <= m; i++)
            {
                Console.Write("{0,6}", i);
            }
            Console.WriteLine();
            for (int i = 1; i <= n; i++)
            {
                Console.Write("{0,-2}", i);
                for (int j = 1; j <= m; j++)
                {
                    Console.Write("{0,-6}", A[i - 1, j - 1]);
                }
                Console.WriteLine();
            }
        }

        //Метод вывода матрицы
        static void Show_matrix(int[,] A, int n, int m)
        {
            for (int i = 1; i <= m; i++)
            {
                Console.Write("{0,3}", i);
            }
            Console.WriteLine();
            for (int i = 1; i <= n; i++)
            {
                Console.Write(i);
                for (int j = 1; j <= m; j++)
                {
                    Console.Write("{0,3}", A[i - 1, j - 1]);
                }
                Console.WriteLine();
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

        //Создаем матрицу инцидентности для ориентированного графа
        static int[,] Create_incidency_matrix_oriented(int n, int m)
        {
            int[,] Incidency_matrix = new int[n, m];
            string[] message = { "выходит из узла", "входит в узел" };
            int a;

            for (int i = 0; i < m; i++)
            {
                Console.Write("Укажите, каким узлам инцидентно ребро " + (i + 1) + ": \n");
                for (int j = 0; j < 2; j++)
                {
                    Console.Write("Ребро " + message[j] + " №: ");
                    do
                    {
                        a = Input_check_N(Console.ReadLine(), "Вы ошиблись с номером, ребро " + message[j] + " №: ");
                        if (a > n)
                        {
                            Console.Write("Узла с таким номером не существует, ребро " + message[j] + " №: ");
                        }
                    } while (a > n);

                    Incidency_matrix[a - 1, i] = 1 - 2 * j;
                }
                Console.WriteLine();
            }
            return Incidency_matrix;
        }
    }
}
