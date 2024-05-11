%{
  #include <stdio.h>
  #include "cgen.h"
  
  #include <stdarg.h>
  #include <stdlib.h>
  #include <string.h>

  extern int yylex(void);
  extern int error;

  #define FILENAME "c_output.c"
  FILE *fp; 

%}

%union
{
	char* str;
}

  %define parse.trace
  %debug
  %define parse.error verbose


  %token <str> TOKEN_POSITIVE_INT
  %token <str> TOKEN_POSITIVE_REAL 
  %token <str> TOKEN_STRING_INP 
  %token <str> TOKEN_IDENTIFIER


  %token KEYWORD_INT
  %token KEYWORD_REAL
  %token KEYWORD_BOOL
  %token KEYWORD_STRING
  %token KEYWORD_TRUE
  %token KEYWORD_FALSE
  %token KEYWORD_IF
  %token KEYWORD_ELSE
  %token KEYWORD_FI
  %token KEYWORD_WHILE
  %token KEYWORD_LOOP
  %token KEYWORD_POOL
  %token KEYWORD_CONST
  %token KEYWORD_LET
  %token KEYWORD_RETURN
  %token KEYWORD_NOT
  %token KEYWORD_AND
  %token KEYWORD_OR
  %token KEYWORD_START
  %token KEYWORD_THEN

  %token OPERATOR_PLUS
  %token OPERATOR_MINUS
  %token OPERATOR_MULTIPLY
  %token OPERATOR_DIVINE
  %token OPERATOR_MODULATE
  %token OPERATOR_EQUAL
  %token OPERATOR_NOT_EQUAL
  %token OPERATOR_LESS
  %token OPERATOR_LESS_OR_EQUAL

  %token DELIMITER_SEMICOLON
  %token DELIMITER_L_PARENTHESIS
  %token DELIMITER_R_PARENTHESIS
  %token DELIMITER_COMMA
  %token DELIMITER_L_BRACKET
  %token DELIMITER_R_BRACKET
  %token DELIMITER_L_BRACE
  %token DELIMITER_R_BRACE
  %token DELIMITER_COLON
  %token DELIMITER_DOT
  %token DELIMITER_ASSIGN
  %token DELIMITER_RETURN_ASSIGN

  %token CAST_INTEGER
  %token CAST_REAL
  %token CAST_BOOL
  %token CAST_STRING

  %token READ_STRING
  %token READ_INTEGER
  %token READ_REAL
  %token WRITE_STRING
  %token WRITE_INT
  %token WRITE_REAL
  



%right KEYWORD_NOT
%right R_PLUS R_MINUS
%left OPERATOR_PLUS OPERATOR_MINUS L_PLUS L_MINUS  
%left OPERATOR_MULTIPLY OPERATOR_DIVINE OPERATOR_MODULATE
%left OPERATOR_EQUAL OPERATOR_NOT_EQUAL OPERATOR_LESS OPERATOR_LESS_OR_EQUAL
%left KEYWORD_AND
%left KEYWORD_OR


%start program

%type <str> start_decl
%type <str> func
%type <str> func_vars
%type <str> func_var

%type <str> main
%type <str> line
%type <str> lines
%type <str> pr_end

%type <str> type_no_str
%type <str> let_ids
%type <str> let_str_ids
%type <str> func_call_ids

%type <str> matrix
%type <str> ident_matrix

%type <str> read_str
%type <str> read

%type <str> simple_expresion
%type <str> str_expr

%type <str> if_cmd
%type <str> while_cmd
%type <str> expresion_iff
%type <str> expresionf


%%


program:   start_decl {  fp = fopen(FILENAME, "a");
  		                 fprintf(fp, "%s",$1);
  		                 fclose(fp);
                         if (yyerror_count == 0) {
                           puts(c_prologue);
                           printf("Expression evaluates to: %s\n", $1); 
                         }
  		              };


start_decl : main  {  $$ = template( "%s", $1); }
           //----------------
           | KEYWORD_LET let_ids type_no_str DELIMITER_SEMICOLON start_decl { $$ = template("%s %s;\n%s", $3, $2, $5); }
           | KEYWORD_LET let_str_ids KEYWORD_STRING DELIMITER_SEMICOLON start_decl { $$ = template("char* %s;\n%s", $2, $5); }
           //----------------
           | KEYWORD_CONST TOKEN_IDENTIFIER DELIMITER_ASSIGN simple_expresion DELIMITER_COLON type_no_str DELIMITER_SEMICOLON start_decl { $$ = template("const %s %s=%s;\n%s", $6, $2, $4, $8); }
           | KEYWORD_CONST TOKEN_IDENTIFIER matrix DELIMITER_ASSIGN str_expr DELIMITER_COLON KEYWORD_STRING DELIMITER_SEMICOLON start_decl { $$ = template("const char* %s=%s;\n%s", $2, $5, $9); };
           //----------------
           | func start_decl { $$ = template("%s\n%s",$1, $2); } 


