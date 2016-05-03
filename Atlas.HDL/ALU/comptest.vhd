--------------------------------------------------------------------------------
-- Company: 
-- Engineer:
--
-- Create Date:   12:58:38 02/14/2016
-- Design Name:   
-- Module Name:   C:/Users/Matthew/Desktop/ALU/comptest.vhd
-- Project Name:  ALU
-- Target Device:  
-- Tool versions:  
-- Description:   
-- 
-- VHDL Test Bench Created by ISE for module: comparators
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
 
-- Uncomment the following library declaration if using
-- arithmetic functions with Signed or Unsigned values
--USE ieee.numeric_std.ALL;
 
ENTITY comptest IS
END comptest;
 
ARCHITECTURE behavior OF comptest IS 
 
    -- Component Declaration for the Unit Under Test (UUT)
 
    COMPONENT comparators
    PORT(
         a : IN  std_logic_vector(7 downto 0);
         b : IN  std_logic_vector(7 downto 0);
         eq : OUT  std_logic;
         neq : OUT  std_logic;
         lt : OUT  std_logic;
         lte : OUT  std_logic;
         gt : OUT  std_logic;
         gte : OUT  std_logic
        );
    END COMPONENT;
    

   --Inputs
   signal a : std_logic_vector(7 downto 0) := (others => '0');
   signal b : std_logic_vector(7 downto 0) := (others => '0');

 	--Outputs
   signal eq : std_logic;
   signal neq : std_logic;
   signal lt : std_logic;
   signal lte : std_logic;
   signal gt : std_logic;
   signal gte : std_logic;
 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   uut: comparators PORT MAP (
          a => a,
          b => b,
          eq => eq,
          neq => neq,
          lt => lt,
          lte => lte,
          gt => gt,
          gte => gte
        );

   -- Stimulus process
   stim_proc: process
   begin		
      -- hold reset state for 100 ns.
      wait for 100 ns;	

       a <= "00000001"; --1
		 b <= "00000001"; --1
		 wait for 10 ns;
		 
		 assert eq = '1' report "eq11 failed";
		 assert neq = '0' report "neq11 failed";
		 assert lt = '0' report "lt11 failed";
		 assert lte = '1' report "lte11 failed";
		 assert gt = '0' report "gt11 failed";
		 assert gte = '1' report "gte11 failed";
		 
		 wait for 10 ns;
		 
		 a <= "00000001"; --1
		 b <= "00000010"; --2
		 wait for 10 ns;
		 
		 assert eq = '0' report "eq12 failed";
		 assert neq = '1' report "neq12 failed";
		 assert lt = '1' report "lt12 failed";
		 assert lte = '1' report "lte12 failed";
		 assert gt = '0' report "gt12 failed";
		 assert gte = '0' report "gte12 failed";

      wait;
   end process;

END;
