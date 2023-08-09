library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity bcd_counter_stdv_tb is
generic( numbits_tb : integer := 2);
end entity;

architecture bcd_counter_stdv_arch_tb of bcd_counter_stdv_tb is
signal clock : bit := '0';
signal reset, ce, dir, wr : bit;
signal din, q : std_logic_vector(numbits_tb-1 downto 0);
begin
	DUT1 : entity work.bcd_counter_stdv(bcd_counter_stdv_arch)
    		generic map(numbits_tb)
            port map(clock, reset, ce, dir, wr, din, q);
     
  	clock <= not clock after 5ns;
    
    STIMULUS : process
    begin
    	reset <= '1'; dir <= '1'; ce <= '1'; wr <= '0'; wait for 10ns;
        reset <= '0'; wait for 40ns;
        reset <= '0'; wr <= '1'; din <= "11"; wait for 10ns;
        reset <= '0'; wr <= '0'; wait for 30ns;
        reset <= '0'; wr <= '0'; dir <= '0'; wait for 40ns;
  	end process STIMULUS;
end architecture;