﻿namespace ManyStepsProceses
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество вершин графа");
            int k = Convert.ToInt32(Console.ReadLine());
            int[,] MasVersh = new int[k, k];
            for(int i = 0; i < k; i++)
            {
                int j = 0;
                foreach (var l in Array.ConvertAll(Console.ReadLine().Split(" "), str => Convert.ToInt32(str)))
                {
                    MasVersh[i, j++] = l;
                }
            }
            ZadachaManyStepsProceses Zad = new ZadachaManyStepsProceses(MasVersh);
        }
        class ZadachaManyStepsProceses
        {
            TreeGraph Graph;
            public ZadachaManyStepsProceses(int[,] MasVersh /*Массив отношений вершин*/)
            {
                Graph = new TreeGraph(MasVersh); //Создаём массив на введённое количество вершин
                List<int> list = new List<int>();
                Graph.sformMarsh(Graph.FirstNode(), list);
                Console.Write("Маршрут ");
                foreach(var i in list)
                {
                    Console.Write($"{i} ");
                }
                Console.WriteLine($"\nДлинна маршрута: {Graph.FirstNode().OptimalPath}");
            }
        }
    }
}