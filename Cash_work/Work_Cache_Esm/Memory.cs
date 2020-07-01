using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work_Cache_Esm
{
    class Memory
    {
        string filename;
        public int CountPages,
            CountLines,
            CountElements;

        BinaryWriter Write;
        BinaryReader Read;

        public Memory(string filename, int i, int j, int k)
        {
            this.filename = filename;
            CountPages = i;
            CountLines = j;
            CountElements = k;
        }

        public void RandomArray(int[,,] arr, int page, int n, int m)
        {
            Random rnd = new Random();

            for (int i = 0; i < page; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < m; k++)
                    {
                        arr[i, j, k] = (rnd.Next(1000, 9999));
                    }
                }
            }
        }

        public void SetLineOnArray(ref int[,,] arr, int[] line, int indexPage, int indexLine, int count)
        {
            for (int i = 0; i < count; i++)
            {
                arr[indexPage, indexLine, i] = line[i];
            }
        }

        public void WriteArray(int[,,] arr, int page, int n, int m)
        {
            using (Write = new BinaryWriter(new FileStream(filename, FileMode.Create)))
            {
                for (int i = 0; i < page; i++)
                {
                    Write.Write((char)10);
                    for (int j = 0; j < n; j++)
                    {
                        for (int k = 0; k < m; k++)
                        {
                            Write.Write(arr[i, j, k]);
                            Write.Write(' ');
                        }
                        Write.Write((char)10);
                    }
                }
            }
        }

        void Positioning(int segment, int line, IDisposable WriteRead)
        {

            int position = (segment + 1) + 
                (segment * (CountLines * ((CountElements * 4) + 5))) +  
                    (line * ((CountElements * 4) + 5));  

            switch (WriteRead)
            {
                case BinaryWriter writer:
                    writer.BaseStream.Position = position;
                    break;
                case BinaryReader reader:
                    reader.BaseStream.Position = position;
                    break;
            }
        }

        public int[] ReadLine(int segment, int line)
        {
            int[] dataFromFile = new int[CountElements];

            using (Read = new BinaryReader(new FileStream(filename, FileMode.Open)))
            {
                Positioning(segment, line, Read);
                for (int i = 0; i < CountElements; i++)
                {
                    dataFromFile[i] = Read.ReadInt32();
                    Read.BaseStream.Position++;
                }
            }
            return dataFromFile;
        }

        public void WriteLine(int segment, int line, int[] temp)
        {
            using (Write = new BinaryWriter(new FileStream(filename, FileMode.Open)))
            {
                Positioning(segment, line, Write);

                for (int i = 0; i < CountElements; i++)
                {
                    Write.Write(temp[i]);
                    Write.Write(' ');
                }
            }
        }
    }

}

