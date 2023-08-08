library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity mux_4x1_bitv_stdv is
	port(	A : in bit_vector(3 downto 0);
    	  Sel : in std_logic_vector(1 downto 0);
         	F : out bit);
end entity;

architecture mux_4x1_bitv_stdv_arch of mux_4x1_bitv_stdv is
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
end architecture;