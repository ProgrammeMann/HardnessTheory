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
                new int[] {0,1,0,0,1},
                new int[] {0,1,1,0,1},
                new int[] {0,1,1,1,1},
                new int[] {1,1,1,0,1}
            };

            int[][] arr2 = new int[][]
            {
                new int[] {1,0,1,1},
                new int[] {1,0,1,0},
                new int[] {0,1,0,0},
            };
            //Транспонирование матрицы и получение единичной главной диагонали
            arr = MatrixTransposer.Transpose(arr);
            MatrixPrinter.Print(arr);
            Console.WriteLine();

            Simplifyer s = new Simplifyer(arr); //Если нет решений, выкинет ошибку
            MatrixPrinter.Print(s.Matrix);

            Table table = new Table(s.Matrix);

            Solver solver = new Solver(table, s.CoveredRows, s.MustBeInEveryAnswer);
            solver.Solve();
            MatrixPrinter.PrintList(solver.BestAnswer);
            Console.Read();
        }
    }
}
