/* A Bison parser, made by GNU Bison 3.0.4.  */

/* Bison implementation for Yacc-like parsers in C

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

/* C LALR(1) parser skeleton written by Richard Stallman, by
   simplifying the original so-called "semantic" parser.  */

/* All symbols defined below should begin with yy or YY, to avoid
   infringing on user name space.  This should be done even for local
   variables, as they might otherwise be expanded by user macros.
   There are some unavoidable exceptions within include files to
   define necessary library symbols; they are noted "INFRINGES ON
   USER NAME SPACE" below.  */

/* Identify Bison output.  */
#define YYBISON 1

/* Bison version.  */
#define YYBISON_VERSION "3.0.4"

/* Skeleton name.  */
#define YYSKELETON_NAME "yacc.c"

/* Pure parsers.  */
#define YYPURE 0

/* Push parsers.  */
#define YYPUSH 0

/* Pull parsers.  */
#define YYPULL 1




/* Copy the first part of user declarations.  */
#line 1 "teacParser.y" /* yacc.c:339  */

  #include <stdio.h>
  #include "cgen.h"
  
  #include <stdarg.h>
  #include <stdlib.h>
  #include <string.h>

  extern int yylex(void);
  extern int error;

  #define FILENAME "c_output.c"
  FILE *fp; 


#line 82 "teacParser.tab.c" /* yacc.c:339  */

# ifndef YY_NULLPTR
#  if defined __cplusplus && 201103L <= __cplusplus
#   define YY_NULLPTR nullptr
#  else
#   define YY_NULLPTR 0
#  endif
# endif

/* Enabling verbose error messages.  */
#ifdef YYERROR_VERBOSE
# undef YYERROR_VERBOSE
# define YYERROR_VERBOSE 1
#else
# define YYERROR_VERBOSE 1
#endif

/* In a future release of Bison, this section will be replaced
   by #include "teacParser.tab.h".  */
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
#line 18 "teacParser.y" /* yacc.c:355  */

	char* str;

#line 188 "teacParser.tab.c" /* yacc.c:355  */
};

typedef union YYSTYPE YYSTYPE;
# define YYSTYPE_IS_TRIVIAL 1
# define YYSTYPE_IS_DECLARED 1
#endif


extern YYSTYPE yylval;

int yyparse (void);

#endif /* !YY_YY_TEACPARSER_TAB_H_INCLUDED  */

/* Copy the second part of user declarations.  */

#line 205 "teacParser.tab.c" /* yacc.c:358  */

#ifdef short
# undef short
#endif

#ifdef YYTYPE_UINT8
typedef YYTYPE_UINT8 yytype_uint8;
#else
typedef unsigned char yytype_uint8;
#endif

#ifdef YYTYPE_INT8
typedef YYTYPE_INT8 yytype_int8;
#else
typedef signed char yytype_int8;
#endif

#ifdef YYTYPE_UINT16
typedef YYTYPE_UINT16 yytype_uint16;
#else
typedef unsigned short int yytype_uint16;
#endif

#ifdef YYTYPE_INT16
typedef YYTYPE_INT16 yytype_int16;
#else
typedef short int yytype_int16;
#endif

#ifndef YYSIZE_T
# ifdef __SIZE_TYPE__
#  define YYSIZE_T __SIZE_TYPE__
# elif defined size_t
#  define YYSIZE_T size_t
# elif ! defined YYSIZE_T
#  include <stddef.h> /* INFRINGES ON USER NAME SPACE */
#  define YYSIZE_T size_t
# else
#  define YYSIZE_T unsigned int
# endif
#endif

#define YYSIZE_MAXIMUM ((YYSIZE_T) -1)

#ifndef YY_
# if defined YYENABLE_NLS && YYENABLE_NLS
#  if ENABLE_NLS
#   include <libintl.h> /* INFRINGES ON USER NAME SPACE */
#   define YY_(Msgid) dgettext ("bison-runtime", Msgid)
#  endif
# endif
# ifndef YY_
#  define YY_(Msgid) Msgid
# endif
#endif

#ifndef YY_ATTRIBUTE
# if (defined __GNUC__                                               \
      && (2 < __GNUC__ || (__GNUC__ == 2 && 96 <= __GNUC_MINOR__)))  \
     || defined __SUNPRO_C && 0x5110 <= __SUNPRO_C
#  define YY_ATTRIBUTE(Spec) __attribute__(Spec)
# else
#  define YY_ATTRIBUTE(Spec) /* empty */
# endif
#endif

#ifndef YY_ATTRIBUTE_PURE
# define YY_ATTRIBUTE_PURE   YY_ATTRIBUTE ((__pure__))
#endif

#ifndef YY_ATTRIBUTE_UNUSED
# define YY_ATTRIBUTE_UNUSED YY_ATTRIBUTE ((__unused__))
#endif

#if !defined _Noreturn \
     && (!defined __STDC_VERSION__ || __STDC_VERSION__ < 201112)
# if defined _MSC_VER && 1200 <= _MSC_VER
#  define _Noreturn __declspec (noreturn)
# else
#  define _Noreturn YY_ATTRIBUTE ((__noreturn__))
# endif
#endif

/* Suppress unused-variable warnings by "using" E.  */
#if ! defined lint || defined __GNUC__
# define YYUSE(E) ((void) (E))
#else
# define YYUSE(E) /* empty */
#endif

#if defined __GNUC__ && 407 <= __GNUC__ * 100 + __GNUC_MINOR__
/* Suppress an incorrect diagnostic about yylval being uninitialized.  */
# define YY_IGNORE_MAYBE_UNINITIALIZED_BEGIN \
    _Pragma ("GCC diagnostic push") \
    _Pragma ("GCC diagnostic ignored \"-Wuninitialized\"")\
    _Pragma ("GCC diagnostic ignored \"-Wmaybe-uninitialized\"")
# define YY_IGNORE_MAYBE_UNINITIALIZED_END \
    _Pragma ("GCC diagnostic pop")
#else
# define YY_INITIAL_VALUE(Value) Value
#endif
#ifndef YY_IGNORE_MAYBE_UNINITIALIZED_BEGIN
# define YY_IGNORE_MAYBE_UNINITIALIZED_BEGIN
# define YY_IGNORE_MAYBE_UNINITIALIZED_END
#endif
#ifndef YY_INITIAL_VALUE
# define YY_INITIAL_VALUE(Value) /* Nothing. */
#endif


#if ! defined yyoverflow || YYERROR_VERBOSE

/* The parser invokes alloca or malloc; define the necessary symbols.  */

# ifdef YYSTACK_USE_ALLOCA
#  if YYSTACK_USE_ALLOCA
#   ifdef __GNUC__
#    define YYSTACK_ALLOC __builtin_alloca
#   elif defined __BUILTIN_VA_ARG_INCR
#    include <alloca.h> /* INFRINGES ON USER NAME SPACE */
#   elif defined _AIX
#    define YYSTACK_ALLOC __alloca
#   elif defined _MSC_VER
#    include <malloc.h> /* INFRINGES ON USER NAME SPACE */
#    define alloca _alloca
#   else
#    define YYSTACK_ALLOC alloca
#    if ! defined _ALLOCA_H && ! defined EXIT_SUCCESS
#     include <stdlib.h> /* INFRINGES ON USER NAME SPACE */
      /* Use EXIT_SUCCESS as a witness for stdlib.h.  */
#     ifndef EXIT_SUCCESS
#      define EXIT_SUCCESS 0
#     endif
#    endif
#   endif
#  endif
# endif

# ifdef YYSTACK_ALLOC
   /* Pacify GCC's 'empty if-body' warning.  */
#  define YYSTACK_FREE(Ptr) do { /* empty */; } while (0)
#  ifndef YYSTACK_ALLOC_MAXIMUM
    /* The OS might guarantee only one guard page at the bottom of the stack,
       and a page size can be as small as 4096 bytes.  So we cannot safely
       invoke alloca (N) if N exceeds 4096.  Use a slightly smaller number
       to allow for a few compiler-allocated temporary stack slots.  */
#   define YYSTACK_ALLOC_MAXIMUM 4032 /* reasonable circa 2006 */
#  endif
# else
#  define YYSTACK_ALLOC YYMALLOC
#  define YYSTACK_FREE YYFREE
#  ifndef YYSTACK_ALLOC_MAXIMUM
#   define YYSTACK_ALLOC_MAXIMUM YYSIZE_MAXIMUM
#  endif
#  if (defined __cplusplus && ! defined EXIT_SUCCESS \
       && ! ((defined YYMALLOC || defined malloc) \
             && (defined YYFREE || defined free)))
#   include <stdlib.h> /* INFRINGES ON USER NAME SPACE */
#   ifndef EXIT_SUCCESS
#    define EXIT_SUCCESS 0
#   endif
#  endif
#  ifndef YYMALLOC
#   define YYMALLOC malloc
#   if ! defined malloc && ! defined EXIT_SUCCESS
void *malloc (YYSIZE_T); /* INFRINGES ON USER NAME SPACE */
#   endif
#  endif
#  ifndef YYFREE
#   define YYFREE free
#   if ! defined free && ! defined EXIT_SUCCESS
void free (void *); /* INFRINGES ON USER NAME SPACE */
#   endif
#  endif
# endif
#endif /* ! defined yyoverflow || YYERROR_VERBOSE */


#if (! defined yyoverflow \
     && (! defined __cplusplus \
         || (defined YYSTYPE_IS_TRIVIAL && YYSTYPE_IS_TRIVIAL)))

/* A type that is properly aligned for any stack member.  */
union yyalloc
{
  yytype_int16 yyss_alloc;
  YYSTYPE yyvs_alloc;
};

/* The size of the maximum gap between one aligned stack and the next.  */
# define YYSTACK_GAP_MAXIMUM (sizeof (union yyalloc) - 1)

/* The size of an array large to enough to hold all stacks, each with
   N elements.  */
# define YYSTACK_BYTES(N) \
     ((N) * (sizeof (yytype_int16) + sizeof (YYSTYPE)) \
      + YYSTACK_GAP_MAXIMUM)

