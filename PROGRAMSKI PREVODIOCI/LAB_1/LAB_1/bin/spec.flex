// import sekcija

%%

// sekcija opcija i deklaracija
%class MPLexer
%function next_token
%line
%column
%debug
%type Yytoken

%eofval{
return new Yytoken( sym.EOF, null, yyline, yycolumn);
%eofval}

%{
//dodatni clanovi generisane klase
KWTable kwTable = new KWTable();
Yytoken getKW()
{
	return new Yytoken( kwTable.find( yytext() ),
	yytext(), yyline, yycolumn );
}
%}

//stanja
%xstate KOMENTAR
//makroi
slovo = [a-zA-Z]
//cifra = [0-9]

%%

// pravila
\*\* { yybegin( KOMENTAR ); }
<KOMENTAR>~"\*\*" { yybegin( YYINITIAL ); }

[\t\n\r ] { ; }
\( { return new Yytoken( sym.LEFTPAR, yytext(), yyline, yycolumn ); }
\) { return new Yytoken( sym.RIGHTPAR, yytext(), yyline, yycolumn ); }

//operatori
\< { return new Yytoken( sym.LESS, yytext(), yyline, yycolumn ); }
\<= { return new Yytoken( sym.LESSEQUAL, yytext(), yyline, yycolumn ); }
\== { return new Yytoken( sym.EQUAL, yytext(), yyline, yycolumn ); }
\<> { return new Yytoken( sym.NONEQUAL, yytext(), yyline, yycolumn ); }
\> { return new Yytoken( sym.GREATER, yytext(), yyline, yycolumn ); }
\>= { return new Yytoken( sym.GREATEREQUAL, yytext(), yyline, yycolumn ); }


//separatori
; { return new Yytoken( sym.SEMICOLON, yytext(), yyline, yycolumn ); }
: { return new Yytoken( sym.COLON, yytext(), yyline, yycolumn ); }
, { return new Yytoken( sym.COMMA, yytext(), yyline, yycolumn ); }
\. { return new Yytoken( sym.DOT, yytext(), yyline, yycolumn ); }
:= { return new Yytoken( sym.ASSIGN, yytext(), yyline, yycolumn ); }

//kljucne reci
{slovo}+ { return getKW(); }
//identifikatori
[a-zA-Z$][a-zA-Z0-9$]* { return new Yytoken(sym.ID, yytext(),yyline, yycolumn ); }
//konstante
(0[0-7]+|0x[0-9a-fA-F]+|[1-9][0-9]*) { return new Yytoken( sym.CONST, yytext(), yyline, yycolumn ); }
([0-9]+\.[0-9]*(E[+-]?[0-9]+)?|\.[0-9]+(E[+-]?[0-9]+)?) { return new Yytoken( sym.CONST, yytext(), yyline, yycolumn ); }
'[^]' { return new Yytoken( sym.CONST,yytext(), yyline, yycolumn ); }
(true|false) { return new Yytoken( sym.CONST,yytext(), yyline, yycolumn ); }
//obrada gresaka
. { if (yytext() != null && yytext().length() > 0) System.out.println( "ERROR: " + yytext() ); }
