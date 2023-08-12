library IEEE;
use IEEE.std_logic_1164.all;

entity bcd_counter_tb is
	generic(n : integer := 9);
end entity;

architecture bcd_counter_arch_tb of bcd_counter_tb is
signal clk : std_logic := '0';
signal rst, smer, ce, wr : std_logic;
signal din, q : integer range 0 to n;
begin
	DUT1 : entity work.bcd_counter(bcd_counter_arch)
    		generic map(n)
            port map(clk, rst, smer, ce, wr, din, q);
   
   	clk <= not clk after 5ns;
   
   	STIMULUS : process
    begin
    	rst <= '1'; wait for 10ns;
        rst <= '0'; wr <= '1'; din <= 4; wait for 10ns;
        rst <= '0'; wr <= '0'; din <= 7; ce <= '1'; smer <= '1'; wait for 120ns;
        rst <= '0'; wr <= '0'; smer <= '0'; wait for 90ns;
  	end process;
end architecture;