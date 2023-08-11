library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity bcd_counter_tb is
end entity;

architecture bcd_counter_arch_tb of bcd_counter_tb is
signal clk : std_logic := '0';
signal rst, we : std_logic;
signal d_in, q : integer range 0 to 9;
begin
	DUT1 : entity work.bcd_counter(bcd_counter_arch)
    		port map(clk, rst, we, d_in, q);
   	
    clk <= not clk after 5ns;
    
    STIMULUS : process
    begin
    	rst <= '1'; we <= '0'; wait for 10ns;
        rst <= '0'; we <= '0'; wait for 30ns;
        rst <= '0'; we <= '1'; d_in <= 6; wait for 10ns;
        rst <= '0'; we <= '0'; wait for 60ns;
  	end process;
end architecture;