main :
KEYWORD_CONST KEYWORD_START DELIMITER_ASSIGN DELIMITER_L_PARENTHESIS DELIMITER_R_PARENTHESIS DELIMITER_COLON KEYWORD_INT DELIMITER_RETURN_ASSIGN DELIMITER_L_BRACE lines pr_end  { $$ = template( "\nint main()\n{\n%s\n}\n\n", $10 ); } ;


pr_end : DELIMITER_R_BRACE  
          { printf("\nThe input program is correct !!!\n");
            fp = fopen(FILENAME, "a");
            fprintf(fp, "/* Antonis Maragoudakis\nA.M = 2013030093 */\n\n");
            fprintf(fp, "#include <stdio.h>\n");
            fprintf(fp, "#include <stdlib.h>\n");
            fprintf(fp, "#include \"teaclib.h\"\n\n");
            fclose(fp); };


lines : line       { $$ = template("%s\n", $1); }
      | line lines { $$ = template("%s\n%s", $1, $2); };

//-----------------------------------------------------------------------------------


func : KEYWORD_CONST TOKEN_IDENTIFIER DELIMITER_ASSIGN DELIMITER_L_PARENTHESIS func_vars DELIMITER_COLON type_no_str DELIMITER_RETURN_ASSIGN DELIMITER_L_BRACE lines DELIMITER_R_BRACE DELIMITER_SEMICOLON { $$ = template("\n%s %s(%s)\n{\n%s}", $7, $2, $5, $10); }
     | KEYWORD_CONST TOKEN_IDENTIFIER DELIMITER_ASSIGN DELIMITER_L_PARENTHESIS func_vars DELIMITER_COLON KEYWORD_STRING DELIMITER_RETURN_ASSIGN DELIMITER_L_BRACE lines DELIMITER_R_BRACE DELIMITER_SEMICOLON { $$ = template("\nchar[] %s(%s)\n{\n%s}", $2, $5, $10); };

func_vars : func_var DELIMITER_R_PARENTHESIS { $$ = template("%s", $1 ); }
          | func_var DELIMITER_COMMA func_vars { $$ = template("%s, %s", $1, $3 ); };

func_var : %empty { $$ = template(""); }
         | TOKEN_IDENTIFIER ident_matrix DELIMITER_COLON type_no_str  { $$ = template("%s %s%s", $4, $1, $2); }
         | TOKEN_IDENTIFIER DELIMITER_L_BRACKET DELIMITER_R_BRACKET DELIMITER_COLON type_no_str { $$ = template("%s %s[]", $5, $1); }
         | TOKEN_IDENTIFIER ident_matrix DELIMITER_COMMA  KEYWORD_STRING  { $$ = template("char* %s", $1); }
         | TOKEN_IDENTIFIER DELIMITER_L_BRACKET DELIMITER_R_BRACKET DELIMITER_COLON KEYWORD_STRING { $$ = template("char* %s", $1); }
         | TOKEN_IDENTIFIER ident_matrix DELIMITER_COMMA func_var DELIMITER_COLON type_no_str  { $$ = template("%s %s%s ,%s", $6, $1, $2, $4 ); }
         | TOKEN_IDENTIFIER DELIMITER_L_BRACKET DELIMITER_R_BRACKET DELIMITER_COMMA func_var DELIMITER_COLON type_no_str { $$ = template("%s %s[] ,%s", $7, $1, $5 ); }
         | TOKEN_IDENTIFIER ident_matrix DELIMITER_COMMA func_var DELIMITER_COLON KEYWORD_STRING  { $$ = template("char* %s ,%s", $1, $2, $4 ); }
         | TOKEN_IDENTIFIER DELIMITER_L_BRACKET DELIMITER_R_BRACKET DELIMITER_COMMA func_var DELIMITER_COLON KEYWORD_STRING { $$ = template("char* %s, %s", $1, $5 ); }


// --------------------------------------------------------------------


line : 
  KEYWORD_LET let_ids type_no_str DELIMITER_SEMICOLON { $$ = template("%s %s;", $3, $2); }
