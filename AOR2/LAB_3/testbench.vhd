-- Code your testbench here
library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity kolo_tb is
	generic(n : integer := 4);
end entity;

architecture kolo_tb_arch of kolo_tb is
signal A , B ,D : std_logic_vector(n downto 0);
signal C : std_logic;
begin
	DUT1:entity work.Kolo(Kolo_arch)
    	generic map(n => n)
        port map(A => A , B=> B , D => D , C=> C);
    STIMULUS: process
    	begin
        	C <= '0';
        	A <= "10100";
            B <= "00000";
            wait for 200 ns;
    end process;
end architecture;   