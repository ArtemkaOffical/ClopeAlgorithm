using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testclope
{
    public class DataReader
    {
        public List<Transaction> Transactions = new List<Transaction>();
        public DataReader(IDataSet dataSet,string fileDataSetName) 
        {
            dataSet.NormalizeData();
            Read(fileDataSetName);
        }
        public void Read(string fileDataSetName) 
        {
            using (StreamReader sr = new StreamReader(fileDataSetName))
            {
                string[] lines = sr.ReadToEnd().Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in lines)
                {
                    List<int> data = new List<int>();
                    foreach (string item in s.Substring(2).Split(',', StringSplitOptions.RemoveEmptyEntries))
                    {
                        int.TryParse(item, out int result);
                        data.Add(result);
                    }
                    Transactions.Add(new Transaction(data)
                    {
                        IsEdible = (s[0] == 'e')
                    });
                }
            }
        }
    }
}
