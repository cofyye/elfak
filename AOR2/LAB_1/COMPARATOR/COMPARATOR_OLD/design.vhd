library IEEE;
use IEEE.std_logic_1164.all;

entity comparator is
	port(A, B : in std_logic_vector(3 downto 0);
         F : out std_logic_vector(3 downto 0));
end entity comparator;

architecture comparator_arch of comparator is
begin
	process(A, B)
    begin
    	if(A > B) then
        	F <= A;
        elsif(A < B) then
        	F <= B;
        else 
        	F <= A;
        end if;
	end process;
end architecture comparator_arch;
    
    	