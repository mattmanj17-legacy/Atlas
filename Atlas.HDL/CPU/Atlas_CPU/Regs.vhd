----------------------------------------------------------------------------------
-- Company: 
-- Engineer: Patrick
-- 
-- Create Date:    09:45:29 05/03/2016 
-- Design Name: 
-- Module Name:    Regs - Behavioral 
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
-- use IEEE.NUMERIC_STD.ALL;

-- Uncomment the following library declaration if instantiating
-- any Xilinx primitives in this code.
--library UNISIM;
--use UNISIM.VComponents.all;

entity Regs is
	port(
		clk : in STD_LOGIC;
		
		newPc : in STD_LOGIC_VECTOR(31 downto 0);
		newSp : in STD_LOGIC_VECTOR(31 downto 0);
		newBp : in STD_LOGIC_VECTOR(31 downto 0);
		
		pcWen : in STD_LOGIC;
		spWen : in STD_LOGIC;
		bpWen : in STD_LOGIC;
		cachedBpWen : in STD_LOGIC;
		
		pc : out STD_LOGIC_VECTOR(31 downto 0);
		pcPlusOne : out STD_LOGIC_VECTOR(31 downto 0);
		
		sp : out STD_LOGIC_VECTOR(31 downto 0);
		spMinusTwo : out STD_LOGIC_VECTOR(31 downto 0);
		
		bp : out STD_LOGIC_VECTOR(31 downto 0);
		bpMinusTwo : out STD_LOGIC_VECTOR(31 downto 0);
		
		cachedBp : out STD_LOGIC_VECTOR(31 downto 0)
	);
end Regs;

architecture Behavioral of Regs is
	signal pcReg : STD_LOGIC_VECTOR(31 downto 0);
	signal spReg : STD_LOGIC_VECTOR(31 downto 0);
	signal bpReg : STD_LOGIC_VECTOR(31 downto 0);
	signal cbpReg : STD_LOGIC_VECTOR(31 downto 0);
begin
	--pc reg
	process(clk) begin
		if(rising_edge(clk)) then
			if(pcWen = '1') then
				pcReg <= newPc;
			end if;
		end if;
	end process;
	
	pcPlusOne <= pcReg + STD_LOGIC_VECTOR(to_unsigned(1,32));
	
	--sp reg
	process(clk) begin
		if(rising_edge(clk)) then
			if(spWen = '1') then
				spReg <= newSp;
			end if;
		end if;
	end process;
	
	spMinusTwo <= spReg - STD_LOGIC_VECTOR(to_unsigned(2,32));
	
	--bp reg
	process(clk) begin
		if(rising_edge(clk)) then
			if(bpWen = '1') then
				bpReg <= newBp;
			end if;
		end if;
	end process;
	
	bpMinusTwo <= bpReg - STD_LOGIC_VECTOR(to_unsigned(2,32));
	
	--cbp reg
	process(clk) begin
		if(rising_edge(clk)) then
			if(cachedBpWen = '1') then
				cbpReg <= bpReg + STD_LOGIC_VECTOR(to_unsigned(2,32));
			end if;
		end if;
	end process;

	pc <= pcReg;
	bp <= bpReg;
	sp <= spReg;
	cachedBp <= cbpreg;
end Behavioral;

