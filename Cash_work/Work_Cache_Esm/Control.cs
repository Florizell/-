using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work_Cache_Esm
{
    class Control
    {
        public static Memory memory;
        public static Cache cache;
        public static int[,,] arr;
        bool isCache = false;

        public Control(int countPages, int countLines, int countElements, string filename)
        {
            memory = new Memory(filename + ".txt", countPages, countLines, countElements);
            arr = new int[countPages, countLines, countElements];
            memory.RandomArray(arr, countPages, countLines, countElements);
            memory.WriteArray(arr, countPages, countLines, countElements);
            cache = new Cache(countLines, countElements);
        }

        public int this[int i, int j, int k]
        {
            get
            {
                return arr[i, j, k];
            }
        }

        public int this[int i, int j]
        {
            get
            {
                return cache[i, j];
            }
        }

        public int this[int i]
        {
            get
            {
                return cache[i];
            }
        }

        public bool IsCache
        {
            get
            {
                return isCache;
            }
        }

        public int[] SearchLine(int indexPage, int indexLine)
        {
            int[] buf = new int[memory.CountElements];

            if (cache.isThereATag(indexPage, indexLine))
            {
                for (int i = 0; i < memory.CountElements; i++)
                {
                    buf[i] = cache[indexLine, i];
                }
                isCache = true;
                return buf;
            }
            isCache = false;

            buf = memory.ReadLine(indexPage, indexLine);

            if (cache[indexLine] != -1)
            {
                int[] old_str = new int[memory.CountElements];
                for (int i = 0; i < memory.CountElements; i++)
                {
                    old_str[i] = cache[indexLine, i];
                }
                memory.WriteLine(cache[indexLine], indexLine, old_str);
                memory.SetLineOnArray(ref arr, old_str, cache[indexLine], indexLine, memory.CountElements);
            }
            cache[indexLine] = indexPage;
            SetLineOnCache(buf, memory.CountElements, indexLine);
            return buf;
        }

        public void SetLineOnCache(int[] buf, int countElements, int indexLine)
        {
            cache.WriteLine(buf, memory.CountElements, indexLine);
        }
    }
}
