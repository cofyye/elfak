library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity bcdCounterDoubleDigit_tb is
end entity bcdCounterDoubleDigit_tb;

architecture bcdCounterDoubleDigit_tb_arch of bcdCounterDoubleDigit_tb is
signal clock : std_logic := '0'; 
signal reset, ce : std_logic;
signal value : std_logic_vector(7 downto 0);
begin
	DUT1 : entity work.bcdCounterDoubleDigit(bcdCounterDoubleDigit_arch)
    port map(clock, reset, ce, value);
    clock <= not clock after 5ns;
    STIMULUS : process
    begin
    	reset <= '1';
        wait for 10 ns;
        reset <= '0';
        ce <= '1';
        wait for 1000 ns;
        ce <= '0';
        wait for 300 ns;
        ce <= '1';
        wait for 500 ns;
  	end process STIMULUS;
end architecture bcdCounterDoubleDigit_tb_arch;