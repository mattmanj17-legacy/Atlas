--------------------------------------------------------------------------------
-- Company: 
-- Engineer:
--
-- Create Date:   14:03:01 05/17/2016
-- Design Name:   
-- Module Name:   C:/Users/mdraper16/Documents/Atlas/Atlas.HDL/CPU/Atlas_CPU/AtlasTest.vhd
-- Project Name:  Atlas_CPU
-- Target Device:  
-- Tool versions:  
-- Description:   
-- 
-- VHDL Test Bench Created by ISE for module: Atlas
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
 
ENTITY AtlasTest IS
END AtlasTest;
 
ARCHITECTURE behavior OF AtlasTest IS 
 
    -- Component Declaration for the Unit Under Test (UUT)
 
    COMPONENT Atlas
    PORT(
         clk : IN  std_logic;
         instrOut : OUT  std_logic_vector(7 downto 0)
        );
    END COMPONENT;
    

   --Inputs
   signal clk : std_logic := '0';

 	--Outputs
   signal instrOut : std_logic_vector(7 downto 0);

   -- Clock period definitions
   constant clk_period : time := 30 ns;
 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   uut: Atlas PORT MAP (
          clk => clk,
          instrOut => instrOut
        );

   -- Clock process definitions
   clk_process :process
   begin
		clk <= '0';
		wait for clk_period/2;
		clk <= '1';
		wait for clk_period/2;
   end process;
END;
