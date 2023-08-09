library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity bcd_counter_stdv is
	generic ( numbits : integer := 2 );
	port ( clock, reset, ce, dir, wr : in bit;
    	   din : in std_logic_vector(numbits-1 downto 0);
           q : out std_logic_vector(numbits-1 downto 0));
end entity;

architecture bcd_counter_stdv_arch of bcd_counter_stdv is
begin
	process(clock, reset)
    variable q_out : std_logic_vector(numbits-1 downto 0);
    begin
    	if(reset = '1') then
        	q_out := std_logic_vector(to_unsigned(0, numbits));
      	elsif(clock'event and clock = '1') then
        	if(wr = '1') then
            	if(din > std_logic_vector(to_unsigned(2**numbits-1, numbits))) then
                	q_out := std_logic_vector(to_unsigned(0, numbits));
               	else
                	q_out := din;
              	end if;
          	elsif(ce = '1') then
            	if(dir = '1') then
                	if(q_out = std_logic_vector(to_unsigned(2**numbits-1, numbits))) then
                    	q_out := std_logic_vector(to_unsigned(0, numbits));
                   	else
                    	q_out := std_logic_vector(to_unsigned(to_integer(unsigned(q_out))+1, numbits));
                   	end if;
              	else
                	if(q_out = std_logic_vector(to_unsigned(0, numbits))) then
                    	q_out := std_logic_vector(to_unsigned(2**numbits-1, numbits));
                   	else
                    	q_out := std_logic_vector(to_unsigned(to_integer(unsigned(q_out))-1, numbits));
                   	end if;
               	end if;
          	end if;
    	end if;
        q <= q_out;
   	end process;
end architecture;
    