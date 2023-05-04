library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity bcdCounterTripleDigit_tb is
end entity bcdCounterTripleDigit_tb;

architecture bcdCounterTripleDigit_tb_arch of bcdCounterTripleDigit_tb is
	signal clock : std_logic := '0';
    signal reset, ce, wr : std_logic;
    signal din_1, din_2, din_3 : std_logic_vector(3 downto 0);
    signal value : std_logic_vector(11 downto 0);
begin
	DUT1 : entity work.bcdCounterTripleDigit(bcdCounterTripleDigit_arch)
    	   port map(clock, reset, ce, wr, din_1, din_2, din_3, value);
    clock <= not clock after 5ns;
    STIMULUS : process
    begin
    	reset <= '1'; wait for 10ns;
        reset <= '0'; wr <= '1';
        din_1 <= "0000"; din_2 <= "0001"; din_3 <= "0010"; wait for 10ns;
        reset <= '0'; wr <= '0'; ce <= '1'; wait for 10000ns;
        reset <= '0'; wr <= '0'; ce <= '0'; wait for 10000ns;
  	end process STIMULUS;
end architecture bcdCounterTripleDigit_tb_arch;