using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallestPartitionProblem.Classes
{
    class Table
    {

        //правильно указывать arr[row][col]
        public Block[] Blocks { get; set; }

        public Table(int[][] transposedMatrix)
        {
            Blocks = new Block[transposedMatrix.Length];
            FillTable(transposedMatrix);
        }

        private void FillTable(int[][] matrix)
        {
            for (int col = 0; col < matrix[0].Length; col++)
            {
                int[] currentCol = new int[matrix.Length];
                int p = 0;
                bool hasOne = false;
                for(int row = 0; row<matrix.Length; row++)
                {
                    if (matrix[row][col] == 1)
                    {
                        hasOne = true;
                    }
                    if (!hasOne)
                    {
                        p++;
                    }
                    currentCol[row] = matrix[row][col];
                }
                if(Blocks[p] == null)
                {
                    Blocks[p] = new Block(p);
                }
                Blocks[p].Columns.Add(currentCol);
            }
        }
    }
}
