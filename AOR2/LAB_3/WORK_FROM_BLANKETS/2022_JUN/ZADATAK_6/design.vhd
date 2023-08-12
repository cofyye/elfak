library IEEE;
use IEEE.std_logic_1164.all;

entity bcd_counter is
	generic(n : integer := 9);
    port(clk, rst, smer, ce, wr : in std_logic;
    	 din : in integer range 0 to n;
         q : out integer range 0 to n);
end entity;

architecture bcd_counter_arch of bcd_counter is
begin
	process(clk, rst)
    variable q_out : integer range 0 to n; 
    begin
    	if(rst = '1') then
        	q_out := 0;
       	elsif(clk'event and clk = '1') then
        	if(wr = '1') then
            	if(din < 0 or din > n) then
                	q_out := 0;
               	else
                	q_out := din;
               	end if;
          	elsif(ce = '1') then
            	if(smer = '1') then
                	if(q_out = n) then
                    	if(din < 0 or din > n) then
                        	q_out := 0;
                       	else
                        	q_out := din;
                      	end if;
                  	else
                    	q_out := q_out + 1;
                   	end if;
              	else
                	if(q_out = 0) then
                    	if(din < 0 or din > n) then
                        	q_out := 0;
                       	else
                        	q_out := din;
                       	end if;
                   	else
                    	q_out := q_out - 1;
                   	end if;
               	end if;
         	end if;
     	end if;
       	q <= q_out;
  	end process;
end architecture;
                
            	