%{

#include "cgen.h"
#include "teacParser.tab.h"


#include <stdio.h>
#include <string.h>
#include <math.h>

int line_num = 1;

%}




IDENTIFIER 				[a-zA-Z_][0-9a-zA-Z_]*
POSITIVE_INT 			0|([1-9][0-9]*)
POSITIVE_REAL   		(({POSITIVE_INT}"."[0-9]*)([eE][+-]?{POSITIVE_INT})?|("."[0-9]*)([eE][+-]?{POSITIVE_INT})?)
STRING_INP  			\"(\\.|[^"\\])*\"



%x comment



%%




int	       	 	{printf("Found  KEYWORD  token INT          command:        '%s'.    At line %d. \n", yytext,line_num); return KEYWORD_INT;}
real			{printf("Found  KEYWORD  token REAL         command:        '%s'.    At line %d. \n", yytext,line_num); return KEYWORD_REAL;}
bool			{printf("Found  KEYWORD  token BOOL         command:        '%s'.    At line %d. \n", yytext,line_num); return KEYWORD_BOOL;}
string			{printf("Found  KEYWORD  token STRING       command:        '%s'.    At line %d. \n", yytext,line_num); return KEYWORD_STRING;}
true			{printf("Found  KEYWORD  token TRUE         command:        '%s'.    At line %d. \n", yytext,line_num); return KEYWORD_TRUE;}
false			{printf("Found  KEYWORD  token FALSE        command:        '%s'.    At line %d. \n", yytext,line_num);  return KEYWORD_FALSE;}
if		    	{printf("Found  KEYWORD  token IF           command:        '%s'.    At line %d. \n", yytext,line_num); return KEYWORD_IF;}
else			{printf("Found  KEYWORD  token ELSE         command:        '%s'.    At line %d. \n", yytext,line_num); return KEYWORD_ELSE;}
fi				{printf("Found  KEYWORD  token FI           command:        '%s'.    At line %d. \n", yytext,line_num); return KEYWORD_FI;}
while			{printf("Found  KEYWORD  token WHILE        command:        '%s'.    At line %d. \n", yytext,line_num); return KEYWORD_WHILE;}
loop			{printf("Found  KEYWORD  token LOOP         command:        '%s'.    At line %d. \n", yytext,line_num); return KEYWORD_LOOP;}
pool			{printf("Found  KEYWORD  token POOL         command:        '%s'.    At line %d. \n", yytext,line_num); return KEYWORD_POOL;}
const           {printf("Found  KEYWORD  token CONST        command:        '%s'.    At line %d. \n", yytext,line_num); return KEYWORD_CONST;}
let				{printf("Found  KEYWORD  token LET          command:        '%s'.    At line %d. \n", yytext,line_num); return KEYWORD_LET;}
return			{printf("Found  KEYWORD  token RETURN       command:        '%s'.    At line %d. \n", yytext,line_num); return KEYWORD_RETURN;}
not				{printf("Found  KEYWORD  token NOT          command:        '%s'.    At line %d. \n", yytext,line_num); return KEYWORD_NOT;}
and				{printf("Found  KEYWORD  token AND          command:        '%s'.    At line %d. \n", yytext,line_num); return KEYWORD_AND;}
or				{printf("Found  KEYWORD  token OR           command:        '%s'.    At line %d. \n", yytext,line_num); return KEYWORD_OR;}
start			{printf("Found  KEYWORD  token START        command:        '%s'.    At line %d. \n", yytext,line_num); return KEYWORD_START;}
then			{printf("Found  KEYWORD  token THEN         command:        '%s'.    At line %d. \n", yytext,line_num); return KEYWORD_THEN;}



"+"             {printf("Found OPERATOR  token PLUS          with symbol:   '%s'.    At line %d. \n", yytext,line_num); return OPERATOR_PLUS;}
"-"             {printf("Found OPERATOR  token MINUS         with symbol:   '%s'.    At line %d. \n", yytext,line_num); return OPERATOR_MINUS;}                
"*"             {printf("Found OPERATOR  token MULTIPLY      with symbol:   '%s'.    At line %d. \n", yytext,line_num); return OPERATOR_MULTIPLY;}                   
"/"             {printf("Found OPERATOR  token DIVINE        with symbol:   '%s'.    At line %d. \n", yytext,line_num); return OPERATOR_DIVINE;}                  
"%"             {printf("Found OPERATOR  token MODULATE      with symbol:   '%s'.    At line %d. \n", yytext,line_num); return OPERATOR_MODULATE;}

"="             {printf("Found OPERATOR  token EQUAL         with symbol:   '%s'.    At line %d. \n", yytext,line_num); return OPERATOR_EQUAL;}             
"!="            {printf("Found OPERATOR  token NOT_EQUAL     with symbol:   '%s'.    At line %d. \n", yytext,line_num); return OPERATOR_NOT_EQUAL;} 
"<"	        	{printf("Found OPERATOR  token OPERATOR_LESS with symbol:   '%s'.    At line %d. \n", yytext,line_num); return OPERATOR_LESS;}
"<="            {printf("Found OPERATOR  token LESS_OR_EQUAL with symbol:   '%s'.    At line %d. \n", yytext,line_num); return OPERATOR_LESS_OR_EQUAL;}




