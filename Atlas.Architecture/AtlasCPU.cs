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
        ArgumentB,
        InByte
    }

    //list of valid instructions: 36 OpCodes
    public enum OpCode : byte
    {
        //four types of opcodes. control flow, ALU ops, Data transfer, stack manipulation
        
        // opcode forms
       
        //control flow 
        // four types
        // jumps
        //  0011 000 C (C = Is conditional (1 = JIF, 0 = JMP))
        // returns
        //  0010 000 V (V = returs a value (1 = RETV, 0 = RET)
        // function calling
        //  0001 000 C (C = call or begin args (1= call, 0 = begin args)
        // misc
        //  0000 000 S (S = system call or not (1= syscall, 0 = NOP)
        
        //ALU ops 
        // 01 FFFF P 0 (FFFF = alu signal, P = pop amout (1 = 1 (NOT, NEG), 0 = 2 (everythin g else)))
        
        //Data transfer 
        // 10 L S NN 00 (L = load or store (1= load, 0 = store), S = signed (1 = signed, 0 = unsigned), NN = size (01 = byte, 10 = half, 11 = word))
        
        //stack manipulation
        // 11 SS NN P 0 (SS = source (00 = base ptr, 01 = top of stack, 11 = literal) only has meaning if push, NN = size (01 = byte, 10 = half, 11 = word), P = Push or Pop (1 = push, 0 = pop))

        //control flow/misc
        NOP         = 0x00, //00 00 00 0 0

        //IB          = 0x01, //00 00 00 0 1 //in byte
        //OB          = 0x02, //00 00 00 1 0 //out byte

        BEGINARGS   = 0x10, //00 01 00 0 0 (call to start passing args to a function, before CALL)
        CALL        = 0x11, //00 01 00 0 1
        RET         = 0x20, //00 10 00 0 0
        RETV        = 0x21, //00 10 00 0 1
        JMP         = 0x30, //00 11 00 0 0
        JIF         = 0x31, //00 11 00 0 1

        //ALU ops 01 FFFF P 0
        //ALU00 (Invert Sign, Subtraction, Addition, Less Than)
        NEG         = 0x42, //01 00 00 1 0
        SUB         = 0x44, //01 00 01 0 0
        ADD         = 0x48, //01 00 10 0 0
        LESS        = 0x4C, //01 00 11 0 0
        //ALU01 (Unsigned Multiplication, Equality Check, *, *)
        MUL         = 0x50, //01 01 00 0 0
        EQU         = 0x54, //01 01 01 0 0
        //ALU10 (Shift Left, Shift Right, *, *)
        SLL         = 0x60, //01 10 00 0 0
        SRL         = 0x64, //01 10 01 0 0
        //ALU11 (Invert, And, Xor, Or)
        XOR         = 0x70, //01 11 00 0 0
        OR          = 0x74, //01 11 01 0 0
        AND         = 0x78, //01 11 10 0 0
        NOT         = 0x7C, //01 11 11 1 0

        //data transfer 10 L S NN 00
        //SB          = 0x84, //10 0 0 01 00
        //SH          = 0x88, //10 0 0 10 00
        SW          = 0x8C, //10 0 0 11 00
        //LUB         = 0xA4, //10 1 0 01 00
        //LUH         = 0xA8, //10 1 0 10 00
        //LB          = 0xB4, //10 1 1 01 00
        //LH          = 0xB8, //10 1 1 10 00
        LW          = 0xBC, //10 1 1 11 00
        
        //stack manipulation 11 SS NN P 0
        //POPB        = 0xC4, //11 00 01 0 0
        //POPH        = 0xC8, //11 00 10 0 0
        POPW        = 0xCC, //11 00 11 0 0
        PUSHBP      = 0xCE, //11 00 11 1 0
        COPY        = 0xDE, //11 01 11 1 0
        //PUSHB       = 0xF6, //11 11 01 1 0
        //PUSHH       = 0xFA, //11 11 10 1 0
        PUSHW       = 0xFE  //11 11 11 1 0
    }

    public enum MemSize
    {
        NONE = 0,
        //BYTE = 1,
        //HALF = 2,
        WORD = 1
    }

    public class AtlasCPU
    {
        public const MemSize OperandSize = MemSize.WORD;
        public const MemSize InstructionSize = MemSize.WORD;

        public static readonly int StackSize = MemSizeToInt(MemSize.WORD) * 64 * 1024; // 64K

        //utility functions for working with opcodes
        public static int ArgSizeFromOpCode(OpCode code)
        {
            switch (code)
            {
                case OpCode.PUSHW:
                    return 1;
                //case OpCode.PUSHH:
                   // return 2;
                //case OpCode.PUSHB:
                    //return 1;
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

        protected int[] m_mem;

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
            return (OpCode)MemValue(MemSize.WORD, ProgramCounter);
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
            OpCode inst = (OpCode)MemValue(MemSize.WORD, ProgramCounter);

            //the literal value used for push instruction
            int inlineLiteral = MemValue((MemSize)ArgSizeFromOpCode(inst), ProgramCounter + MemSizeToInt(InstructionSize));

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
                //+2 word
                case OpCode.BEGINARGS:
                    //pop none, push two word (old base pointer and room for return address)
                    newSP = StackPointer + 2 * MemSizeToInt(MemSize.WORD);
                    break;
                //+1 word
                case OpCode.PUSHW:
                case OpCode.PUSHBP:
                case OpCode.COPY:
                    //pop none off stack, push word
                    newSP = StackPointer + MemSizeToInt(MemSize.WORD);
                    break;
                //+0 word
                //pop one word off stack, push word
                case OpCode.LW:
                case OpCode.NOT:
                case OpCode.NEG:
                // pop none, push none
                case OpCode.NOP:
                    newSP = StackPointer;
                    break;
                //-1 word
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
                //-2 word
                case OpCode.SW:
                case OpCode.JIF:
                    // pop two word, push none
                    newSP = StackPointer - 2 * MemSizeToInt(MemSize.WORD);
                    break;
                //BP-2 word
                case OpCode.RET:
                    // set stack pointer to base pointer, pop two words
                    newSP = BasePointer - 2 * MemSizeToInt(MemSize.WORD);
                    break;
                //BP-1 word
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
                case OpCode.SW:
                case OpCode.JMP:
                case OpCode.JIF:
                case OpCode.PUSHBP:
                case OpCode.COPY:
                case OpCode.PUSHW:
                case OpCode.POPW:
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
                case OpCode.SW:
                case OpCode.PUSHBP:
                case OpCode.COPY:
                case OpCode.POPW:
                case OpCode.BEGINARGS:
                    newPC = ProgramCounter + MemSizeToInt(InstructionSize);
                    break;
                case OpCode.PUSHW:
                    //advance past word literal
                    newPC = ProgramCounter + MemSizeToInt(InstructionSize) + MemSizeToInt(MemSize.WORD);
                    break;
                case OpCode.JMP:
                case OpCode.CALL:
                    newPC = stackArgB;
                    break;
                case OpCode.JIF:
                    if (stackArgA != 0) { newPC = stackArgB; }
                    else { newPC = ProgramCounter + MemSizeToInt(InstructionSize); }
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
                case OpCode.LW:
                    writeSource = AtlasWriteSource.LoadedMemory;
                    break;
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
                //0 word
                case OpCode.NOP:
                case OpCode.JMP:
                case OpCode.JIF:
                case OpCode.POPW:
                case OpCode.RET:
                    bytesWritten = MemSize.NONE;
                    break;
                //1 word
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
                case OpCode.RET:
                    writeAddress = 0;
                    break;
                //replace top of stack
                case OpCode.LW:
                case OpCode.NEG:
                case OpCode.NOT:
                    writeAddress = StackPointer - MemSizeToInt(MemSize.WORD);
                    break;
                //write to memory address
                case OpCode.SW:
                    writeAddress = stackArgA;
                    break;
                //push
                case OpCode.PUSHBP:
                case OpCode.COPY:
                case OpCode.PUSHW:
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
            return m_mem[address];
        }

        private void WriteMem(int value, int address, MemSize memSize)
        {
            m_mem[address] = value;
        }

        /*
        private int SignExtend(int value, MemSize oldMemSize, MemSize newMemSize)
        {
            int oldSize = MemSizeToInt(oldMemSize);
            int newSize = MemSizeToInt(newMemSize);

            sbyte mostSignificantByte = (sbyte)ByteFromInt(value, oldSize - 1);

            byte extend = mostSignificantByte < 0 ? byte.MaxValue 0xFFFF  : (byte)0;

            return IntFromBytes(
                newSize >= 4 ? (oldSize >= 4 ? ByteFromInt(value, 3) : extend) : (byte)0,
                newSize >= 3 ? (oldSize >= 3 ? ByteFromInt(value, 2) : extend) : (byte)0,
                newSize >= 2 ? (oldSize >= 2 ? ByteFromInt(value, 1) : extend) : (byte)0,
                newSize >= 1 ? (oldSize >= 1 ? ByteFromInt(value, 0) : extend) : (byte)0
                );
        }
        */
        
        private int LoadMemory(OpCode inst, int stackArgA, int stackArgB)
        {
            switch(inst)
            {
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