# define YYCOPY_NEEDED 1

/* Relocate STACK from its old location to the new one.  The
   local variables YYSIZE and YYSTACKSIZE give the old and new number of
   elements in the stack, and YYPTR gives the new location of the
   stack.  Advance YYPTR to a properly aligned location for the next
   stack.  */
# define YYSTACK_RELOCATE(Stack_alloc, Stack)                           \
    do                                                                  \
      {                                                                 \
        YYSIZE_T yynewbytes;                                            \
        YYCOPY (&yyptr->Stack_alloc, Stack, yysize);                    \
        Stack = &yyptr->Stack_alloc;                                    \
        yynewbytes = yystacksize * sizeof (*Stack) + YYSTACK_GAP_MAXIMUM; \
        yyptr += yynewbytes / sizeof (*yyptr);                          \
      }                                                                 \
    while (0)

#endif

#if defined YYCOPY_NEEDED && YYCOPY_NEEDED
/* Copy COUNT objects from SRC to DST.  The source and destination do
   not overlap.  */
# ifndef YYCOPY
#  if defined __GNUC__ && 1 < __GNUC__
#   define YYCOPY(Dst, Src, Count) \
      __builtin_memcpy (Dst, Src, (Count) * sizeof (*(Src)))
#  else
#   define YYCOPY(Dst, Src, Count)              \
      do                                        \
        {                                       \
          YYSIZE_T yyi;                         \
          for (yyi = 0; yyi < (Count); yyi++)   \
            (Dst)[yyi] = (Src)[yyi];            \
        }                                       \
      while (0)
#  endif
# endif
#endif /* !YYCOPY_NEEDED */

/* YYFINAL -- State number of the termination state.  */
#define YYFINAL  12
/* YYLAST -- Last index in YYTABLE.  */
#define YYLAST   580

/* YYNTOKENS -- Number of terminals.  */
#define YYNTOKENS  62
/* YYNNTS -- Number of nonterminals.  */
#define YYNNTS  24
/* YYNRULES -- Number of rules.  */
#define YYNRULES  109
/* YYNSTATES -- Number of states.  */
#define YYNSTATES  313

/* YYTRANSLATE[YYX] -- Symbol number corresponding to YYX as returned
   by yylex, with out-of-bounds checking.  */
#define YYUNDEFTOK  2
#define YYMAXUTOK   316

#define YYTRANSLATE(YYX)                                                \
  ((unsigned int) (YYX) <= YYMAXUTOK ? yytranslate[YYX] : YYUNDEFTOK)

/* YYTRANSLATE[TOKEN-NUM] -- Symbol number corresponding to TOKEN-NUM
   as returned by yylex, without out-of-bounds checking.  */
static const yytype_uint8 yytranslate[] =
{
       0,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     1,     2,     3,     4,
       5,     6,     7,     8,     9,    10,    11,    12,    13,    14,
      15,    16,    17,    18,    19,    20,    21,    22,    23,    24,
      25,    26,    27,    28,    29,    30,    31,    32,    33,    34,
      35,    36,    37,    38,    39,    40,    41,    42,    43,    44,
      45,    46,    47,    48,    49,    50,    51,    52,    53,    54,
      55,    56,    57,    58,    59,    60,    61
};

#if YYDEBUG
  /* YYRLINE[YYN] -- Source line where rule number YYN was defined.  */
static const yytype_uint16 yyrline[] =
{
       0,   136,   136,   146,   148,   149,   151,   152,   154,   158,
     161,   171,   172,   177,   178,   180,   181,   183,   184,   185,
     186,   187,   188,   189,   190,   191,   198,   199,   201,   202,
     204,   205,   206,   207,   209,   211,   212,   216,   218,   219,
     224,   225,   226,   227,   228,   229,   231,   232,   233,   234,
     238,   239,   241,   242,   246,   247,   248,   252,   253,   254,
     255,   256,   257,   258,   265,   266,   267,   268,   269,   270,
     271,   272,   273,   274,   275,   276,   277,   278,   279,   280,
     281,   282,   283,   284,   285,   286,   288,   289,   295,   296,
     298,   304,   307,   308,   309,   313,   314,   329,   330,   332,
     333,   335,   336,   337,   338,   340,   342,   344,   346,   347
};
#endif

#if YYDEBUG || YYERROR_VERBOSE || 1
/* YYTNAME[SYMBOL-NUM] -- String name of the symbol SYMBOL-NUM.
   First, the terminals, then, starting at YYNTOKENS, nonterminals.  */
static const char *const yytname[] =
{
  "$end", "error", "$undefined", "TOKEN_POSITIVE_INT",
  "TOKEN_POSITIVE_REAL", "TOKEN_STRING_INP", "TOKEN_IDENTIFIER",
  "KEYWORD_INT", "KEYWORD_REAL", "KEYWORD_BOOL", "KEYWORD_STRING",
  "KEYWORD_TRUE", "KEYWORD_FALSE", "KEYWORD_IF", "KEYWORD_ELSE",
  "KEYWORD_FI", "KEYWORD_WHILE", "KEYWORD_LOOP", "KEYWORD_POOL",
  "KEYWORD_CONST", "KEYWORD_LET", "KEYWORD_RETURN", "KEYWORD_NOT",
  "KEYWORD_AND", "KEYWORD_OR", "KEYWORD_START", "KEYWORD_THEN",
  "OPERATOR_PLUS", "OPERATOR_MINUS", "OPERATOR_MULTIPLY",
  "OPERATOR_DIVINE", "OPERATOR_MODULATE", "OPERATOR_EQUAL",
  "OPERATOR_NOT_EQUAL", "OPERATOR_LESS", "OPERATOR_LESS_OR_EQUAL",
  "DELIMITER_SEMICOLON", "DELIMITER_L_PARENTHESIS",
  "DELIMITER_R_PARENTHESIS", "DELIMITER_COMMA", "DELIMITER_L_BRACKET",
  "DELIMITER_R_BRACKET", "DELIMITER_L_BRACE", "DELIMITER_R_BRACE",
  "DELIMITER_COLON", "DELIMITER_DOT", "DELIMITER_ASSIGN",
  "DELIMITER_RETURN_ASSIGN", "CAST_INTEGER", "CAST_REAL", "CAST_BOOL",
  "CAST_STRING", "READ_STRING", "READ_INTEGER", "READ_REAL",
  "WRITE_STRING", "WRITE_INT", "WRITE_REAL", "R_PLUS", "R_MINUS", "L_PLUS",
  "L_MINUS", "$accept", "program", "start_decl", "main", "pr_end", "lines",
  "func", "func_vars", "func_var", "line", "let_ids", "let_str_ids",
  "matrix", "ident_matrix", "func_call_ids", "type_no_str",
  "simple_expresion", "str_expr", "read", "read_str", "while_cmd",
  "if_cmd", "expresion_iff", "expresionf", YY_NULLPTR
};
#endif

# ifdef YYPRINT
/* YYTOKNUM[NUM] -- (External) token number corresponding to the
   (internal) symbol number NUM (which must be that of a token).  */
static const yytype_uint16 yytoknum[] =
{
       0,   256,   257,   258,   259,   260,   261,   262,   263,   264,
     265,   266,   267,   268,   269,   270,   271,   272,   273,   274,
     275,   276,   277,   278,   279,   280,   281,   282,   283,   284,
     285,   286,   287,   288,   289,   290,   291,   292,   293,   294,
     295,   296,   297,   298,   299,   300,   301,   302,   303,   304,
     305,   306,   307,   308,   309,   310,   311,   312,   313,   314,
     315,   316
};
# endif

#define YYPACT_NINF -134

#define yypact_value_is_default(Yystate) \
  (!!((Yystate) == (-134)))

#define YYTABLE_NINF -47

#define yytable_value_is_error(Yytable_value) \
  0

  /* YYPACT[STATE-NUM] -- Index in YYTABLE of the portion describing
     STATE-NUM.  */
static const yytype_int16 yypact[] =
{
     147,    24,    38,    10,  -134,  -134,   147,    47,    21,   102,
      61,    40,  -134,  -134,     4,    91,    33,    51,    98,  -134,
     214,    52,  -134,  -134,  -134,   135,   103,   107,   169,  -134,
    -134,  -134,   -28,  -134,  -134,   214,   214,   214,   259,   110,
     156,   292,  -134,    14,   176,   102,  -134,   214,   270,    38,
     206,    14,   196,   147,   147,  -134,   214,   214,  -134,   525,
     525,   525,   114,   175,    87,   391,   193,   197,   214,   214,
     214,   214,   214,   214,   214,   214,   214,   214,   214,    61,
    -134,   200,   205,  -134,   212,    -1,    98,  -134,  -134,  -134,
      94,  -134,  -134,  -134,  -134,  -134,  -134,   222,   378,   346,
     174,   111,     6,  -134,   260,  -134,  -134,  -134,   240,  -134,
     538,   538,   189,   189,   189,   215,   215,   215,   215,   233,
     237,   264,   271,    98,  -134,  -134,   273,  -134,  -134,   214,
    -134,   140,   106,    61,   230,   235,   243,  -134,   147,  -134,
     244,   238,   248,  -134,  -134,   260,   100,  -134,   245,  -134,
     242,   249,   111,  -134,   147,   250,   211,   246,  -134,  -134,
     113,    20,    20,  -134,    20,   273,  -134,   236,  -134,  -134,
     151,   214,   214,   289,    38,   214,   269,   274,   280,   265,
      20,   267,   275,  -134,  -134,   214,   261,   512,  -134,   365,
    -134,    71,    61,   318,   456,    19,   214,   214,   293,  -134,
     294,  -134,  -134,   297,    78,    20,    20,   214,   285,   296,
     311,  -134,   308,   312,   404,   417,  -134,  -134,   313,   470,
     317,   258,   330,   310,    14,  -134,  -134,   329,   331,   332,
     335,  -134,  -134,  -134,   152,   347,   348,    61,   320,  -134,
    -134,  -134,  -134,   157,   214,   214,   360,    38,   214,   349,
     353,   354,   370,   143,  -134,  -134,   367,   394,   214,   384,
     401,  -134,    89,    61,   433,   484,    22,   214,   214,   420,
    -134,  -134,   446,   431,    78,   459,   214,   450,   473,   474,
    -134,   485,   486,   430,   443,  -134,  -134,   501,   498,   514,
    -134,   328,    14,  -134,  -134,   515,   527,   528,   529,  -134,
    -134,  -134,    61,   522,  -134,  -134,  -134,  -134,   539,   564,
    -134,   540,  -134
};

  /* YYDEFACT[STATE-NUM] -- Default reduction number in state STATE-NUM.
     Performed when YYTABLE does not specify something else to do.  Zero
     means the default is an error.  */
