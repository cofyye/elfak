library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity mux_2x1_unsigned_int_tb is
end entity;

architecture mux_2x1_unsigned_int_arch_tb of mux_2x1_unsigned_int_tb is

signal A : unsigned(1 downto 0);
signal Sel : integer range 0 to 1;
signal F : std_logic;

begin
	DUT1 : entity work.mux_2x1_unsigned_int(mux_2x1_unsigned_int_arch)
    	   port map(A, Sel, F);
   	
    STIMULUS : process
    begin
    	A <= "00";
        Sel <= 0;
        wait for 10ns;
        A <= "01";
        Sel <= 0;
        wait for 10ns;
        A <= "10";
        Sel <= 0;
        wait for 10ns;
        A <= "11";
        Sel <= 0;
        wait for 10ns;
        A <= "XX";
        Sel <= 0;
        wait for 10ns;
    	A <= "00";
        Sel <= 1;
        wait for 10ns;
        A <= "01";
        Sel <= 1;
        wait for 10ns;
        A <= "10";
        Sel <= 1;
        wait for 10ns;
        A <= "11";
        Sel <= 1;
        wait for 10ns;
        A <= "XX";
        Sel <= 1;
        wait for 10ns;
  	end process STIMULUS;
end architecture;