library IEEE;
use IEEE.std_logic_1164.all;

entity d_flipflop_async_tb is
end entity d_flipflop_async_tb;

architecture d_flipflop_async_tb_arch of d_flipflop_async_tb is
	signal C1, D1, Q1, Qn1 : std_logic;
begin
	DUT1 : entity work.d_flipflop_async(d_flipflop_async_arch)
    	   port map(C1, D1, Q1, Qn1);
    
    STIMULUS : process
    begin
    	C1 <= '0'; D1 <= '0'; wait for 10ns;
        C1 <= '0'; D1 <= '1'; wait for 10ns;
        C1 <= '1'; D1 <= '0'; wait for 10ns;
        C1 <= '1'; D1 <= '1'; wait for 10ns;
        C1 <= 'X'; D1 <= 'X'; wait for 10ns;
    end process;
end architecture d_flipflop_async_tb_arch;