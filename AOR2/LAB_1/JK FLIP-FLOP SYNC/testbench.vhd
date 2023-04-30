library IEEE;
use IEEE.std_logic_1164.all;

entity jk_flipflop_sync_tb is
end entity jk_flipflop_sync_tb;

architecture jk_flipflop_sync_tb_arch of jk_flipflop_sync_tb is
	signal J, K, clock, Q, Qn : std_logic;
begin
	DUT1 : entity work.jk_flipflop_sync(jk_flipflop_sync_arch)
    	   port map(J, K, clock, Q, Qn);
    CLOCK_PROCESS : process
    begin
    	clock <= '0'; wait for 10ns;
        clock <= '1'; wait for 10ns;
    end process CLOCK_PROCESS;
    STIMULUS : process
    begin
    	J <= '0'; K <= '0'; wait for 10ns;
        J <= '1'; K <= '0'; wait for 10ns;
        J <= '0'; K <= '1'; wait for 10ns;
        J <= '1'; K <= '1'; wait for 10ns;
        J <= 'X'; K <= 'X'; wait for 10ns;
    end process STIMULUS;
end architecture jk_flipflop_sync_tb_arch;