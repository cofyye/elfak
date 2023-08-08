library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity jk_flipflop_async_tb is
end entity;

architecture jk_flipflop_async_arch_tb of jk_flipflop_async_tb is

signal clock : std_logic := '0';
signal J, K, reset, Q, Qn : std_logic;

begin
	
    DUT1 : entity work.jk_flipflop_async(jk_flipflop_async_arch)
    		port map(J, K, clock, reset, Q, Qn);

    clock <= not clock after 5ns;
    
   	STIMULUS : process
    begin
    
   		J <= '0';
        K <= '0';
        reset <= '1';
        wait for 10ns;
        
        J <= '0';
        K <= '0';
        reset <= '0';
        wait for 20ns;
        
        J <= '0';
        K <= '1';
        reset <= '1';
        wait for 30ns;
        
        J <= '0';
        K <= '1';
        reset <= '0';
        wait for 40ns;
        
        J <= '1';
        K <= '0';
        reset <= '1';
       	wait for 50ns;
        
        J <= '1';
        K <= '0';
        reset <= '0';
        wait for 60ns;
        
        J <= '1';
        K <= '1';
        reset <= '0';
        wait for 70ns;
       
  	end process;
end architecture;