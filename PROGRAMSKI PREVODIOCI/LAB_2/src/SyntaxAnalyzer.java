
import java.util.Stack;
import java.io.IOException;
import java.util.HashMap;


public class SyntaxAnalyzer {
	Integer r1=1,r2=2,r3=3,r4=4,r5=5,r6=6,r7=7,r8=8,r9=9,s1=10,s2=11,s3=12,s4=13,s5=14,s6=15,s7=16,s8=17,s9=18,s10=19,s11=20,s12=21,s13=22,s14=23,s15=24,s16=25,s17=26,s18=27,s19=28,acc=29;
	
	HashMap<Integer,HashMap<Integer,Integer>>actions=new HashMap<>();
	HashMap<Integer,HashMap<Integer,Integer>>prelazi=new HashMap<>();
	class rule
	{
		public int size;
		public int left;
	}
	
	rule rules[];
	public SyntaxAnalyzer()
	{
		for(int i=0;i<20;i++)
		{
			actions.put(i, new HashMap<>());
			prelazi.put(i, new HashMap<>());
		}
		actions.get(0).put(sym.ID, s2);
		prelazi.get(0).put(sym.FD, 1);
		
		actions.get(1).put(sym.EOF,acc);
		
		actions.get(2).put(sym.LEFTPAR,s3);
		
		actions.get(3).put(sym.ID,s16);
		prelazi.get(3).put(sym.PS, 4);
		prelazi.get(3).put(sym.P, 19);
		
		actions.get(4).put(sym.RIGHTPAR, s5);
		actions.get(4).put(sym.COMMA, s14);
		
		actions.get(5).put(sym.ARROW, s6);
		
		actions.get(6).put(sym.ID, s11);
		actions.get(6).put(sym.CONST, s12);
		prelazi.get(6).put(sym.E, 7);
		prelazi.get(6).put(sym.T, 13);
		
		actions.get(7).put(sym.SEMICOLON, s8);
		actions.get(7).put(sym.MUL, s9);
		
		actions.get(8).put(sym.EOF, r1);
		
		actions.get(9).put(sym.ID, s11);
		actions.get(9).put(sym.CONST, s12);
		prelazi.get(9).put(sym.T, 10);
		
		actions.get(10).put(sym.SEMICOLON, r6);
		actions.get(10).put(sym.MUL, r6);
		
		actions.get(11).put(sym.SEMICOLON, r8);
		actions.get(11).put(sym.MUL, r8);
		
		actions.get(12).put(sym.SEMICOLON, r9);
		actions.get(12).put(sym.MUL, r9);
		
		actions.get(13).put(sym.SEMICOLON, r7);
		actions.get(13).put(sym.MUL, r7);
		
		actions.get(14).put(sym.ID, s16);
		prelazi.get(14).put(sym.P, 15);
		
		actions.get(15).put(sym.RIGHTPAR, r2);
		actions.get(15).put(sym.COMMA, r2);
		
		actions.get(16).put(sym.RIGHTPAR, r4);
		actions.get(16).put(sym.COMMA, r4);
		actions.get(16).put(sym.ASSIGN, s17);
		
		actions.get(17).put(sym.CONST, s18);
		
		actions.get(18).put(sym.RIGHTPAR, r5);
		actions.get(18).put(sym.COMMA, r5);
		
		actions.get(19).put(sym.RIGHTPAR, r3);
		actions.get(19).put(sym.COMMA, r3);
		
		rules=new rule[9];
		for(int i=0;i<9;i++)
			rules[i]=new rule();
		rules[0].size=7;
		rules[0].left=sym.FD;
		
		rules[1].size=3;
		rules[1].left=sym.PS;
		
		rules[2].size=1;
		rules[2].left=sym.PS;
		
		rules[3].size=1;
		rules[3].left=sym.P;
		
		rules[4].size=3;
		rules[4].left=sym.P;
		
		rules[5].size=3;
		rules[5].left=sym.E;
		
		rules[6].size=1;
		rules[6].left=sym.E;
		
		rules[7].size=1;
		rules[7].left=sym.T;
		
		rules[8].size=1;
		rules[8].left=sym.T;
		
	}
	public boolean SA_LR(MPLexer lex)
	{
	 // Inicijalizacija
	Stack<Integer>stek=new Stack<>();
		stek.push(0);
	 boolean prepoznat=false;
	 boolean greska=false;
	 Integer next;
	try {
		next = lex.next_token().m_index;
		do{
			 Integer top=stek.peek();
			 //System.out.println(top+" "+next);
			 if(actions.get(top).containsKey(next))
			 {
				 Integer a=actions.get(top).get(next);
				 if(a==acc)
				 {
					 prepoznat=true;
				 }
				 else if(a>r9)
				 {
					 //shift
					 stek.push(next);
					 int k=a-r9;
					 stek.push(k);
					 next = lex.next_token().m_index;
				 }
				 else
				 {
					 //reduce
					 int k=a-1;
					 for(int i=0;i<2*rules[k].size;i++)
					 	stek.pop();
					 int top_state=stek.peek();
					 stek.push(rules[k].left);
					 int prelaz=prelazi.get(top_state).get(rules[k].left);
					 stek.push(prelaz);
				 }
			 }
			 else
			 {
				 greska=true;
			 }
		 }
		 while(!(prepoznat || greska));
	} catch (IOException e) {
		// TODO Auto-generated catch block
		e.printStackTrace();
	}
	
	 return prepoznat;
	} 
	
	
	
}
