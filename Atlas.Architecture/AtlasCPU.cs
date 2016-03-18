using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Atlas.Architecture
{
    //where does the data for a write operation come from
    public enum AtlasWriteSource
    {
        DisableWrite,
        Alu,
        LoadedMemory,
        BasePointer,
        NextInstructionAddress,
        Literal,
        ArgumentB
    }

    //list of valid instructions: 31 OpCodes
    public enum OpCode : byte
    {
        NOP = 0x00,
        ADD,
        SUB,
        NEG,
        LESS,
        EQU,
        MUL,
        SLL,
        SRL,
        NOT,
        AND,
        OR,
        XOR,
        LB,
        LUB,
        LH,
        LUH,
        LW,
        SB,
        SH,
        SW,
        JMP,
        JIF,
        PUSHBP,
        COPY,
        PUSHB,
        PUSHH,
        PUSHW,
        POPB,
        POPH,
        POPW,
        BEGINARGS,
        CALL,
        SYSCALL,
        RETV,
        RET
    }

    public enum MemSize
    {
        NONE = 0,
        BYTE = 1,
        HALF = 2,
        WORD = 4
    }

    public abstract class AtlasCPU
    {
        public const MemSize OperandSize = MemSize.WORD;
        public const MemSize InstructionSize = MemSize.BYTE;

        public static readonly int StackSize = MemSizeToInt(MemSize.BYTE) * 64 * 1024; // 64K

        //utility functions for working with opcodes
        public static int ArgSizeFromOpCode(OpCode code)
        {
            switch (code)
            {
                case OpCode.PUSHW:
                    return 4;
                case OpCode.PUSHH:
                    return 2;
                case OpCode.PUSHB:
                case OpCode.SYSCALL:
                    return 1;
                default:
                    return 0;
            }
        }

        public static int MemSizeToInt(MemSize size)
        {
            return (int)size;
        }
        
        public static int IntFromBytes(byte b3, byte b2, byte b1, byte b0)
        {
            return (b3 << (8 * 3)) + (b2 << (8 * 2)) + (b1 << (8 * 1)) + (b0 << (8 * 0));
        }

        public static byte ByteFromInt(int value, int Byte)
        {
            int mask = 0xFF << (Byte * 8);
            return (byte)((value & mask) >> (Byte * 8));
        }

        protected byte[] m_mem;

        public void LoadProgram(byte[] program)
        {
            //for now, mem just grows to accomadate program and stack
            m_mem = new byte[program.Length + StackSize];
            program.CopyTo(m_mem, 0);

            ProgramCounter = 0;
            StackPointer = program.Length;
            BasePointer = program.Length;
        }

        public OpCode GetCurrentInstruction()
        {
            return (OpCode)MemValue(MemSize.BYTE, ProgramCounter);
        }

        private byte[] GetStackFrame()
        {
            var ret = m_mem.ToList().GetRange(BasePointer, StackPointer - BasePointer).ToArray();
            return ret;
        }

        int cahcedbp = -1;

        protected int insts = 0;

        public void ClockPulse()
        {
            if (cahcedbp == -1) cahcedbp = BasePointer;

            insts += 1;
            
            /*fetch*/
            //the current instruction
            //TODO check FOR UNRECOGNIZED INSTRUCTONS
            OpCode inst = (OpCode)MemValue(MemSize.BYTE, ProgramCounter);

            //the literal value used for push instruction
            int inlineLiteral = MemValue((MemSize)ArgSizeFromOpCode(inst), ProgramCounter + MemSizeToInt(InstructionSize));

            if(inst == OpCode.SYSCALL)
            {
                SystemCall(inlineLiteral, GetStackFrame());
                ProgramCounter = ProgramCounter + MemSizeToInt(InstructionSize) + MemSizeToInt(MemSize.BYTE);
                return;
            }

            //the arguments passed to this opcode
            //BUG this can crash if ... there is nothing in the stack??? huh
            int stackArgB = MemValue(MemSize.WORD, StackPointer - MemSizeToInt(OperandSize));
            int stackArgA = MemValue(MemSize.WORD, StackPointer - 2 * MemSizeToInt(OperandSize));

            // the return address, stored one word below the cached base pointer (see implimentation of BEGINARGS and CALL)
            int returnAddress = MemValue(MemSize.WORD, BasePointer - MemSizeToInt(MemSize.WORD));

            // the old base pointer, stored two words below the cahced base pointer (see implimentation of BEGINARGS and CALL)
            int oldBp = MemValue(MemSize.WORD, BasePointer - 2 * MemSizeToInt(MemSize.WORD));

            /*decode*/
            //what is the new stack pointer
            int newSP = StackPointer;
            switch (inst)
            {
                //+8
                case OpCode.BEGINARGS:
                    //pop none, push two word (old base pointer and room for return address)
                    newSP = StackPointer + 2 * MemSizeToInt(MemSize.WORD);
                    break;
                //+4
                case OpCode.PUSHW:
                case OpCode.PUSHBP:
                case OpCode.COPY:
                    //pop none off stack, push word
                    newSP = StackPointer + MemSizeToInt(MemSize.WORD);
                    break;
                //+2
                case OpCode.PUSHH:
                    //pop none off stack, push half
                    newSP = StackPointer + MemSizeToInt(MemSize.HALF);
                    break;
                //+1
                case OpCode.PUSHB:
                    //pop none off stack, push half
                    newSP = StackPointer + MemSizeToInt(MemSize.BYTE);
                    break;
                //+0
                //pop one word off stack, push word
                case OpCode.LB:
                case OpCode.LUB:
                case OpCode.LH:
                case OpCode.LUH:
                case OpCode.LW:
                case OpCode.NOT:
                case OpCode.NEG:
                // pop none, push none
                case OpCode.NOP:
                    newSP = StackPointer;
                    break;
                //-1
                case OpCode.POPB:
                    //pop byte off stack, push none
                    newSP = StackPointer + MemSizeToInt(MemSize.BYTE);
                    break;
                //-2
                case OpCode.POPH:
                    //pop half off stack, push none
                    newSP = StackPointer + MemSizeToInt(MemSize.HALF);
                    break;
                //-4
                //pop two word off stack, push word
                case OpCode.ADD:
                case OpCode.SUB:
                case OpCode.LESS:
                case OpCode.EQU:
                case OpCode.MUL:
                case OpCode.SLL:
                case OpCode.SRL:
                case OpCode.AND:
                case OpCode.OR:
                case OpCode.XOR:
                //pop word off stack, push none
                case OpCode.POPW:
                case OpCode.JMP:
                case OpCode.CALL:
                    newSP = StackPointer - MemSizeToInt(MemSize.WORD);
                    break;
                //-8
                case OpCode.SB:
                case OpCode.SH:
                case OpCode.SW:
                case OpCode.JIF:
                    // pop two word, push none
                    newSP = StackPointer - 2 * MemSizeToInt(MemSize.WORD);
                    break;
                //BP-8
                case OpCode.RET:
                    // set stack pointer to base pointer, pop two words
                    newSP = BasePointer - 2 * MemSizeToInt(MemSize.WORD);
                    break;
                //BP-4
                case OpCode.RETV:
                    // set stack pointer to base pointer, pop two words, push word
                    newSP = BasePointer - MemSizeToInt(MemSize.WORD);
                    break;

            }

            //what is the new base pointer
            int newBp = BasePointer;
            switch (inst)
            {
                case OpCode.ADD:
                case OpCode.SUB:
                case OpCode.NEG:
                case OpCode.LESS:
                case OpCode.EQU:
                case OpCode.MUL:
                case OpCode.SLL:
                case OpCode.SRL:
                case OpCode.NOT:
                case OpCode.AND:
                case OpCode.OR:
                case OpCode.XOR:
                case OpCode.LB:
                case OpCode.LUB:
                case OpCode.LH:
                case OpCode.LUH:
                case OpCode.LW:
                case OpCode.SB:
                case OpCode.SH:
                case OpCode.SW:
                case OpCode.JMP:
                case OpCode.JIF:
                case OpCode.PUSHBP:
                case OpCode.COPY:
                case OpCode.PUSHW:
                case OpCode.PUSHH:
                case OpCode.PUSHB:
                case OpCode.POPW:
                case OpCode.POPH:
                case OpCode.POPB:
                case OpCode.NOP:
                    newBp = BasePointer;
                    break;
                case OpCode.BEGINARGS:
                    //cahce off the new bp start of function parameters/local variable space
                    cahcedbp = StackPointer + 2 * MemSizeToInt(MemSize.WORD);
                    newBp = BasePointer;
                    break;
                case OpCode.CALL:
                    newBp = cahcedbp;
                    break;
                case OpCode.RETV:
                case OpCode.RET:
                    //point to old base pointer
                    newBp = oldBp;
                    break;
            }

            //what is the new program counter
            int newPC = ProgramCounter;
            switch (inst)
            {
                case OpCode.NOP:
                case OpCode.ADD:
                case OpCode.SUB:
                case OpCode.NEG:
                case OpCode.LESS:
                case OpCode.EQU:
                case OpCode.MUL:
                case OpCode.SLL:
                case OpCode.SRL:
                case OpCode.NOT:
                case OpCode.AND:
                case OpCode.OR:
                case OpCode.XOR:
                case OpCode.LB:
                case OpCode.LUB:
                case OpCode.LH:
                case OpCode.LUH:
                case OpCode.LW:
                case OpCode.SB:
                case OpCode.SH:
                case OpCode.SW:
                case OpCode.PUSHBP:
                case OpCode.COPY:
                case OpCode.POPW:
                case OpCode.POPH:
                case OpCode.POPB:
                case OpCode.BEGINARGS:
                    newPC = ProgramCounter + MemSizeToInt(InstructionSize);
                    break;
                case OpCode.PUSHW:
                    //advance past word literal
                    newPC = ProgramCounter + MemSizeToInt(InstructionSize) + MemSizeToInt(MemSize.WORD);
                    break;
                case OpCode.PUSHH:
                    //advance past half literal
                    newPC = ProgramCounter + MemSizeToInt(InstructionSize) + MemSizeToInt(MemSize.HALF);
                    break;
                case OpCode.PUSHB:
                    //advance past byte literal
                    newPC = ProgramCounter + MemSizeToInt(InstructionSize) + MemSizeToInt(MemSize.BYTE);
                    break;
                case OpCode.JMP:
                case OpCode.CALL:
                    newPC = stackArgB;
                    break;
                case OpCode.JIF:
                    if (stackArgA != 0) { newPC = stackArgB; }
                    else { newPC = ProgramCounter + 1; }
                    break;
                case OpCode.RETV:
                case OpCode.RET:
                    newPC = returnAddress;
                    break;
            }

            //what is the source for the memory write
            AtlasWriteSource writeSource = AtlasWriteSource.DisableWrite;
            switch (inst)
            {
                case OpCode.POPW:
                case OpCode.POPH:
                case OpCode.POPB:
                case OpCode.JMP:
                case OpCode.JIF:
                case OpCode.RET:
                case OpCode.NOP:
                    writeSource = AtlasWriteSource.DisableWrite;
                    break;
                case OpCode.ADD:
                case OpCode.SUB:
                case OpCode.NEG:
                case OpCode.LESS:
                case OpCode.EQU:
                case OpCode.MUL:
                case OpCode.SLL:
                case OpCode.SRL:
                case OpCode.NOT:
                case OpCode.AND:
                case OpCode.OR:
                case OpCode.XOR:
                    writeSource = AtlasWriteSource.Alu;
                    break;
                case OpCode.LB:
                case OpCode.LUB:
                case OpCode.LH:
                case OpCode.LUH:
                case OpCode.LW:
                    writeSource = AtlasWriteSource.LoadedMemory;
                    break;
                case OpCode.SB:
                case OpCode.SH:
                case OpCode.SW:
                case OpCode.RETV:
                case OpCode.COPY:
                    writeSource = AtlasWriteSource.ArgumentB;
                    break;
                case OpCode.PUSHBP:
                case OpCode.BEGINARGS:
                    writeSource = AtlasWriteSource.BasePointer;
                    break;
                case OpCode.PUSHW:
                case OpCode.PUSHH:
                case OpCode.PUSHB:
                    writeSource = AtlasWriteSource.Literal;
                    break;
                case OpCode.CALL:
                    writeSource = AtlasWriteSource.NextInstructionAddress;
                    break;
            }

            //how much will be writen to memory
            MemSize bytesWritten = MemSize.NONE;
            switch (inst)
            {
                //0
                case OpCode.NOP:
                case OpCode.JMP:
                case OpCode.JIF:
                case OpCode.POPW:
                case OpCode.POPH:
                case OpCode.POPB:
                case OpCode.RET:
                    bytesWritten = MemSize.NONE;
                    break;
                //1
                case OpCode.SB:
                case OpCode.PUSHB:
                    bytesWritten = MemSize.BYTE;
                    break;
                //2
                case OpCode.SH:
                case OpCode.PUSHH:
                    bytesWritten = MemSize.HALF;
                    break;
                //4
                case OpCode.ADD:
                case OpCode.SUB:
                case OpCode.NEG:
                case OpCode.LESS:
                case OpCode.EQU:
                case OpCode.MUL:
                case OpCode.SLL:
                case OpCode.SRL:
                case OpCode.NOT:
                case OpCode.AND:
                case OpCode.OR:
                case OpCode.XOR:
                case OpCode.LB:
                case OpCode.LUB:
                case OpCode.LH:
                case OpCode.LUH:
                case OpCode.LW:
                case OpCode.SW:
                case OpCode.PUSHBP:
                case OpCode.COPY:
                case OpCode.PUSHW:
                case OpCode.BEGINARGS:
                case OpCode.CALL:
                case OpCode.RETV:
                    bytesWritten = MemSize.WORD;
                    break;
            }

            //where in memory are we writing
            int writeAddress = 0; // 0 = invalid address/ NULL
            switch (inst)
            {
                //write disabled
                case OpCode.NOP:
                case OpCode.JMP:
                case OpCode.JIF:
                case OpCode.POPW:
                case OpCode.POPH:
                case OpCode.POPB:
                case OpCode.RET:
                    writeAddress = 0;
                    break;
                //replace top of stack
                case OpCode.LB:
                case OpCode.LUB:
                case OpCode.LH:
                case OpCode.LUH:
                case OpCode.LW:
                case OpCode.NEG:
                case OpCode.NOT:
                    writeAddress = StackPointer - MemSizeToInt(MemSize.WORD);
                    break;
                //write to memory address
                case OpCode.SB:
                case OpCode.SH:
                case OpCode.SW:
                    writeAddress = stackArgA;
                    break;
                //push
                case OpCode.PUSHBP:
                case OpCode.COPY:
                case OpCode.PUSHW:
                case OpCode.PUSHH:
                case OpCode.PUSHB:
                case OpCode.BEGINARGS:
                    writeAddress = StackPointer;
                    break;
                //pop two word, push
                case OpCode.ADD:
                case OpCode.SUB:
                case OpCode.LESS:
                case OpCode.EQU:
                case OpCode.MUL:
                case OpCode.SLL:
                case OpCode.SRL:
                case OpCode.AND:
                case OpCode.OR:
                case OpCode.XOR:
                    writeAddress = StackPointer - 2 * MemSizeToInt(MemSize.WORD);
                    break;
                //write in the space allocated by BEGINARGS
                case OpCode.CALL:
                    writeAddress = cahcedbp - MemSizeToInt(MemSize.WORD);
                    break;
                //push onto stack (after returning to the previos frame). oldSP = BasePointer - 2 * WORD, becasue in the last call frame we said BasePointer = StackPointer + 2 * WORD
                case OpCode.RETV:
                    writeAddress = BasePointer - 2 * MemSizeToInt(MemSize.WORD);
                    break;
            }

            /*Execute*/
            int aluResult = ALUCalculation(inst, stackArgA, stackArgB);
            int loadResult = LoadMemory(inst, stackArgA, stackArgB);

            /*WriteBack*/
            int writeVal = 0;
            switch (writeSource)
            {
                case AtlasWriteSource.DisableWrite:
                    writeVal = -1;
                    break;
                case AtlasWriteSource.Alu:
                    writeVal = aluResult;
                    break;
                case AtlasWriteSource.LoadedMemory:
                    writeVal = loadResult;
                    break;
                case AtlasWriteSource.BasePointer:
                    writeVal = BasePointer;
                    break;
                case AtlasWriteSource.NextInstructionAddress:
                    writeVal = ProgramCounter + 1;
                    break;
                case AtlasWriteSource.Literal:
                    writeVal = inlineLiteral;
                    break;
                case AtlasWriteSource.ArgumentB:
                    writeVal = stackArgB;
                    break;
            }

            WriteMem(writeVal, writeAddress, bytesWritten);

            StackPointer = newSP;
            BasePointer = newBp;
            ProgramCounter = newPC;
        }

        protected int MemValue(MemSize memSize, int address)
        {
            int size = MemSizeToInt(memSize);
            return IntFromBytes(
                size >= 4 ? m_mem[address + 3] : (byte)0,
                size >= 3 ? m_mem[address + 2] : (byte)0,
                size >= 2 ? m_mem[address + 1] : (byte)0,
                size >= 1 ? m_mem[address] : (byte)0
                );
        }

        private void WriteMem(int value, int address, MemSize memSize)
        {
            int size = MemSizeToInt(memSize);
            if (size >= 4) { m_mem[address + 3] = ByteFromInt(value, 3); }
            if (size >= 3) { m_mem[address + 2] = ByteFromInt(value, 2); }
            if (size >= 2) { m_mem[address + 1] = ByteFromInt(value, 1); }
            if (size >= 1) { m_mem[address] = ByteFromInt(value, 0); }
        }

        private void SystemCall(int value, byte[] stackFrame)
        {
            if (value == 1)
            {
                if (stackFrame.Length != 4)
                {
                    throw new InvalidOperationException("incorect arguments passed to print system call");
                }

                int charAddr = IntFromBytes(stackFrame[3], stackFrame[2], stackFrame[1], stackFrame[0]);

                SysPrint(charAddr);
            }
            else if (value == 2)
            {
                if (stackFrame.Length != 4)
                {
                    throw new InvalidOperationException("incorect arguments passed to printint system call");
                }

                int toPrint = IntFromBytes(stackFrame[3], stackFrame[2], stackFrame[1], stackFrame[0]);
                SysPrintInt(toPrint);
            }
            else if(value == 3)
            {
                SysClearSreen();
            }
            else if (value == 4)
            {
                if (stackFrame.Length != 4 * 5)
                {
                    throw new InvalidOperationException("incorect arguments passed to printint system call");
                }

                int b = IntFromBytes(stackFrame[3], stackFrame[2], stackFrame[1], stackFrame[0]);
                int g = IntFromBytes(stackFrame[7], stackFrame[6], stackFrame[5], stackFrame[4]);
                int r = IntFromBytes(stackFrame[11], stackFrame[10], stackFrame[9], stackFrame[8]);
                int y = IntFromBytes(stackFrame[15], stackFrame[14], stackFrame[13], stackFrame[12]);
                int x = IntFromBytes(stackFrame[19], stackFrame[18], stackFrame[17], stackFrame[16]);

                SysSetPix(x, y, r, g, b);
            }
            else if (value == 5)
            {
                SysSwapBuffer();
            }
            else
            {
                throw new InvalidOperationException("invalid system call number");
            }
        }

        protected abstract void SysPrint(int charAddr);
        protected abstract void SysPrintInt(int toPrint);
        protected abstract void SysClearSreen();
        protected abstract void SysSetPix(int x, int y, int r, int g, int b);
        protected abstract void SysSwapBuffer();

        private int SignExtend(int value, MemSize oldMemSize, MemSize newMemSize)
        {
            int oldSize = MemSizeToInt(oldMemSize);
            int newSize = MemSizeToInt(newMemSize);

            sbyte mostSignificantByte = (sbyte)ByteFromInt(value, oldSize - 1);

            byte extend = mostSignificantByte < 0 ? byte.MaxValue/*0xFFFF*/ : (byte)0;

            return IntFromBytes(
                newSize >= 4 ? (oldSize >= 4 ? ByteFromInt(value, 3) : extend) : (byte)0,
                newSize >= 3 ? (oldSize >= 3 ? ByteFromInt(value, 2) : extend) : (byte)0,
                newSize >= 2 ? (oldSize >= 2 ? ByteFromInt(value, 1) : extend) : (byte)0,
                newSize >= 1 ? (oldSize >= 1 ? ByteFromInt(value, 0) : extend) : (byte)0
                );
        }
        
        private int LoadMemory(OpCode inst, int stackArgA, int stackArgB)
        {
            switch(inst)
            {
                case OpCode.LB:
                    return SignExtend(MemValue(MemSize.BYTE, stackArgB), MemSize.BYTE, MemSize.WORD);
                case OpCode.LUB:
                    return MemValue(MemSize.BYTE, stackArgB);
                case OpCode.LH:
                    return SignExtend(MemValue(MemSize.HALF, stackArgB), MemSize.HALF, MemSize.WORD);
                case OpCode.LUH:
                    return MemValue(MemSize.HALF, stackArgB);
                case OpCode.LW:
                    return MemValue(MemSize.WORD, stackArgB);
                default:
                    return 0;
            }
        }

        private int ALUCalculation(OpCode inst, int stackArgA, int stackArgB)
        {
            switch(inst)
            {
                case OpCode.ADD:
                    return stackArgA + stackArgB;
                case OpCode.SUB:
                    return stackArgA - stackArgB;
                case OpCode.NEG:
                    return -stackArgB;
                case OpCode.LESS:
                    return stackArgA < stackArgB ? 1 : 0;
                case OpCode.EQU:
                    return stackArgA == stackArgB ? 1 : 0;
                case OpCode.MUL: //MUL treats operands as unsigned
                    return (int)((uint)stackArgA * (uint)stackArgB);
                case OpCode.SLL:
                    return stackArgA << stackArgB;
                case OpCode.SRL: //SRL treats operands as unsigned
                    return (int)((uint)stackArgA >> stackArgB);
                case OpCode.NOT:
                    return ~stackArgB;
                case OpCode.AND:
                    return stackArgA & stackArgB;
                case OpCode.OR:
                    return stackArgA | stackArgB;
                case OpCode.XOR:
                    return stackArgA ^ stackArgB;
                default:
                    return 0;
            }
        }
        
        //Address of the current instruction
        protected int ProgramCounter;

        // points to the first byte ABOVE the current call frame
        protected int StackPointer;
        // points to first byte AT THE BASE of the current call frame
        protected int BasePointer;
    }
}
