----------------------------------------------------------------------------------
-- Company: 
-- Engineer: Patrick
-- 
-- Create Date:    09:43:06 05/03/2016 
-- Design Name: 
-- Module Name:    NewSP - Behavioral 
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

entity NewSP is
	port( oldSP : in STD_LOGIC_VECTOR(31 downto 0);
			oldBP : in STD_LOGIC_VECTOR(31 downto 0);
			spOffset : in STD_LOGIC_VECTOR(2 downto 0);
			useBP : in STD_LOGIC;
			newSPOut : out STD_LOGIC_VECTOR(31 downto 0) );
	
end NewSP;

architecture Behavioral of NewSP is
	signal offsetVal : STD_LOGIC_VECTOR(31 downto 0);
	signal decidedSource : STD_LOGIC_VECTOR(31 downto 0);
	
begin
	offsetVal <= 
		STD_LOGIC_VECTOR(to_signed(0,32)) when spOffset = "000" else
		STD_LOGIC_VECTOR(to_signed(1,32)) when spOffset = "001" else
		STD_LOGIC_VECTOR(to_signed(2,32)) when spOffset = "010" else
		STD_LOGIC_VECTOR(to_signed(-1,32)) when spOffset = "101" else
		STD_LOGIC_VECTOR(to_signed(-2,32)) when spOffset = "110" else
		STD_LOGIC_VECTOR(to_signed(0,32)); 
		
	decidedSource <=
		oldSP when useBP = '0' else
		oldBP;
		
	newSPOut <= decidedSource + offsetVal;

end Behavioral;

