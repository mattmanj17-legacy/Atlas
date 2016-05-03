--------------------------------------------------------------------------------
-- Company: 
-- Engineer:
--
-- Create Date:   10:06:03 05/03/2016
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
LIBRARY ieee;
USE ieee.std_logic_1164.ALL;
 
-- Uncomment the following library declaration if using
-- arithmetic functions with Signed or Unsigned values
--USE ieee.numeric_std.ALL;
 
ENTITY NewPC_TestBench IS
END NewPC_TestBench;
 
ARCHITECTURE behavior OF NewPC_TestBench IS 
 
    -- Component Declaration for the Unit Under Test (UUT)
 
    COMPONENT NewPC
    PORT(
        );
    END COMPONENT;
    
 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   uut: NewPC PORT MAP (
        );

   -- Stimulus process
   stim_proc: process
   begin		
      -- hold reset state for 100 ns.
      wait for 100 ns;	

      wait for <clock>_period*10;

      -- insert stimulus here 

      wait;
   end process;

END;
