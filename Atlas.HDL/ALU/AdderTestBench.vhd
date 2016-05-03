--------------------------------------------------------------------------------
-- Company: 
-- Engineer:
--
-- Create Date:   09:52:41 02/11/2016
-- Design Name:   
-- Module Name:   C:/Users/pprice16/Desktop/CS401/ALU/AdderTestBench.vhd
-- Project Name:  ALU
-- Target Device:  
-- Tool versions:  
-- Description:   
-- 
-- VHDL Test Bench Created by ISE for module: adder
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
USE ieee.numeric_std.ALL;
 
-- Uncomment the following library declaration if using
-- arithmetic functions with Signed or Unsigned values
--USE ieee.numeric_std.ALL;
 
ENTITY AdderTestBench IS
END AdderTestBench;
 
ARCHITECTURE behavior OF AdderTestBench IS 
 
    -- Component Declaration for the Unit Under Test (UUT)
 
    COMPONENT adder
    PORT(
         a : IN  std_logic_vector(7 downto 0);
         b : IN  std_logic_vector(7 downto 0);
         cin : IN  std_logic;
         sum : OUT  std_logic_vector(7 downto 0);
         cout : OUT  std_logic
        );
    END COMPONENT;
    

   --Inputs
   signal a : std_logic_vector(7 downto 0) := (others => '0');
   signal b : std_logic_vector(7 downto 0) := (others => '0');
   signal cin : std_logic := '0';

 	--Outputs
   signal sum : std_logic_vector(7 downto 0);
   signal cout : std_logic;
   -- No clocks detected in port list. Replace <clock> below with 
   -- appropriate port name 

 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   uut: adder PORT MAP (
          a => a,
          b => b,
          cin => cin,
          sum => sum,
          cout => cout
        );

   -- Stimulus process
   stim_proc: process
   begin		
      -- hold reset state for 100 ns.
      wait for 100 ns;	
		a <= std_logic_vector(to_unsigned(7, 8));
		b <= std_logic_vector(to_unsigned(7, 8));
		cin <= '0';
		wait for 10 ns;
		assert sum = std_logic_vector(to_unsigned(14, 8))
			report "7 + 7 failed!";
		a <= std_logic_vector(to_unsigned(8, 8));
		b <= std_logic_vector(to_unsigned(1, 8));
		cin <= '1';
		wait for 10 ns;
		assert sum = std_logic_vector(to_unsigned(10, 8))
			report "8 + 1 + carry failed!";
		a <= std_logic_vector(to_unsigned(64, 8));
		b <= std_logic_vector(to_unsigned(84, 8));
		cin <= '0';
		wait for 10 ns;
		assert sum = std_logic_vector(to_unsigned(148, 8))
			report "64 + 84 failed!";
		a <= std_logic_vector(to_unsigned(46, 8));
		b <= std_logic_vector(to_unsigned(12, 8));
		cin <= '0';
		wait for 10 ns;
		assert sum = std_logic_vector(to_unsigned(58, 8))
			report "46 + 12 failed!";
		a <= std_logic_vector(to_unsigned(1, 8));
		b <= std_logic_vector(to_unsigned(0, 8));
		cin <= '0';
		wait for 10 ns;
		assert sum = std_logic_vector(to_unsigned(1, 8))
			report "1 + 0 failed!";
		a <= std_logic_vector(to_unsigned(2, 8));
		b <= std_logic_vector(to_unsigned(15, 8));
		cin <= '1';
		wait for 10 ns;
		assert sum = std_logic_vector(to_unsigned(18, 8))
			report "2 + 15 + carry failed!";

      -- insert stimulus here 

      wait;
   end process;

END;