static const yytype_uint8 yydefact[] =
{
       0,     0,     0,     0,     2,     3,     0,     0,     0,     0,
       0,     0,     1,     8,     0,     0,     0,     0,     0,    40,
       0,     0,    57,    58,    59,     0,     0,     0,     0,    50,
      64,    65,    52,    68,    69,     0,     0,     0,    17,     0,
       0,     0,    66,     0,     0,     0,    43,     0,     0,     0,
      41,     0,     0,     0,     0,    51,    54,     0,    67,    74,
      72,    73,    52,     0,     0,     0,     0,     0,     0,     0,
       0,     0,     0,     0,     0,     0,     0,     0,     0,     0,
      86,     0,     0,    87,     0,     0,     0,    42,    44,    48,
       0,    60,    61,    62,    63,     4,     5,     0,    55,     0,
       0,    67,     0,    15,    17,    70,    88,    89,    84,    85,
      75,    76,    77,    78,    79,    80,    81,    82,    83,     0,
       0,     0,     0,     0,    41,    45,     0,    47,    71,    54,
      53,     0,    17,     0,     0,     0,    52,    16,     0,    90,
       0,     0,     0,    49,    56,    17,     0,    20,     0,    18,
       0,     0,     0,     6,     0,     0,     0,     0,    21,    19,
       0,     0,     0,     7,     0,     0,    46,     0,    24,    22,
      52,     0,     0,     0,     0,     0,     0,     0,     0,     0,
      11,     0,     0,    25,    23,    54,     0,     0,    35,     0,
      36,     0,     0,     0,     0,     0,     0,     0,     0,    12,
       0,    10,     9,     0,     0,     0,     0,     0,     0,     0,
       0,    34,     0,     0,     0,     0,    14,    13,     0,     0,
       0,     0,     0,     0,     0,    26,    27,     0,     0,     0,
       0,    37,    28,    29,     0,     0,     0,     0,     0,    31,
      30,    32,    33,    52,     0,     0,     0,     0,     0,     0,
       0,     0,     0,    95,    92,    91,     0,     0,    54,     0,
       0,   106,     0,     0,     0,     0,     0,     0,     0,     0,
      96,    38,     0,     0,     0,     0,     0,     0,     0,     0,
     105,     0,     0,     0,     0,    94,    39,     0,     0,     0,
      93,     0,     0,    97,    98,     0,     0,     0,     0,   107,
      99,   100,     0,     0,   102,   101,   103,   104,     0,     0,
     108,     0,   109
};

  /* YYPGOTO[NTERM-NUM].  */
static const yytype_int16 yypgoto[] =
{
    -134,  -134,    -2,  -134,  -134,  -133,  -134,   475,   -97,  -134,
       0,     1,    -8,   -56,  -121,   -68,   -15,   -34,  -134,  -134,
     333,   336,   324,  -134
};

  /* YYDEFGOTO[NTERM-NUM].  */
static const yytype_int16 yydefgoto[] =
{
      -1,     3,     4,     5,   202,   179,     6,    63,    64,   180,
      88,    89,    16,    58,    97,    26,    98,    82,    42,    83,
     190,   188,   252,   253
};

  /* YYTABLE[YYPACT[STATE-NUM]] -- What to do in state STATE-NUM.  If
     positive, shift that token.  If negative, reduce the rule whose
     number is the opposite.  If YYTABLE_NINF, syntax error.  */
static const yytype_int16 yytable[] =
{
      41,    21,    10,    11,    13,    48,   101,    28,   144,    56,
      12,   119,    57,    22,    23,    24,   134,    90,    46,    80,
      59,    60,    61,    65,    80,   212,   170,    80,   281,   181,
       7,   182,    65,   171,   135,   148,   172,    85,   123,   173,
     174,   175,    99,   124,     9,    29,    25,   199,   157,     8,
      27,    95,    96,   108,   109,   110,   111,   112,   113,   114,
     115,   116,   117,   118,   203,   149,    81,    17,    22,    23,
      24,    81,   221,   222,    81,   176,   177,   178,   159,    43,
     152,    30,    31,    80,    32,    99,   125,    14,    44,    33,
      34,    49,   169,    15,    30,    31,    50,    32,    51,   184,
      35,    25,    33,    34,    45,    36,    37,    22,    23,    24,
     158,    14,   136,    35,   186,    47,   147,   207,    36,    37,
      22,    23,    24,   168,   209,   103,   104,   143,    38,    14,
      81,    39,    40,   126,   156,   276,   153,   273,   127,    53,
      25,    18,    14,    54,    39,    40,    19,    66,    20,   243,
     132,    56,   163,    25,   100,   133,   187,   189,   243,   245,
     194,   213,   246,   247,   248,   244,     1,     2,   245,   256,
     220,   246,   247,   248,   192,   193,    52,    30,    31,   145,
      32,   214,   215,   208,   146,    33,    34,   259,   185,   219,
     238,    57,   223,    67,   258,   278,    35,    57,   249,   250,
     251,    36,    37,    91,    92,    93,    94,   249,   250,   251,
      55,    47,    68,    69,    84,   131,   -46,    30,    31,   102,
      32,    75,    76,    77,    78,    33,    34,    39,    40,   187,
     189,   106,   282,   265,   308,   107,    35,   120,    68,    69,
     289,    36,    37,    22,    23,    24,   183,   263,   264,   121,
     165,    47,   283,   284,   277,   166,   122,    51,   303,   288,
     128,   291,    30,    31,    69,    62,   136,    39,    40,   138,
      33,    34,   234,   235,   140,   139,    25,   150,   141,   142,
     154,    35,   151,   100,   161,   155,    36,    37,    14,   160,
     167,   162,   164,    68,    69,   191,    47,    70,    71,    72,
      73,    74,    75,    76,    77,    78,   195,   204,   198,    86,
     200,   196,    39,    40,    87,    68,    69,   197,   201,    70,
      71,    72,    73,    74,    75,    76,    77,    78,   210,   216,
     217,   224,   225,    68,    69,   218,    79,    70,    71,    72,
      73,    74,    75,    76,    77,    78,   227,   226,   236,   231,
     228,    68,    69,   233,   237,    70,    71,    72,    73,    74,
      75,    76,    77,    78,   257,   239,   262,   240,   241,    68,
      69,   242,   302,    70,    71,    72,    73,    74,    75,    76,
      77,    78,   206,   254,   255,   269,   266,   130,    68,    69,
     267,   268,    70,    71,    72,    73,    74,    75,    76,    77,
      78,    68,    69,   271,   272,    70,    71,    72,    73,    74,
      75,    76,    77,    78,    68,    69,   275,   129,    70,    71,
      72,    73,    74,    75,    76,    77,    78,    68,    69,   105,
     274,    70,    71,    72,    73,    74,    75,    76,    77,    78,
      68,    69,   229,   279,    70,    71,    72,    73,    74,    75,
      76,    77,    78,    68,    69,   230,   285,    70,    71,    72,
      73,    74,    75,    76,    77,    78,    68,    69,   297,   287,
      70,    71,    72,    73,    74,    75,    76,    77,    78,    68,
      69,   298,   286,    70,    71,    72,    73,    74,    75,    76,
      77,    78,   211,    68,    69,   290,   292,    70,    71,    72,
      73,    74,    75,    76,    77,    78,   232,    68,    69,   293,
     294,    70,    71,    72,    73,    74,    75,    76,    77,    78,
     280,    68,    69,   295,   296,    70,    71,    72,    73,    74,
      75,    76,    77,    78,   300,    68,    69,   299,   205,    70,
      71,    72,    73,    74,    75,    76,    77,    78,    68,    69,
     301,   304,    70,    71,    72,    73,    74,    75,    76,    77,
      78,    68,    69,   305,   306,   307,   309,    72,    73,    74,
      75,    76,    77,    78,   311,   310,   312,   270,   261,   137,
     260
};

