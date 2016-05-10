----------------------------------------------------------------------------------
-- Company: 
-- Engineer: Matt
-- 
-- Create Date:    09:46:12 05/03/2016 
-- Design Name: 
-- Module Name:    ALU - Behavioral 
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

entity ALU is
	port ( a, b : in STD_LOGIC_VECTOR( 31 downto 0 );
			f : in STD_LOGIC_VECTOR( 3 downto 0 );
			Y : out STD_LOGIC_VECTOR( 31 downto 0 ) );
end ALU;

architecture Behavioral of ALU is
	signal ALU00, ALU01, ALU10, ALU11 : STD_LOGIC_VECTOR( 31 downto 0 );
	
	--arith signals ALU00
	signal notBandCarry, zeroA, select00 : STD_LOGIC;
	signal a00, b00 : STD_LOGIC_VECTOR(31 downto 0);
	signal a00Final : STD_LOGIC_VECTOR(31 downto 0);
	
	--ALU01 signals
	signal MULT : STD_LOGIC_VECTOR((32*2)-1 downto 0);
	signal EQU : STD_LOGIC_VECTOR(31 downto 0);
	
	--ALU10 signals
	component Shifter
    port (
        inword   : in std_logic_vector(31 downto 0);
        shift    : in std_logic_vector(4 downto 0);
		  right	  : in std_logic;
        outword  : out std_logic_vector(31 downto 0)
		  );
   end component;
	
	signal right : STD_LOGIC;
begin
	--select sub operation based on last two bits of control signal
		
	--ALU00 (Addition, Subtraction, Less Than, Invert Sign)	
	
	--should b b inverted, and what is the value of the carry
	notBandCarry <= NOT f(1) OR f(0);
	
	b00 <= 
		b when notBandCarry = '0' else
		not b;
		
	--should a be zero
	zeroA <= NOT f(1) AND NOT f(0);
	
	a00 <= 
		a when zeroA = '0' else
		STD_LOGIC_VECTOR(to_unsigned(0, 32));
		
	--perform the arithmatic
	a00Final <= a00 + b00 + notBandCarry;
	
	-- are we returning all of a00final, or just the last bit zero extended
	select00 <= f(1) AND f(0);
	
	ALU00 <= 
		a00Final when select00 = '0' else
		EXT( "0" & a00Final(31), 32);
		
	--ALU01 (Unsigned Multiplication, Equality Check, UNUSED, UNUSED)
	MULT <= a * b;
	
	EQU <= 
		STD_LOGIC_VECTOR(to_unsigned(1, 32)) when a = b else
		STD_LOGIC_VECTOR(to_unsigned(0, 32));
	
	ALU01 <= 
		MULT(31 downto 0) when f(1 downto 0) = "00" else
		EQU when f(1 downto 0) = "01" else
		STD_LOGIC_VECTOR(to_unsigned(0, 32)); -- unused
		
	--ALU10 (Shift Left logical, Shift Right logical, UNUSED, UNUSED)
	
	right <= 
		'0' when f(1 downto 0) = "00" else
		'1';
	
	SHIFTComponent : Shifter
			port map (inword => a, shift => b(4 downto 0), right => right, outword => ALU10);
	
	--ALU11 (Xor, Or, And, Invert logical)
	ALU11 <= 
		a XOR b when f(1 downto 0) = "00" else
		a OR b when f(1 downto 0) = "01" else
		a AND b when f(1 downto 0) = "10" else
		NOT b;
		
	--select output based on first two bits of control signal
	y <= 
		ALU00 when f(3 downto 2) = "00" else
		ALU01 when f(3 downto 2) = "01" else
		ALU10 when f(3 downto 2) = "10" else
		ALU11;
		
end Behavioral;

