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
                { 0, 1, 1, 0 },
                { 0, 0, 1, 1 },
                { 1, 0, 0, 1 },
                { 0, 1, 0, 0 }
            };
            string s;
            int n;

            Console.Write("Начальный узел: ");
            s = Console.ReadLine();
            n = int.Parse(s);


            for (int i = 1; i <= 4; i++)
            {
                Console.Write("{0,3}", i);
            }
            Console.WriteLine();
            for (int i = 1; i <= 4; i++)
            {
                Console.Write(i);
                for (int j = 1; j <= 4; j++)
                {
                    Console.Write("{0,3}", A[i - 1, j - 1]);
                }
                Console.WriteLine();
            }
            

            int[] Graphmap = new int[4];
            int[] Queue = new int[4];
            BFS(n, A);
        }

        //0 - вершина не найдена
        //-1 - вершина найдена, но могут быть необследованные смежные вершины
        //1 - вершина обследована
        static void BFS(int n, int[,] A)
        {
            int[] Graphmap = new int[4];
            int[] Queue = new int[4];

            Queue[0] = n;
            Graphmap[n - 1] = -1;

            while (Queue[0] != 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (A[Queue[0] - 1, i] == 1 & Graphmap[i] == 0)
                    {
                        Graphmap[i] = -1;
                        for (int j = 0; j < 4; j++)
                        {
                            if (Queue[j] == 0)
                            {
                                Queue[j] = i + 1;
                                break;
                            }
                        }
                    }
                }
                Graphmap[Queue[0] - 1] = 1;

                Console.WriteLine(Queue[0]);

                for (int i = 0; i < 3; i++)
                {
                    Queue[i] = Queue[i + 1];
                }
                Queue[3] = 0;
            }
        }
    }
}
