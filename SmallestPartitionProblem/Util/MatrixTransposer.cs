using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallestPartitionProblem.Util
{
    class MatrixTransposer
    {
        public static int[][] Transpose(int[][] matrix) {
            int height = matrix.Length;
            int length = matrix[0].Length;

            int[][] newMatrix = new int[length][];
            for(int i = 0; i < length; i++)
            {
                newMatrix[i] = new int[height];
            }
            for (int coordX = 0; coordX < length; coordX++) {
                for (int coordY = 0; coordY < height; coordY++)
                {
                    if (coordY != coordX)
                    {
                        newMatrix[coordX][coordY] = matrix[coordY][coordX];
                    }
                    else
                    {
                        newMatrix[coordX][coordY] = 1;
                    }
                }

            }
            return newMatrix;
        }
    }
}
