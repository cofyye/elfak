library IEEE;
use IEEE.std_logic_1164.all;

entity full_gate_tb is
	generic(
    			n : integer := 4;
                whichGate : integer := 1
           	);
end entity;

architecture full_gate_arch_tb of full_gate_tb is
signal full_a, full_out : std_logic;
signal full_which_gate : integer range 0 to 1;
signal full_b : std_logic_vector(n-1 downto 0);
begin
	DUT1 : entity work.full_gate(full_gate_arch)
    			generic map(n, whichGate)
    			port map(full_a, full_b, full_out);
                         
 	STIMULUS : process
    begin
    	full_a <= '0';
        full_b <= "0000";
        wait for 10ns;
        
        full_a <= '0';
        full_b <= "0010";
        wait for 10ns;
  	end process;
end architecture;