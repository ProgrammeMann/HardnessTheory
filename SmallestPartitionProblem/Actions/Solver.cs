using SmallestPartitionProblem.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallestPartitionProblem.Actions
{
    class Solver
    {
        public List<int[]> MustBeInEveryAnswer { get; set; }

        public List<int[]> BestAnswer { get; set; }

        List<int[]> CurrentAnswer = new List<int[]>();

        bool[] CoveredRows { get; set;}
        public Table Table { get; set; }

        public Solver(Table t, bool[] coveredRows, List<int[]> mustBeInEveryAnswer)
        {

            MustBeInEveryAnswer = mustBeInEveryAnswer;
            Table = t;
            CoveredRows = new bool[t.Blocks.Count()];
            for(int i = 0; i < CoveredRows.Length; i++)
            {
                CoveredRows[i] = coveredRows[i];
            }
        }

        

        public void Solve() {
            
            int p = FindP(); //индекс текущего блока
            if(p > -1 && !CoveredRows[p] && Table.Blocks[p] != null && Table.Blocks.Count() != 0)
            {
                FindAnswer(p, 0);
            }
            if(BestAnswer != null)
            {
                BestAnswer.AddRange(MustBeInEveryAnswer);
            }
        }


        private void FindAnswer(int p, int currentColumnInBlock) {
            int goodColumnIndex = FindGoodColumnInBlock(p, currentColumnInBlock); //Если вернется -1 то шаг 4
            if (goodColumnIndex != -1)
            {
                bool isSolved = true;
                foreach (var r in CoveredRows)
                {
                    if (!r)
                    {
                        isSolved = false;
                        break;
                    }
                }
                if (isSolved)
                {
                    if (BestAnswer == null || BestAnswer.Count > CurrentAnswer.Count)
                    {
                        RewriteBestAnswer(CurrentAnswer);
                    }
                    if (Table.Blocks[p].Columns.Count > currentColumnInBlock + 1)
                    {
                        CurrentAnswer.RemoveAt(CurrentAnswer.Count - 1);
                        CleanCoveredRows(Table.Blocks[p].Columns[goodColumnIndex]);
                        FindAnswer(p, currentColumnInBlock + 1);
                    }
                }
                else
                {
                    int newP;
                    if ((newP = FindP()) > -1)
                    {
                        FindAnswer(newP, 0);
                    }
                }
            }else
            {
                if(CurrentAnswer.Count == 0)
                {
                    return;
                }
                if (Table.Blocks[p].Columns.Count > currentColumnInBlock + 1)
                {
                    CurrentAnswer.RemoveAt(CurrentAnswer.Count - 1);
                    CleanCoveredRows(Table.Blocks[p].Columns[goodColumnIndex]);
                    FindAnswer(p, currentColumnInBlock + 1);
                }
            }
        }

        private void RewriteBestAnswer(List<int[]> currentAnswer)
        {
            if(BestAnswer == null)
            {
                BestAnswer = new List<int[]>();
                for(int i = 0; i < currentAnswer.Count; i++)
                {
                    BestAnswer.Add(new int[currentAnswer[0].Length]);
                }
            }
            for(int i = 0; i < BestAnswer.Count; i++)
            {
                for(int j = 0; j < BestAnswer[0].Length; j++)
                {
                    BestAnswer[i][j] = currentAnswer[i][j];
                }
            }
        }

        private void CleanCoveredRows(int[] rows)
        {
            for (int elemntIndex = 0; elemntIndex < rows.Length; elemntIndex++)
            {
                if (rows[elemntIndex] == 1)
                {
                    CoveredRows[elemntIndex] = false;
                }
            }
        }

        private bool HasColumnCoveredRows(int p, int i)
        {
            for (int elemntIndex = 0; elemntIndex < Table.Blocks[p].Columns[i].Length; elemntIndex++)
            {
                if (Table.Blocks[p].Columns[i][elemntIndex] == 1 && CoveredRows[elemntIndex])
                {
                    return true;
                }
            }
            return false;
        }

        private int FindP()
        {
            for (int p = 0; p < CoveredRows.Length; p++) //обход всех блоков поиск нужного p
            {
                if (!CoveredRows[p] && Table.Blocks[p] != null) //если такой р ннайден
                {
                    return p;
                }
            }
            return -1;
        }

        private int FindGoodColumnInBlock(int p, int startIndex)
        {
            for (int i = startIndex; i < Table.Blocks[p].Columns.Count; i++)
            {
                if (!HasColumnCoveredRows(p, i))
                {
                    for (int elemntIndex = 0; elemntIndex < Table.Blocks[p].Columns[i].Length; elemntIndex++)
                    {
                        if (Table.Blocks[p].Columns[i][elemntIndex] == 1)
                        {
                            CoveredRows[elemntIndex] = true;
                        }
                    }
                    CurrentAnswer.Add(Table.Blocks[p].Columns[i]);

                    return i;
                }
            }
            return -1;
        }
    }
}
