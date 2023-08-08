library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity mux_4x1_signed_int is
	port(	A : in signed(3 downto 0);
    	  Sel : in integer range 0 to 5;
         	F : out std_logic);
end entity;

architecture mux_4x1_signed_int_arch of mux_4x1_signed_int is
begin
	process(A, Sel)
    begin
    	if(Sel = 0) then
        	F <= A(0);
        elsif(Sel = 1) then 
        	F <= A(1);
        elsif(Sel = 2) then
        	F <= A(2);
       	elsif(Sel = 3) then
        	F <= A(3);
       	else 
        	F <= 'Z';
      	end if;
   	end process;
end architecture;