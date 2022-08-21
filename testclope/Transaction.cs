using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testclope
{
    public class Transaction
    {
        private List<int> _data = new List<int>();
        public bool IsEdible { get; set; }

        public Transaction(List<int> data) 
        {
            this._data = data;
        }

        public int GetDataCount() 
        {
            return _data.Count;
        }

        public int GetUniqDataCount() 
        {
            return _data.Distinct().Count();
        }

        public int GetCurrentParam(int key) 
        {
            return _data[key];
        }
    }
}
