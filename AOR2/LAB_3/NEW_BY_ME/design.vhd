library IEEE;
use IEEE.std_logic_1164.all;

entity xor_gate is
	port ( xorg_a, xorg_b : in std_logic;
    	   xorg_q : out std_logic);
end entity;

architecture xor_gate_arch of xor_gate is
begin
	xorg_q <= xorg_a xor xorg_b;
end architecture;


library IEEE;
use IEEE.std_logic_1164.all;

entity and_gate is
	port ( andg_a, andg_b : in std_logic;
    	   andg_q : out std_logic);
end entity;

architecture and_gate_arch of and_gate is
begin
	andg_q <= andg_a and andg_b;
end architecture;


library IEEE;
use IEEE.std_logic_1164.all;

entity full_gate is
	generic (bits : integer := 2);
	port (xor_a : in std_logic;
    	  xor_b, and_a : in std_logic_vector(bits-1 downto 0);
    	  q : out std_logic);
end entity;

architecture full_gate_arch of full_gate is
signal g_Pom, g_Results : std_logic_vector(bits downto 0);
begin
	g_Pom(0) <= xor_a;
	LABEL_FOR_GEN : for i in 0 to bits generate
    signal local_Pom : std_logic;
    begin
    	LABEL_IF1_GEN : if (i < bits) generate
        begin
    		XOR_GATE_LABEL : entity work.xor_gate(xor_gate_arch)
    						port map(xorg_a => g_Pom(i),
                    		 	 xorg_b => xor_b(i),
                             	 xorg_q => local_Pom);
			
   			AND_GATE_LABEL : entity work.and_gate(and_gate_arch)
    						port map(andg_a => and_a(i),
                    		 	 andg_b => local_Pom,
                             	 andg_q => g_Pom(i+1));
     	else generate
        begin
        	q <= g_Pom(i-1);
      	end generate LABEL_IF1_GEN;
  	end generate LABEL_FOR_GEN;
end architecture;
