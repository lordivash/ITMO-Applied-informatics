using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4_poisk_v_shiriny
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] A = {
                { 0, 1, 4, 0, 2, 0 },
                { 0, 0, 0, 9, 0, 0 },
                { 4, 0, 0, 7, 0, 0 },
                { 0, 9, 7, 0, 0, 2 },
                { 0, 0, 0, 0, 0, 8 },
                { 0, 0, 0, 0, 0, 0 }
            };
            string s;
            int n;

            Console.Write("Начальный узел n = ");
            s = Console.ReadLine();
            n = int.Parse(s);

            Console.WriteLine();
            Dextra(n, A);
        }

        static void Dextra(int n, int[,] A)
        {
            bool[] visited = new bool[6];
            int[] distance = new int[6];
            int node_min = 0;

            //Инициализация
            for (int i = 0; i < 6; i++)
            {
                distance[i] = int.MaxValue;
            }
            distance[n - 1] = 0;

            //Начало алгоритма
            for (int j = 0; j < 6; j++)
            {
                //Поиск вершины с наименьшим значением пути
                int min = int.MaxValue;
                for (int i = 0; i < 6; i++)
                {
                    if (visited[i] == false & distance[i] <= min)
                    {
                        min = distance[i];
                        node_min = i;
                    }
                }

                //Пересмотрение расстояний до ближайших вершин
                for (int i = 0; i < 6; i++)
                {
                    if (A[node_min, i] > 0 & distance[node_min] + A[node_min, i] <= distance[i])
                    {
                        distance[i] = distance[node_min] + A[node_min, i];
                    }
                }

                visited[node_min] = true;
                Console.WriteLine("Выполнилось для узла " + (node_min + 1));
            }

            //Вывод ответа
            for(int i = 0; i < 6; i++)
            {
                Console.Write("\nРасстояние от " + n + " до " + (i+1) + " узла = " + distance[i]);
            }
        }
    }
}
