using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenAlg
{
    public class GenTools
    {
        private List<float> X, Y;

        public int Count
        {
            get { return X.Count; }
        }

        public GenTools(int n)
        {
            X = new List<float>();
            Y = new List<float>();
            Random rnd = new Random();
            for (int i = 0; i < n; ++i)
            {
                X.Add(rnd.Next(40));
                Y.Add(rnd.Next(40));
            }
        }

        private List<char[]> FloatToBin(float num)
        {
            List<char[]> list = new List<char[]>();

            //конвертим целую часть в двоичный код
            int intPart = (int)num;
            string s1 = Convert.ToString(intPart, 2);
            if (s1.Length < 16)
            {
                int n = 16 - s1.Length;
                for (int i = 0; i < n; ++i)
                    s1 = '0' + s1;
            }

            list.Add(s1.ToCharArray());

            //конвертим дробную часть в двоичный код
            float floatPart = num - intPart;
            string s2 = string.Empty;
            for (int i = 0; i < 16; i++)
            {
                floatPart = floatPart * 2;
                if (floatPart >= 1)
                {
                    s2 += 1;
                    floatPart = floatPart - 1;
                }
                else
                {
                    s2 += 0;
                }
            }
            list.Add(s2.ToCharArray());

            return list;
        }

        private float BinToFloat(List<char[]> list)
        {
            //конвертим целую часть двоичного кода в float
            string s1 = new string(list[0]);
            //while (s1[0] == '0')
            //  s1 = s1.Substring(1, s1.Length - 1);

            float intPart = Convert.ToInt32(s1, 2);

            //конвертим дробную часть двоичного кода в float
            float floatPart = 0;
            for (int i = -1, j = 0; i >= -8 && j < 8; i--, j++)
            {
                floatPart += int.Parse(list[1][j].ToString()) * (float)Math.Pow(2, i);
            }

            float num = intPart + floatPart;

            return num;
        }

        public void Mutate(int Ind)
        {
            List<char[]> BinX = FloatToBin(X[Ind]);
            List<char[]> BinY = FloatToBin(Y[Ind]);
            Random rnd = new Random();
            for (int i = 0; i <= BinX.Count; i++)
            {
                if (rnd.Next(9) < 4)
                {
                    if (BinX[0][i] == '0')
                        BinX[0][i] = '1';
                    else
                        BinX[0][i] = '0';
                    if (BinX[1][i] == '0')
                        BinX[1][i] = '1';
                    else
                        BinX[1][i] = '0';
                }
                if (rnd.Next(9) < 4)
                {
                    if (BinY[0][i] == '0')
                        BinY[0][i] = '1';
                    else
                        BinY[0][i] = '0';
                    if (BinY[1][i] == '0')
                        BinY[1][i] = '1';
                    else
                        BinY[1][i] = '0';
                }
                /*
                BinX[0][i] =  ((Convert.ToInt16(BinX[0][i]) + 1) % 2);
                BinX[1][i] = Convert.ToChar((Convert.ToInt16(BinX[1][i]) + 1) % 2);
                BinY[0][i] = Convert.ToChar((Convert.ToInt16(BinY[0][i]) + 1) % 2);
                BinY[1][i] = Convert.ToChar((Convert.ToInt16(BinY[1][i]) + 1) % 2);
                 */
            }

            X[Ind] = BinToFloat(BinX);
            Y[Ind] = BinToFloat(BinY);
        }

        public void Crossing(int Ind1, int Ind2)
        {
            List<char[]> BinX_1 = FloatToBin(X[Ind1]);
            List<char[]> BinY_1 = FloatToBin(Y[Ind1]);

            List<char[]> BinX_2 = FloatToBin(X[Ind2]);
            List<char[]> BinY_2 = FloatToBin(Y[Ind2]);

            for (int i = 2; i <= 12; i++)
            {
                char c = BinX_1[0][i];
                BinX_1[0][i] = BinX_2[0][i];
                BinX_2[0][i] = c;
                c = BinY_1[0][i];
                BinY_1[0][i] = BinY_2[0][i];
                BinY_2[0][i] = c;
            }

            for (int i = 4; i <= 10; i++)
            {
                char c = BinX_1[0][i];
                BinX_1[1][i] = BinX_2[1][i];
                BinX_2[1][i] = c;
                c = BinY_1[1][i];
                BinY_1[1][i] = BinY_2[1][i];
                BinY_2[1][i] = c;
            }


            X[Ind1] = BinToFloat(BinX_1);
            Y[Ind1] = BinToFloat(BinY_1);

            X[Ind2] = BinToFloat(BinX_2);
            Y[Ind2] = BinToFloat(BinY_2);

        }

        public void Duel(int Ind1, int Ind2, Func<float, float, float> f)
        {
            float res_1 = f(X[Ind1], Y[Ind1]);
            float res_2 = f(X[Ind2], Y[Ind2]);
            if (res_1 < res_2)
            {
                X[Ind2] = X[Ind1];
                Y[Ind2] = Y[Ind1];
            }
            else
            {
                X[Ind1] = X[Ind2];
                Y[Ind1] = Y[Ind2];
            }
        }
        public void Print()
        {
            for (int i = 0; i < X.Count; i++)
                Console.Write(X[i] + " ");
            Console.WriteLine();
            for (int i = 0; i < Y.Count; i++)
                Console.Write(Y[i] + " ");
        }
    }
}
