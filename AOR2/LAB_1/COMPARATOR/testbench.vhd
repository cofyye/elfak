library IEEE;
use IEEE.std_logic_1164.all;

entity comparator_tb is
end entity comparator_tb;

architecture comparator_tb_arch of comparator_tb is
	signal A, B, F : std_logic_vector(3 downto 0);
begin
	DUT1 : entity work.comparator(comparator_arch)
    	   port map(A, B, F);
    STIMULUS : process
    begin
    	A <= "0001"; B <= "0100"; wait for 10ns;
        A <= "0101"; B <= "0101"; wait for 10ns;
        A <= "0101"; B <= "0100"; wait for 10ns;
        A <= "1000"; B <= "1100"; wait for 10ns;
        A <= "0000"; B <= "0110"; wait for 10ns;
        A <= "1001"; B <= "0101"; wait for 10ns;
        A <= "0101"; B <= "0111"; wait for 10ns;
    end process STIMULUS;
end architecture comparator_tb_arch;