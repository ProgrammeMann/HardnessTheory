using SmallestPartitionProblem.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmallestPartitionProblem.Actions
{
    class Simplifyer
    {

        public int[][] Matrix { get; set; }
        public List<int[]> MustBeInEveryAnswer { get; set; } = new List<int[]>();
        bool[] CoveredRows { get; set; }

        public Simplifyer(int[][] matrix)
        {
            Matrix = matrix;
            CoveredRows = new bool[Matrix.Length];
            for(int i = 0; i < Matrix.Length; i++)
            {
                CoveredRows[i] = false;
            }
            Simplify();

        }

        private void Simplify() {
            DoStepOne();
            DoStepTwo();
        }

        private void DoStepOne() {
            foreach(var line in Matrix)
            {
                int count = 0;
                for (int row = 0; row < line.Length && count == 0 ; row++) {
                    count += line[row]; 
                }
                if(count == 0)
                {
                    throw new NoAnswerException("Не канает");
                }
            }
        }

        private void DoStepTwo() { //если в строке одна единичка - я запоминаю ее индекс
            List<int> indexes = new List<int>();  //Список индексов столбцов, входящих в каждое решение
            List<int>[] coveredRowsInLines = new List<int>[Matrix.Length];
            for (int line = 0; line < Matrix.Length; line++)
            {
                coveredRowsInLines[line] = new List<int>();
                int onesCount = 0; //число единиц в строке
                int lastOneIndex = -1; //номер последней единицы
                for(int col = 0; col < Matrix[line].Length; col++)
                {
                    if(Matrix[line][col] == 1)
                    {
                        onesCount++;
                        lastOneIndex = col;
                        coveredRowsInLines[line].Add(col);
                    }
                }
                if(onesCount == 1)
                {
                    indexes.Add(lastOneIndex);
                }
            }
            foreach(var col in indexes)
            {
                int[] currentColumn = new int[Matrix.Length];
                for(int line = 0; line < Matrix.Length; line++)
                {
                    currentColumn[line] = Matrix[line][col];
                    CoveredRows[line] = CoveredRows[line] || (Matrix[line][col] == 1);
                }
                MustBeInEveryAnswer.Add(currentColumn);
            }
            DoStepThree(coveredRowsInLines);
            CleanMatrix(indexes);
        }

        private void CleanMatrix(List<int> indexes)
        {
            List<int>[] newMatrix = new List<int>[Matrix.Length];
            for(int i = 0; i < Matrix.Length; i++)
            {
                newMatrix[i] = new List<int>(Matrix[i]);
                foreach(var row in indexes)
                {
                    newMatrix[i].RemoveAt(row);
                }
                Matrix[i] = newMatrix[i].ToArray();
            }

        }

        private void DoStepThree(List<int>[] coveredRowsInLines) {
            List<int> linesForDelete = new List<int>();
            int count = coveredRowsInLines.Count();
            //если i-й полностью соддержится в j удаляем j
            for (int i = 0; i < count; i++)
            {
                
                for(int j = 0; j < count; j++)
                {
                    if (i != j) {
                        bool contains = true;
                        foreach (var element in coveredRowsInLines[i])
                        {
                            if (!coveredRowsInLines[j].Contains(element))
                            {
                                contains = false;
                                break;
                            }
                        }
                        if (contains)
                        {
                            linesForDelete.Add(j);
                        }
                    }
                }

            }

            List<int[]> newMatrix = new List<int[]>(Matrix);
            foreach(var line in linesForDelete)
            {
                newMatrix.RemoveAt(line);
            }
            for(int i = 0; i < Matrix.Length; i++)
            {
                Matrix = newMatrix.ToArray();
            }
        }
    }

    public class NoAnswerException : ApplicationException
    {
        public NoAnswerException() { }

        public NoAnswerException(string message) : base(message) { }

        public NoAnswerException(string message, Exception inner) : base(message, inner) { }

        protected NoAnswerException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
