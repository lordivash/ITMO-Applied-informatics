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
                { 0, 1, 0, 0, 1 },
                { 1, 0, 1, 1, 0 },
                { 0, 1, 0, 0, 1 },
                { 0, 1, 0, 0, 1 },
                { 1, 0, 1, 1, 0 }
            };
            string s;
            int n;

            Console.Write("Начальный узел: ");
            s = Console.ReadLine();
            n = int.Parse(s);

            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    Console.WriteLine("A [" + i + "][" + j + "] = " + A[i - 1, j - 1]);
                }
            }

            bool[] Graphmap = new bool[5];
            Graphmap[n - 1] = true;
            DFS(n, A, Graphmap);
        }

        //true - вершина посещена
        //false - вершина не посещена
        static void DFS(int n, int[,] A, bool[] Graphmap)
        {
            Console.WriteLine(n);
            for (int i = 0; i < 5; i++)
            {
                if (A[n - 1, i] == 1 & Graphmap[i] == false)
                {
                    Graphmap[i] = true;
                    DFS(i + 1, A, Graphmap);
                }
            }
        }
    }
}