static const yytype_uint16 yycheck[] =
{
      15,     9,     2,     2,     6,    20,    62,     3,   129,    37,
       0,    79,    40,     7,     8,     9,    10,    51,    18,     5,
      35,    36,    37,    38,     5,     6,     6,     5,     6,   162,
       6,   164,    47,    13,   102,   132,    16,    45,    39,    19,
      20,    21,    57,    44,     6,    41,    40,   180,   145,    25,
      10,    53,    54,    68,    69,    70,    71,    72,    73,    74,
      75,    76,    77,    78,   185,   133,    52,    46,     7,     8,
       9,    52,   205,   206,    52,    55,    56,    57,   146,    46,
     136,     3,     4,     5,     6,   100,    86,    40,    37,    11,
      12,    39,   160,    46,     3,     4,    44,     6,    46,   167,
      22,    40,    11,    12,     6,    27,    28,     7,     8,     9,
      10,    40,     6,    22,   170,    37,    10,    46,    27,    28,
       7,     8,     9,    10,   192,    38,    39,   126,    37,    40,
      52,    53,    54,    39,   142,    46,   138,   258,    44,    36,
      40,    39,    40,    36,    53,    54,    44,    37,    46,     6,
      39,    37,   154,    40,    40,    44,   171,   172,     6,    16,
     175,   195,    19,    20,    21,    13,    19,    20,    16,   237,
     204,    19,    20,    21,   174,   174,    41,     3,     4,    39,
       6,   196,   197,   191,    44,    11,    12,   243,    37,   204,
     224,    40,   207,    37,    37,   263,    22,    40,    55,    56,
      57,    27,    28,     7,     8,     9,    10,    55,    56,    57,
      41,    37,    23,    24,    38,    41,    10,     3,     4,    44,
       6,    32,    33,    34,    35,    11,    12,    53,    54,   244,
     245,    38,   266,   248,   302,    38,    22,    37,    23,    24,
     274,    27,    28,     7,     8,     9,    10,   247,   247,    44,
      39,    37,   267,   268,   262,    44,    44,    46,   292,   274,
      38,   276,     3,     4,    24,     6,     6,    53,    54,    36,
      11,    12,    14,    15,    10,    38,    40,    47,     7,     6,
      36,    22,    47,    40,    42,    47,    27,    28,    40,    44,
      44,    42,    42,    23,    24,     6,    37,    27,    28,    29,
      30,    31,    32,    33,    34,    35,    37,    46,    43,    39,
      43,    37,    53,    54,    44,    23,    24,    37,    43,    27,
      28,    29,    30,    31,    32,    33,    34,    35,    10,    36,
      36,    46,    36,    23,    24,    38,    44,    27,    28,    29,
      30,    31,    32,    33,    34,    35,    38,    36,    18,    36,
      38,    23,    24,    36,    44,    27,    28,    29,    30,    31,
      32,    33,    34,    35,    44,    36,     6,    36,    36,    23,
      24,    36,    44,    27,    28,    29,    30,    31,    32,    33,
      34,    35,    17,    36,    36,    15,    37,    41,    23,    24,
      37,    37,    27,    28,    29,    30,    31,    32,    33,    34,
      35,    23,    24,    36,    10,    27,    28,    29,    30,    31,
      32,    33,    34,    35,    23,    24,    15,    39,    27,    28,
      29,    30,    31,    32,    33,    34,    35,    23,    24,    38,
      46,    27,    28,    29,    30,    31,    32,    33,    34,    35,
      23,    24,    38,    10,    27,    28,    29,    30,    31,    32,
      33,    34,    35,    23,    24,    38,    36,    27,    28,    29,
      30,    31,    32,    33,    34,    35,    23,    24,    38,    38,
      27,    28,    29,    30,    31,    32,    33,    34,    35,    23,
      24,    38,    36,    27,    28,    29,    30,    31,    32,    33,
      34,    35,    36,    23,    24,    36,    46,    27,    28,    29,
      30,    31,    32,    33,    34,    35,    36,    23,    24,    36,
      36,    27,    28,    29,    30,    31,    32,    33,    34,    35,
      36,    23,    24,    38,    38,    27,    28,    29,    30,    31,
      32,    33,    34,    35,    36,    23,    24,    36,    26,    27,
      28,    29,    30,    31,    32,    33,    34,    35,    23,    24,
      36,    36,    27,    28,    29,    30,    31,    32,    33,    34,
      35,    23,    24,    36,    36,    36,    44,    29,    30,    31,
      32,    33,    34,    35,    10,    36,    36,   253,   245,   104,
     244
};

  /* YYSTOS[STATE-NUM] -- The (internal number of the) accessing
     symbol of state STATE-NUM.  */
static const yytype_uint8 yystos[] =
{
       0,    19,    20,    63,    64,    65,    68,     6,    25,     6,
      72,    73,     0,    64,    40,    46,    74,    46,    39,    44,
      46,    74,     7,     8,     9,    40,    77,    10,     3,    41,
       3,     4,     6,    11,    12,    22,    27,    28,    37,    53,
      54,    78,    80,    46,    37,     6,    72,    37,    78,    39,
      44,    46,    41,    36,    36,    41,    37,    40,    75,    78,
      78,    78,     6,    69,    70,    78,    37,    37,    23,    24,
      27,    28,    29,    30,    31,    32,    33,    34,    35,    44,
       5,    52,    79,    81,    38,    74,    39,    44,    72,    73,
      79,     7,     8,     9,    10,    64,    64,    76,    78,    78,
      40,    75,    44,    38,    39,    38,    38,    38,    78,    78,
      78,    78,    78,    78,    78,    78,    78,    78,    78,    77,
      37,    44,    44,    39,    44,    72,    39,    44,    38,    39,
      41,    41,    39,    44,    10,    77,     6,    69,    36,    38,
      10,     7,     6,    73,    76,    39,    44,    10,    70,    77,
      47,    47,    75,    64,    36,    47,    74,    70,    10,    77,
      44,    42,    42,    64,    42,    39,    44,    44,    10,    77,
       6,    13,    16,    19,    20,    21,    55,    56,    57,    67,
      71,    67,    67,    10,    77,    37,    75,    78,    83,    78,
      82,     6,    72,    73,    78,    37,    37,    37,    43,    67,
      43,    43,    66,    76,    46,    26,    17,    46,    74,    77,
      10,    36,     6,    79,    78,    78,    36,    36,    38,    78,
      79,    67,    67,    78,    46,    36,    36,    38,    38,    38,
      38,    36,    36,    36,    14,    15,    18,    44,    79,    36,
      36,    36,    36,     6,    13,    16,    19,    20,    21,    55,
      56,    57,    84,    85,    36,    36,    77,    44,    37,    75,
      83,    82,     6,    72,    73,    78,    37,    37,    37,    15,
      84,    36,    10,    76,    46,    15,    46,    74,    77,    10,
      36,     6,    79,    78,    78,    36,    36,    38,    78,    79,
      36,    78,    46,    36,    36,    38,    38,    38,    38,    36,
      36,    36,    44,    79,    36,    36,    36,    36,    77,    44,
      36,    10,    36
};

  /* YYR1[YYN] -- Symbol number of symbol that rule YYN derives.  */
static const yytype_uint8 yyr1[] =
{
       0,    62,    63,    64,    64,    64,    64,    64,    64,    65,
      66,    67,    67,    68,    68,    69,    69,    70,    70,    70,
      70,    70,    70,    70,    70,    70,    71,    71,    71,    71,
      71,    71,    71,    71,    71,    71,    71,    71,    71,    71,
      72,    72,    72,    72,    72,    72,    73,    73,    73,    73,
      74,    74,    75,    75,    76,    76,    76,    77,    77,    77,
      77,    77,    77,    77,    78,    78,    78,    78,    78,    78,
      78,    78,    78,    78,    78,    78,    78,    78,    78,    78,
      78,    78,    78,    78,    78,    78,    79,    79,    80,    80,
      81,    82,    83,    83,    83,    84,    84,    85,    85,    85,
      85,    85,    85,    85,    85,    85,    85,    85,    85,    85
};

  /* YYR2[YYN] -- Number of symbols on the right hand side of rule YYN.  */
static const yytype_uint8 yyr2[] =
{
       0,     2,     1,     1,     5,     5,     8,     9,     2,    11,
       1,     1,     2,    12,    12,     2,     3,     0,     4,     5,
       4,     5,     6,     7,     6,     7,     4,     4,     5,     5,
       5,     5,     5,     5,     3,     2,     2,     5,     7,     8,
       2,     3,     4,     3,     4,     5,     3,     5,     4,     6,
       2,     3,     0,     3,     0,     1,     3,     1,     1,     1,
       3,     3,     3,     3,     1,     1,     1,     2,     1,     1,
       3,     4,     2,     2,     2,     3,     3,     3,     3,     3,
       3,     3,     3,     3,     3,     3,     1,     1,     3,     3,
       3,     5,     5,     8,     7,     1,     2,     4,     4,     5,
       5,     5,     5,     5,     5,     3,     2,     5,     7,     8
};


#define yyerrok         (yyerrstatus = 0)
#define yyclearin       (yychar = YYEMPTY)
#define YYEMPTY         (-2)
#define YYEOF           0

#define YYACCEPT        goto yyacceptlab
#define YYABORT         goto yyabortlab
#define YYERROR         goto yyerrorlab


#define YYRECOVERING()  (!!yyerrstatus)

#define YYBACKUP(Token, Value)                                  \
do                                                              \
  if (yychar == YYEMPTY)                                        \
    {                                                           \
      yychar = (Token);                                         \
      yylval = (Value);                                         \
      YYPOPSTACK (yylen);                                       \
      yystate = *yyssp;                                         \
      goto yybackup;                                            \
    }                                                           \
  else                                                          \
    {                                                           \
      yyerror (YY_("syntax error: cannot back up")); \
      YYERROR;                                                  \
    }                                                           \
while (0)

/* Error token number */
#define YYTERROR        1
#define YYERRCODE       256



/* Enable debugging if requested.  */
#if YYDEBUG

# ifndef YYFPRINTF
#  include <stdio.h> /* INFRINGES ON USER NAME SPACE */
#  define YYFPRINTF fprintf
# endif

# define YYDPRINTF(Args)                        \
do {                                            \
  if (yydebug)                                  \
    YYFPRINTF Args;                             \
} while (0)

/* This macro is provided for backward compatibility. */
#ifndef YY_LOCATION_PRINT
# define YY_LOCATION_PRINT(File, Loc) ((void) 0)
#endif


# define YY_SYMBOL_PRINT(Title, Type, Value, Location)                    \
do {                                                                      \
  if (yydebug)                                                            \
    {                                                                     \
      YYFPRINTF (stderr, "%s ", Title);                                   \
      yy_symbol_print (stderr,                                            \
                  Type, Value); \
      YYFPRINTF (stderr, "\n");                                           \
    }                                                                     \
} while (0)


/*----------------------------------------.
| Print this symbol's value on YYOUTPUT.  |
`----------------------------------------*/

static void
yy_symbol_value_print (FILE *yyoutput, int yytype, YYSTYPE const * const yyvaluep)
{
  FILE *yyo = yyoutput;
  YYUSE (yyo);
  if (!yyvaluep)
    return;
# ifdef YYPRINT
  if (yytype < YYNTOKENS)
    YYPRINT (yyoutput, yytoknum[yytype], *yyvaluep);
# endif
  YYUSE (yytype);
}


