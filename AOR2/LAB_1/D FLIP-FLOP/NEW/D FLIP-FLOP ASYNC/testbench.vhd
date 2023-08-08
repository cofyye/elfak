library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity d_flipflop_async_tb is
end entity;

architecture d_flipflop_async_arch_tb of d_flipflop_async_tb is

signal clock : std_logic := '0';
signal D, reset, Q, Qn : std_logic;

begin
	
    DUT1 : entity work.d_flipflop_async(d_flipflop_async_arch)
    		port map(D, clock, reset, Q, Qn);

    clock <= not clock after 5ns;
    
   	STIMULUS : process
    begin
    
   		D <= '1';
        reset <= '1';
        wait for 10ns;
        
        D <= '1';
        reset <= '0';
        wait for 20ns;
        
        D <= '0';
        reset <= '0';
        wait for 30ns;
        
        D <= '0';
        reset <= '1';
        wait for 40ns;
       
  	end process;
end architecture;