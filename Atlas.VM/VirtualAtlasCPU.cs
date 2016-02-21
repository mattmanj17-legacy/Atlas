using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Atlas.Architecture;
using Atlas.Assembler;
using Antlr4.Runtime;

namespace Atlas.VM
{
    class VirtualAtlasCPU : AtlasCPU
    {
        static void Main(string[] args)
        {
            AtlasAssembler assembler = new AtlasAssembler();

            byte[] program = assembler.Assemble(new AntlrFileStream(args[0]));

            Console.WriteLine("assembled program bytes");
            foreach(byte b in program)
            {
                Console.WriteLine(b.ToString("X2"));
            }

            Console.Write("Press any key to begin debugging...");
            Console.ReadLine();

            VirtualAtlasCPU cpu = new VirtualAtlasCPU();
            cpu.LoadProgram(program);

            while(true)
            {
                Console.Clear();
                Console.WriteLine("Current Instruction : " + cpu.GetCurrentInstruction());
                Console.WriteLine("Stack Frame : ");
                byte[] frame = cpu.GetStackFrame().Reverse().ToArray();
                foreach(byte b in frame)
                {
                    Console.WriteLine(b.ToString("X2"));
                }
                Console.Write("Press any key to single step...");
                //TODO create a way to set watches
                Console.ReadLine();
                cpu.ClockPulse();
            }
        }
    }
}