/*--------------------------------.
| Print this symbol on YYOUTPUT.  |
`--------------------------------*/

static void
yy_symbol_print (FILE *yyoutput, int yytype, YYSTYPE const * const yyvaluep)
{
  YYFPRINTF (yyoutput, "%s %s (",
             yytype < YYNTOKENS ? "token" : "nterm", yytname[yytype]);

  yy_symbol_value_print (yyoutput, yytype, yyvaluep);
  YYFPRINTF (yyoutput, ")");
}

/*------------------------------------------------------------------.
| yy_stack_print -- Print the state stack from its BOTTOM up to its |
| TOP (included).                                                   |
`------------------------------------------------------------------*/

static void
yy_stack_print (yytype_int16 *yybottom, yytype_int16 *yytop)
{
  YYFPRINTF (stderr, "Stack now");
  for (; yybottom <= yytop; yybottom++)
    {
      int yybot = *yybottom;
      YYFPRINTF (stderr, " %d", yybot);
    }
  YYFPRINTF (stderr, "\n");
}

# define YY_STACK_PRINT(Bottom, Top)                            \
do {                                                            \
  if (yydebug)                                                  \
    yy_stack_print ((Bottom), (Top));                           \
} while (0)


/*------------------------------------------------.
| Report that the YYRULE is going to be reduced.  |
`------------------------------------------------*/

static void
yy_reduce_print (yytype_int16 *yyssp, YYSTYPE *yyvsp, int yyrule)
{
  unsigned long int yylno = yyrline[yyrule];
  int yynrhs = yyr2[yyrule];
  int yyi;
  YYFPRINTF (stderr, "Reducing stack by rule %d (line %lu):\n",
             yyrule - 1, yylno);
  /* The symbols being reduced.  */
  for (yyi = 0; yyi < yynrhs; yyi++)
    {
      YYFPRINTF (stderr, "   $%d = ", yyi + 1);
      yy_symbol_print (stderr,
                       yystos[yyssp[yyi + 1 - yynrhs]],
                       &(yyvsp[(yyi + 1) - (yynrhs)])
                                              );
      YYFPRINTF (stderr, "\n");
    }
}

# define YY_REDUCE_PRINT(Rule)          \
do {                                    \
  if (yydebug)                          \
    yy_reduce_print (yyssp, yyvsp, Rule); \
} while (0)

/* Nonzero means print parse trace.  It is left uninitialized so that
   multiple parsers can coexist.  */
int yydebug;
#else /* !YYDEBUG */
# define YYDPRINTF(Args)
# define YY_SYMBOL_PRINT(Title, Type, Value, Location)
# define YY_STACK_PRINT(Bottom, Top)
# define YY_REDUCE_PRINT(Rule)
#endif /* !YYDEBUG */


/* YYINITDEPTH -- initial size of the parser's stacks.  */
#ifndef YYINITDEPTH
# define YYINITDEPTH 200
#endif

/* YYMAXDEPTH -- maximum size the stacks can grow to (effective only
   if the built-in stack extension method is used).

   Do not make this value too large; the results are undefined if
   YYSTACK_ALLOC_MAXIMUM < YYSTACK_BYTES (YYMAXDEPTH)
   evaluated with infinite-precision integer arithmetic.  */

#ifndef YYMAXDEPTH
# define YYMAXDEPTH 10000
#endif


#if YYERROR_VERBOSE

# ifndef yystrlen
#  if defined __GLIBC__ && defined _STRING_H
#   define yystrlen strlen
#  else
/* Return the length of YYSTR.  */
static YYSIZE_T
yystrlen (const char *yystr)
{
  YYSIZE_T yylen;
  for (yylen = 0; yystr[yylen]; yylen++)
    continue;
  return yylen;
}
#  endif
# endif

# ifndef yystpcpy
#  if defined __GLIBC__ && defined _STRING_H && defined _GNU_SOURCE
#   define yystpcpy stpcpy
#  else
/* Copy YYSRC to YYDEST, returning the address of the terminating '\0' in
   YYDEST.  */
static char *
yystpcpy (char *yydest, const char *yysrc)
{
  char *yyd = yydest;
  const char *yys = yysrc;

  while ((*yyd++ = *yys++) != '\0')
    continue;

  return yyd - 1;
}
#  endif
# endif

# ifndef yytnamerr
/* Copy to YYRES the contents of YYSTR after stripping away unnecessary
   quotes and backslashes, so that it's suitable for yyerror.  The
   heuristic is that double-quoting is unnecessary unless the string
   contains an apostrophe, a comma, or backslash (other than
   backslash-backslash).  YYSTR is taken from yytname.  If YYRES is
   null, do not copy; instead, return the length of what the result
   would have been.  */
static YYSIZE_T
yytnamerr (char *yyres, const char *yystr)
{
  if (*yystr == '"')
    {
      YYSIZE_T yyn = 0;
      char const *yyp = yystr;

      for (;;)
        switch (*++yyp)
          {
          case '\'':
          case ',':
            goto do_not_strip_quotes;

          case '\\':
            if (*++yyp != '\\')
              goto do_not_strip_quotes;
            /* Fall through.  */
          default:
            if (yyres)
              yyres[yyn] = *yyp;
            yyn++;
            break;

          case '"':
            if (yyres)
              yyres[yyn] = '\0';
            return yyn;
          }
    do_not_strip_quotes: ;
    }

  if (! yyres)
    return yystrlen (yystr);

  return yystpcpy (yyres, yystr) - yyres;
}
# endif

/* Copy into *YYMSG, which is of size *YYMSG_ALLOC, an error message
   about the unexpected token YYTOKEN for the state stack whose top is
   YYSSP.

   Return 0 if *YYMSG was successfully written.  Return 1 if *YYMSG is
   not large enough to hold the message.  In that case, also set
   *YYMSG_ALLOC to the required number of bytes.  Return 2 if the
   required number of bytes is too large to store.  */
static int
yysyntax_error (YYSIZE_T *yymsg_alloc, char **yymsg,
                yytype_int16 *yyssp, int yytoken)
{
  YYSIZE_T yysize0 = yytnamerr (YY_NULLPTR, yytname[yytoken]);
  YYSIZE_T yysize = yysize0;
  enum { YYERROR_VERBOSE_ARGS_MAXIMUM = 5 };
  /* Internationalized format string. */
  const char *yyformat = YY_NULLPTR;
  /* Arguments of yyformat. */
  char const *yyarg[YYERROR_VERBOSE_ARGS_MAXIMUM];
  /* Number of reported tokens (one for the "unexpected", one per
     "expected"). */
  int yycount = 0;

  /* There are many possibilities here to consider:
     - If this state is a consistent state with a default action, then
       the only way this function was invoked is if the default action
       is an error action.  In that case, don't check for expected
       tokens because there are none.
     - The only way there can be no lookahead present (in yychar) is if
       this state is a consistent state with a default action.  Thus,
       detecting the absence of a lookahead is sufficient to determine
       that there is no unexpected or expected token to report.  In that
       case, just report a simple "syntax error".
     - Don't assume there isn't a lookahead just because this state is a
       consistent state with a default action.  There might have been a
       previous inconsistent state, consistent state with a non-default
       action, or user semantic action that manipulated yychar.
     - Of course, the expected token list depends on states to have
       correct lookahead information, and it depends on the parser not
       to perform extra reductions after fetching a lookahead from the
       scanner and before detecting a syntax error.  Thus, state merging
       (from LALR or IELR) and default reductions corrupt the expected
       token list.  However, the list is correct for canonical LR with
       one exception: it will still contain any token that will not be
       accepted due to an error action in a later state.
  */
  if (yytoken != YYEMPTY)
    {
      int yyn = yypact[*yyssp];
      yyarg[yycount++] = yytname[yytoken];
      if (!yypact_value_is_default (yyn))
        {
          /* Start YYX at -YYN if negative to avoid negative indexes in
             YYCHECK.  In other words, skip the first -YYN actions for
             this state because they are default actions.  */
          int yyxbegin = yyn < 0 ? -yyn : 0;
          /* Stay within bounds of both yycheck and yytname.  */
          int yychecklim = YYLAST - yyn + 1;
          int yyxend = yychecklim < YYNTOKENS ? yychecklim : YYNTOKENS;
          int yyx;

          for (yyx = yyxbegin; yyx < yyxend; ++yyx)
            if (yycheck[yyx + yyn] == yyx && yyx != YYTERROR
                && !yytable_value_is_error (yytable[yyx + yyn]))
              {
                if (yycount == YYERROR_VERBOSE_ARGS_MAXIMUM)
                  {
                    yycount = 1;
                    yysize = yysize0;
                    break;
                  }
                yyarg[yycount++] = yytname[yyx];
                {
                  YYSIZE_T yysize1 = yysize + yytnamerr (YY_NULLPTR, yytname[yyx]);
                  if (! (yysize <= yysize1
                         && yysize1 <= YYSTACK_ALLOC_MAXIMUM))
                    return 2;
                  yysize = yysize1;
                }
              }
        }
    }

  switch (yycount)
    {
# define YYCASE_(N, S)                      \
      case N:                               \
        yyformat = S;                       \
      break
      YYCASE_(0, YY_("syntax error"));
      YYCASE_(1, YY_("syntax error, unexpected %s"));
      YYCASE_(2, YY_("syntax error, unexpected %s, expecting %s"));
      YYCASE_(3, YY_("syntax error, unexpected %s, expecting %s or %s"));
      YYCASE_(4, YY_("syntax error, unexpected %s, expecting %s or %s or %s"));
      YYCASE_(5, YY_("syntax error, unexpected %s, expecting %s or %s or %s or %s"));
# undef YYCASE_
    }

  {
    YYSIZE_T yysize1 = yysize + yystrlen (yyformat);
    if (! (yysize <= yysize1 && yysize1 <= YYSTACK_ALLOC_MAXIMUM))
      return 2;
    yysize = yysize1;
  }

  if (*yymsg_alloc < yysize)
    {
      *yymsg_alloc = 2 * yysize;
      if (! (yysize <= *yymsg_alloc
             && *yymsg_alloc <= YYSTACK_ALLOC_MAXIMUM))
        *yymsg_alloc = YYSTACK_ALLOC_MAXIMUM;
      return 1;
    }

  /* Avoid sprintf, as that infringes on the user's name space.
     Don't have undefined behavior even if the translation
     produced a string with the wrong number of "%s"s.  */
  {
    char *yyp = *yymsg;
    int yyi = 0;
    while ((*yyp = *yyformat) != '\0')
      if (*yyp == '%' && yyformat[1] == 's' && yyi < yycount)
        {
          yyp += yytnamerr (yyp, yyarg[yyi++]);
          yyformat += 2;
        }
      else
        {
          yyp++;
          yyformat++;
        }
  }
  return 0;
}
#endif /* YYERROR_VERBOSE */

