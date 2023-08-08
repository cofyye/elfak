library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity mux_4x1_unsigned_stdv_tb is
end entity;

architecture mux_4x1_unsigned_stdv_arch_tb of mux_4x1_unsigned_stdv_tb is

signal A : unsigned(3 downto 0);
signal Sel : std_logic_vector(1 downto 0);
signal F : std_logic;

begin
	DUT1 : entity work.mux_4x1_unsigned_stdv(mux_4x1_unsigned_stdv_arch)
    	   port map(A, Sel, F);
   	
    STIMULUS : process
    begin
    	A <= "1010";
        Sel <= "00";
        wait for 10ns;
        A <= "1010";
        Sel <= "01";
        wait for 10ns;
        A <= "1010";
        Sel <= "10";
        wait for 10ns;
        A <= "1010";
        Sel <= "11";
        wait for 10ns;
    	A <= "0101";
        Sel <= "00";
        wait for 10ns;
        A <= "0101";
        Sel <= "01";
        wait for 10ns;
        A <= "0101";
        Sel <= "10";
        wait for 10ns;
        A <= "0101";
        Sel <= "11";
        wait for 10ns;

    	A <= "1010";
        Sel <= "XX";
        wait for 10ns;
  	end process STIMULUS;
end architecture;