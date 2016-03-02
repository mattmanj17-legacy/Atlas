using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Atlas.Architecture;
using Atlas.Assembler;
using Atlas.AtlasCC;
using Antlr4.Runtime;

namespace Atlas.VM
{
    class VirtualAtlasCPU : AtlasCPU
    {
        static void Main(string[] args)
        {
            AtlasCCompilerFrontEnd compiler = new AtlasCCompilerFrontEnd();

            string source = File.ReadAllText(args[0]);

            Console.WriteLine("original c code\n");
            Console.WriteLine(source);

            Console.Write("Press any key to begin compiling...");
            Console.ReadLine();

            string compiled = compiler.Compile(new AntlrInputStream(source));

            if(compiled == null)
            {
                return;
            }

            Console.WriteLine("compiled program\n");
            Console.WriteLine(compiled);

            Console.Write("Press any key to begin assembling...");
            Console.ReadLine();

            AtlasAssembler assembler = new AtlasAssembler();

            byte[] program = assembler.Assemble(new AntlrInputStream(compiled));

            if (program == null)
            {
                return;
            }

            Console.WriteLine("assembled program bytes\n");
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