/*-----------------------------------------------.
| Release the memory associated to this symbol.  |
`-----------------------------------------------*/

static void
yydestruct (const char *yymsg, int yytype, YYSTYPE *yyvaluep)
{
  YYUSE (yyvaluep);
  if (!yymsg)
    yymsg = "Deleting";
  YY_SYMBOL_PRINT (yymsg, yytype, yyvaluep, yylocationp);

  YY_IGNORE_MAYBE_UNINITIALIZED_BEGIN
  YYUSE (yytype);
  YY_IGNORE_MAYBE_UNINITIALIZED_END
}




/* The lookahead symbol.  */
int yychar;

/* The semantic value of the lookahead symbol.  */
YYSTYPE yylval;
/* Number of syntax errors so far.  */
int yynerrs;


/*----------.
| yyparse.  |
`----------*/

int
yyparse (void)
{
    int yystate;
    /* Number of tokens to shift before error messages enabled.  */
    int yyerrstatus;

    /* The stacks and their tools:
       'yyss': related to states.
       'yyvs': related to semantic values.

       Refer to the stacks through separate pointers, to allow yyoverflow
       to reallocate them elsewhere.  */

    /* The state stack.  */
    yytype_int16 yyssa[YYINITDEPTH];
    yytype_int16 *yyss;
    yytype_int16 *yyssp;

    /* The semantic value stack.  */
    YYSTYPE yyvsa[YYINITDEPTH];
    YYSTYPE *yyvs;
    YYSTYPE *yyvsp;

    YYSIZE_T yystacksize;

  int yyn;
  int yyresult;
  /* Lookahead token as an internal (translated) token number.  */
  int yytoken = 0;
  /* The variables used to return semantic value and location from the
     action routines.  */
  YYSTYPE yyval;

#if YYERROR_VERBOSE
  /* Buffer for error messages, and its allocated size.  */
  char yymsgbuf[128];
  char *yymsg = yymsgbuf;
  YYSIZE_T yymsg_alloc = sizeof yymsgbuf;
#endif

#define YYPOPSTACK(N)   (yyvsp -= (N), yyssp -= (N))

  /* The number of symbols on the RHS of the reduced rule.
     Keep to zero when no symbol should be popped.  */
  int yylen = 0;

  yyssp = yyss = yyssa;
  yyvsp = yyvs = yyvsa;
  yystacksize = YYINITDEPTH;

  YYDPRINTF ((stderr, "Starting parse\n"));

  yystate = 0;
  yyerrstatus = 0;
  yynerrs = 0;
  yychar = YYEMPTY; /* Cause a token to be read.  */
  goto yysetstate;

/*------------------------------------------------------------.
| yynewstate -- Push a new state, which is found in yystate.  |
`------------------------------------------------------------*/
 yynewstate:
  /* In all cases, when you get here, the value and location stacks
     have just been pushed.  So pushing a state here evens the stacks.  */
  yyssp++;

 yysetstate:
  *yyssp = yystate;

  if (yyss + yystacksize - 1 <= yyssp)
    {
      /* Get the current used size of the three stacks, in elements.  */
      YYSIZE_T yysize = yyssp - yyss + 1;

#ifdef yyoverflow
      {
        /* Give user a chance to reallocate the stack.  Use copies of
           these so that the &'s don't force the real ones into
           memory.  */
        YYSTYPE *yyvs1 = yyvs;
        yytype_int16 *yyss1 = yyss;

        /* Each stack pointer address is followed by the size of the
           data in use in that stack, in bytes.  This used to be a
           conditional around just the two extra args, but that might
           be undefined if yyoverflow is a macro.  */
        yyoverflow (YY_("memory exhausted"),
                    &yyss1, yysize * sizeof (*yyssp),
                    &yyvs1, yysize * sizeof (*yyvsp),
                    &yystacksize);

        yyss = yyss1;
        yyvs = yyvs1;
      }
#else /* no yyoverflow */
# ifndef YYSTACK_RELOCATE
      goto yyexhaustedlab;
# else
      /* Extend the stack our own way.  */
      if (YYMAXDEPTH <= yystacksize)
        goto yyexhaustedlab;
      yystacksize *= 2;
      if (YYMAXDEPTH < yystacksize)
        yystacksize = YYMAXDEPTH;

      {
        yytype_int16 *yyss1 = yyss;
        union yyalloc *yyptr =
          (union yyalloc *) YYSTACK_ALLOC (YYSTACK_BYTES (yystacksize));
        if (! yyptr)
          goto yyexhaustedlab;
        YYSTACK_RELOCATE (yyss_alloc, yyss);
        YYSTACK_RELOCATE (yyvs_alloc, yyvs);
#  undef YYSTACK_RELOCATE
        if (yyss1 != yyssa)
          YYSTACK_FREE (yyss1);
      }
# endif
#endif /* no yyoverflow */

      yyssp = yyss + yysize - 1;
      yyvsp = yyvs + yysize - 1;

      YYDPRINTF ((stderr, "Stack size increased to %lu\n",
                  (unsigned long int) yystacksize));

      if (yyss + yystacksize - 1 <= yyssp)
        YYABORT;
    }

  YYDPRINTF ((stderr, "Entering state %d\n", yystate));

  if (yystate == YYFINAL)
    YYACCEPT;

  goto yybackup;

/*-----------.
| yybackup.  |
`-----------*/
yybackup:

  /* Do appropriate processing given the current state.  Read a
     lookahead token if we need one and don't already have one.  */

  /* First try to decide what to do without reference to lookahead token.  */
  yyn = yypact[yystate];
  if (yypact_value_is_default (yyn))
    goto yydefault;

  /* Not known => get a lookahead token if don't already have one.  */

  /* YYCHAR is either YYEMPTY or YYEOF or a valid lookahead symbol.  */
  if (yychar == YYEMPTY)
    {
      YYDPRINTF ((stderr, "Reading a token: "));
      yychar = yylex ();
    }

  if (yychar <= YYEOF)
    {
      yychar = yytoken = YYEOF;
      YYDPRINTF ((stderr, "Now at end of input.\n"));
    }
  else
    {
      yytoken = YYTRANSLATE (yychar);
      YY_SYMBOL_PRINT ("Next token is", yytoken, &yylval, &yylloc);
    }

  /* If the proper action on seeing token YYTOKEN is to reduce or to
     detect an error, take that action.  */
  yyn += yytoken;
  if (yyn < 0 || YYLAST < yyn || yycheck[yyn] != yytoken)
    goto yydefault;
  yyn = yytable[yyn];
  if (yyn <= 0)
    {
      if (yytable_value_is_error (yyn))
        goto yyerrlab;
      yyn = -yyn;
      goto yyreduce;
    }

  /* Count tokens shifted since error; after three, turn off error
     status.  */
  if (yyerrstatus)
    yyerrstatus--;

  /* Shift the lookahead token.  */
  YY_SYMBOL_PRINT ("Shifting", yytoken, &yylval, &yylloc);

  /* Discard the shifted token.  */
  yychar = YYEMPTY;

  yystate = yyn;
  YY_IGNORE_MAYBE_UNINITIALIZED_BEGIN
  *++yyvsp = yylval;
  YY_IGNORE_MAYBE_UNINITIALIZED_END

  goto yynewstate;


/*-----------------------------------------------------------.
| yydefault -- do the default action for the current state.  |
`-----------------------------------------------------------*/
yydefault:
  yyn = yydefact[yystate];
  if (yyn == 0)
    goto yyerrlab;
  goto yyreduce;


/*-----------------------------.
| yyreduce -- Do a reduction.  |
`-----------------------------*/
yyreduce:
  /* yyn is the number of a rule to reduce with.  */
  yylen = yyr2[yyn];

  /* If YYLEN is nonzero, implement the default value of the action:
     '$$ = $1'.

     Otherwise, the following line sets YYVAL to garbage.
     This behavior is undocumented and Bison
     users should not rely upon it.  Assigning to YYVAL
     unconditionally makes the parser a bit smaller, and it avoids a
     GCC warning that YYVAL may be used uninitialized.  */
  yyval = yyvsp[1-yylen];


  YY_REDUCE_PRINT (yyn);
  switch (yyn)
    {
        case 2:
#line 136 "teacParser.y" /* yacc.c:1646  */
    {  fp = fopen(FILENAME, "a");
  		                 fprintf(fp, "%s",(yyvsp[0].str));
  		                 fclose(fp);
                         if (yyerror_count == 0) {
                           puts(c_prologue);
                           printf("Expression evaluates to: %s\n", (yyvsp[0].str)); 
                         }
  		              }
#line 1552 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 3:
#line 146 "teacParser.y" /* yacc.c:1646  */
    {  (yyval.str) = template( "%s", (yyvsp[0].str)); }
#line 1558 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 4:
#line 148 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s %s;\n%s", (yyvsp[-2].str), (yyvsp[-3].str), (yyvsp[0].str)); }
#line 1564 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 5:
#line 149 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("char* %s;\n%s", (yyvsp[-3].str), (yyvsp[0].str)); }
#line 1570 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 6:
#line 151 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("const %s %s=%s;\n%s", (yyvsp[-2].str), (yyvsp[-6].str), (yyvsp[-4].str), (yyvsp[0].str)); }
#line 1576 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 7:
#line 152 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("const char* %s=%s;\n%s", (yyvsp[-7].str), (yyvsp[-4].str), (yyvsp[0].str)); }
#line 1582 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 8:
#line 154 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s\n%s",(yyvsp[-1].str), (yyvsp[0].str)); }
#line 1588 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 9:
#line 158 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template( "\nint main()\n{\n%s\n}\n\n", (yyvsp[-1].str) ); }
#line 1594 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 10:
#line 162 "teacParser.y" /* yacc.c:1646  */
    { printf("\nThe input program is correct !!!\n");
            fp = fopen(FILENAME, "a");
            fprintf(fp, "/* Antonis Maragoudakis\nA.M = 2013030093 */\n\n");
            fprintf(fp, "#include <stdio.h>\n");
            fprintf(fp, "#include <stdlib.h>\n");
            fprintf(fp, "#include \"teaclib.h\"\n\n");
            fclose(fp); }
#line 1606 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 11:
#line 171 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s\n", (yyvsp[0].str)); }
#line 1612 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 12:
#line 172 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s\n%s", (yyvsp[-1].str), (yyvsp[0].str)); }
#line 1618 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 13:
#line 177 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("\n%s %s(%s)\n{\n%s}", (yyvsp[-5].str), (yyvsp[-10].str), (yyvsp[-7].str), (yyvsp[-2].str)); }
#line 1624 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 14:
#line 178 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("\nchar[] %s(%s)\n{\n%s}", (yyvsp[-10].str), (yyvsp[-7].str), (yyvsp[-2].str)); }
#line 1630 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 15:
#line 180 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s", (yyvsp[-1].str) ); }
#line 1636 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 16:
#line 181 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s, %s", (yyvsp[-2].str), (yyvsp[0].str) ); }
#line 1642 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 17:
#line 183 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template(""); }
#line 1648 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 18:
#line 184 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s %s%s", (yyvsp[0].str), (yyvsp[-3].str), (yyvsp[-2].str)); }
#line 1654 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 19:
#line 185 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s %s[]", (yyvsp[0].str), (yyvsp[-4].str)); }
#line 1660 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 20:
#line 186 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("char* %s", (yyvsp[-3].str)); }
#line 1666 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 21:
#line 187 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("char* %s", (yyvsp[-4].str)); }
#line 1672 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 22:
#line 188 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s %s%s ,%s", (yyvsp[0].str), (yyvsp[-5].str), (yyvsp[-4].str), (yyvsp[-2].str) ); }
#line 1678 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 23:
#line 189 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s %s[] ,%s", (yyvsp[0].str), (yyvsp[-6].str), (yyvsp[-2].str) ); }
#line 1684 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 24:
#line 190 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("char* %s ,%s", (yyvsp[-5].str), (yyvsp[-4].str), (yyvsp[-2].str) ); }
#line 1690 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 25:
#line 191 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("char* %s, %s", (yyvsp[-6].str), (yyvsp[-2].str) ); }
#line 1696 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 26:
#line 198 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s %s;", (yyvsp[-1].str), (yyvsp[-2].str)); }
#line 1702 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 27:
#line 199 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("char* %s;", (yyvsp[-2].str)); }
#line 1708 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 28:
#line 201 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s%s=%s;", (yyvsp[-4].str),(yyvsp[-3].str) , (yyvsp[-1].str)); }
#line 1714 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 29:
#line 202 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s%s=%s;", (yyvsp[-4].str),(yyvsp[-3].str) , (yyvsp[-1].str)); }
#line 1720 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 30:
#line 204 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("printf(\"%%s\", %s);", (yyvsp[-2].str)); }
#line 1726 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 31:
#line 205 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("printf(\"%%s\", %s);", (yyvsp[-2].str)); }
#line 1732 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 32:
#line 206 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("printf(\"%%d\", %s);", (yyvsp[-2].str)); }
#line 1738 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 33:
#line 207 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("printf(\"%%g\", %s);", (yyvsp[-2].str)); }
#line 1744 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 34:
#line 209 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("return %s;", (yyvsp[-1].str)); }
#line 1750 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 35:
#line 211 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("if %s", (yyvsp[0].str)); }
#line 1756 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 36:
#line 212 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = (yyvsp[0].str);}
#line 1762 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 37:
#line 216 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s(%s);", (yyvsp[-4].str), (yyvsp[-2].str) ); }
#line 1768 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 38:
#line 218 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("const %s %s=%s;", (yyvsp[-1].str), (yyvsp[-5].str), (yyvsp[-3].str) ); }
#line 1774 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 39:
#line 219 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("const char* %s=%s;", (yyvsp[-6].str), (yyvsp[-3].str) ); }
#line 1780 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 40:
#line 224 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s", (yyvsp[-1].str) ); }
#line 1786 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 41:
#line 225 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s%s", (yyvsp[-2].str), (yyvsp[-1].str) ); }
#line 1792 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 42:
#line 226 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s=%s", (yyvsp[-3].str), (yyvsp[-1].str) ); }
#line 1798 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 43:
#line 227 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s, %s", (yyvsp[-2].str), (yyvsp[0].str) ); }
#line 1804 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 44:
#line 228 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s%s, %s", (yyvsp[-3].str) ,(yyvsp[-2].str) ,(yyvsp[0].str) ); }
#line 1810 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 45:
#line 229 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s=%s, %s", (yyvsp[-4].str), (yyvsp[-2].str), (yyvsp[0].str) ); }
#line 1816 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 46:
#line 231 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s%s", (yyvsp[-2].str),(yyvsp[-1].str) ); }
#line 1822 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 47:
#line 232 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s%s=%s", (yyvsp[-4].str),(yyvsp[-3].str),(yyvsp[-1].str) ); }
#line 1828 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 48:
#line 233 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s%s, %s", (yyvsp[-3].str) ,(yyvsp[-2].str), (yyvsp[0].str) ); }
#line 1834 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 49:
#line 234 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s%s=%s, %s", (yyvsp[-5].str) ,(yyvsp[-4].str), (yyvsp[-2].str), (yyvsp[0].str) ); }
#line 1840 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 50:
#line 238 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("[]"); }
#line 1846 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 51:
#line 239 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("[%s]", (yyvsp[-1].str) ); }
#line 1852 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 52:
#line 241 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template(""); }
#line 1858 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 53:
#line 242 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("[%s]", (yyvsp[-1].str)); }
#line 1864 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 54:
#line 246 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template(""); }
#line 1870 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 55:
#line 247 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s", (yyvsp[0].str)); }
#line 1876 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 56:
#line 248 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s, %s", (yyvsp[-2].str), (yyvsp[0].str)); }
#line 1882 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 57:
#line 252 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("int"); }
#line 1888 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 58:
#line 253 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("double"); }
#line 1894 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 59:
#line 254 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("int"); }
#line 1900 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 60:
#line 255 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("int*"); }
#line 1906 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 61:
#line 256 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("double*"); }
#line 1912 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 62:
#line 257 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("int*"); }
#line 1918 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 63:
#line 258 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("char**"); }
#line 1924 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 64:
#line 265 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s ", (yyvsp[0].str)); }
#line 1930 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 65:
#line 266 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s ", (yyvsp[0].str)); }
#line 1936 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 66:
#line 267 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s ", (yyvsp[0].str)); }
#line 1942 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 67:
#line 268 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s%s ", (yyvsp[-1].str), (yyvsp[0].str)); }
#line 1948 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 68:
#line 269 "teacParser.y" /* yacc.c:1646  */
    {(yyval.str) = template("1");}
#line 1954 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 69:
#line 270 "teacParser.y" /* yacc.c:1646  */
    {(yyval.str) = template("0");}
#line 1960 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 70:
#line 271 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("(%s)", (yyvsp[-1].str)); }
#line 1966 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 71:
#line 272 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s(%s)", (yyvsp[-3].str), (yyvsp[-1].str) ); }
#line 1972 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 72:
#line 273 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("+%s ", (yyvsp[0].str)); }
#line 1978 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 73:
#line 274 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("-%s ", (yyvsp[0].str)); }
#line 1984 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 74:
#line 275 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template( "not%s", (yyvsp[0].str)); }
#line 1990 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 75:
#line 276 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s+%s", (yyvsp[-2].str), (yyvsp[0].str)); }
#line 1996 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 76:
#line 277 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s-%s", (yyvsp[-2].str), (yyvsp[0].str)); }
#line 2002 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 77:
#line 278 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s*%s", (yyvsp[-2].str), (yyvsp[0].str)); }
#line 2008 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 78:
#line 279 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s/%s", (yyvsp[-2].str), (yyvsp[0].str)); }
#line 2014 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 79:
#line 280 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s %% %s", (yyvsp[-2].str), (yyvsp[0].str)); }
#line 2020 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 80:
#line 281 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s==%s", (yyvsp[-2].str), (yyvsp[0].str)); }
#line 2026 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 81:
#line 282 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s!=%s", (yyvsp[-2].str), (yyvsp[0].str)); }
#line 2032 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 82:
#line 283 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s<%s", (yyvsp[-2].str), (yyvsp[0].str)); }
#line 2038 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 83:
#line 284 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s<=%s", (yyvsp[-2].str), (yyvsp[0].str)); }
#line 2044 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 84:
#line 285 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s&&%s", (yyvsp[-2].str), (yyvsp[0].str)); }
#line 2050 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 85:
#line 286 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s||%s", (yyvsp[-2].str), (yyvsp[0].str)); }
#line 2056 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 86:
#line 288 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s ", (yyvsp[0].str)); }
#line 2062 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 87:
#line 289 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s ", (yyvsp[0].str)); }
#line 2068 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 88:
#line 295 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("atoi(readString())"); }
#line 2074 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 89:
#line 296 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("atof(readString())"); }
#line 2080 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 90:
#line 298 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("fgets()"); }
#line 2086 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 91:
#line 304 "teacParser.y" /* yacc.c:1646  */
    {(yyval.str) = template( "while (%s)\n{\n%s}", (yyvsp[-4].str), (yyvsp[-2].str)); }
#line 2092 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 92:
#line 307 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("(%s)\n{\n%s}", (yyvsp[-4].str), (yyvsp[-2].str)); }
#line 2098 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 93:
#line 308 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("(%s)\n{\n%s}\nelse if %s ", (yyvsp[-7].str), (yyvsp[-5].str), (yyvsp[-2].str)); }
#line 2104 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 94:
#line 309 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("(%s)\n{\n%s}\nelse\n{\n%s}", (yyvsp[-6].str), (yyvsp[-4].str), (yyvsp[-2].str)); }
#line 2110 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 95:
#line 313 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = (yyvsp[0].str); }
#line 2116 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 96:
#line 314 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s\n%s", (yyvsp[-1].str) , (yyvsp[0].str)); }
#line 2122 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 97:
#line 329 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s %s;", (yyvsp[-1].str), (yyvsp[-2].str)); }
#line 2128 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 98:
#line 330 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("char* %s;", (yyvsp[-2].str)); }
#line 2134 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 99:
#line 332 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s%s=%s;", (yyvsp[-4].str),(yyvsp[-3].str) , (yyvsp[-1].str)); }
#line 2140 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 100:
#line 333 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s%s=%s;", (yyvsp[-4].str),(yyvsp[-3].str) , (yyvsp[-1].str)); }
#line 2146 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 101:
#line 335 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("printf(\"%%s\", %s);", (yyvsp[-2].str)); }
#line 2152 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 102:
#line 336 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("printf(\"%%s\", %s);", (yyvsp[-2].str)); }
#line 2158 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 103:
#line 337 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("printf(\"%%d\", %s);", (yyvsp[-2].str)); }
#line 2164 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 104:
#line 338 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("printf(\"%%g\", %s);", (yyvsp[-2].str)); }
#line 2170 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 105:
#line 340 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("return %s;", (yyvsp[-1].str)); }
#line 2176 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 106:
#line 342 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = (yyvsp[0].str);}
#line 2182 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 107:
#line 344 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("%s(%s);", (yyvsp[-4].str), (yyvsp[-2].str) ); }
#line 2188 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 108:
#line 346 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("const %s %s=%s;", (yyvsp[-1].str), (yyvsp[-5].str), (yyvsp[-3].str) ); }
#line 2194 "teacParser.tab.c" /* yacc.c:1646  */
    break;

  case 109:
#line 347 "teacParser.y" /* yacc.c:1646  */
    { (yyval.str) = template("const char* %s=%s;", (yyvsp[-6].str), (yyvsp[-3].str) ); }
#line 2200 "teacParser.tab.c" /* yacc.c:1646  */
    break;


#line 2204 "teacParser.tab.c" /* yacc.c:1646  */
      default: break;
    }
  /* User semantic actions sometimes alter yychar, and that requires
     that yytoken be updated with the new translation.  We take the
     approach of translating immediately before every use of yytoken.
     One alternative is translating here after every semantic action,
     but that translation would be missed if the semantic action invokes
     YYABORT, YYACCEPT, or YYERROR immediately after altering yychar or
     if it invokes YYBACKUP.  In the case of YYABORT or YYACCEPT, an
     incorrect destructor might then be invoked immediately.  In the
     case of YYERROR or YYBACKUP, subsequent parser actions might lead
     to an incorrect destructor call or verbose syntax error message
     before the lookahead is translated.  */
  YY_SYMBOL_PRINT ("-> $$ =", yyr1[yyn], &yyval, &yyloc);

  YYPOPSTACK (yylen);
  yylen = 0;
  YY_STACK_PRINT (yyss, yyssp);

  *++yyvsp = yyval;

  /* Now 'shift' the result of the reduction.  Determine what state
     that goes to, based on the state we popped back to and the rule
     number reduced by.  */

  yyn = yyr1[yyn];

  yystate = yypgoto[yyn - YYNTOKENS] + *yyssp;
  if (0 <= yystate && yystate <= YYLAST && yycheck[yystate] == *yyssp)
    yystate = yytable[yystate];
  else
    yystate = yydefgoto[yyn - YYNTOKENS];

  goto yynewstate;


/*--------------------------------------.
| yyerrlab -- here on detecting error.  |
`--------------------------------------*/
yyerrlab:
  /* Make sure we have latest lookahead translation.  See comments at
     user semantic actions for why this is necessary.  */
  yytoken = yychar == YYEMPTY ? YYEMPTY : YYTRANSLATE (yychar);

  /* If not already recovering from an error, report this error.  */
  if (!yyerrstatus)
    {
      ++yynerrs;
#if ! YYERROR_VERBOSE
      yyerror (YY_("syntax error"));
#else
# define YYSYNTAX_ERROR yysyntax_error (&yymsg_alloc, &yymsg, \
                                        yyssp, yytoken)
      {
        char const *yymsgp = YY_("syntax error");
        int yysyntax_error_status;
        yysyntax_error_status = YYSYNTAX_ERROR;
        if (yysyntax_error_status == 0)
          yymsgp = yymsg;
        else if (yysyntax_error_status == 1)
          {
            if (yymsg != yymsgbuf)
              YYSTACK_FREE (yymsg);
            yymsg = (char *) YYSTACK_ALLOC (yymsg_alloc);
            if (!yymsg)
              {
                yymsg = yymsgbuf;
                yymsg_alloc = sizeof yymsgbuf;
                yysyntax_error_status = 2;
              }
            else
              {
                yysyntax_error_status = YYSYNTAX_ERROR;
                yymsgp = yymsg;
              }
          }
        yyerror (yymsgp);
        if (yysyntax_error_status == 2)
          goto yyexhaustedlab;
      }
# undef YYSYNTAX_ERROR
#endif
    }



  if (yyerrstatus == 3)
    {
      /* If just tried and failed to reuse lookahead token after an
         error, discard it.  */

      if (yychar <= YYEOF)
        {
          /* Return failure if at end of input.  */
          if (yychar == YYEOF)
            YYABORT;
        }
      else
        {
          yydestruct ("Error: discarding",
                      yytoken, &yylval);
          yychar = YYEMPTY;
        }
    }

  /* Else will try to reuse lookahead token after shifting the error
     token.  */
  goto yyerrlab1;


