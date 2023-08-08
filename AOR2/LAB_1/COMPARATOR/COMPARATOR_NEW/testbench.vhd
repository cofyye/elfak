library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity comparator_tb is
end entity;

architecture comparator_arch_tb of comparator_tb is

signal A, B : std_logic_vector(3 downto 0);
signal F : integer range 0 to 2;

begin
	
    DUT1 : entity work.comparator(comparator_arch)
    		port map(A, B, F);
            
   	STIMULUS : process
    begin
    	
        A <= "0101";
        B <= "0010";
        wait for 10ns;
        
        A <= "1010";
        B <= "1100";
        wait for 10ns;
        
        A <= "1010";
        B <= "1010";
        wait for 10ns;
       
  	end process STIMULUS;
end architecture;