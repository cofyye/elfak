library IEEE;
use IEEE.std_logic_1164.all;

entity full_gate_tb is
	generic(n : integer := 2);
end entity;

architecture full_gate_arch_tb of full_gate_tb is
signal full_xor_a, full_xor_b : std_logic_vector(n-1 downto 0);
signal full_and_a, full_out : std_logic;
begin
	DUT1 : entity work.full_gate(full_gate_arch)
    			generic map(n)
    			port map(full_xor_a, full_xor_b, full_and_a, full_out);
                         
 	STIMULUS : process
    begin
    	full_and_a <= '0';
        full_xor_a <= "00";
        full_xor_b <= "00";
        wait for 10ns;
        
        full_and_a <= '1';
        full_xor_a <= "10";
        full_xor_b <= "10";
        wait for 10ns;
  	end process;
end architecture;