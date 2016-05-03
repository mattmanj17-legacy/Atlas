library ieee;
use ieee.std_logic_1164.all;
USE ieee.numeric_std.ALL;
entity barrelshifter is
    generic ( N : integer := 8 );
	 port (
        inword   : in std_logic_vector(N-1 downto 0);
        shift    : in std_logic_vector(N-1 downto 0);
		  left	  : in std_logic;
		  rotate : in std_logic;
		  arith  : in std_logic;
        outword  : out std_logic_vector(N-1 downto 0)
		  );
end entity barrelshifter;

architecture barrel of barrelshifter is
begin
   -- Each bit in the output vector has its own mux.
   -- Use the input word bit position as an "offset," to which the new
   -- position is subtracted, modulo the word size, to give the mux select.
   EachBit: for bitpos in inword'range generate
		process(shift, inword, left, rotate, arith)
		begin
			if(left = '1') then
				if bitpos >= to_integer(unsigned(shift)) or rotate = '1' then
					outword(bitpos) <= inword((bitpos - to_integer(unsigned(shift))) mod (N));
				else
					outword(bitpos) <= '0';
				end if;
			else
				if bitpos <= (N - to_integer(unsigned(shift)) - 1) or rotate = '1' then
					outword(bitpos) <= inword((bitpos + to_integer(unsigned(shift))) mod (N));
				else
					outword(bitpos) <= arith and inword(n-1);
				end if;
			end if;
		end process;
   end generate EachBit;
end architecture barrel;