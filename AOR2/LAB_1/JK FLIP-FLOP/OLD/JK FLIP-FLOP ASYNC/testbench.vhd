library IEEE;
use IEEE.std_logic_1164.all;

entity jk_flipflop_async_tb is
end entity jk_flipflop_async_tb;

architecture jk_flipflop_async_tb_arch of jk_flipflop_async_tb is
	signal J, K, Q, Qn : std_logic;
begin
	DUT1 : entity work.jk_flipflop_async(jk_flipflop_async_arch)
    	   port map(J, K, Q, Qn);
    STIMULUS : process
    begin
    	J <= '0'; K <= '0'; wait for 10ns;
        J <= '1'; K <= '0'; wait for 10ns;
        J <= '0'; K <= '1'; wait for 10ns;
        J <= '1'; K <= '1'; wait for 10ns;
        J <= 'X'; K <= 'X'; wait for 10ns;
    end process STIMULUS;
end architecture jk_flipflop_async_tb_arch;