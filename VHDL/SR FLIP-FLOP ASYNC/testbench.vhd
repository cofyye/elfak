library IEEE;
use IEEE.std_logic_1164.all;
 
entity sr_tb is
end entity;
 
architecture sr_tb_arch of sr_tb is
    signal S, R, Q, Qn : std_logic;
    
    begin
        DUT1 : entity work.sr(sr_arch)
               port map(S, R, Q, Qn);
        
        STIMULUS : process
        begin 
            S <= '0'; R <= '0';
            wait for 10ns;
            S <= '0'; R <= '1';
            wait for 10ns;
            S <= '1'; R <= '0';
            wait for 10ns;
            S <= '1'; R <= '1';
            wait for 10ns;
            S <= 'X'; R <= 'X';
            wait for 10ns;
        end process;
end architecture sr_tb_arch;