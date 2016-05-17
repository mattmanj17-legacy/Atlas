----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    12:02:59 05/17/2016 
-- Design Name: 
-- Module Name:    Atlas - Behavioral 
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

entity Atlas is
	port(
		clk : in STD_LOGIC;
		instrOut : out STD_LOGIC_VECTOR(7 downto 0)
	);
end Atlas;

architecture Behavioral of Atlas is
	component DPU is
		port(
			clk : in STD_LOGIC;
			
			--alu control signal
			f : in STD_LOGIC_VECTOR( 3 downto 0 );
			
			--memory control signals
			memWen : in STD_LOGIC;
			
			--newbp control signals
			newBPSource : in STD_LOGIC_VECTOR(1 downto 0);
			
			--new pc control signals
			pcOffset : in STD_LOGIC;
			jmpType : in STD_LOGIC_VECTOR (1 downto 0);
			
			--newsp control signals
			useBP : in STD_LOGIC;
			spOffset : in STD_LOGIC_VECTOR(2 downto 0);
			
			--regs control signals
			pcWen : in STD_LOGIC;
			spWen : in STD_LOGIC;
			bpWen : in STD_LOGIC;
			cachedBpWen : in STD_LOGIC;
			
			--wAddr control Signals
			selectPointer : in STD_LOGIC_VECTOR(1 downto 0);
			pointerOffsetPositive : in STD_LOGIC_VECTOR(1 downto 0);
			argAorPointer : in STD_LOGIC;
			
			--wData control Signals
			selectData : in STD_LOGIC_VECTOR(2 downto 0);
			
			--instruction goes to CU
			instruction : out STD_LOGIC_VECTOR(31 downto 0)
		);
	end component;
	component CU is
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
	end component;
	
	signal instr: STD_LOGIC_VECTOR(31 downto 0);	
	signal pcWen: STD_LOGIC;
	signal spWen: STD_LOGIC;
	signal bpWen: STD_LOGIC;
	signal memWen: STD_LOGIC;
	signal cachedBpWen: STD_LOGIC;
	signal f: STD_LOGIC_VECTOR(3 downto 0);
	signal newBPSource: STD_LOGIC_VECTOR(1 downto 0);
	signal useBP: STD_LOGIC;
	signal spOffset: STD_LOGIC_VECTOR(2 downto 0);
	signal selectPointer: STD_LOGIC_VECTOR(1 downto 0);
	signal pointerOffsetPositive: STD_LOGIC_VECTOR(1 downto 0);
	signal argAorPointer: STD_LOGIC;
	signal pcOffset: STD_LOGIC;
	signal selectData: STD_LOGIC_VECTOR(2 downto 0);
	signal jmpType: STD_LOGIC_VECTOR(1 downto 0);
begin
	DPUComp : DPU port map (
		clk => clk ,							
		f => f ,
		memWen => memWen ,
		newBPSource => newBPSource ,
		pcOffset => pcOffset ,
		jmpType => jmpType ,
		useBP => useBP ,
		spOffset => spOffset ,
		pcWen => pcWen ,
		spWen => spWen ,
		bpWen => bpWen ,
		cachedBpWen => cachedBpWen ,
		selectPointer => selectPointer ,
		pointerOffsetPositive => pointerOffsetPositive ,
		argAorPointer => argAorPointer ,
		selectData => selectData ,
		instruction => instr
	);
	CUComp : CU port map (
		f => f ,
		memWen => memWen , 
		newBPSource => newBPSource ,
		pcOffset => pcOffset ,
		jmpType => jmpType ,
		useBP => useBP ,
		spOffset => spOffset ,
		pcWen => pcWen ,
		spWen => spWen ,
		bpWen => bpWen ,
		cachedBpWen => cachedBpWen ,
		selectPointer => selectPointer ,
		pointerOffsetPositive => pointerOffsetPositive ,
		argAorPointer => argAorPointer ,
		selectData => selectData ,
		instr => instr
	);

	instrOut <= instr(7 downto 0);
end Behavioral;

