library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity comparator is
	port(
    		A, B : in std_logic_vector(3 downto 0);
         	F : out integer range 0 to 2);
end entity;

architecture comparator_arch of comparator is
begin
	process(A, B)
    begin
    	if(A > B) then
        	F <= 1;
       	elsif(A = B) then
        	F <= 0;
       	else
        	F <= 2;
       	end if;
  	end process;
end architecture;