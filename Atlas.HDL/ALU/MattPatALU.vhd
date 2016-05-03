----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    10:36:07 02/11/2016 
-- Design Name: 
-- Module Name:    MattPatALU - Behavioral 
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
use work.divPack.all;
entity alu is
	generic ( N : integer := 32 );
	port ( a, b : in STD_LOGIC_VECTOR( N-1 downto 0 );
			f : in STD_LOGIC_VECTOR( 3 downto 0 );
			Y : out STD_LOGIC_VECTOR( N-1 downto 0 ) );
end alu;
architecture Behavioral of alu is
	signal ALU00, ALU01, ALU10, ALU11 : STD_LOGIC_VECTOR( N-1 downto 0 );
	
	--arith signals
	signal notBandCarry, zeroA, select00 : STD_LOGIC;
	signal a00, b00 : STD_LOGIC_VECTOR(N-1 downto 0);
	signal a00Final : STD_LOGIC_VECTOR(N-1 downto 0);
	
	--mult signal
	signal MULT : STD_LOGIC_VECTOR((N*2)-1 downto 0);
	signal MODU : STD_LOGIC_VECTOR((N*2)-1 downto 0);
	
	--shifting signals
	signal left : std_logic;
	signal rotate : std_logic;
	signal arith : std_logic;
	
	component barrelshifter
    generic ( N : integer := 8 );
	 port (
        inword   : in std_logic_vector(N-1 downto 0);
        shift    : in std_logic_vector(N-1 downto 0);
		  left	  : in std_logic;
		  rotate : in std_logic;
		  arith  : in std_logic;
        outword  : out std_logic_vector(N-1 downto 0)
		  );
   end component;
begin
	SHIFTER : barrelshifter
			generic map( N => N )
			port map (inword => a, shift => b, left => left, rotate => rotate, arith => arith, outword => ALU10);
	
	process(a,b,f,notBandCarry,zeroA,select00,a00,b00,a00Final,left,rotate,arith,ALU00,ALU01,ALU10,ALU11,MULT)
	begin
		--select sub operation based on last two bits of control signal
		
		--ALU00 (Addition, Subtraction, Less Than, Invert Sign)
		
		--neg 00
		--sub 01
		--add 10
		--lt 11
		
		--should b b inverted, and what is the value of the carry
		notBandCarry <= NOT f(1) OR f(0);
		case notBandCarry is
			when '0' => b00 <= b;
			when '1' => b00 <= NOT b;
			when others => b00 <= STD_LOGIC_VECTOR(to_unsigned(0,N));
		end case;
		
		--should a be zero
		zeroA <= NOT f(1) AND NOT f(0);
		case zeroA is
			when '0' => a00 <= a;
			when '1' => a00 <= STD_LOGIC_VECTOR(to_unsigned(0, N));
			when others => a00 <= STD_LOGIC_VECTOR(to_unsigned(0,N));
		end case;
		
		--perform the arithmatic
		a00Final <= a00 + b00 + notBandCarry;
		
		-- are we returning all of a00final, or just the last bit zero extended
		select00 <= f(1) AND f(0);
		case select00 is
			when '0' => ALU00 <= a00Final;
			when '1' => ALU00 <= EXT( "0" & a00Final(N-1), N);
			when others => ALU00 <= STD_LOGIC_VECTOR(to_unsigned(0,N));
		end case;
		
		--ALU01 (Multiplication, division, Modulus, Return Zero[this could be replaced by somthing else later])
		--NOTE: all values are treeted as unsigned
		
		MULT <= a * b;
		MODU <= a - divide(a,b) * b;
		
		case f(1 downto 0) is
			--mul
			when "00" => ALU01 <= MULT(N-1 downto 0);
			--div
			when "01" => ALU01 <= divide(a,b);
			--mod
			when "10" => ALU01 <= MODU(N-1 downto 0);
			--zero
			when "11" => ALU01 <= STD_LOGIC_VECTOR(to_unsigned(0,N));
			when others => ALU01 <= STD_LOGIC_VECTOR(to_unsigned(0,N));
		end case;
		
		--ALU10 (Shift Left, Shift Right, Arithmetic Shift Right, Rotate Right)
		case f(1 downto 0) is
			--sll
			when "00" => left <= '1'; rotate <= '0'; arith <= '0';
			--srl
			when "01" => left <= '0'; rotate <= '0'; arith <= '0';
			--sra
			when "10" => left <= '0'; rotate <= '0'; arith <= '1';
			--ror
			when "11" => left <= '0'; rotate <= '1'; arith <= '0';
			when others => left <= '0'; rotate <= '0'; arith <= '0';
		end case;
		
		--ALU11 (Invert, And, Xor, Or)
		case f(1 downto 0) is
			--xor
			when "00" => ALU11 <= a XOR b;
			--or
			when "01" => ALU11 <= a OR b;
			--and
			when "10" => ALU11 <= a AND b;
			--not
			when "11" => ALU11 <= NOT a;
			when others => ALU11 <= STD_LOGIC_VECTOR(to_unsigned(0,N));
		end case;
	
		--select output based on first two bits of control signal
		case f(3 downto 2) is
			when "00" => y <= ALU00;
			when "01" => y <= ALU01;
			when "10" => y <= ALU10;
			when "11" => y <= ALU11;
			when others => y <= STD_LOGIC_VECTOR(to_unsigned(0,N));
		end case;
	end process;
end Behavioral;


