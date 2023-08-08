library IEEE;
use IEEE.std_logic_1164.all;

entity d_flipflop_sync is
	port(clock, D : in std_logic;
    	 Q, Qn : out std_logic);
end entity d_flipflop_sync;

architecture d_flipflop_sync_arch of d_flipflop_sync is
begin
	process(clock)
    begin
    	if(clock'event and clock = '1') then
          	if(D = '0') then
              	Q <= '0';
              	Qn <= '1';
          	elsif(D = '1') then
              	Q <= '1';
              	Qn <= '0';
            end if;
        end if;
	end process;
end architecture d_flipflop_sync_arch;