| KEYWORD_LET let_str_ids KEYWORD_STRING DELIMITER_SEMICOLON { $$ = template("char* %s;", $2); }
//--------------
| TOKEN_IDENTIFIER ident_matrix DELIMITER_ASSIGN simple_expresion DELIMITER_SEMICOLON { $$ = template("%s%s=%s;", $1,$2 , $4); }
| TOKEN_IDENTIFIER ident_matrix DELIMITER_ASSIGN str_expr DELIMITER_SEMICOLON { $$ = template("%s%s=%s;", $1,$2 , $4); }
//--------------
| WRITE_STRING DELIMITER_L_PARENTHESIS str_expr DELIMITER_R_PARENTHESIS DELIMITER_SEMICOLON { $$ = template("printf(\"%%s\", %s);", $3); }
| WRITE_STRING DELIMITER_L_PARENTHESIS TOKEN_IDENTIFIER DELIMITER_R_PARENTHESIS DELIMITER_SEMICOLON { $$ = template("printf(\"%%s\", %s);", $3); }
| WRITE_INT DELIMITER_L_PARENTHESIS simple_expresion DELIMITER_R_PARENTHESIS DELIMITER_SEMICOLON { $$ = template("printf(\"%%d\", %s);", $3); }
| WRITE_REAL DELIMITER_L_PARENTHESIS simple_expresion DELIMITER_R_PARENTHESIS DELIMITER_SEMICOLON  { $$ = template("printf(\"%%g\", %s);", $3); }
//--------------
| KEYWORD_RETURN simple_expresion DELIMITER_SEMICOLON { $$ = template("return %s;", $2); }
//--------------
| KEYWORD_IF if_cmd  { $$ = template("if %s", $2); }
| KEYWORD_WHILE while_cmd { $$ = $2;}
//--------------
// | KEYWORD_IF if_cmd  { $$ = $2;} // kalesma gia if me mono ena fi sto telos
//--------------
| TOKEN_IDENTIFIER DELIMITER_L_PARENTHESIS func_call_ids DELIMITER_R_PARENTHESIS DELIMITER_SEMICOLON { $$ = template("%s(%s);", $1, $3 ); }
//--------------
| KEYWORD_CONST TOKEN_IDENTIFIER DELIMITER_ASSIGN simple_expresion DELIMITER_COLON type_no_str DELIMITER_SEMICOLON { $$ = template("const %s %s=%s;", $6, $2, $4 ); }
| KEYWORD_CONST TOKEN_IDENTIFIER matrix DELIMITER_ASSIGN str_expr DELIMITER_COLON KEYWORD_STRING DELIMITER_SEMICOLON { $$ = template("const char* %s=%s;", $2, $5 ); };


// ---------------------------------------------

let_ids  :  TOKEN_IDENTIFIER DELIMITER_COLON   { $$ = template("%s", $1 ); }
         |  TOKEN_IDENTIFIER matrix DELIMITER_COLON   { $$ = template("%s%s", $1, $2 ); }
         |  TOKEN_IDENTIFIER DELIMITER_ASSIGN simple_expresion DELIMITER_COLON  { $$ = template("%s=%s", $1, $3 ); }
         |  TOKEN_IDENTIFIER DELIMITER_COMMA let_ids { $$ = template("%s, %s", $1, $3 ); }
         |  TOKEN_IDENTIFIER matrix DELIMITER_COMMA let_ids { $$ = template("%s%s, %s", $1 ,$2 ,$4 ); }
         |  TOKEN_IDENTIFIER DELIMITER_ASSIGN simple_expresion DELIMITER_COMMA let_ids { $$ = template("%s=%s, %s", $1, $3, $5 ); };

let_str_ids : TOKEN_IDENTIFIER matrix DELIMITER_COLON  { $$ = template("%s%s", $1,$2 ); }
            | TOKEN_IDENTIFIER matrix DELIMITER_ASSIGN str_expr DELIMITER_COLON  { $$ = template("%s%s=%s", $1,$2,$4 ); }
            | TOKEN_IDENTIFIER matrix DELIMITER_COMMA let_str_ids { $$ = template("%s%s, %s", $1 ,$2, $4 ); };
            | TOKEN_IDENTIFIER matrix DELIMITER_ASSIGN str_expr DELIMITER_COMMA let_str_ids { $$ = template("%s%s=%s, %s", $1 ,$2, $4, $6 ); };

//-------------------------------------------------

matrix : DELIMITER_L_BRACKET DELIMITER_R_BRACKET { $$ = template("[]"); }
       | DELIMITER_L_BRACKET TOKEN_POSITIVE_INT DELIMITER_R_BRACKET { $$ = template("[%s]", $2 ); };

ident_matrix : %empty { $$ = template(""); }
              | DELIMITER_L_BRACKET simple_expresion DELIMITER_R_BRACKET { $$ = template("[%s]", $2); };

