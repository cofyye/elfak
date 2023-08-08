library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity mux_2x1_signed_stdv_tb is
end entity;

architecture mux_2x1_signed_stdv_arch_tb of mux_2x1_signed_stdv_tb is

signal A : signed(1 downto 0);
signal Sel : std_logic_vector(0 downto 0);
signal F : std_logic;

begin
	DUT1 : entity work.mux_2x1_signed_stdv(mux_2x1_signed_stdv_arch)
    	   port map(A, Sel, F);
   	
    STIMULUS : process
    begin
    	A <= "00";
        Sel <= "0";
        wait for 10ns;
        A <= "01";
        Sel <= "0";
        wait for 10ns;
        A <= "10";
        Sel <= "0";
        wait for 10ns;
        A <= "11";
        Sel <= "0";
        wait for 10ns;
        A <= "XX";
        Sel <= "0";
        wait for 10ns;
    	A <= "00";
        Sel <= "1";
        wait for 10ns;
        A <= "01";
        Sel <= "1";
        wait for 10ns;
        A <= "10";
        Sel <= "1";
        wait for 10ns;
        A <= "11";
        Sel <= "1";
        wait for 10ns;
        A <= "XX";
        Sel <= "1";
        wait for 10ns;

    	A <= "00";
        Sel <= "X";
        wait for 10ns;
        A <= "01";
        Sel <= "X";
        wait for 10ns;
        A <= "10";
        Sel <= "X";
        wait for 10ns;
        A <= "11";
        Sel <= "X";
        wait for 10ns;
  	end process STIMULUS;
end architecture;