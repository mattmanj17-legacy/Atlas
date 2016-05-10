----------------------------------------------------------------------------------
-- Company: 
-- Engineer: Matt
-- 
-- Create Date:    09:45:58 05/03/2016 
-- Design Name: 
-- Module Name:    Memory - Behavioral 
-- Project Name: 
-- Target Devices: 
-- Tool versions: 
-- Description: 
--
-- Dependencies: 
--
-- Revision: 
-- Revision 0.01 - File Created
-- Additional Comments: 
--
----------------------------------------------------------------------------------
library IEEE;
use IEEE.STD_LOGIC_1164.ALL;
use IEEE.STD_LOGIC_ARITH.ALL;
use IEEE.STD_LOGIC_UNSIGNED.ALL;
use IEEE.NUMERIC_STD.ALL;

-- Uncomment the following library declaration if using
-- arithmetic functions with Signed or Unsigned values
--use IEEE.NUMERIC_STD.ALL;

-- Uncomment the following library declaration if instantiating
-- any Xilinx primitives in this code.
--library UNISIM;
--use UNISIM.VComponents.all;

entity Memory is
	port(
		clk : in STD_LOGIC;
	
		pcPlusOne : in STD_LOGIC_VECTOR(31 downto 0);
		pc : in STD_LOGIC_VECTOR(31 downto 0);
		spMinusTwo : in STD_LOGIC_VECTOR(31 downto 0);
		bpMinusTwo : in STD_LOGIC_VECTOR(31 downto 0);
		memReadAddr : in STD_LOGIC_VECTOR(31 downto 0);
		
		wAddr : in STD_LOGIC_VECTOR(31 downto 0);
		wData : in STD_LOGIC_VECTOR(31 downto 0);
		
		wEN : in STD_LOGIC; -- write enabled
		
		lit : out STD_LOGIC_VECTOR(31 downto 0);
		instruction : out STD_LOGIC_VECTOR(31 downto 0);
		argA : out STD_LOGIC_VECTOR(31 downto 0);
		argB : out STD_LOGIC_VECTOR(31 downto 0);
		memReadVal : out STD_LOGIC_VECTOR(31 downto 0);
		retAddr : out STD_LOGIC_VECTOR(31 downto 0);
		oldBP : out STD_LOGIC_VECTOR(31 downto 0)
	);
end Memory;

architecture Behavioral of Memory is
	type mem_array is array((2**8-1) downto 0) of STD_LOGIC_VECTOR(31 downto 0);
	signal mem : mem_array;
begin
	process(clk) begin
		if rising_edge(clk) then
			if wEN = '1' then mem(conv_integer(wAddr)) <= wData; end if;
		end if;
	end process;
	
	lit <= (mem(conv_integer(pcPlusOne)));
	instruction <= mem(conv_integer(pc));
	argA <= (mem(conv_integer(spMinusTwo)));
	argB <= (mem(conv_integer(spMinusTwo + 1)));
	memReadVal <= (mem(conv_integer(memReadAddr)));
	retAddr <= (mem(conv_integer(bpMinusTwo)));
	oldBP <= (mem(conv_integer(bpMinusTwo + 1)));
	
end Behavioral;

