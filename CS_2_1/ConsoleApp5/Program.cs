using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_2_1
{
    class Program
    {
        static void Main(string[] args)
        {

            string n0 = Console.ReadLine();
            string n1 = Console.ReadLine();
            Multiply(n0, n1);
            Console.ReadKey();
        }

        public static void Multiply(string n0, string n1)
        {
            Int64 multiplicand, multiplier, product = 0;
            Int64 firstMultiplicand, firstMultiplier;
            firstMultiplicand = multiplicand = Int32.Parse(n0);
            firstMultiplier = multiplier = Int32.Parse(n1);
            for (int i = 0; i < 32; ++i)
            {
                Console.WriteLine("Крок №" + (i + 1) + ":\n");
                Console.WriteLine(value: $"Multiplicand:\n{output(Convert.ToString(multiplicand, 2))}\nMultiplier:\n{output(Convert.ToString(multiplier, 2))}\n");

                short lsb = (short)(multiplier & 1);

                bool v = lsb == 1;
                if (v)
                {
                    Console.WriteLine("Додавання.\n");
                    product += multiplicand;
                }
                Console.WriteLine("Результат:\n" + output(Convert.ToString(product, 2)) + "\n");
                Console.WriteLine("Зсуваємо бiти:");


                multiplicand <<= 1;
                multiplier >>= 1;
            }

            Console.WriteLine(value: $"\n{output(Convert.ToString(firstMultiplicand, 2))}\nx\n{output(Convert.ToString(firstMultiplier, 2))}\n=\n{output(Convert.ToString(product, 2))}\n");

            Console.WriteLine(firstMultiplicand + " x " + firstMultiplier + " = " + product);
        }

        static string output(string a)
        {
            int count = 64 - a.Length;
            string b = "";
            for (int i = 0; i < count; ++i)
            {
                b += "0";
            }

            return b + a;
        }
    }
}