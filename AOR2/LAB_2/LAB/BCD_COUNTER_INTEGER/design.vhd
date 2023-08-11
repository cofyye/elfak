library IEEE;
use IEEE.std_logic_1164.all;

entity bcd_counter_int is
	generic ( num : integer := 9 );
	port ( clock, reset, ce, dir, wr : in bit;
    	   din : in integer range 0 to num;
           q : out integer range 0 to num);
end entity;

architecture bcd_counter_int_arch of bcd_counter_int is
begin
	process(clock, reset)
    variable q_out : integer range 0 to num;
    begin
    	if(reset = '1') then
        	q_out := 0;
      	elsif(clock'event and clock = '1') then
        	if(wr = '1') then
            	if(din < 0 or din > num) then
                	q_out := 0;
               	else
                	q_out := din;
              	end if;
          	elsif(ce = '1') then
            	if(dir = '1') then
                	if(q_out = num) then
                    	q_out := 0;
                   	else
                    	q_out := q_out + 1;
                   	end if;
              	else
                	if(q_out = 0) then
                    	q_out := num;
                   	else
                    	q_out := q_out - 1;
                   	end if;
               	end if;
          	end if;
    	end if;
        q <= q_out;
   	end process;
end architecture;
    