-- Code your design here
library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity OrGate is
	port(A : in std_logic;
    	 B : in std_logic;
         Y : out std_logic);
end entity;

architecture OrGate_arch of OrGate is
begin
	Y <= A or B;
end architecture;

library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity XorGate is
	port(A : in std_logic;
    	 B : in std_logic;
         Y : out std_logic);
end entity;

architecture XorGate_arch of XorGate is
begin
	Y <= A xor B;
end architecture;



-- SAD IDE ONO PRAVO--
library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.numeric_std.all;

entity Kolo is
	generic(n : integer := 4);
    port(A : in std_logic_vector(n downto 0);
    	 B : in std_logic_vector(n downto 0);
         C : in std_logic;
         D : out std_logic_vector(n downto 0));
end entity;

architecture Kolo_arch of Kolo is
signal C_ulaz : std_logic_vector(n downto 0);
signal Xor_izlaz : std_logic_vector(n downto 0);
begin
	C_ulaz(0) <= C;
    
    ForLbl : for i in 0 to n generate
    begin
    	If1lbl : if i=0 generate
        Xor_Gate: entity work.XorGate(XorGate_arch)
        	port map(A => A(0) , B => B(0) , Y => Xor_izlaz(0));
            
        Or_Gate: entity work.OrGate(OrGate_arch)
        	port map(A => Xor_izlaz(0) , B => C_ulaz(0) , Y =>D(0));
        C_ulaz(i+1) <= D(0);
        end generate;
        
        If2lbl : if i/=0 and i/=n generate
        Xor_Gate : entity work.XorGate(XorGate_arch)
        	port map(A => A(i) , B=> B(i) , Y=>Xor_izlaz(i));
        Or_Gate: entity work.OrGate(OrGate_arch)
        	port map(A => Xor_izlaz(i) , B => C_ulaz(i) , Y =>D(i));
        C_ulaz(i+1) <= D(i);
        end generate;
        
         If3lbl : if i=n generate
        Xor_Gate : entity work.XorGate(XorGate_arch)
        	port map(A => A(i) , B=> B(i) , Y=>Xor_izlaz(i));
        Or_Gate: entity work.OrGate(OrGate_arch)
        	port map(A => Xor_izlaz(i) , B => C_ulaz(i) , Y =>D(i));
       end generate;
       
    end generate;
end architecture;