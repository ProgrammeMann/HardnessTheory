using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallestPartitionProblem.Classes
{
    class Block
    {
        public int IndexP { get; set; }
        public List<int[]> Columns { get; set; } = new List<int[]>();

        public Block(int p)
        {
            IndexP = p;
        }
    }
}
