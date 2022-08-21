using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testclope
{
    public class ClopeData
    {
        public void Init(List<Cluster> clusters, List<Transaction> transactions, double r) 
        {
            Cluster firstCluster = new Cluster();
            Transaction transaction = transactions.First();
            firstCluster.AddTransaction(transaction);
            clusters.Add(firstCluster);
            double maxCost;
            double delta;
            foreach (var item in transactions)
            {
                maxCost = AddCost(null,item,r);
                Cluster best = new Cluster();
                foreach (var cluster in clusters)
                {                  
                    delta = AddCost(cluster, item, r);
                    if (delta > maxCost)
                    {
                        maxCost = delta;
                        best = cluster;
                    }               
                }
                if (best.Size == 0)
                {
                    best.AddTransaction(item);
                    clusters.Add(best);
                }
                else
                {
                    clusters.First(x => x == best).AddTransaction(item);
                }
            }
        }

        public void Iterate(List<Cluster> clusters, List<Transaction> transactions, double r) 
        {
            bool moved = true;
            while (moved)
            {
                moved = false;

                foreach (Transaction transaction in transactions)
                {
                    double maxCost = AddCost(null, transaction, r);
                    var cluster = clusters.Find(x=>x.Transactions.Contains(transaction));
                    var bestIdCluster = -1;
                    double remCost = RemoveCost(cluster, transaction, r);
                    var clustersWithoutTransactions = clusters.Where(kvp => !kvp.Transactions.Contains(transaction));
                    foreach (var pair in clustersWithoutTransactions)
                    {
                        double addCost = AddCost(pair, transaction, r);
                        if (addCost > maxCost)
                        {
                            maxCost = addCost;
                            bestIdCluster = clusters.IndexOf(pair);
                        }
                    }
                    if (maxCost + remCost > 0)
                    {
                        if (bestIdCluster == -1)
                        {
                            var clast = new Cluster();
                            clusters.First(x=>x==cluster).RemoveTransaction(transaction);
                            bestIdCluster = clusters.Count + 1;
                            clast.AddTransaction(transaction);
                            clusters.Add(clast);
                        }
                        else
                        {
                            clusters.First(x => x == cluster).RemoveTransaction(transaction);
                            clusters[bestIdCluster].AddTransaction(transaction);
                        
                        }
                        moved = true;
                    }
                }
            }
        }

        public double AddCost(Cluster cluster, Transaction transaction,double r) 
        {
            if (cluster == null)
                return transaction.GetDataCount() / Math.Pow(transaction.GetUniqDataCount(), r);
            int S_new = cluster.Square + transaction.GetDataCount();
            int W_new = cluster.Width;
            for (int i = 0; i < transaction.GetDataCount(); i++)
            {
                if (!cluster.Occ.ContainsKey(transaction.GetCurrentParam(i))) 
                {
                    W_new++;
                }
            }
            return S_new * (cluster.Size + 1) / Math.Pow(W_new, r) - ((cluster.Square * cluster.Size) / Math.Pow(cluster.Width, r));
        }

        public double RemoveCost(Cluster cluster, Transaction transaction, double r) 
        {
            if(cluster.Size == 1) 
                return -cluster.Square / Math.Pow(cluster.Width, r);          
            int S_new = cluster.Square - transaction.GetDataCount();
            int W_new = cluster.Width;
            for (int i = 0; i < transaction.GetDataCount(); i++)
            {
                if (cluster.Occ[transaction.GetCurrentParam(i)] == 1)
                {
                    W_new--;
                }
            }
            return S_new * (cluster.Size - 1) / Math.Pow(W_new, r) - ((cluster.Square * cluster.Size) / Math.Pow(cluster.Width, r));
        }
    }
}
