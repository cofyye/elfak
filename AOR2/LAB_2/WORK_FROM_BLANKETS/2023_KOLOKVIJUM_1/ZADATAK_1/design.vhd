library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity bcd_counter is
	port(clk, rst, we : in std_logic;
    	 d_in : in integer range 0 to 9;
    	 q : out integer range 0 to 9);
end entity;

architecture bcd_counter_arch of bcd_counter is
signal go_count : std_logic;
signal return_out : integer range 0 to 9;
begin
	process(clk, rst)
    begin
    	if(rst = '1') then
        	return_out <= 0;
            go_count <= '0';
      	elsif(clk'event and clk = '1') then
        	if(we = '1') then
            	if(d_in < 0 and d_in > 9) then
                	return_out <= 0;
               	else
                	return_out <= d_in;
              	end if;
           	else
            	go_count <= not go_count;
            	if(go_count = '1') then
                	if(return_out > 9) then
                    	return_out <= 0;
                   	else
                    	return_out <= return_out + 1;
                   	end if;
               	end if;
          	end if;
   		end if;
        q <= return_out;
   	end process;
end architecture;