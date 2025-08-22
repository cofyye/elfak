library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity bcdCounterDoubleDigit_tb is
end entity bcdCounterDoubleDigit_tb;

architecture bcdCounterDoubleDigit_tb_arch of bcdCounterDoubleDigit_tb is
	signal clock : std_logic := '0';
    signal reset, ce, wr : std_logic;
    signal din_1, din_2 : std_logic_vector(3 downto 0);
    signal value : std_logic_vector(7 downto 0);
begin
	DUT1 : entity work.bcdCounterDoubleDigit(bcdCounterDoubleDigit_arch)
    	   port map(clock, reset, ce, wr, din_1, din_2, value);
    clock <= not clock after 5ns;
    STIMULUS : process
    begin
    	reset <= '1'; wait for 10ns;
        reset <= '0'; wr <= '1'; 
        din_1 <= "0011"; din_2 <= "0111"; wait for 10ns;
        reset <= '0'; wr <= '0'; ce <= '1'; wait for 1000ns;
        reset <= '0'; wt <= '0'; ce <= '0'; wait for 1000ns;
  	end process STIMULUS;
end architecture bcdCounterDoubleDigit_tb_arch;