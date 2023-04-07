library IEEE;
use IEEE.std_logic_1164.all;

entity d_flipflop_async is
	port(C, D : in std_logic;
    	 Q, Qn : out std_logic);
end entity d_flipflop_async;

architecture d_flipflop_async_arch of d_flipflop_async is
begin
	process(C, D)
    begin
    	if(C = '0' and D = '0') then
        	Q <= Q;
            Q <= Qn;
        elsif(C = '0' and D = '1') then
        	Q <= Q;
            Qn <= Qn;
        elsif(C = '1' and D = '0') then
        	Q <= '0';
            Qn <= '1';
       	elsif(C = '1' and D = '1') then
        	Q <= '1';
            Qn <= '0';
        end if;
	end process;
end architecture d_flipflop_async_arch;