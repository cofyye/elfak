library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity d_flipflop_async is
	port( D, clock, reset : in std_logic;
    	  Q, Qn : out std_logic);
end entity;

architecture d_flipflop_async_arch of d_flipflop_async is
begin
	process(clock, reset)
    begin
    	if(reset = '1') then
        	Q <= '0';
            Qn <= '1';
    	elsif(clock'event and clock = '1') then
           	if(D = '1') then
            	Q <= '1';
                Qn <= '0';
           	elsif(D = '0') then
            	Q <= '0';
                Qn <= '1';
          	else
            	Q <= '0';
                Qn <= '1';
          	end if;
     	end if;
  	end process;
end architecture;