library IEEE;
use IEEE.std_logic_1164.all;

entity bcd_counter_int_tb is
generic( num_tb : integer := 9);
end entity;

architecture bcd_counter_int_arch_tb of bcd_counter_int_tb is
signal clock : bit := '0';
signal reset, ce, dir, wr : bit;
signal din, q : integer range 0 to num_tb;
begin
	DUT1 : entity work.bcd_counter_int(bcd_counter_int_arch)
    		generic map(num_tb)
            port map(clock, reset, ce, dir, wr, din, q);
     
  	clock <= not clock after 5ns;
    
    STIMULUS : process
    begin
    	reset <= '1'; dir <= '1'; ce <= '1'; wr <= '0'; wait for 10ns;
        reset <= '0'; wait for 40ns;
        reset <= '0'; wr <= '1'; din <= 6; wait for 10ns;
        reset <= '0'; wr <= '0'; wait for 90ns;
        reset <= '0'; wr <= '0'; dir <= '0'; wait for 90ns;
  	end process STIMULUS;
end architecture;