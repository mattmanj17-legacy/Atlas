--------------------------------------------------------------------------------
-- Company: 
-- Engineer:
--
-- Create Date:   10:49:57 05/05/2016
-- Design Name:   
-- Module Name:   C:/Users/pprice16/Documents/Atlas/Atlas.HDL/CPU/Atlas_CPU/NewBP_TestBench.vhd
-- Project Name:  Atlas_CPU
-- Target Device:  
-- Tool versions:  
-- Description:   
-- 
-- VHDL Test Bench Created by ISE for module: NewBP
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
 
ENTITY NewBP_TestBench IS
END NewBP_TestBench;
 
ARCHITECTURE behavior OF NewBP_TestBench IS 
 
    -- Component Declaration for the Unit Under Test (UUT)
 
    COMPONENT NewBP
    PORT(
         currBP : IN  std_logic_vector(31 downto 0);
         oldBP : IN  std_logic_vector(31 downto 0);
         cachedBP : IN  std_logic_vector(31 downto 0);
         newBPSource : IN  std_logic_vector(1 downto 0);
         newBPOut : OUT  std_logic_vector(31 downto 0)
        );
    END COMPONENT;
    

   --Inputs
   signal currBP : std_logic_vector(31 downto 0) := (others => '0');
   signal oldBP : std_logic_vector(31 downto 0) := (others => '0');
   signal cachedBP : std_logic_vector(31 downto 0) := (others => '0');
   signal newBPSource : std_logic_vector(1 downto 0) := (others => '0');

 	--Outputs
   signal newBPOut : std_logic_vector(31 downto 0);
 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   uut: NewBP PORT MAP (
          currBP => currBP,
          oldBP => oldBP,
          cachedBP => cachedBP,
          newBPSource => newBPSource,
          newBPOut => newBPOut
        );
 
   -- Stimulus process
   stim_proc: process
   begin		
	
		currBP <= STD_LOGIC_VECTOR(to_unsigned(16#0000000F#, 32));
		oldBP <= STD_LOGIC_VECTOR(to_unsigned(16#00000F00#, 32));
		cachedBP <= STD_LOGIC_VECTOR(to_unsigned(16#00F00000#, 32));
		
		newBPSource <= "00";
      wait for 10 ns;	
		assert newBPOut = STD_LOGIC_VECTOR(to_unsigned(16#0000000F#, 32))
			report "currBP source failed";
			
		newBPSource <= "01";
      wait for 10 ns;	
		assert newBPOut = STD_LOGIC_VECTOR(to_unsigned(16#00000F00#, 32))
			report "oldBP source failed";
		
		newBPSource <= "10";
      wait for 10 ns;	
		assert newBPOut = STD_LOGIC_VECTOR(to_unsigned(16#00F00000#, 32))
			report "cachedBP source failed";

      wait;
   end process;

END;
