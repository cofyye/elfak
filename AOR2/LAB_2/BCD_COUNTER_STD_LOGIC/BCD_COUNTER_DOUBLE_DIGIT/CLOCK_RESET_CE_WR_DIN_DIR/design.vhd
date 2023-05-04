library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity bcdCounterDoubleDigit is
	port(clock, reset, ce, wr, dir : in std_logic;
    	 din_1, din_2 : in std_logic_vector(3 downto 0);
         value : out std_logic_vector(7 downto 0));
end entity bcdCounterDoubleDigit;

architecture bcdCounterDoubleDigit_arch of bcdCounterDoubleDigit is
begin
	process(clock, reset)
    variable c1 , c2 : std_logic_vector(3 downto 0);
    begin
    	if(reset = '1') then
        	c1 := "0000";
            c2 := "0000";
       	elsif(clock'event and clock = '1') then
        	if(wr = '1') then
            	c1 := din_1;
                c2 := din_2;
           	elsif(ce = '1') then
            	if(dir = '1') then
                  if(c1 = "1001" and c2 = "1001") then
                      c1 := "0000";
                      c2 := "0000";
                  elsif(c2 = "1001") then
                      c2 := "0000";
                      c1 := std_logic_vector(to_unsigned((to_integer(unsigned(c1))) + 1, 4));
                  else
                      c2 := std_logic_vector(to_unsigned((to_integer(unsigned(c2))) + 1, 4));
                  end if;
               	else
                  if(c1 = "0000" and c2 = "0000") then
                      c1 := "1001";
                      c2 := "1001";
                  elsif(c2 = "0000") then
                      c2 := "1001";
                      c1 := std_logic_vector(to_unsigned((to_integer(unsigned(c1))) - 1, 4));
                  else
                      c2 := std_logic_vector(to_unsigned((to_integer(unsigned(c2))) - 1, 4));
                  end if;	
         		end if;
           	end if;
    	end if;
        value <= c1 & c2;
   	end process;
end architecture bcdCounterDoubleDigit_arch;