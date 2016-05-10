----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    09:41:54 05/03/2016 
-- Design Name: 
-- Module Name:    DPU - Behavioral 
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

entity DPU is
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
		selectPointer : in STD_LOGIC_VECTOR(2 downto 0);
		pointerOffsetPositive : in STD_LOGIC;
		argAorPointer : in STD_LOGIC;
		
		--wData control Signals
		selectData : in STD_LOGIC_VECTOR(2 downto 0);
		
		--instruction goes to CU
		instruction : out STD_LOGIC_VECTOR(31 downto 0)
	);
end DPU;

architecture Behavioral of DPU is
	component ALU
    port ( a, b : in STD_LOGIC_VECTOR( 31 downto 0 );
			f : in STD_LOGIC_VECTOR( 3 downto 0 );
			Y : out STD_LOGIC_VECTOR( 31 downto 0 ) );
   end component;
	component Memory
		port(
			clk : in STD_LOGIC;
		
			pcPlusOne : in STD_LOGIC_VECTOR(31 downto 0);
			pc : in STD_LOGIC_VECTOR(31 downto 0);
			spMinusTwo : in STD_LOGIC_VECTOR(31 downto 0);
			bpMinusTwo : in STD_LOGIC_VECTOR(31 downto 0);
			memReadAddr : in STD_LOGIC_VECTOR(31 downto 0);
			
			wAddr : in STD_LOGIC_VECTOR(31 downto 0);
			wData : in STD_LOGIC_VECTOR(31 downto 0);
			
			wEN : in STD_LOGIC; -- write enabled
			
			lit : out STD_LOGIC_VECTOR(31 downto 0);
			instruction : out STD_LOGIC_VECTOR(31 downto 0);
			argA : out STD_LOGIC_VECTOR(31 downto 0);
			argB : out STD_LOGIC_VECTOR(31 downto 0);
			memReadVal : out STD_LOGIC_VECTOR(31 downto 0);
			retAddr : out STD_LOGIC_VECTOR(31 downto 0);
			oldBP : out STD_LOGIC_VECTOR(31 downto 0)
		);
   end component;
	component NewBP
		port( currBP : in STD_LOGIC_VECTOR(31 downto 0);
			oldBP : in STD_LOGIC_VECTOR(31 downto 0);
			cachedBP : in STD_LOGIC_VECTOR(31 downto 0);
			newBPSource : in STD_LOGIC_VECTOR(1 downto 0);
			newBPOut : out STD_LOGIC_VECTOR(31 downto 0) );
   end component;
	component NewPC
		port ( pc : in STD_LOGIC_VECTOR (31 downto 0);
			retAddr: in STD_LOGIC_VECTOR (31 downto 0);
			argA, argB : in STD_LOGIC_VECTOR (31 downto 0);
			pcOffset : in STD_LOGIC;
			jmpType : in STD_LOGIC_VECTOR (1 downto 0);
			newPCOut : out STD_LOGIC_VECTOR (31 downto 0) );
   end component;
	component NewSP
		port( oldSP : in STD_LOGIC_VECTOR(31 downto 0);
			oldBP : in STD_LOGIC_VECTOR(31 downto 0);
			spOffset : in STD_LOGIC_VECTOR(2 downto 0);
			useBP : in STD_LOGIC;
			newSPOut : out STD_LOGIC_VECTOR(31 downto 0) );
   end component;
	component Regs
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
   end component;
	component SelectWAddr
		port(
		cbp : in STD_LOGIC_VECTOR(31 downto 0);
		bp : in STD_LOGIC_VECTOR(31 downto 0);
		sp : in STD_LOGIC_VECTOR(31 downto 0);
		selectPointer : in STD_LOGIC_VECTOR(2 downto 0);
		pointerOffsetPositive : in STD_LOGIC;
		
		argA : in STD_LOGIC_VECTOR(31 downto 0);
		argAorPointer : in STD_LOGIC;
		
		wAddr : out STD_LOGIC_VECTOR(31 downto 0)
	);
   end component;
	component SelectWData
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
   end component;
	
	--alu signals
	signal ArgA, ArgB : STD_LOGIC_VECTOR(31 downto 0);
	signal ALUOut : STD_LOGIC_VECTOR(31 downto 0);
	
	--mem signals
	signal pcPlusOne : STD_LOGIC_VECTOR(31 downto 0);
	signal pc : STD_LOGIC_VECTOR(31 downto 0);
	signal spMinusTwo : STD_LOGIC_VECTOR(31 downto 0);
	signal bpMinusTwo : STD_LOGIC_VECTOR(31 downto 0);
	signal memReadAddr : STD_LOGIC_VECTOR(31 downto 0);
	signal wAddr : STD_LOGIC_VECTOR(31 downto 0);
	signal wData : STD_LOGIC_VECTOR(31 downto 0);
	signal lit : STD_LOGIC_VECTOR(31 downto 0);
	signal memReadVal : STD_LOGIC_VECTOR(31 downto 0);
	signal retAddr : STD_LOGIC_VECTOR(31 downto 0);
	signal oldBP : STD_LOGIC_VECTOR(31 downto 0);
	
	--bp signals
	signal currBP : STD_LOGIC_VECTOR(31 downto 0);
	signal cachedBP : STD_LOGIC_VECTOR(31 downto 0);
	signal newBPOut : STD_LOGIC_VECTOR(31 downto 0);
	
	--pc signals
	signal newPCOut : STD_LOGIC_VECTOR(31 downto 0);
	
	--sp signals
	signal oldSP : STD_LOGIC_VECTOR(31 downto 0);
	signal newSPOut : STD_LOGIC_VECTOR(31 downto 0);
	
	--reg signals
	signal sp : STD_LOGIC_VECTOR(31 downto 0);
	signal bp : STD_LOGIC_VECTOR(31 downto 0);
