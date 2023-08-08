library IEEE;
use IEEE.std_logic_1164.all;

entity jk_flipflop_sync is
	port(J, K, clock : in std_logic;
         Q, Qn : out std_logic);
end entity jk_flipflop_sync;

architecture jk_flipflop_sync_arch of jk_flipflop_sync is
begin
	process(clock)
    begin
    	if(clock'event and clock = '1') then
          if(J = '0' and K = '0') then
              Q <= Q;
              Qn <= Qn;
          elsif(J = '0' and K = '1') then
              Q <= '0';
              Qn <= '1';
          elsif(J = '1' and K = '0') then
              Q <= '1';
              Qn <= '0';
          elsif(J = '1' and K = '1') then
              Q <= Qn;
              Qn <= Q;
          else
              Q <= 'Z';
              Qn <= 'Z';
          end if;
   		end if;
	end process;
end architecture jk_flipflop_sync_arch;
    
    	