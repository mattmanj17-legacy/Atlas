--------------------------------------------------------------------------------
-- Company: 
-- Engineer:
--
-- Create Date:   14:59:12 05/04/2016
-- Design Name:   
-- Module Name:   C:/Users/Matthew/Desktop/Atlas/Atlas.HDL/CPU/Atlas_CPU/ALUTestBench.vhd
-- Project Name:  Atlas_CPU
-- Target Device:  
-- Tool versions:  
-- Description:   
-- 
-- VHDL Test Bench Created by ISE for module: ALU
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
LIBRARY ieee;
USE ieee.std_logic_1164.ALL;
use IEEE.STD_LOGIC_UNSIGNED.ALL;
use IEEE.NUMERIC_STD.ALL;
 
-- Uncomment the following library declaration if using
-- arithmetic functions with Signed or Unsigned values
--USE ieee.numeric_std.ALL;
 
ENTITY ALUTestBench IS
END ALUTestBench;
 
ARCHITECTURE behavior OF ALUTestBench IS 
 
    -- Component Declaration for the Unit Under Test (UUT)
 
    COMPONENT ALU
    PORT(
         a : IN  std_logic_vector(31 downto 0);
         b : IN  std_logic_vector(31 downto 0);
         f : IN  std_logic_vector(3 downto 0);
         Y : OUT  std_logic_vector(31 downto 0)
        );
    END COMPONENT;
    

   --Inputs
   signal a : std_logic_vector(31 downto 0) := (others => '0');
   signal b : std_logic_vector(31 downto 0) := (others => '0');
   signal f : std_logic_vector(3 downto 0) := (others => '0');

 	--Outputs
   signal Y : std_logic_vector(31 downto 0);
 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   uut: ALU PORT MAP (
          a => a,
          b => b,
          f => f,
          Y => Y
        );
 

   -- Stimulus process
   stim_proc: process
   begin		
      -- hold reset state for 100 ns.
      wait for 100 ns;	

		-- A = 3, B = 2;
		a <= std_logic_vector(to_unsigned(3, 32));
		b <= std_logic_vector(to_unsigned(2, 32));
		
		--ALU00 (Invert Sign, Subtraction, Addition, Less Than)
		--neg 00 : Y = -B
		f <= "0000";
		wait for 10 ns;
		assert Y = std_logic_vector(to_signed(-2, 32)) report "0000 failed Y:" & integer'image(to_integer(unsigned(Y))) & " a:" & integer'image(to_integer(unsigned(a))) & " b:" & integer'image(to_integer(unsigned(b)));
		wait for 10 ns;
		--sub 01 : Y = A - B
		f <= "0001";
		wait for 10 ns;
		assert Y = std_logic_vector(to_unsigned(1, 32)) report "0001 failed Y:" & integer'image(to_integer(unsigned(Y))) & " a:" & integer'image(to_integer(unsigned(a))) & " b:" & integer'image(to_integer(unsigned(b)));
		wait for 10 ns;
		--add 10 : Y = A + B
		f <= "0010";
		wait for 10 ns;
		assert Y = std_logic_vector(to_unsigned(5, 32)) report "0010 failed Y:" & integer'image(to_integer(unsigned(Y))) & " a:" & integer'image(to_integer(unsigned(a))) & " b:" & integer'image(to_integer(unsigned(b)));
		wait for 10 ns;
		--lt  11 : Y = A < B
		f <= "0011";
		wait for 10 ns;
		assert Y = std_logic_vector(to_unsigned(0, 32)) report "0011 failed Y:" & integer'image(to_integer(unsigned(Y))) & " a:" & integer'image(to_integer(unsigned(a))) & " b:" & integer'image(to_integer(unsigned(b)));
		wait for 10 ns;
		
		--ALU01 (Multiplication, Equality check, *, *)
		--mul 00 : Y = A * B
		f <= "0100";
		wait for 10 ns;
		assert Y = std_logic_vector(to_unsigned(6, 32)) report "0100 failed Y:" & integer'image(to_integer(unsigned(Y))) & " a:" & integer'image(to_integer(unsigned(a))) & " b:" & integer'image(to_integer(unsigned(b)));
		wait for 10 ns;
		--equ 01 : Y = A == B
		f <= "0101";
		wait for 10 ns;
		assert Y = std_logic_vector(to_unsigned(0, 32)) report "0101 failed Y:" & integer'image(to_integer(unsigned(Y))) & " a:" & integer'image(to_integer(unsigned(a))) & " b:" & integer'image(to_integer(unsigned(b)));
		wait for 10 ns;
		
		--ALU10 (Shift Left, Shift Right, *, *)
		--sll 00 : Y = A << B
		f <= "1000";
		wait for 10 ns;
		assert Y = std_logic_vector(to_unsigned(12, 32)) report "1000 failed Y:" & integer'image(to_integer(unsigned(Y))) & " a:" & integer'image(to_integer(unsigned(a))) & " b:" & integer'image(to_integer(unsigned(b)));
		wait for 10 ns;
		--srl 01 : Y = A >> B
		f <= "1001";
		wait for 10 ns;
		assert Y = std_logic_vector(to_unsigned(0, 32)) report "1001 failed Y:" & integer'image(to_integer(unsigned(Y))) & " a:" & integer'image(to_integer(unsigned(a))) & " b:" & integer'image(to_integer(unsigned(b)));
		wait for 10 ns;
		
		--ALU11 (Xor, Or, And, Invert)
		--xor 00 : Y = A ^ B
		f <= "1100";
		wait for 10 ns;
		assert Y = std_logic_vector(to_unsigned(1, 32)) report "1100 failed Y:" & integer'image(to_integer(unsigned(Y))) & " a:" & integer'image(to_integer(unsigned(a))) & " b:" & integer'image(to_integer(unsigned(b)));
		wait for 10 ns;
		--or  01 : Y = A | B
		f <= "1101";
		wait for 10 ns;
		assert Y = std_logic_vector(to_unsigned(3, 32)) report "1101 failed Y:" & integer'image(to_integer(unsigned(Y))) & " a:" & integer'image(to_integer(unsigned(a))) & " b:" & integer'image(to_integer(unsigned(b)));
		wait for 10 ns;
		--and 10 : Y = A & B
		f <= "1110";
		wait for 10 ns;
		assert Y = std_logic_vector(to_unsigned(2, 32)) report "1110 failed Y:" & integer'image(to_integer(unsigned(Y))) & " a:" & integer'image(to_integer(unsigned(a))) & " b:" & integer'image(to_integer(unsigned(b)));
		wait for 10 ns;
		--not 11 : Y = ~B
		f <= "1111";
		wait for 10 ns;
		assert Y = "11111111111111111111111111111101" report "1111 failed Y:" & integer'image(to_integer(unsigned(Y))) & " a:" & integer'image(to_integer(unsigned(a))) & " b:" & integer'image(to_integer(unsigned(b)));
		wait for 10 ns;
		
      wait;
   end process;

END;
