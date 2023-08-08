library IEEE;
use IEEE.std_logic_1164.all;

entity mux2to1 is
	port(A : in bit_vector(1 downto 0);
         Sel : in std_logic_vector(1 downto 0);
    	 F : out bit);
end entity;

architecture mux2to1_arch of mux2to1 is
begin
	process(A, Sel)
    begin
    	if(Sel = "01") then
        	F <= A(0);
        elsif(Sel = "10") then
        	F <= A(1);
        else
        	F <= '0';
        end if;
	end process;
end architecture mux2to1_arch;