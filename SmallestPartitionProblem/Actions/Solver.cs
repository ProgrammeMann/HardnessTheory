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
        public List<int[]> BestAnswer { get; set; }

        List<int[]> CurrentAnswer = new List<int[]>();

        bool[] CoveredRows { get; set;}
        public Table Table { get; set; }

        public Solver(Table t)
        {
            Table = t;
            CoveredRows = new bool[t.Blocks.Count()];
            for(int i = 0; i < CoveredRows.Length; i++)
            {
                CoveredRows[i] = false;
            }
        }

        public void Solve() {
            
            int p = FindP(); //индекс текущего блока
            if(p > -1 && !CoveredRows[p] && Table.Blocks[p] != null && Table.Blocks.Count() != 0)
            {
                FindAnswer(p, 0);
            }
        }


        private void FindAnswer(int p, int currentColumnInBlock) {
                //for (int i = 0; i < Table.Blocks[p].Columns.Count; i++) {   
                //    if (!HasColumnCoveredRows(p, i))
                //    {
                //        for (int elemntIndex = 0; elemntIndex < Table.Blocks[p].Columns[i].Length; elemntIndex++)
                //        {
                //            if(Table.Blocks[p].Columns[i][elemntIndex] == 1)
                //            {
                //                CoveredRows[elemntIndex] = true;
                //            }
                //        }
                //        CurrentAnswer.Add(Table.Blocks[p].Columns[i]);
                //    }
                //}
            int goodColumnIndex = FindGoodColumnInBlock(p, currentColumnInBlock); //Если вернется -1 то шаг 4
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
                    BestAnswer = CurrentAnswer;
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
                FindAnswer(FindP(), 0);
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
