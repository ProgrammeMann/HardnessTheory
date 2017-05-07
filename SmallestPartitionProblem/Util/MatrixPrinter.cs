using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallestPartitionProblem.Util
{
    class MatrixPrinter
    {

        public static void Print(int[][] matrix)
        {
            foreach(var r in matrix)
            {
                foreach(var c in r)
                {
                    Console.Write(c);
                }
                Console.WriteLine();
            }

        }

        public static void PrintList(List<int[]> list)
        {
            for (int i = 0; i < list[0].Length;i++)
            {
                for(int j = 0; j < list.Count(); j++)
                {
                    Console.Write(list[j][i]);
                }
                Console.WriteLine();
            }
        }
    }
}
