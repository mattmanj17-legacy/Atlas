----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    12:16:31 05/03/2016 
-- Design Name: 
-- Module Name:    Shifter - Behavioral 
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

entity Shifter is
	port (
        inword   : in std_logic_vector(31 downto 0);
        shift    : in std_logic_vector(4 downto 0);
		  right	  : in std_logic;
        outword  : out std_logic_vector(31 downto 0)
		  );
end Shifter;

architecture Behavioral of Shifter is
	signal leftWord : std_logic_vector(31 downto 0);
	signal rightWord : std_logic_vector(31 downto 0);
begin
	leftWord <= 
		inword                   											when shift = "00000" else
		inword(30 downto 0) & '0' 											when shift = "00001" else
		inword(29 downto 0) & "00" 										when shift = "00010" else
		inword(28 downto 0) & "000" 										when shift = "00011" else
		inword(27 downto 0) & "0000" 										when shift = "00100" else
		inword(26 downto 0) & "00000" 									when shift = "00101" else
		inword(25 downto 0) & "000000" 									when shift = "00110" else
		inword(24 downto 0) & "0000000" 									when shift = "00111" else
		inword(23 downto 0) & "00000000" 								when shift = "01000" else
		inword(22 downto 0) & "000000000" 								when shift = "01001" else
		inword(21 downto 0) & "0000000000" 								when shift = "01010" else
		inword(20 downto 0) & "00000000000" 							when shift = "01011" else
		inword(19 downto 0) & "000000000000" 							when shift = "01100" else
		inword(18 downto 0) & "0000000000000" 							when shift = "01101" else
		inword(17 downto 0) & "00000000000000" 						when shift = "01110" else
		inword(16 downto 0) & "000000000000000" 						when shift = "01111" else
		inword(15 downto 0) & "0000000000000000" 						when shift = "10000" else
		inword(14 downto 0) & "00000000000000000" 					when shift = "10001" else
		inword(13 downto 0) & "000000000000000000" 					when shift = "10010" else
		inword(12 downto 0) & "0000000000000000000" 					when shift = "10011" else
		inword(11 downto 0) & "00000000000000000000" 				when shift = "10100" else
		inword(10 downto 0) & "000000000000000000000" 				when shift = "10101" else
		inword(9 downto 0)  & "0000000000000000000000" 				when shift = "10110" else
		inword(8 downto 0)  & "00000000000000000000000" 			when shift = "10111" else
		inword(7 downto 0)  & "000000000000000000000000" 			when shift = "11000" else
		inword(6 downto 0)  & "0000000000000000000000000" 			when shift = "11001" else
		inword(5 downto 0)  & "00000000000000000000000000" 		when shift = "11010" else
		inword(4 downto 0)  & "000000000000000000000000000" 		when shift = "11011" else
		inword(3 downto 0)  & "0000000000000000000000000000" 		when shift = "11100" else
		inword(2 downto 0)  & "00000000000000000000000000000" 	when shift = "11101" else
		inword(1 downto 0)  & "000000000000000000000000000000" 	when shift = "11110" else
		inword(0)           & "0000000000000000000000000000000"; 
		
	rightWord <= 
		inword                   												when shift = "00000" else
		'0' 											& inword(31 downto 1) 	when shift = "00001" else
		"00" 											& inword(31 downto 2) 	when shift = "00010" else
		"000" 										& inword(31 downto 3) 	when shift = "00011" else
		"0000" 										& inword(31 downto 4) 	when shift = "00100" else
		"00000" 										& inword(31 downto 5) 	when shift = "00101" else
		"000000" 									& inword(31 downto 6) 	when shift = "00110" else
		"0000000" 									& inword(31 downto 7) 	when shift = "00111" else
		"00000000" 									& inword(31 downto 8) 	when shift = "01000" else
		"000000000" 								& inword(31 downto 9) 	when shift = "01001" else
		"0000000000" 								& inword(31 downto 10) 	when shift = "01010" else
		"00000000000" 								& inword(31 downto 11) 	when shift = "01011" else
		"000000000000" 							& inword(31 downto 12) 	when shift = "01100" else
		"0000000000000" 							& inword(31 downto 13) 	when shift = "01101" else
		"00000000000000" 							& inword(31 downto 14) 	when shift = "01110" else
		"000000000000000" 						& inword(31 downto 15) 	when shift = "01111" else
		"0000000000000000" 						& inword(31 downto 16) 	when shift = "10000" else
		"00000000000000000" 						& inword(31 downto 17) 	when shift = "10001" else
		"000000000000000000" 					& inword(31 downto 18) 	when shift = "10010" else
		"0000000000000000000" 					& inword(31 downto 19) 	when shift = "10011" else
		"00000000000000000000" 					& inword(31 downto 20) 	when shift = "10100" else
		"000000000000000000000" 				& inword(31 downto 21) 	when shift = "10101" else
		"0000000000000000000000" 				& inword(31 downto 22) 	when shift = "10110" else
		"00000000000000000000000" 				& inword(31 downto 23) 	when shift = "10111" else
		"000000000000000000000000" 			& inword(31 downto 24) 	when shift = "11000" else
		"0000000000000000000000000" 			& inword(31 downto 25) 	when shift = "11001" else
		"00000000000000000000000000" 			& inword(31 downto 26) 	when shift = "11010" else
		"000000000000000000000000000" 		& inword(31 downto 27) 	when shift = "11011" else
		"0000000000000000000000000000" 		& inword(31 downto 28) 	when shift = "11100" else
		"00000000000000000000000000000" 		& inword(31 downto 29) 	when shift = "11101" else
		"000000000000000000000000000000" 	& inword(31 downto 30) 	when shift = "11110" else
		"0000000000000000000000000000000" 	& inword(31); 
   
	outword <= 
		leftWord when right = '0' else
		rightWord;
	
end Behavioral;

