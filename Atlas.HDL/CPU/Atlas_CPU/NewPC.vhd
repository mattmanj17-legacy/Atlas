----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    09:42:49 05/03/2016 
-- Design Name: 
-- Module Name:    NewPC - Behavioral 
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

entity NewPC is
	port ( pc : in STD_LOGIC_VECTOR (31 downto 0);
			retAddr: in STD_LOGIC_VECTOR (31 downto 0);
			argA, argB : in STD_LOGIC_VECTOR (31 downto 0);
			pcOffset : in STD_LOGIC;
			jmpType : in STD_LOGIC_VECTOR (1 downto 0);
			newPCOut : out STD_LOGIC_VECTOR (31 downto 0) );
end NewPC;

architecture Behavioral of NewPC is
	signal adjPC : STD_LOGIC_VECTOR (31 downto 0);
	signal pcOffsetVal : STD_LOGIC_VECTOR (31 downto 0);
	signal conditionalResult : STD_LOGIC_VECTOR(31 downto 0);
begin
	pcOffsetVal <= 
		STD_LOGIC_VECTOR(to_unsigned(1,32)) when pcOffset = '0' else
		STD_LOGIC_VECTOR(to_unsigned(2,32)); 
		
	adjPC <= pc + pcOffsetVal;
	
	conditionalResult <= 
		adjPC when argA = "00000000000000000000000000000000" else
		argB;
	
	newPCOut <= 
		adjPC when jmpType = "00" else 
		argB when jmpType = "01" else 
		conditionalResult when jmpType = "10" else 
		retAddr;
		
end Behavioral;