/*---------------------------------------------------.
| yyerrorlab -- error raised explicitly by YYERROR.  |
`---------------------------------------------------*/
yyerrorlab:

  /* Pacify compilers like GCC when the user code never invokes
     YYERROR and the label yyerrorlab therefore never appears in user
     code.  */
  if (/*CONSTCOND*/ 0)
     goto yyerrorlab;

  /* Do not reclaim the symbols of the rule whose action triggered
     this YYERROR.  */
  YYPOPSTACK (yylen);
  yylen = 0;
  YY_STACK_PRINT (yyss, yyssp);
  yystate = *yyssp;
  goto yyerrlab1;


/*-------------------------------------------------------------.
| yyerrlab1 -- common code for both syntax error and YYERROR.  |
`-------------------------------------------------------------*/
yyerrlab1:
  yyerrstatus = 3;      /* Each real token shifted decrements this.  */

  for (;;)
    {
      yyn = yypact[yystate];
      if (!yypact_value_is_default (yyn))
        {
          yyn += YYTERROR;
          if (0 <= yyn && yyn <= YYLAST && yycheck[yyn] == YYTERROR)
            {
              yyn = yytable[yyn];
              if (0 < yyn)
                break;
            }
        }

      /* Pop the current state because it cannot handle the error token.  */
      if (yyssp == yyss)
        YYABORT;


      yydestruct ("Error: popping",
                  yystos[yystate], yyvsp);
      YYPOPSTACK (1);
      yystate = *yyssp;
      YY_STACK_PRINT (yyss, yyssp);
    }

  YY_IGNORE_MAYBE_UNINITIALIZED_BEGIN
  *++yyvsp = yylval;
  YY_IGNORE_MAYBE_UNINITIALIZED_END


  /* Shift the error token.  */
  YY_SYMBOL_PRINT ("Shifting", yystos[yyn], yyvsp, yylsp);

  yystate = yyn;
  goto yynewstate;


