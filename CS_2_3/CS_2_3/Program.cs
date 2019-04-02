using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_2_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("a = ");
            float a = float.Parse(Console.ReadLine());
            Console.Write("b = ");
            float b = float.Parse(Console.ReadLine());

            string a_string = SingleToBinaryString(a), b_string = SingleToBinaryString(b);
            int sign = int.Parse(a_string[0].ToString()), b_sign = int.Parse(b_string[0].ToString());
            int ex = Convert.ToInt32(a_string.Substring(1, 8), 2), b_ex = Convert.ToInt32(b_string.Substring(1, 8), 2);
            int mant = Convert.ToInt32(a_string.Substring(9), 2), b_mant = Convert.ToInt32(b_string.Substring(9), 2);
            int singresult = 0, exresult = ex, mantresult = 0;

            mant = mant + (1 << 23);
            b_mant = b_mant + (1 << 23);


            if (ex > b_ex)
            {
                exresult = ex;
                b_mant = b_mant >> (ex - b_ex);
            }
            if (b_ex > ex)
            {
                exresult = b_ex;
                mant = mant >> (b_ex - ex);
            }


            Console.WriteLine("Result exponent: " + new string('0', 8 - Convert.ToString(exresult, 2).Length) + Convert.ToString(exresult, 2));
            Console.WriteLine("shifted a-mantisa: " + new string('0', 32 - Convert.ToString(mant, 2).Length) + Convert.ToString(mant, 2));
            Console.WriteLine("shifted a-mantisa: " + new string('0', 32 - Convert.ToString(b_mant, 2).Length) + Convert.ToString(b_mant, 2));


            if (sign == b_sign)
            {
                singresult = sign;
                mantresult = mant + b_mant;
            }
            else if (mant > b_mant && sign == 0 && b_sign == 1)
            {
                singresult = 0;
                mantresult = mant - b_mant;
            }
            else if (b_mant > mant && sign == 0 && b_sign == 1)
            {
                singresult = 1;
                mantresult = b_mant - mant;
            }
            else if (mant > b_mant && sign == 1 && b_sign == 0)
            {
                singresult = 1;
                mantresult = mant - b_mant;
            }
            else if (b_mant > mant && sign == 1 && b_sign == 0)
            {
                singresult = 0;
                mantresult = b_mant - mant;
            }

            Console.WriteLine("Add significants: " +
                new string('0', 32 - Convert.ToString(mantresult, 2).Length) + Convert.ToString(mantresult, 2));


            while ((mantresult >> 24) > 0)
            {
                mantresult = mantresult >> 1;
                exresult++;
            }
            while ((mantresult & (1 << 23)) != (1 << 23))
            {
                mantresult = mantresult << 1;
                exresult--;

            }

            mantresult = mantresult & ((1 << 23) - 1);
            string result_ex_string = new string('0', 8 - Convert.ToString(exresult, 2).Length) + Convert.ToString(exresult, 2);
            string result_mantisa_string = new string('0', 23 - Convert.ToString(mantresult, 2).Length) + Convert.ToString(mantresult, 2);
            string result_string = singresult.ToString() + result_ex_string + result_mantisa_string;



            Console.WriteLine("Exponent = " + Convert.ToString(exresult, 2));
            Console.WriteLine("Mantisa = " + Convert.ToString(mantresult, 2));

            Console.WriteLine("My bits: " + result_string);
            Console.WriteLine("Real bits: " + SingleToBinaryString(a + b));

            Console.WriteLine("Real = " + (a + b));
            Console.WriteLine("My = " + BinaryStringToSingle(result_string));


            Console.ReadLine();
        }

        public static string SingleToBinaryString(float f)
        {
            byte[] b = BitConverter.GetBytes(f);
            int i = BitConverter.ToInt32(b, 0);
            string result = Convert.ToString(i, 2);
            return new String('0', 32 - result.Length) + result;
        }

        public static float BinaryStringToSingle(string s)
        {
            int i = Convert.ToInt32(s, 2);
            byte[] b = BitConverter.GetBytes(i);
            return BitConverter.ToSingle(b, 0);

        }
    }
}