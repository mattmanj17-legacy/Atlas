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
			pcOffset : in STD_LOGIC_VECTOR (1 downto 0);
			jmpType : in STD_LOGIC_VECTOR (1 downto 0);
			newPCOut : out STD_LOGIC_VECTOR (31 downto 0) );
end NewPC;

architecture Behavioral of NewPC is
	signal adjPC : STD_LOGIC_VECTOR (31 downto 0);
	signal pcOffsetVal : STD_LOGIC_VECTOR (31 downto 0);
	
begin
	--calc PC offset for most instructions
	case pcOffset is
		when "00" => --normal inst
			pcOffsetVal <= STD_LOGIC_VECTOR(to_unsigned(1,32));
			
		when "01" => --increment past byte literal
			pcOffsetVal <= STD_LOGIC_VECTOR(to_unsigned(2,32));
		
		when "10" => --increment past half literal
			pcOffsetVal <= STD_LOGIC_VECTOR(to_unsigned(3,32));

		when "11" => --increment past word literal
			pcOffsetVal <= STD_LOGIC_VECTOR(to_unsigned(5,32));
	end case;
	adjPC <= pc + pcOffsetVal;
	
	case jmpType is
		when "00" => --goto next instruction (most instructions)
			newPCOut <= adjPC;
			
		when "01" => --unconditional jump (JMP)
			newPCOut <= argB;
		
		when "10" => --conditional jump (JIF)
			case argA is
				when STD_LOGIC_VECTOR(to_unsigned(0,32)) =>
					newPCOut <= adjPC;
				
				when others =>
					newPCOut <= argB;
			end case;

		when "11" => --return from func (RET, RETV)
			newPCOut <= retAddr;
	end case;
end Behavioral;

