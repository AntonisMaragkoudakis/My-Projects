/* A Bison parser, made by GNU Bison 3.0.4.  */

/* Bison interface for Yacc-like parsers in C

   Copyright (C) 1984, 1989-1990, 2000-2015 Free Software Foundation, Inc.

   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <http://www.gnu.org/licenses/>.  */

/* As a special exception, you may create a larger work that contains
   part or all of the Bison parser skeleton and distribute that work
   under terms of your choice, so long as that work isn't itself a
   parser generator using the skeleton or a modified version thereof
   as a parser skeleton.  Alternatively, if you modify or redistribute
   the parser skeleton itself, you may (at your option) remove this
   special exception, which will cause the skeleton and the resulting
   Bison output files to be licensed under the GNU General Public
   License without this special exception.

   This special exception was added by the Free Software Foundation in
   version 2.2 of Bison.  */

#ifndef YY_YY_TEACPARSER_TAB_H_INCLUDED
# define YY_YY_TEACPARSER_TAB_H_INCLUDED
/* Debug traces.  */
#ifndef YYDEBUG
# define YYDEBUG 1
#endif
#if YYDEBUG
extern int yydebug;
#endif

/* Token type.  */
#ifndef YYTOKENTYPE
# define YYTOKENTYPE
  enum yytokentype
  {
    TOKEN_POSITIVE_INT = 258,
    TOKEN_POSITIVE_REAL = 259,
    TOKEN_STRING_INP = 260,
    TOKEN_IDENTIFIER = 261,
    KEYWORD_INT = 262,
    KEYWORD_REAL = 263,
    KEYWORD_BOOL = 264,
    KEYWORD_STRING = 265,
    KEYWORD_TRUE = 266,
    KEYWORD_FALSE = 267,
    KEYWORD_IF = 268,
    KEYWORD_ELSE = 269,
    KEYWORD_FI = 270,
    KEYWORD_WHILE = 271,
    KEYWORD_LOOP = 272,
    KEYWORD_POOL = 273,
    KEYWORD_CONST = 274,
    KEYWORD_LET = 275,
    KEYWORD_RETURN = 276,
    KEYWORD_NOT = 277,
    KEYWORD_AND = 278,
    KEYWORD_OR = 279,
    KEYWORD_START = 280,
    KEYWORD_THEN = 281,
    OPERATOR_PLUS = 282,
    OPERATOR_MINUS = 283,
    OPERATOR_MULTIPLY = 284,
    OPERATOR_DIVINE = 285,
    OPERATOR_MODULATE = 286,
    OPERATOR_EQUAL = 287,
    OPERATOR_NOT_EQUAL = 288,
    OPERATOR_LESS = 289,
    OPERATOR_LESS_OR_EQUAL = 290,
    DELIMITER_SEMICOLON = 291,
    DELIMITER_L_PARENTHESIS = 292,
    DELIMITER_R_PARENTHESIS = 293,
    DELIMITER_COMMA = 294,
    DELIMITER_L_BRACKET = 295,
    DELIMITER_R_BRACKET = 296,
    DELIMITER_L_BRACE = 297,
    DELIMITER_R_BRACE = 298,
    DELIMITER_COLON = 299,
    DELIMITER_DOT = 300,
    DELIMITER_ASSIGN = 301,
    DELIMITER_RETURN_ASSIGN = 302,
    CAST_INTEGER = 303,
    CAST_REAL = 304,
    CAST_BOOL = 305,
    CAST_STRING = 306,
    READ_STRING = 307,
    READ_INTEGER = 308,
    READ_REAL = 309,
    WRITE_STRING = 310,
    WRITE_INT = 311,
    WRITE_REAL = 312,
    R_PLUS = 313,
    R_MINUS = 314,
    L_PLUS = 315,
    L_MINUS = 316
  };
#endif

/* Value type.  */
#if ! defined YYSTYPE && ! defined YYSTYPE_IS_DECLARED

union YYSTYPE
{
#line 18 "teacParser.y" /* yacc.c:1909  */

	char* str;

#line 120 "teacParser.tab.h" /* yacc.c:1909  */
};

typedef union YYSTYPE YYSTYPE;
# define YYSTYPE_IS_TRIVIAL 1
# define YYSTYPE_IS_DECLARED 1
#endif


extern YYSTYPE yylval;

int yyparse (void);

#endif /* !YY_YY_TEACPARSER_TAB_H_INCLUDED  */
