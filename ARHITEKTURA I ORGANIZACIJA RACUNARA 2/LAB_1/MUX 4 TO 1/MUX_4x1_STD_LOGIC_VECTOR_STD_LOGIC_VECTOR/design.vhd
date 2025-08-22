library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity mux_4x1_stdv_stdv is
	port(	A : in std_logic_vector(3 downto 0);
    	  Sel : in std_logic_vector(1 downto 0);
         	F : out std_logic);
end entity;

architecture mux_4x1_stdv_stdv_arch of mux_4x1_stdv_stdv is
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
        	F <= 'Z';
      	end if;
   	end process;
end architecture;