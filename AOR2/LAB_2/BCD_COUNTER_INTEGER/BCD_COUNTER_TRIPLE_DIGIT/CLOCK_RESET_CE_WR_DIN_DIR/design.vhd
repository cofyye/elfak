library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity BcdCounterTripleDigit is
	port(clock, reset, ce, wr, dir : in std_logic;
    	 din : in integer range 0 to 999;
    	 value : out integer range 0 to 999);
end entity BcdCounterTripleDigit;

architecture BcdCounterTripleDigit_arch of BcdCounterTripleDigit is
begin
	process(clock, reset)
    variable q : integer range 0 to 99;
    begin
    	if(reset = '1') then
        	q := 0;
      	elsif(clock'event and clock = '1') then
        	if(wr = '1') then
            	q := din;
           	elsif(ce = '1') then
            	if(dir = '1') then
                    if(q = 999) then
                        q := 0;
                    else
                        q := q + 1;
                    end if;
               	else
                    if(q = 0) then
                        q := 999;
                    else
                        q := q - 1;
                    end if;  
            	end if;
     		end if;
     	end if;
        value <= q;
	end process;
end architecture BcdCounterTripleDigit_arch;