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

namespace Atlas.VM
{
    class VirtualAtlasCPU : AtlasCPU
    {
        public string cout = "";
        
        protected override void SystemCall(int value)
        {
            byte[] previosFrame = GetStackFrame(1);
            
            if(value == 1)
            {
                if(previosFrame.Length != 4)
                {
                    throw new InvalidOperationException("incorect arguments passed to print system call");
                }
                
                int charAddr = IntFromBytes(previosFrame[3], previosFrame[2], previosFrame[1], previosFrame[0]);

                char c = (char)MemValue(MemSize.BYTE, charAddr);
                while(c != '\0')
                {
                    Console.Write(c);
                    //cout += c;
                    charAddr++;
                    c = (char)MemValue(MemSize.BYTE, charAddr);
                }
                Console.WriteLine();
            }
            else
            {
                throw new InvalidOperationException("invalid system call number");
            }
        } 
        
        static void Main(string[] args)
        {
            AtlasCCompilerFrontEnd compiler = new AtlasCCompilerFrontEnd();

            string source = File.ReadAllText(args[0]);

            //Console.WriteLine("original c code\n");
            //Console.WriteLine(source);

            //Console.Write("Press any key to begin compiling...");
            //Console.ReadLine();

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

            //.WriteLine("assembled program bytes\n");
            //foreach(byte b in program)
            //{
                //Console.WriteLine(b.ToString("X2"));
            //

            //Console.Write("Press any key to begin emulation...");
            //Console.ReadLine();

            VirtualAtlasCPU cpu = new VirtualAtlasCPU();
            cpu.LoadProgram(program);

            //Console.Clear();

            while(true)
            {
                //Console.Clear();
                //Console.WriteLine(cpu.cout);
                //.WriteLine("--------------------------");
                //.WriteLine("Current Instruction : " + cpu.GetCurrentInstruction());
                //Console.WriteLine("Stack Frame : ");
                //byte[] frame = cpu.GetStackFrame().Reverse().ToArray();
                //foreach(byte b in frame)
                //{
                    //Console.WriteLine(b.ToString("X2"));
                //}
                //Console.Write("Press any key to single step...");
                //Console.ReadLine();
                //Thread.Sleep(500);
                cpu.ClockPulse();
            }
        }
    }
}
