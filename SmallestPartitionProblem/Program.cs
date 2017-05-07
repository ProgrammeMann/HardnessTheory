using SmallestPartitionProblem.Actions;
using SmallestPartitionProblem.Classes;
using SmallestPartitionProblem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallestPartitionProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            //сначала строка потом ряд
            int[][] arr = new int[][]
            {
                new int[] {1,0,0,1,0},
                new int[] {0,1,1,0,1},
                new int[] {0,1,1,0,1},
                new int[] {1,0,1,0,0},
                new int[] {0,0,1,0,0}
            };

            int[][] arr2 = new int[][]
            {
                new int[] {1,0,1,0 },
                new int[] {0,1,0,0},
                new int[] {0,1,0,1},
            };
            //Транспонирование матрицы и получение единичной главной диагонали
            arr = MatrixTransposer.Transpose(arr);
            MatrixPrinter.Print(arr);
            Console.WriteLine();

            Table table = new Table(arr);

            Solver solver = new Solver(table);
            solver.Solve();
            MatrixPrinter.PrintList(solver.BestAnswer);
            Console.Read();
        }
    }
}
