library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity bcdCounterTripleDigit is
	port(clock, reset, ce, wr : in std_logic;
    	 din_1, din_2, din_3 : in std_logic_vector(3 downto 0);
         value : out std_logic_vector(11 downto 0));
end entity bcdCounterTripleDigit;

architecture bcdCounterTripleDigit_arch of bcdCounterTripleDigit is
begin
	process(clock, reset)
    variable c1 , c2, c3 : std_logic_vector(3 downto 0);
    begin
    	if(reset = '1') then
        	c1 := "0000";
            c2 := "0000";
       	elsif(clock'event and clock = '1') then
        	if(wr = '1') then
            	c1 := din_1;
                c2 := din_2;
                c3 := din_3;
           	elsif(ce = '1') then
             	if(c1 = "1001" and c2 = "1001" and c3 = "1001") then
                  	c1 := "0000";
                    c2 := "0000";
                   	c3 := "0000";
               	elsif(c2 = "1001" and c3 = "1001") then
                    c2 := "0000";
                    c3 := "0000";
                    c1 := std_logic_vector(to_unsigned((to_integer(unsigned(c1))) + 1, 4));
              	elsif(c3 = "1001") then
                  	c3 := "0000";
                    c2 := std_logic_vector(to_unsigned((to_integer(unsigned(c2))) + 1, 4));
                else
                    c3 := std_logic_vector(to_unsigned((to_integer(unsigned(c3))) + 1, 4));
                end if;
           	end if;
    	end if;
        value <= c1 & c2 & c3;
   	end process;
end architecture bcdCounterTripleDigit_arch;