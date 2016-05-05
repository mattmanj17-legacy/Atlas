--------------------------------------------------------------------------------
-- Company: 
-- Engineer:Patrick/Matt
--
-- Create Date:   10:08:53 05/03/2016
-- Design Name:   
-- Module Name:   C:/Users/pprice16/Documents/Atlas/Atlas.HDL/CPU/Atlas_CPU/NewPC_TestBench.vhd
-- Project Name:  Atlas_CPU
-- Target Device:  
-- Tool versions:  
-- Description:   
-- 
-- VHDL Test Bench Created by ISE for module: NewPC
-- 
-- Dependencies:
-- 
-- Revision:
-- Revision 0.01 - File Created
-- Additional Comments:
--
-- Notes: 
-- This testbench has been automatically generated using types std_logic and
-- std_logic_vector for the ports of the unit under test.  Xilinx recommends
-- that these types always be used for the top-level I/O of a design in order
-- to guarantee that the testbench will bind correctly to the post-implementation 
-- simulation model.
--------------------------------------------------------------------------------
library IEEE;
use IEEE.STD_LOGIC_1164.ALL;
use IEEE.STD_LOGIC_ARITH.ALL;
use IEEE.STD_LOGIC_UNSIGNED.ALL;
use IEEE.NUMERIC_STD.ALL;
 
-- Uncomment the following library declaration if using
-- arithmetic functions with Signed or Unsigned values
--USE ieee.numeric_std.ALL;
 
ENTITY NewPC_TestBench IS
END NewPC_TestBench;
 
ARCHITECTURE behavior OF NewPC_TestBench IS 
 
    -- Component Declaration for the Unit Under Test (UUT)
 
    COMPONENT NewPC
    PORT(
         pc : IN  std_logic_vector(31 downto 0);
         retAddr : IN  std_logic_vector(31 downto 0);
         argA : IN  std_logic_vector(31 downto 0);
         argB : IN  std_logic_vector(31 downto 0);
         pcOffset : IN  std_logic_vector(1 downto 0);
         jmpType : IN  std_logic_vector(1 downto 0);
         newPCOut : OUT  std_logic_vector(31 downto 0)
        );
    END COMPONENT;
    

   --Inputs
   signal pc : std_logic_vector(31 downto 0) := (others => '0');
   signal retAddr : std_logic_vector(31 downto 0) := (others => '0');
   signal argA : std_logic_vector(31 downto 0) := (others => '0');
   signal argB : std_logic_vector(31 downto 0) := (others => '0');
   signal pcOffset : std_logic_vector(1 downto 0) := (others => '0');
   signal jmpType : std_logic_vector(1 downto 0) := (others => '0');

 	--Outputs
   signal newPCOut : std_logic_vector(31 downto 0);
 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   uut: NewPC PORT MAP (
          pc => pc,
          retAddr => retAddr,
          argA => argA,
          argB => argB,
          pcOffset => pcOffset,
          jmpType => jmpType,
          newPCOut => newPCOut
        );

   -- Stimulus process
   stim_proc: process
   begin		
      -- insert stimulus here 
		pc <= STD_LOGIC_VECTOR(to_unsigned(16#00000000#, 32));
		
		argB <= STD_LOGIC_VECTOR(to_unsigned(16#0FFFFFFF#, 32));
		argA <= STD_LOGIC_VECTOR(to_unsigned(16#00000000#, 32));
		retAddr <= STD_LOGIC_VECTOR(to_unsigned(16#0FFFFFFE#, 32));
		pcOffset <= "00";
		jmpType <= "00";
		
		--test non-jump/goto instructions
      wait for 20 ns;	
		assert newPCOut = STD_LOGIC_VECTOR(to_unsigned(1, 32))
			report "increment +1 failed";
			
		pcOffset <= "01";
      wait for 20 ns;	
		assert newPCOut = STD_LOGIC_VECTOR(to_unsigned(2, 32))
			report "increment +2 failed";
			
		pcOffset <= "10";
      wait for 20 ns;	
		assert newPCOut = STD_LOGIC_VECTOR(to_unsigned(3, 32))
			report "increment +3 failed";
			
		pcOffset <= "11";
      wait for 10 ns;	
		assert newPCOut = STD_LOGIC_VECTOR(to_unsigned(5, 32))
			report "increment +5 failed";
		
		--test jumps
		jmpType <= "01";
      wait for 20 ns;	
		assert newPCOut = argB 
			report "unconditional jmp failed";
			
		--test conditional jmp on argA
		jmpType <= "10";
		pcOffset <= "00";
		argA <= STD_LOGIC_VECTOR(to_unsigned(16#00000001#, 32));
      wait for 20 ns;	
		assert newPCOut = argB 
			report "conditional jmp (argA == true) failed";
			
		jmpType <= "10";
		pcOffset <= "00";
		argA <= STD_LOGIC_VECTOR(to_unsigned(16#00000000#, 32));
      wait for 20 ns;	
		assert newPCOut = pc + STD_LOGIC_VECTOR(to_unsigned(16#00000001# , 32))
			report "conditional jmp (argA == false) failed";
			
			
		--test return from func
		jmpType <= "11";
		retAddr <= STD_LOGIC_VECTOR(to_unsigned(16#0F0E0D0C#, 32));
      wait for 20 ns;	
		assert newPCOut = STD_LOGIC_VECTOR(to_unsigned(16#0F0E0D0C# , 32))
			report "return failed";
		
      wait;
   end process;

END;
