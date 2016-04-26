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
using System.Threading;

namespace Atlas.CLI
{
    public class AtlasCLI
    {
        static void Main(string[] args)
        {
            if (args[0] == "-c")
            {
                AtlasCCompilerFrontEnd compiler = new AtlasCCompilerFrontEnd();

                string source = File.ReadAllText(args[1]);

                string compiled = compiler.Compile(new AntlrInputStream(source));

                if (compiled != null)
                {
                    File.WriteAllText(args[2], compiled);
                }
                Environment.Exit(0);
            }
            else if (args[0] == "-a")
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
                    cpu.ClockPulse();
                }
            } 
        }
    }
}
