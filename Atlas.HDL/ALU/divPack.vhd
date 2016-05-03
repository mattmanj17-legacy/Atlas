--
--	Package File Template
--
--	Purpose: This package defines supplemental types, subtypes, 
--		 constants, and functions 
--
--   To use any of the example code shown below, uncomment the lines and modify as necessary
--

library IEEE;
use IEEE.STD_LOGIC_1164.all;
use ieee.numeric_std.all;

package divPack is

-- type <new_type> is
--  record
--    <type_name>        : std_logic_vector( 7 downto 0);
--    <type_name>        : std_logic;
-- end record;
--
-- Declare constants
--
-- constant <constant_name>		: time := <time_unit> ns;
-- constant <constant_name>		: integer := <value;
--
-- Declare functions and procedure
--
-- function <function_name>  (signal <signal_name> : in <type_declaration>) return <type_declaration>;

function  divide  (a : STD_LOGIC_VECTOR; b : STD_LOGIC_VECTOR) return STD_LOGIC_VECTOR;

-- procedure <procedure_name> (<type_declaration> <constant_name>	: in <type_declaration>);
--

end divPack;

package body divPack is

---- Example 1
--  function <function_name>  (signal <signal_name> : in <type_declaration>  ) return <type_declaration> is
--    variable <variable_name>     : <type_declaration>;
--  begin
--    <variable_name> := <signal_name> xor <signal_name>;
--    return <variable_name>; 
--  end <function_name>;

function  divide  (a : STD_LOGIC_VECTOR; b : STD_LOGIC_VECTOR) return STD_LOGIC_VECTOR is
	variable a1 : STD_LOGIC_VECTOR(a'length-1 downto 0):=a;
	variable b1 : STD_LOGIC_VECTOR(b'length-1 downto 0):=b;
	variable p1 : STD_LOGIC_VECTOR(b'length-1 downto 0):= (others => '0');
	variable i : integer:=0;
begin
	for i in 0 to b'length-1 loop
		p1(b'length-1 downto 1) := p1(b'length-2 downto 0);
		p1(0) := a1(a'length-1);
		a1(a'length-1 downto 1) := a1(a'length-2 downto 0);
		p1 := std_logic_vector(unsigned(p1) - unsigned(b1));
		if(p1(b'length-1) ='1') then
			a1(0) :='0';
			p1 := std_logic_vector(unsigned(p1) + unsigned(b1));
		else
			a1(0) :='1';
		end if;
	end loop;
	return a1;
end divide;

---- Example 2
--  function <function_name>  (signal <signal_name> : in <type_declaration>;
--                         signal <signal_name>   : in <type_declaration>  ) return <type_declaration> is
--  begin
--    if (<signal_name> = '1') then
--      return <signal_name>;
--    else
--      return 'Z';
--    end if;
--  end <function_name>;

---- Procedure Example
--  procedure <procedure_name>  (<type_declaration> <constant_name>  : in <type_declaration>) is
--    
--  begin
--    
--  end <procedure_name>;
 
end divPack;
