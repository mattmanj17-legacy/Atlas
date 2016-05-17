using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Atlas.Architecture;

namespace Atlas
{
    class Program
    {
        static void Main(string[] args)
        {
            String inputFileName = "fib.bin";
            String outputFileName = "boopout.txt";
            byte[] fileBytes = File.ReadAllBytes(inputFileName);
            
            StringBuilder sb = new StringBuilder();
            int count = 0;
            for (int i = 0; i < fileBytes.Length; i+= 4 )
            {
                count++;
                sb.Insert(0,"\", ");
                sb.Insert(0,AtlasCPU.IntFromBytes(fileBytes[i + 3], fileBytes[i + 2], fileBytes[i + 1], fileBytes[i + 0]).ToString("X8"));
                sb.Insert(0,"X\"");
            }
            while(count++ < 256)
            {
                sb.Insert(0, "\", ");
                sb.Insert(0, "00000000");
                sb.Insert(0, "X\"");
            }
            File.WriteAllText(outputFileName, sb.ToString());
        }
    }
}
