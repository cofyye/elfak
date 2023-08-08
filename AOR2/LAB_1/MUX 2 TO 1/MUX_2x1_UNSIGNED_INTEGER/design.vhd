library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity mux_2x1_unsigned_int is
	port(	A : in unsigned(1 downto 0);
    	  Sel : in integer range 0 to 1;
         	F : out std_logic);
end entity;

architecture mux_2x1_unsigned_int_arch of mux_2x1_unsigned_int is
begin
	process(A, Sel)
    begin
    	if(A = "00" and Sel = 0) then
        	F <= '0';
        elsif(A = "01" and Sel = 0) then 
        	F <= '0';
        elsif(A = "10" and Sel = 0) then
        	F <= '1';
       	elsif(A = "11" and Sel = 0) then
        	F <= '1';
    	elsif(A = "00" and Sel = 1) then
        	F <= '0';
        elsif(A = "01" and Sel = 1) then 
        	F <= '1';
        elsif(A = "10" and Sel = 1) then
        	F <= '0';
       	elsif(A = "11" and Sel = 1) then
        	F <= '1';
       	else 
        	F <= 'Z';
      	end if;
   	end process;
end architecture;