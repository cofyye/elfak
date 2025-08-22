library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity BcdCounterTripleDigit_tb is
end entity BcdCounterTripleDigit_tb;

architecture BcdCounterTripleDigit_tb_arch of BcdCounterTripleDigit_tb is
	signal clock : std_logic := '0';
    signal reset, ce, wr, dir : std_logic;
    signal value, din : integer range 0 to 99;
begin
	DUT1 : entity work.BcdCounterTripleDigit(BcdCounterTripleDigit_arch)
    	   port map(clock, reset, ce, wr, dir, din, value);
   	clock <= not clock after 5ns;
    STIMULUS : process
    begin
    	reset <= '1'; wait for 10ns;
        reset <= '0'; wr <= '1'; din <= 9; wait for 20ns;
        reset <= '0'; wr <= '0'; ce <= '1'; dir <= '1'; wait for 990ns;
        reset <= '0'; wr <= '0'; ce <= '0'; dir <= '0'; wait for 990ns;
  	end process STIMULUS;
end architecture BcdCounterTripleDigit_tb_arch;