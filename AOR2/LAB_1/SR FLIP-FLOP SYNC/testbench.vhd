-- testbench.vhd

library IEEE;
use IEEE.std_logic_1164.all;

entity sr_sync_tb is
end entity;

architecture sr_sync_tb_arch of sr_sync_tb is
	signal S, R, clock, Q, Qn : std_logic;
begin
	DUT1 : entity work.sr_sync(sr_sync_arch)
    	   port map(S, R, clock, Q, Qn);
    
	CLOCK_PROCESS : process
    begin
    	clock <= '0'; wait for 10ns;
        clock <= '1'; wait for 10ns;
   	end process CLOCK_PROCESS;
    
    STIMULUS : process
    begin
    	S <= 'X'; R <= 'X'; wait for 10ns;
        S <= 'X'; R <= 'X'; wait for 10ns;
        S <= '0'; R <= '0'; wait for 10ns;
        S <= '0'; R <= '0'; wait for 10ns;
        S <= '0'; R <= '1'; wait for 10ns;
        S <= '0'; R <= '1'; wait for 10ns;
        S <= '1'; R <= '0'; wait for 10ns;
        S <= '1'; R <= '0'; wait for 10ns;
        S <= '1'; R <= '1'; wait for 10ns;
        S <= '1'; R <= '1'; wait for 10ns;
   	end process;
end architecture sr_sync_tb_arch;