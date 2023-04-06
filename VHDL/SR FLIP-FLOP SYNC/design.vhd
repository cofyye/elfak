-- design.vhd

library IEEE;
use IEEE.std_logic_1164.all;

entity sr_sync is
	port(S, R, clock : in std_logic;
    	 Q, Qn : out std_logic);
end entity;

architecture sr_sync_arch of sr_sync is
begin
   	process(clock)
	begin
    	if(clock'event and clock = '1') then
        	if(S = '0' and R = '0') then
            	Q <= Q;
                Qn <= Qn;
            elsif(S = '0' and R = '1') then
            	Q <= '0';
                Qn <= '1';
            elsif(S = '1' and R = '0') then
            	Q <= '1';
                Qn <= '0';
           	elsif(S = '1' and R = '1') then
            	Q <= '0';
                Qn <= '0';
            end if;
		end if;
	end process;
end architecture sr_sync_arch;