begin
	ALUComp 		: ALU port map (
		a => ArgA, 
		b => ArgB, 
		f => f, 
		Y => ALUOut
	);
	MemComp 		: Memory port map (
		clk => clk, 
		pcPlusOne => pcPlusOne, 
		pc => pc, 
		spMinusTwo => spMinusTwo,
		bpMinusTwo => bpMinusTwo,
		memReadAddr => memReadAddr,
		wAddr => wAddr,
		wData => wData,
		wEn => memWen,
		lit => lit,
		instruction => instruction,
		ArgA => ArgA,
		ArgB => ArgB,
		memReadVal => memReadVal,
		retAddr => retAddr,
		oldBP => oldBP
	);
	BPComp 		: newBp port map (
		currBP => currBP,
		oldBP => oldBP,
		cachedBP => cachedBP,
		newBPSource => newBPSource,
		newBPOut => newBPOut
	);
	PCUComp 		: NewPC port map (
		pc => pc,
		retAddr => retAddr,
		ArgA => ArgA,
		ArgB => ArgB,
		pcOffset => pcOffset,
		jmpType => jmpType,
		newPCOut => newPCOut
	);
	SPComp 		: NewSp port map (
		oldSP => oldSP,
		oldBP => oldBP,
		spOffset => spOffset,
		useBP => useBP,
		newSPOut => newSPOut
	);
	RegComp 		: Regs port map (
		clk => clk,
		newPc => newPCOut,
		newSp => newSPOut,
		newBp => newBPOut,
		pcWen => pcWen,
		spWen => spWen,
		bpWen => bpWen,
		cachedBpWen => cachedBpWen,
		pc => pc,
		pcPlusOne => pcPlusOne,
		sp => sp,
		spMinusTwo => spMinusTwo,
		bp => bp,
		bpMinusTwo => bpMinusTwo,
		cachedBp => cachedBp
	);
	WAddrComp 	: SelectWAddr port map (
		cbp => cachedBp,
		bp => bp,
		sp => sp,
		selectPointer => selectPointer,
		pointerOffsetPositive => pointerOffsetPositive,
		argA => argA,
		argAorPointer => argAorPointer,
		wAddr => wAddr
	);
	WDataComp 	: SelectWData port map (
		lit => lit,
		pcPlusOne => pcPlusOne,
		aluRes => ALUOut,
		argB => argB,
		memRead => memReadVal,
		bp => bp,
		selectData => selectData,
		wData => wData
	);
end Behavioral;

