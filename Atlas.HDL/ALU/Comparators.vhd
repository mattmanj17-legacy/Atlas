library IEEE; use IEEE.STD_LOGIC_1164.ALL;
entity comparators is
	 generic ( N: integer := 8 );
	 Port ( 	a, b : in STD_LOGIC_VECTOR (N-1 downto 0);
				eq, neq, lt, lte, gt, gte : out STD_LOGIC );
end comparators;
architecture Behavioral of comparators is
begin
	 eq <= '1' when ( a = b ) else '0';
	 neq <= '1' when ( a /= b ) else '0';
	 lt <= '1' when ( a < b ) else '0';
	 lte <= '1' when ( a <= b ) else '0';
	 gt <= '1' when ( a > b ) else '0';
	 gte <= '1' when ( a >= b ) else '0';
end Behavioral;

