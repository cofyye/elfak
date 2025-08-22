library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity mux_4x1_bitv_int_tb is
end entity;

architecture mux_4x1_bitv_int_arch_tb of mux_4x1_bitv_int_tb is

signal A : bit_vector(3 downto 0);
signal Sel : integer range 0 to 5;
signal F : bit;

begin
	DUT1 : entity work.mux_4x1_bitv_int(mux_4x1_bitv_int_arch)
    	   port map(A, Sel, F);
   	
    STIMULUS : process
    begin
    	A <= "1010";
        Sel <= 0;
        wait for 10ns;
        A <= "1010";
        Sel <= 1;
        wait for 10ns;
        A <= "1010";
        Sel <= 2;
        wait for 10ns;
        A <= "1010";
        Sel <= 3;
        wait for 10ns;
    	A <= "0101";
        Sel <= 0;
        wait for 10ns;
        A <= "0101";
        Sel <= 1;
        wait for 10ns;
        A <= "0101";
        Sel <= 2;
        wait for 10ns;
        A <= "0101";
        Sel <= 3;
        wait for 10ns;

    	A <= "1010";
        Sel <= 4;
        wait for 10ns;
  	end process STIMULUS;
end architecture;