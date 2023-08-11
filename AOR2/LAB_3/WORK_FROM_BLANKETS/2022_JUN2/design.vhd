library IEEE;
use IEEE.std_logic_1164.all;

entity or_gate is
	port(or_a, or_b : in std_logic;
    	 or_c : out std_logic);
end entity;

architecture or_gate_arch of or_gate is
begin
	or_c <= or_a or or_b;
end architecture;

library IEEE;
use IEEE.std_logic_1164.all;

entity and_gate is
	port(and_a, and_b : in std_logic;
    	 and_c : out std_logic);
end entity;

architecture and_gate_arch of and_gate is
begin
	and_c <= and_a and and_b;
end architecture;

library IEEE;
use IEEE.std_logic_1164.all;

entity full_gate is
	generic(
    			n : integer := 4;
                whichGate : integer := 1
          	);
	port(
   		 	full_a : in std_logic;
            full_b : in std_logic_vector(n-1 downto 0);
         	full_out : out std_logic
         );
end entity;

architecture full_gate_arch of full_gate is
signal g_Pom : std_logic_vector(n downto 0);
begin
	g_Pom(0) <= full_a;
    MAIN_GEN : for i in 0 to n generate
    begin
      	IF_LESS_THAN : if (i < n) generate
      	begin
        	IF_WHICH_GATE : if (whichGate = 1) generate
            begin
       			LABEL_AND_GATE : entity work.and_gate(and_gate_arch)
            					port map(and_a => g_Pom(i),
                                		 and_b => full_b(i),
                                         and_c => g_Pom(i+1));
         	else generate
            begin
       			LABEL_OR_GATE : entity work.or_gate(or_gate_arch)
            					port map(or_a => g_Pom(i),
                                		 or_b => full_b(i),
                                         or_c => g_Pom(i+1));
          	end generate IF_WHICH_GATE;
      	else generate
       	begin
       		full_out <= g_Pom(i);
       end generate IF_LESS_THAN;
	end generate MAIN_GEN;
end architecture;