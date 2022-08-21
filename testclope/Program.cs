namespace testclope
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Cluster> clusters = new List<Cluster>();

            DataReader dataReader = new DataReader(new MushroomsDataSet("agaricus-lepiota.data", "Normalizeddata.txt"), "Normalizeddata.txt");
            
            ClopeData clopeData = new ClopeData();
            clopeData.Init(clusters,dataReader.Transactions,2.6);
            clopeData.Iterate(clusters,dataReader.Transactions,2.6);

            var newClusters = clusters.Where(x => x.Size != 0);

            foreach (var cluster in newClusters)
            {
                int edibleClass = 0;
                int poisonousClass = 0;
                foreach (var transaction in cluster.Transactions)
                {
                    if(transaction.IsEdible)
                        edibleClass++;                  
                    else
                        poisonousClass++;                   
                }
                Console.WriteLine($"Cluster - {clusters.IndexOf(cluster)+1};       edible-{edibleClass}; poisonous-{poisonousClass}");
            }

            Console.ReadKey();
        }
       
    }
}