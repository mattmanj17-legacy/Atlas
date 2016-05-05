----------------------------------------------------------------------------------
-- Company: 
-- Engineer: Matt
-- 
-- Create Date:    09:44:59 05/03/2016 
-- Design Name: 
-- Module Name:    SelectWData - Behavioral 
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

-- Uncomment the following library declaration if using
-- arithmetic functions with Signed or Unsigned values
--use IEEE.NUMERIC_STD.ALL;

-- Uncomment the following library declaration if instantiating
-- any Xilinx primitives in this code.
--library UNISIM;
--use UNISIM.VComponents.all;

entity SelectWData is
	port(
		lit : in STD_LOGIC_VECTOR(31 downto 0);
		pcPlusOne : in STD_LOGIC_VECTOR(31 downto 0);
		aluRes : in STD_LOGIC_VECTOR(31 downto 0);
		argB : in STD_LOGIC_VECTOR(31 downto 0);
		memRead : in STD_LOGIC_VECTOR(31 downto 0);
		bp : in STD_LOGIC_VECTOR(31 downto 0);
		
		selectData : in STD_LOGIC_VECTOR(2 downto 0);
		
		wData : out STD_LOGIC_VECTOR(31 downto 0)
	);
end SelectWData;

architecture Behavioral of SelectWData is
begin
	wData <= 
		lit 			when selectData = "000" else
		pcPlusOne 	when selectData = "001" else
		aluRes 		when selectData = "010" else
		argB 			when selectData = "011" else
		memRead 		when selectData = "100" else
		bp;
end Behavioral;

