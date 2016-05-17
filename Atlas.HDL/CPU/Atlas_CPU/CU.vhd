----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    10:20:31 05/12/2016 
-- Design Name: 
-- Module Name:    CU - Behavioral 
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

entity CU is
	port(
		instr: in STD_LOGIC_VECTOR(31 downto 0);
		
		pcWen: out STD_LOGIC;
		spWen: out STD_LOGIC;
		bpWen: out STD_LOGIC;
		memWen: out STD_LOGIC;
		cachedBpWen: out STD_LOGIC;
		f: out STD_LOGIC_VECTOR(3 downto 0);
		newBPSource: out STD_LOGIC_VECTOR(1 downto 0);
		useBP: out STD_LOGIC;
		spOffset: out STD_LOGIC_VECTOR(2 downto 0);
		selectPointer: out STD_LOGIC_VECTOR(1 downto 0);
		pointerOffsetPositive: out STD_LOGIC_VECTOR(1 downto 0);
		argAorPointer: out STD_LOGIC;
		pcOffset: out STD_LOGIC;
		selectData: out STD_LOGIC_VECTOR(2 downto 0);
		jmpType: out STD_LOGIC_VECTOR(1 downto 0)
	);
end CU;

architecture Behavioral of CU is

begin

	-- program counter is always written
	pcWen <= '1';

	-- stack pointer is akways written
	spWen <= '1';
	
	-- base pointer is always written
	bpWen <= '1';
	
	-- memory write enable
	memWen <= 	'0' when instr(7 downto 0) = x"00" or 		-- NOP
								instr(7 downto 0) = x"30" or 		-- JMP
								instr(7 downto 0) = x"31" or 		-- JIF
								instr(7 downto 0) = x"CC" or 		-- POPW
								instr(7 downto 0) = x"20" else	-- RET
					'1';
					
	-- cached base pointer write enable
	cachedBpWen <= '1' when instr(7 downto 0) = x"10" else -- BEGINARGS
						'0';
						
	-- alu op
	f <= instr(5 downto 2);
	
	-- new base pointer source
	newBPSource <= "10" when instr(7 downto 0) = x"11" else -- CALL
						"01" when instr(7 downto 0) = x"20" or instr(7 downto 0) = x"21" else -- RET & RETV
						"00";
						
	-- new base pointer
	useBP <= '1' when instr(7 downto 0) = x"20" or instr(7 downto 0) = x"21" else -- RET & RETV
				'0';
				
	-- stack pointer offset
	spOffset <=	"010" when instr(7 downto 0) = x"10" else -- BEGINARGS -- +2
					"001" when instr(7 downto 0) = x"FE" or instr(7 downto 0) = x"CE" or instr(7 downto 0) = x"DE" else -- PUSHW & PUSHBP & COPY -- +1
					"000" when instr(7 downto 0) = x"BC" or instr(7 downto 0) = x"7C" or -- LW & NOT -- +0
									instr(7 downto 0) = x"42" or instr(7 downto 0) = x"00" else --  & NEG & NOP 
					"110" when instr(7 downto 0) = x"8C" or instr(7 downto 0) = x"31" or instr(7 downto 0) = x"20" else --SW & JIF & RET -- -2
					"101"; -- -1
	
	
	
	-- select pointer
	selectPointer <=  "00" when instr(7 downto 0) = x"11" else -- CALL
							"01" when instr(7 downto 0) = x"21" else -- RETV
							"10";
							
	-- pointer offset
	pointerOffsetPositive <= 	"01" when 	instr(7 downto 0) = x"BC" or 		-- LW
											instr(7 downto 0) = x"42" or 		-- NEG
											instr(7 downto 0) = x"7C" or 		-- NOT
											instr(7 downto 0) = x"11" else	-- CALL
							"10" when instr(7 downto 0) = x"21" or instr(7 downto 6) = "01" else -- RETV & ALU OPS
							"00";
							
	-- arg A or pointer
	argAorPointer <= 	'0' when instr(7 downto 0) = x"8C" else -- SW
							'1';
							
	-- program counter offset
	pcOffset <=	'1' when instr(7 downto 0) = x"FE" else -- PUSHW
					'0';
					
	-- select data
	selectData <= 	"000" -- literal
							when instr(7 downto 0) = x"FE" else -- PUSHW
						"001" -- pcplusone
							when instr(7 downto 0) = x"11" else -- CALL
						"011" -- argb
							when instr(7 downto 0) = x"8C" or instr(7 downto 0) = x"21" or instr(7 downto 0) = x"DE" else -- SW & RETV & COPY
						"100" -- memread
							when instr(7 downto 0) = x"BC" else -- LW
						"101" -- bp
							when instr(7 downto 0) = x"CE" or instr(7 downto 0) = x"10" else -- PUSHBP & BEGINARGS
						"010"; -- alu result -> ALU OPS & JMP & JIF & RET & NOP
	
	--jump type
	jmpType <= 	"11" when instr(7 downto 0) = x"20" or instr(7 downto 0) = x"21" else -- RET & RETV
					"10" when instr(7 downto 0) = x"31" else -- JIF
					"01" when instr(7 downto 0) = x"30" or instr(7 downto 0) = x"11" else -- JMP & CALL
					"00";
					
							
							
	
end Behavioral;