//-------------------------------------------------

func_call_ids : %empty { $$ = template(""); }
              | simple_expresion { $$ = template("%s", $1); }
              | simple_expresion DELIMITER_COMMA func_call_ids { $$ = template("%s, %s", $1, $3); };

//---------------------------------------------------

type_no_str : KEYWORD_INT { $$ = template("int"); }
             | KEYWORD_REAL { $$ = template("double"); }
             | KEYWORD_BOOL { $$ = template("int"); }
             | DELIMITER_L_BRACKET DELIMITER_R_BRACKET KEYWORD_INT { $$ = template("int*"); }
             | DELIMITER_L_BRACKET DELIMITER_R_BRACKET KEYWORD_REAL { $$ = template("double*"); }
             | DELIMITER_L_BRACKET DELIMITER_R_BRACKET KEYWORD_BOOL { $$ = template("int*"); }
             | DELIMITER_L_BRACKET DELIMITER_R_BRACKET KEYWORD_STRING { $$ = template("char**"); };


//-----------------------------------------------------


simple_expresion : 
  TOKEN_POSITIVE_INT { $$ = template("%s ", $1); }
| TOKEN_POSITIVE_REAL { $$ = template("%s ", $1); }
| read                { $$ = template("%s ", $1); }
| TOKEN_IDENTIFIER ident_matrix  { $$ = template("%s%s ", $1, $2); }
| KEYWORD_TRUE {$$ = template("1");}
| KEYWORD_FALSE {$$ = template("0");}
| DELIMITER_L_PARENTHESIS simple_expresion DELIMITER_R_PARENTHESIS { $$ = template("(%s)", $2); }
| TOKEN_IDENTIFIER DELIMITER_L_PARENTHESIS func_call_ids DELIMITER_R_PARENTHESIS  { $$ = template("%s(%s)", $1, $3 ); }
| OPERATOR_PLUS simple_expresion %prec R_PLUS { $$ = template("+%s ", $2); }
| OPERATOR_MINUS simple_expresion %prec R_MINUS { $$ = template("-%s ", $2); }
| KEYWORD_NOT simple_expresion { $$ = template( "not%s", $2); }
| simple_expresion OPERATOR_PLUS simple_expresion %prec L_PLUS { $$ = template("%s+%s", $1, $3); }
| simple_expresion OPERATOR_MINUS simple_expresion %prec L_MINUS { $$ = template("%s-%s", $1, $3); }
| simple_expresion OPERATOR_MULTIPLY simple_expresion { $$ = template("%s*%s", $1, $3); }
| simple_expresion OPERATOR_DIVINE simple_expresion { $$ = template("%s/%s", $1, $3); }
| simple_expresion OPERATOR_MODULATE simple_expresion { $$ = template("%s %% %s", $1, $3); };
| simple_expresion OPERATOR_EQUAL simple_expresion { $$ = template("%s==%s", $1, $3); }
| simple_expresion OPERATOR_NOT_EQUAL simple_expresion { $$ = template("%s!=%s", $1, $3); }
| simple_expresion OPERATOR_LESS simple_expresion { $$ = template("%s<%s", $1, $3); }
| simple_expresion OPERATOR_LESS_OR_EQUAL simple_expresion { $$ = template("%s<=%s", $1, $3); }
| simple_expresion KEYWORD_AND simple_expresion { $$ = template("%s&&%s", $1, $3); }
| simple_expresion KEYWORD_OR simple_expresion { $$ = template("%s||%s", $1, $3); };

str_expr : TOKEN_STRING_INP { $$ = template("%s ", $1); }
         | read_str         { $$ = template("%s ", $1); };


//--------------------------------------------------------------


read  : READ_INTEGER DELIMITER_L_PARENTHESIS  DELIMITER_R_PARENTHESIS  { $$ = template("atoi(readString())"); }
      | READ_REAL DELIMITER_L_PARENTHESIS  DELIMITER_R_PARENTHESIS     { $$ = template("atof(readString())"); };

read_str :   READ_STRING DELIMITER_L_PARENTHESIS  DELIMITER_R_PARENTHESIS  { $$ = template("fgets()"); };


//-----------------------------------------------------------------


while_cmd: simple_expresion KEYWORD_LOOP lines KEYWORD_POOL DELIMITER_SEMICOLON {$$ = template( "while (%s)\n{\n%s}", $1, $3); };


