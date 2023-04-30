library IEEE;
use IEEE.std_logic_1164.all;

entity mux2to1_tb is
end entity mux2to1_tb;

architecture mux2to1_tb_arch of mux2to1_tb is
	signal A : bit_vector(1 downto 0);
    signal Sel : std_logic_vector(1 downto 0);
    signal F : bit;
begin
	DUT1 : entity work.mux2to1(mux2to1_arch)
    	   port map(A, Sel, F);
    STIMULUS : process
    begin
    	A <= "01"; wait for 10ns;
        Sel <= "00"; wait for 10ns;
        Sel <= "01"; wait for 10ns;
        Sel <= "10"; wait for 10ns;
        Sel <= "11"; wait for 10ns;
        Sel <= "XX"; wait for 10ns;
    end process STIMULUS;
end architecture mux2to1_tb_arch;