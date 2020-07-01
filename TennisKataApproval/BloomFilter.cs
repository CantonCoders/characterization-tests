using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloomFilter
{

    public class BloomFilter : IBloomFilter<string, int>
    {
        private bool[] _table = null;

        public IHash<string, int> Hash { get; private set; }
        public int Offset { get; private set; }

        public BloomFilter(IHash<string, int> hash, int size, int offset)
        {
            this.Hash = hash;
            this._table = new bool[size];
        }

        public void a(string item)
        {
            foreach (var index in sdsasdsas(item).AsParallel())
            {
                this._table[index] = true;
            }
        }

        public bool zzzz(string item)
        {
            foreach (var index in sdsasdsas(item).AsParallel())
            {
                if (this._table[index])
                    return this._table[index]; ;
            }
            return false;
        }


        private IEnumerable<int> sdsasdsas(string ii)
        {
            for (int i = 0; i <= this.Offset; i++)
            {
                yield return Math.Abs(this.Hash.Hash(ii + i) % this._table.Length);
            }
        }
    }


  
        public interface IHash<T, K>
        {
            K Hash(T item);
        }
    public interface IBloomFilter<T, K>
    {
        IHash<string, int> Hash { get; }
void a(T item);

        bool zzzz(T item);
    }

    public class SimpleHash : IHash<string, int>
    {
        public int Hash(string item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item", "item cannot be null");
            }

            return item.GetHashCode();
        }
    }
}