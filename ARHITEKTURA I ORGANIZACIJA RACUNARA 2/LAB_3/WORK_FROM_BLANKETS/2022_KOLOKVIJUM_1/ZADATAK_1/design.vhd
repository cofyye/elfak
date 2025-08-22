library IEEE;
use IEEE.std_logic_1164.all;

entity xor_gate is
	port(xor_a, xor_b : in std_logic;
    	 xor_c : out std_logic);
end entity;

architecture xor_gate_arch of xor_gate is
begin
	xor_c <= xor_a xor xor_b;
end architecture;

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

entity full_gate is
	generic(n : integer := 2);
	port(
            full_xor_a, full_xor_b : in std_logic_vector(n-1 downto 0);
            full_and_a : in std_logic;
         	full_out : out std_logic
         );
end entity;

architecture full_gate_arch of full_gate is
signal g_Pom : std_logic_vector(n downto 0);
begin
	g_Pom(0) <= full_and_a;
    LABEL_FOR_GEN : for i in 0 to n generate
    	signal local_Pom : std_logic;
   	begin
    	LABEL_IF1_GEN : if (i < n) generate
        begin
       		LABEL_XOR_GATE : entity work.xor_gate(xor_gate_arch)
            				port map(xor_a => full_xor_a(i),
                            		 xor_b => full_xor_b(i),
                                     xor_c => local_Pom);
        	LABEL_OR_GATE : entity work.or_gate(or_gate_arch)
            				port map(or_a => local_Pom,
                            		 or_b => g_Pom(i),
                                     or_c => g_Pom(i+1));
    	else generate
        begin
        	full_out <= g_Pom(i);
       	end generate LABEL_IF1_GEN;
  	end generate LABEL_FOR_GEN;
end architecture;