library IEEE;
use IEEE.std_logic_1164.all;
 
entity d_flipflop_wReset_sync is
    port(reset, clock, D : in std_logic;
         Q, Qn : out std_logic);
end entity d_flipflop_wReset_sync;
 
architecture d_flipflop_wReset_sync_arch of d_flipflop_wReset_sync is
begin
    process(clock)
    begin
        if(clock'event and clock = '1') then
            if(reset = '1') then
                Q <= '0';
                Qn <= '0';
            elsif(reset = '0' and D = '0') then
                Q <= D;
                Qn <= not D;
            elsif(reset = '0' and D = '1') then
                Q <= D;
                Qn <= not D;
            end if;
        end if;
    end process;
end architecture d_flipflop_wReset_sync_arch;