if_cmd : simple_expresion KEYWORD_THEN lines KEYWORD_FI DELIMITER_SEMICOLON                             { $$ = template("(%s)\n{\n%s}", $1, $3); }
       | simple_expresion KEYWORD_THEN lines KEYWORD_ELSE KEYWORD_IF if_cmd KEYWORD_FI DELIMITER_SEMICOLON   { $$ = template("(%s)\n{\n%s}\nelse if %s ", $1, $3, $6); }
       | simple_expresion KEYWORD_THEN lines KEYWORD_ELSE expresion_iff KEYWORD_FI DELIMITER_SEMICOLON  { $$ = template("(%s)\n{\n%s}\nelse\n{\n%s}", $1, $3, $5); }


expresion_iff : 
               expresionf  { $$ = $1; }
             | expresionf expresion_iff { $$ = template("%s\n%s", $1 , $2); };
  
//---------------------------------------------------------------------------------

// if me ena mono fi sto telos

//if_cmd:
//    simple_expresion KEYWORD_THEN lines KEYWORD_FI DELIMITER_SEMICOLON  {$$ = template( "if (%s)\n{\n%s\n}", $1, $3); }   
//  | simple_expresion KEYWORD_THEN lines KEYWORD_ELSE KEYWORD_IF if_cmd {$$ = template( "if (%s)\n{\n%s\n}\nelse %s", $1, $3, $6); }
//  | simple_expresion KEYWORD_THEN lines KEYWORD_ELSE expresion_iff KEYWORD_FI DELIMITER_SEMICOLON  {$$ = template( "if (%s)\n{\n%s\n}\nelse\n{\n%s\n}", $1, $3, $5); }
//  | simple_expresion KEYWORD_THEN lines KEYWORD_ELSE expresion_iff KEYWORD_IF if_cmd KEYWORD_FI DELIMITER_SEMICOLON  {$$ = template( "if (%s)\n{\n%s\n}\nelse\n{\n%s\n%s\n}", $1, $3, $5, $7); };

//---------------------------------------------------------------------------------

expresionf : 
  KEYWORD_LET let_ids type_no_str DELIMITER_SEMICOLON { $$ = template("%s %s;", $3, $2); }
| KEYWORD_LET let_str_ids KEYWORD_STRING DELIMITER_SEMICOLON { $$ = template("char* %s;", $2); }
//--------------
| TOKEN_IDENTIFIER ident_matrix DELIMITER_ASSIGN simple_expresion DELIMITER_SEMICOLON { $$ = template("%s%s=%s;", $1,$2 , $4); }
| TOKEN_IDENTIFIER ident_matrix DELIMITER_ASSIGN str_expr DELIMITER_SEMICOLON { $$ = template("%s%s=%s;", $1,$2 , $4); }
//--------------
| WRITE_STRING DELIMITER_L_PARENTHESIS str_expr DELIMITER_R_PARENTHESIS DELIMITER_SEMICOLON { $$ = template("printf(\"%%s\", %s);", $3); }
| WRITE_STRING DELIMITER_L_PARENTHESIS TOKEN_IDENTIFIER DELIMITER_R_PARENTHESIS DELIMITER_SEMICOLON { $$ = template("printf(\"%%s\", %s);", $3); }
| WRITE_INT DELIMITER_L_PARENTHESIS simple_expresion DELIMITER_R_PARENTHESIS DELIMITER_SEMICOLON { $$ = template("printf(\"%%d\", %s);", $3); }
| WRITE_REAL DELIMITER_L_PARENTHESIS simple_expresion DELIMITER_R_PARENTHESIS DELIMITER_SEMICOLON  { $$ = template("printf(\"%%g\", %s);", $3); }
//--------------
| KEYWORD_RETURN simple_expresion DELIMITER_SEMICOLON { $$ = template("return %s;", $2); }
//--------------
| KEYWORD_WHILE while_cmd { $$ = $2;}
//--------------
| TOKEN_IDENTIFIER DELIMITER_L_PARENTHESIS func_call_ids DELIMITER_R_PARENTHESIS DELIMITER_SEMICOLON { $$ = template("%s(%s);", $1, $3 ); }
//--------------
| KEYWORD_CONST TOKEN_IDENTIFIER DELIMITER_ASSIGN simple_expresion DELIMITER_COLON type_no_str DELIMITER_SEMICOLON { $$ = template("const %s %s=%s;", $6, $2, $4 ); }
| KEYWORD_CONST TOKEN_IDENTIFIER matrix DELIMITER_ASSIGN str_expr DELIMITER_COLON KEYWORD_STRING DELIMITER_SEMICOLON { $$ = template("const char* %s=%s;", $2, $5 ); };

//------------------------------------------------------------------------------

%%
int main () {
  if ( yyparse() == 0 )
    printf("Accepted!\n \n");
  else
    printf("Rejected!\n \n");
}
