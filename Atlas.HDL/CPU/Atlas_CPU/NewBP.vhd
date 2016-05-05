----------------------------------------------------------------------------------
-- Company: 
-- Engineer: Patrick
-- 
-- Create Date:    09:44:30 05/03/2016 
-- Design Name: 
-- Module Name:    NewBP - Behavioral 
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

entity NewBP is
	port( currBP : in STD_LOGIC_VECTOR(31 downto 0);
			oldBP : in STD_LOGIC_VECTOR(31 downto 0);
			cachedBP : in STD_LOGIC_VECTOR(31 downto 0);
			newBPSource : in STD_LOGIC_VECTOR(1 downto 0);
			newBPOut : out STD_LOGIC_VECTOR(31 downto 0) );
end NewBP;

architecture Behavioral of NewBP is

begin

	newBPOut <=
		currBP when newBPSource = "00" else
		oldBP when newBPSource = "01" else
		cachedBP when newBPSource = "10" else
		currBP;

end Behavioral;

