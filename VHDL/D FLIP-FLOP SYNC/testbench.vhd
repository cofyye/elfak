library IEEE;
use IEEE.std_logic_1164.all;

entity d_flipflop_sync_tb is
end entity d_flipflop_sync_tb;

architecture d_flipflop_sync_tb_arch of d_flipflop_sync_tb is
	signal clock1, D1, Q1, Qn1 : std_logic;
begin
	DUT1 : entity work.d_flipflop_sync(d_flipflop_sync_arch)
    	   port map(clock1, D1, Q1, Qn1);
    
    CLOCK : process
    begin
		clock1 <= '0'; wait for 10ns;
        clock1 <= '1'; wait for 10ns;
    end process;
    
    STIMULUS : process
    begin
       	D1 <= '0'; wait for 10ns;
        D1 <= '0'; wait for 10ns;
       	D1 <= '1'; wait for 10ns;
        D1 <= '1'; wait for 10ns;
        D1 <= 'X'; wait for 10ns;
       	D1 <= 'X'; wait for 10ns;
    end process;
end architecture d_flipflop_sync_tb_arch;