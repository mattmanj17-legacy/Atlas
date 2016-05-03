--------------------------------------------------------------------------------
-- Company: 
-- Engineer:
--
-- Create Date:   12:40:47 02/13/2016
-- Design Name:   
-- Module Name:   C:/Users/Matthew/Desktop/ALU/ShifterTest.vhd
-- Project Name:  ALU
-- Target Device:  
-- Tool versions:  
-- Description:   
-- 
-- VHDL Test Bench Created by ISE for module: barrelshifter
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
use IEEE.STD_LOGIC_ARITH.ALL;
use IEEE.STD_LOGIC_UNSIGNED.ALL;
use IEEE.NUMERIC_STD.ALL;
 
-- Uncomment the following library declaration if using
-- arithmetic functions with Signed or Unsigned values
--USE ieee.numeric_std.ALL;
 
ENTITY ShifterTest IS
END ShifterTest;
 
ARCHITECTURE behavior OF ShifterTest IS 
 
    -- Component Declaration for the Unit Under Test (UUT)
 
    COMPONENT barrelshifter
    PORT(
         inword : IN  std_logic_vector(7 downto 0);
         shift : IN  natural;
			left	  : in std_logic;
		  rotate : in std_logic;
		  arith  : in std_logic;
         outword : OUT  std_logic_vector(7 downto 0)
        );
    END COMPONENT;
    

   --Inputs
   signal inword : std_logic_vector(7 downto 0) := (others => '0');
   signal shift : natural;
	
	signal left	  : std_logic;
	signal rotate : std_logic;
	signal arith  : std_logic;

 	--Outputs
   signal outword : std_logic_vector(7 downto 0);
   -- No clocks detected in port list. Replace <clock> below with 
   -- appropriate port name
 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   uut: barrelshifter PORT MAP (
          inword => inword,
          shift => shift,
			 left => left,
			 rotate => rotate,
			 arith => arith,
          outword => outword
        );
 

   -- Stimulus process
   stim_proc: process
   begin		
      -- hold reset state for 100 ns.
      wait for 100 ns;	

      inword <= "11001001";
		left <= '1';
		rotate <= '0';
		arith <= '0';
		
		shift <= 0;
		wait for 10 ns;
		shift <= 1;
		wait for 10 ns;
		shift <= 2;
		wait for 10 ns;
		shift <= 3;
		wait for 10 ns;
		shift <= 4;
		wait for 10 ns;
		shift <= 5;
		wait for 10 ns;
		shift <= 6;
		wait for 10 ns;
		shift <= 7;
		wait for 10 ns;
		shift <= 8;
		wait for 10 ns;
		
		
      wait;
   end process;

END;
