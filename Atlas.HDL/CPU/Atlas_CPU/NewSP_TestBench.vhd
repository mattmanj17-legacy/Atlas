--------------------------------------------------------------------------------
-- Company: 
-- Engineer:
--
-- Create Date:   10:08:50 05/05/2016
-- Design Name:   
-- Module Name:   C:/Users/pprice16/Documents/Atlas/Atlas.HDL/CPU/Atlas_CPU/NewSP_TestBench.vhd
-- Project Name:  Atlas_CPU
-- Target Device:  
-- Tool versions:  
-- Description:   
-- 
-- VHDL Test Bench Created by ISE for module: NewSP
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
 
ENTITY NewSP_TestBench IS
END NewSP_TestBench;
 
ARCHITECTURE behavior OF NewSP_TestBench IS 
 
    -- Component Declaration for the Unit Under Test (UUT)
 
    COMPONENT NewSP
    PORT(
         oldSP : IN  std_logic_vector(31 downto 0);
         oldBP : IN  std_logic_vector(31 downto 0);
         spOffset : IN  std_logic_vector(2 downto 0);
         useBP : IN  std_logic;
         newSPOut : OUT  std_logic_vector(31 downto 0)
        );
    END COMPONENT;
    

   --Inputs
   signal oldSP : std_logic_vector(31 downto 0) := (others => '0');
   signal oldBP : std_logic_vector(31 downto 0) := (others => '0');
   signal spOffset : std_logic_vector(2 downto 0) := (others => '0');
   signal useBP : std_logic := '0';

 	--Outputs
   signal newSPOut : std_logic_vector(31 downto 0);
 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   uut: NewSP PORT MAP (
          oldSP => oldSP,
          oldBP => oldBP,
          spOffset => spOffset,
          useBP => useBP,
          newSPOut => newSPOut
        );

   -- Stimulus process
   stim_proc: process
   begin		
	
		oldSP <= STD_LOGIC_VECTOR(to_unsigned(16#00000008#, 32));
		oldBP <= STD_LOGIC_VECTOR(to_unsigned(16#00080008#, 32));
		
		-- Test SP Source
		spOffset <= "000";
		useBP <= '0';
		
		wait for 10 ns;
		assert newSPOut = STD_LOGIC_VECTOR(to_unsigned(16#00000008#, 32))
			report "failed SP+0";
			
		spOffset <= "001";
		wait for 10 ns;
		assert newSPOut = STD_LOGIC_VECTOR(to_unsigned(16#00000009#, 32))
			report "failed SP+1";
			
		spOffset <= "010";
		wait for 10 ns;
		assert newSPOut = STD_LOGIC_VECTOR(to_unsigned(16#0000000A#, 32))
			report "failed SP+2";
			
		spOffset <= "101";
		wait for 10 ns;
		assert newSPOut = STD_LOGIC_VECTOR(to_unsigned(16#00000007#, 32))
			report "failed SP-1";
			
		spOffset <= "110";
		wait for 10 ns;
		assert newSPOut = STD_LOGIC_VECTOR(to_unsigned(16#00000006#, 32))
			report "failed SP-2";
			
			
		-- Test BP Source	
		spOffset <= "000";
		useBP <= '1';
		wait for 10 ns;
		assert newSPOut = STD_LOGIC_VECTOR(to_unsigned(16#00080008#, 32))
			report "failed BP+0";
			
		spOffset <= "001";
		wait for 10 ns;
		assert newSPOut = STD_LOGIC_VECTOR(to_unsigned(16#00080009#, 32))
			report "failed BP+1";
			
		spOffset <= "010";
		wait for 10 ns;
		assert newSPOut = STD_LOGIC_VECTOR(to_unsigned(16#0008000A#, 32))
			report "failed BP+2";
			
		spOffset <= "101";
		wait for 10 ns;
		assert newSPOut = STD_LOGIC_VECTOR(to_unsigned(16#00080007#, 32))
			report "failed BP-1";
			
		spOffset <= "110";
		wait for 10 ns;
		assert newSPOut = STD_LOGIC_VECTOR(to_unsigned(16#00080006#, 32))
			report "failed BP-2";
			
      wait;
   end process;

END;