";"				{printf("Found DELIMITER token SEMICOLON     with symbol:   '%s'.    At line %d. \n", yytext,line_num); return DELIMITER_SEMICOLON;}
"("				{printf("Found DELIMITER token L_PARENTHESIS with symbol:   '%s'.    At line %d. \n", yytext,line_num); return DELIMITER_L_PARENTHESIS;}
")"				{printf("Found DELIMITER token R_PARENTHESIS with symbol:   '%s'.    At line %d. \n", yytext,line_num); return DELIMITER_R_PARENTHESIS;}
","				{printf("Found DELIMITER token COMMA         with symbol:   '%s'.    At line %d. \n", yytext,line_num); return DELIMITER_COMMA;}
"["				{printf("Found DELIMITER token L_BRACKET     with symbol:   '%s'.    At line %d. \n", yytext,line_num); return DELIMITER_L_BRACKET;}
"]"				{printf("Found DELIMITER token R_BRACKET     with symbol:   '%s'.    At line %d. \n", yytext,line_num); return DELIMITER_R_BRACKET;}
"{"				{printf("Found DELIMITER token L_BRACE       with symbol:   '%s'.    At line %d. \n", yytext,line_num); return DELIMITER_L_BRACE;}
"}"				{printf("Found DELIMITER token R_BRACE       with symbol:   '%s'.    At line %d. \n", yytext,line_num); return DELIMITER_R_BRACE;}
":"     		{printf("Found DELIMITER token COLON         with symbol:   '%s'.    At line %d. \n", yytext,line_num); return DELIMITER_COLON;}
"."				{printf("Found DELIMITER token DOT           with symbol:   '%s'.    At line %d. \n", yytext,line_num); return DELIMITER_DOT;}
"<-"			{printf("Found DELIMITER token ASSIGN        with symbol:   '%s'.    At line %d. \n", yytext,line_num); return DELIMITER_ASSIGN;}
"=>"            {printf("Found DELIMITER token RETURN_ASIGN  with symbol:   '%s'.    At line %d. \n", yytext,line_num); return DELIMITER_RETURN_ASSIGN;}



readString    {printf("Found   READ    token READ_STRING   thats a:        %s.     At line %d. \n", yytext,line_num);return READ_STRING;}                     
readInt       {printf("Found   READ    token READ_INTEGER  thats a:        %s.     At line %d. \n", yytext,line_num);return READ_INTEGER;}                     
readReal      {printf("Found   READ    token READ_REAL     thats a:        %s.     At line %d. \n", yytext,line_num);return READ_REAL;}                     
writeString   {printf("Found   WRITE   token WRITE_STRING  thats a:        %s.     At line %d. \n", yytext,line_num);return WRITE_STRING;}                      
writeInt      {printf("Found   WRITE   token WRITE_INT     thats a:        %s.     At line %d. \n", yytext,line_num);return WRITE_INT;}                   
writeReal     {printf("Found   WRITE   token WRITE_REAL    thats a:        %s.     At line %d. \n", yytext,line_num);return WRITE_REAL;}


 	 		 
{POSITIVE_INT}  {printf("Found   VALUE   token POSITIVE_INT  with value:    (%s).    At line %d. \n", yytext,line_num); yylval.str = strdup(yytext); return TOKEN_POSITIVE_INT;}		 
{POSITIVE_REAL} {printf("Found   VALUE   token POSITIVE_REAL with value:    (%s).    At line %d. \n", yytext,line_num); yylval.str = strdup(yytext); return TOKEN_POSITIVE_REAL;}	 
{STRING_INP}    {printf("Found   VALUE   token STRING_INP    with value:    '%s'.    At line %d. \n", yytext,line_num); yylval.str = strdup(yytext); return TOKEN_STRING_INP;}	 
{IDENTIFIER}    {printf("Found   VALUE   token IDENTIFIER    with value:    '%s'.    At line %d. \n", yytext,line_num); yylval.str = strdup(yytext); return TOKEN_IDENTIFIER;}
		 


"--"[^\n]*  	 printf("Line %d: line comment: %s\n", line_num, yytext);/* eat line comments */

"(*"                    { BEGIN(comment); printf("comment started at line: %d\n", line_num); }

<comment>[^*\n]*        /* eat anything that's not a '*' */
<comment>"*"+[^*)\n]*   /* eat up '*'s not followed by '/'s */
<comment>\n             ++line_num;
<comment>"*"+")"        { BEGIN(INITIAL); printf("comment ended at line: %d\n", line_num); }

<<EOF>> 		return EOF;

[ \r\t]		/* skip whitespace */
\n          ++line_num;
.           { printf("Line %d Lexical Error: Unrecognized literal %s\n", line_num, yytext); }
