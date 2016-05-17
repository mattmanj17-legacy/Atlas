using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Atlas.Architecture;
using Atlas.Assembler;
using Antlr4.Runtime;
using System.Threading;

namespace Atlas.CLI
{
    public class AtlasCLI
    {
        static void Main(string[] args)
        {
            if (args[0] == "-a")
            {
                AtlasAssembler assembler = new AtlasAssembler();

                string compiled = File.ReadAllText(args[1]);

                byte[] program = assembler.Assemble(new AntlrInputStream(compiled));

                if (program != null)
                {
                    File.WriteAllBytes(args[2], program);
                }
                Environment.Exit(0);
            }
            else if (args[0] == "-e")
            {
                AtlasCPU cpu = new AtlasCPU();

                byte[] program = File.ReadAllBytes(args[1]);

                cpu.LoadProgram(program);

                Console.Clear();
                while (true)
                {
                    Console.Clear();

                    Console.WriteLine("pc : " + cpu.ProgramCounter.ToString("X8"));
                    Console.WriteLine("bp : " + cpu.BasePointer.ToString("X8"));
                    Console.WriteLine("sp : " + cpu.StackPointer.ToString("X8"));
                    Console.WriteLine("inst : " + cpu.GetCurrentInstruction().ToString());
                    Console.WriteLine("inst code : " + cpu.MemValue(MemSize.WORD,cpu.ProgramCounter).ToString("X8"));

                    Console.WriteLine("stack frame:");
                    foreach(var item in cpu.GetStackFrame())
                    {
                        Console.WriteLine(item.ToString("X8"));
                    }

                    Console.ReadLine();

                    cpu.ClockPulse();
                }
            } 
        }
    }
}
