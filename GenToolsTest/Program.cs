using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenAlg;

namespace GenToolsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int popCount = 20, iters = 10;
            string str;

            bool flag = true;
            while (flag)
            {
                Console.Write("Задайте размер популяции ");
                str = Console.ReadLine();

                flag = !Int32.TryParse(str, out popCount) || popCount <= 0;
            }

            flag = true;
            while (flag)
            {
                Console.Write("Задайте количество итераций ");
                str = Console.ReadLine();

                flag = !Int32.TryParse(str, out iters) || iters <= 0;
            }

            Random rnd = new Random();
            GenTools population = new GenTools(popCount);
            population.Print();
            Console.WriteLine();
            for (int i = 0; i < iters; ++i)
            {
                Console.WriteLine("Итерация {0}", i);
                if (rnd.Next(9) < 2)
                {
                    int Ind1 = rnd.Next(population.Count);
                    int Ind2 = rnd.Next(population.Count);
                    while (Ind1 == Ind2)
                        Ind2 = rnd.Next(population.Count);

                    population.Crossing(Ind1, Ind2);
                }

                if (rnd.Next(9) < 1)
                    population.Mutate(rnd.Next(population.Count));

                int Ind_1 = rnd.Next(population.Count);
                int Ind_2 = rnd.Next(population.Count);
                while (Ind_1 == Ind_2)
                    Ind_2 = rnd.Next(population.Count);

                population.Duel(Ind_1, Ind_2, (x, y) => x + y);
                population.Print();
                Console.WriteLine();
            }
            Console.Read();
        }
    }
}
