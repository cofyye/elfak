// import sekcija
import java_cup.runtime.*;

%%

// sekcija opcija i deklaracija
%class MPLexer

%cup

%line 
%column 

%eofval{
	return new Symbol( sym.EOF );
%eofval}

%{
   public int getLine()
   {
      return yyline;
   }
%}

//stanja
%xstate KOMENTAR 

//makroi
slovo = [a-zA-Z]
cifra = [0-9]
oct = [0-7]
hex = [0-9a-fA-F]

%%

// pravila
\*\* { yybegin( KOMENTAR ); }
<KOMENTAR>~"\*\*" { yybegin( YYINITIAL ); }

[\t\n\r ] { ; }

// Separatori
\(                          { return new Symbol(sym.LEFTPAR); }
\)                          { return new Symbol(sym.RIGHTPAR); }
;                           { return new Symbol(sym.SEMI); }
:                           { return new Symbol(sym.COLON); }
,                           { return new Symbol(sym.COMMA); }
\.                          { return new Symbol(sym.DOT); }
:=                          { return new Symbol(sym.ASSIGN); }

// Operatori
\<                          { return new Symbol(sym.LESS); }
\<=                         { return new Symbol(sym.LEQ); }
\==                         { return new Symbol(sym.EQUAL); }
\<>                         { return new Symbol(sym.NEQ); }
\>                          { return new Symbol(sym.GREATER); }
\>=                         { return new Symbol(sym.GEQ); }

// Ključne reči
"program"                   { return new Symbol(sym.PROGRAM); }
"begin"                     { return new Symbol(sym.BEGIN); }
"end"                       { return new Symbol(sym.END); }
"integer"                   { return new Symbol(sym.INTEGER); }
"char"                      { return new Symbol(sym.CHAR); }
"real"                      { return new Symbol(sym.REAL); }
"boolean"                   { return new Symbol(sym.BOOLEAN); }
"while"                     { return new Symbol(sym.WHILE); }
"else"                      { return new Symbol(sym.ELSE); }
"and"                       { return new Symbol(sym.AND); }
"or"                        { return new Symbol(sym.OR); }

// Identifikatori
({slovo}|[$])({slovo}|{cifra}|[$])* { return new Symbol(sym.ID, yyline, yytext()); }

// Konstante


0|([1-9]{cifra}*)          { return new Symbol(sym.DECCONST, new Integer( yytext() )); }
0{oct}+                    { return new Symbol(sym.OCTCONST, new Integer( yytext() )); }
0x{hex}+                   { return new Symbol(sym.HEXCONST, new Integer( yytext() )); }

[1-9]{cifra}*\.{cifra}+([eE][+-]{cifra}*)?  { return new Symbol(sym.REALCONST, new Double(yytext())); }
\.{cifra}+([eE][+-]{cifra}*)?  { return new Symbol(sym.REALCONST, new Double(yytext())); }
0\.{cifra}+([eE][+-]{cifra}*)?  { return new Symbol(sym.REALCONST, new Double(yytext())); }

'[^]'                      { return new Symbol(sym.CHARCONST, new Character( yytext().charAt(1) )); }
// Obrada grešaka
.                           { System.out.println("ERROR: " + yytext()); } 