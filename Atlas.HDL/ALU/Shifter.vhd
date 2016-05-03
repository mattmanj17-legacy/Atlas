library ieee;
use ieee.std_logic_1164.all;
entity barrelshifter is
    generic ( N : integer := 32 );
	 port (
        inword   : in std_logic_vector(N-1 downto 0);
        shift    : in natural;
        outword  : out std_logic_vector(N-1 downto 0));
end entity barrelshifter;

architecture barrel of barrelshifter is
begin
   -- Each bit in the output vector has its own mux.
   -- Use the input word bit position as an "offset," to which the new
   -- position is subtracted, modulo the word size, to give the mux select.
       EachBit: for bitpos in inword'range generate
      outword(bitpos) <= inword((bitpos - shift) mod (inword'high + 1));
   end generate EachBit;
end architecture barrel;