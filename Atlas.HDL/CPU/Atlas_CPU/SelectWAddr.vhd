----------------------------------------------------------------------------------
-- Company: 
-- Engineer: Matt
-- 
-- Create Date:    09:44:10 05/03/2016 
-- Design Name: 
-- Module Name:    SelectWAddr - Behavioral 
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

entity SelectWAddr is
	port(
		cbp : in STD_LOGIC_VECTOR(31 downto 0);
		bp : in STD_LOGIC_VECTOR(31 downto 0);
		sp : in STD_LOGIC_VECTOR(31 downto 0);
		selectPointer : in STD_LOGIC_VECTOR(1 downto 0);
		pointerOffsetPositive : in STD_LOGIC_VECTOR(1 downto 0);
		
		argA : in STD_LOGIC_VECTOR(31 downto 0);
		argAorPointer : in STD_LOGIC;
		
		wAddr : out STD_LOGIC_VECTOR(31 downto 0)
	);
end SelectWAddr;

architecture Behavioral of SelectWAddr is
	signal pointer : STD_LOGIC_VECTOR(31 downto 0);
	signal pointerOffset : STD_LOGIC_VECTOR(31 downto 0);
	signal adjustedPointer : STD_LOGIC_VECTOR(31 downto 0);
begin
	pointer <= 
		cbp when selectPointer = "00" else
		bp  when selectPointer = "01" else
		sp;
		
	pointerOffset <= 
		STD_LOGIC_VECTOR(to_signed(0,32)) when pointerOffsetPositive = "00" else
		STD_LOGIC_VECTOR(to_signed(-1,32)) when pointerOffsetPositive = "01" else
		STD_LOGIC_VECTOR(to_signed(-2,32));
		
	adjustedPointer <= pointer + pointerOffset;
	
	wAddr <= 
		argA when argAorPointer = '0' else
		adjustedPointer;

end Behavioral;

