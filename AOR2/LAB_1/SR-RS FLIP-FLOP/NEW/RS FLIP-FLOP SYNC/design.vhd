library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity rs_flipflop_sync is
	port( R, S, clock : in std_logic;
    	  Q, Qn : out std_logic);
end entity;

architecture rs_flipflop_sync_arch of rs_flipflop_sync is
begin
	process(clock)
    begin
    	if(clock'event and clock = '1') then
        	if(R = '0' and S = '0') then
            	Q <= Q;
                Qn <= Qn;
           	elsif(R = '1' and S = '0') then
            	Q <= '0';
                Qn <= '1';
           	elsif(R = '0' and S = '1') then
            	Q <= '1';
                Qn <= '0';
           	elsif(R = '1' and S = '1') then
            	Q <= 'X';
                Qn <= 'X';
          	else
            	Q <= 'X';
                Qn <= 'X';
      		end if;
     	end if;
  	end process;
end architecture;