library IEEE;
use IEEE.std_logic_1164.all;

entity mux4to1 is
	port(A : in bit_vector(3 downto 0);
         Sel : in std_logic_vector(1 downto 0);
    	 F : out bit);
end entity mux4to1;

architecture mux4to1_arch of mux4to1 is
begin
	process(A, Sel)
    begin
    	if(Sel = "00") then
        	F <= A(0);
        elsif(Sel = "01") then
        	F <= A(1);
        elsif(Sel = "10") then
        	F <= A(2);
        elsif(Sel = "11") then
        	F <= A(3);
        else
        	F <= '0';
        end if;
	end process;
end architecture mux4to1_arch;