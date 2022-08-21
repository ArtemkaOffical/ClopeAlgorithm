using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testclope
{
    public class Cluster
    {


        public int Size { get; private set; }
        public double Height { get; private set; }
        public int Width { get; private set; }
        public int Square { get; private set; }
        public Dictionary<int, int> Occ { get; private set; } = new Dictionary<int, int>();
        public List<Transaction> Transactions = new List<Transaction>();

        public override string ToString()
        {
            return $"Size:{Size}; Square:{Square}; Width:{Width}; Height:{Height}";
        }

        public void AddTransaction(Transaction transaction) 
        {
            Square += transaction.GetDataCount();
           
            for (int i = 0; i < transaction.GetDataCount(); i++)
            {
                int item = transaction.GetCurrentParam(i);
                if (!Occ.ContainsKey(item))
                    Occ.Add(item, 0);
                Occ[item]++;
            }
            Width = Occ.Count;
            Size++;
            Height = (double)Square / Width;
            Transactions.Add(transaction);
        }
        public void RemoveTransaction(Transaction transaction)
        {
            Square -= transaction.GetDataCount();
            for (int i = 0; i < transaction.GetDataCount(); i++)
            {
                int item = transaction.GetCurrentParam(i);
                if (Occ.ContainsKey(item))
                {
                    Occ[item]--;
                    if (Occ[item] == 0)
                        Occ.Remove(item);
                }
            }
            Width = Occ.Count;
            Size--;
            Height = (double)Square / Width;
            Transactions.Remove(transaction);
        }
    }
}
