library IEEE;
use IEEE.std_logic_1164.all;

entity full_gate_tb is
generic(bits : integer := 2);
end entity;

architecture full_gate_arch_tb of full_gate_tb is
signal xor_a, q : std_logic;
signal xor_b, and_a : std_logic_vector(bits-1 downto 0);
begin
    DUT1 : entity work.full_gate(full_gate_arch)
    	 	port map(xor_a, xor_b, and_a, q);
            
   	STIMULUS : process
    begin
    	xor_a <= '0';
        xor_b <= "00";
        and_a <= "00";
        wait for 10ns;
        
    	xor_a <= '1';
        xor_b <= "01";
        and_a <= "10";
        wait for 10ns;
        
 	end process STIMULUS;
end architecture;