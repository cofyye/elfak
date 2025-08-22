library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity rs_flipflop_async_tb is
end entity;

architecture rs_flipflop_async_arch_tb of rs_flipflop_async_tb is

signal clock : std_logic := '0';
signal R, S, Q, Qn : std_logic;

begin
	
    DUT1 : entity work.rs_flipflop_async(rs_flipflop_async_arch)
    		port map(R, S, clock, Q, Qn);
    
    clock <= not clock after 5ns;
    
   	STIMULUS : process
    begin
    
        S<='1';
        R<='0';
        wait for 10ns;
        S<='1';
        R<='1';
        wait for 20ns;
        S<='0';
        R<='1';
        wait for 30ns;
        S<='0';
        R<='0';
        wait for 40ns;
        S<='1';
        R<='0';
        wait for 50ns;
       
  	end process;
end architecture;