/*-------------------------------------.
| yyacceptlab -- YYACCEPT comes here.  |
`-------------------------------------*/
yyacceptlab:
  yyresult = 0;
  goto yyreturn;

/*-----------------------------------.
| yyabortlab -- YYABORT comes here.  |
`-----------------------------------*/
yyabortlab:
  yyresult = 1;
  goto yyreturn;

#if !defined yyoverflow || YYERROR_VERBOSE
/*-------------------------------------------------.
| yyexhaustedlab -- memory exhaustion comes here.  |
`-------------------------------------------------*/
yyexhaustedlab:
  yyerror (YY_("memory exhausted"));
  yyresult = 2;
  /* Fall through.  */
#endif

yyreturn:
  if (yychar != YYEMPTY)
    {
      /* Make sure we have latest lookahead translation.  See comments at
         user semantic actions for why this is necessary.  */
      yytoken = YYTRANSLATE (yychar);
      yydestruct ("Cleanup: discarding lookahead",
                  yytoken, &yylval);
    }
  /* Do not reclaim the symbols of the rule whose action triggered
     this YYABORT or YYACCEPT.  */
  YYPOPSTACK (yylen);
  YY_STACK_PRINT (yyss, yyssp);
  while (yyssp != yyss)
    {
      yydestruct ("Cleanup: popping",
                  yystos[*yyssp], yyvsp);
      YYPOPSTACK (1);
    }
#ifndef yyoverflow
  if (yyss != yyssa)
    YYSTACK_FREE (yyss);
#endif
#if YYERROR_VERBOSE
  if (yymsg != yymsgbuf)
    YYSTACK_FREE (yymsg);
#endif
  return yyresult;
}
#line 351 "teacParser.y" /* yacc.c:1906  */

int main () {
  if ( yyparse() == 0 )
    printf("Accepted!\n \n");
  else
    printf("Rejected!\n \n");
}
