--------------------------------------------------------------------------------
-- Company: 
-- Engineer:
--
-- Create Date:   14:38:34 05/04/2016
-- Design Name:   
-- Module Name:   C:/Users/Matthew/Desktop/Atlas/Atlas.HDL/CPU/Atlas_CPU/ShiftTestBench.vhd
-- Project Name:  Atlas_CPU
-- Target Device:  
-- Tool versions:  
-- Description:   
-- 
-- VHDL Test Bench Created by ISE for module: Shifter
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
use IEEE.NUMERIC_STD.ALL;
 
-- Uncomment the following library declaration if using
-- arithmetic functions with Signed or Unsigned values
--USE ieee.numeric_std.ALL;
 
ENTITY ShiftTestBench IS
END ShiftTestBench;
 
ARCHITECTURE behavior OF ShiftTestBench IS 
 
    -- Component Declaration for the Unit Under Test (UUT)
 
    COMPONENT Shifter
    PORT(
         inword : IN  std_logic_vector(31 downto 0);
         shift : IN  std_logic_vector(4 downto 0);
         right : IN  std_logic;
         outword : OUT  std_logic_vector(31 downto 0)
        );
    END COMPONENT;
    

   --Inputs
   signal inword : std_logic_vector(31 downto 0) := (others => '0');
   signal shift : std_logic_vector(4 downto 0) := (others => '0');
   signal right : std_logic := '0';

 	--Outputs
   signal outword : std_logic_vector(31 downto 0);
 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   uut: Shifter PORT MAP (
          inword => inword,
          shift => shift,
          right => right,
          outword => outword
        );
 

   -- Stimulus process
   stim_proc: process
   begin		
		
		inword <= "10100100010000100000100000010000";
		
		right <= '0';
		
		for I in 0 to 32 loop
			shift <= STD_LOGIC_VECTOR(to_unsigned(I, 5));
			wait for 5 ns;
			assert outword = std_logic_vector(unsigned(inword) sll I) report "left shift failed";
		end loop;
		
		right <= '1';
		
		for I in 0 to 32 loop
			shift <= STD_LOGIC_VECTOR(to_unsigned(I, 5));
			wait for 5 ns;
			assert outword = std_logic_vector(unsigned(inword) srl I) report "right shift failed";
		end loop;
		
      wait;
   end process;

END;
