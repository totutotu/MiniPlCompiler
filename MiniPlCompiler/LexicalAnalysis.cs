using System;
using System.Text;
using System.Collections.Generic;
using Helpers;

namespace LexicalAnalysis
{
  class Scanner
  {
    private String[] reservedWords = { "var", "int", "print", "for",
    "print", "end", "in", "do", "bool", "assert", "read", "string" };
    private Char[] operators = { '+', '-', '*', '/', '<', '=', '&' };
    private String program;
    private String currentToken = "";
    private StringBuilder sb = new StringBuilder();
    private Char currentChar;
    private List<Token> tokens = new List<Token>();
    private Boolean isInteger = false;
    private Boolean isString = false;
    private Boolean isColon = false;
    private Boolean error = false;

    public Scanner(String text)
    {
      program = text;
    }

    public List<Token> scan()
    {
      for (int i = 0; i < program.Length; i++)
      {
        currentChar = program[i];

        if (sb.Length == 0)
        {
          isInteger = Char.IsNumber(currentChar);
        }
        if (isWhiteSpace(currentChar))
        {
          handleCompletedToken(sb.ToString());
          sb.Clear();
          continue;
        }
        else if (isColon)
        {
          if (currentChar == '=')
          {
            sb.Append(currentChar.ToString());
            handleCompletedToken(sb.ToString());
            sb.Clear();
            isColon = false;

            continue;
          }
          else
          {
            handleCompletedToken(sb.ToString());
            sb.Clear();
          }
          isColon = false;
        }
        if (currentChar == ';')
        {
          handleCompletedToken(sb.ToString());
          handleEndLine(currentChar);
          sb.Clear();

        }
        else if (isOperator(currentChar))
        {
          handleCompletedToken(sb.ToString());
          handleOperators(currentChar);
          sb.Clear();
        }
        else if (currentChar == '!')
        {
          handleCompletedToken(sb.ToString());
          handleUnaryOperators(currentChar);
          sb.Clear();
        }
        else if (currentChar == ')' || currentChar == '(')
        {
          handleCompletedToken(sb.ToString());
          handleParenthesis(currentChar);
          sb.Clear();
        }
        else if (currentChar == ':')
        {
          sb.Append(currentChar.ToString());
          isColon = true;
        }
        else if (true)
        {
          if (isInteger)
          {
            if (!Char.IsNumber(currentChar))
            {
              error = true;
            }
          }
          sb.Append(currentChar);
        }
      }

      tokens.ForEach(token => Console.WriteLine(token.Lexeme + ", Type: " + token.Kind));
      return tokens;
    }

    public void handleOperators(Char a)
    {
      tokens.Add(new Token() { Kind = "Operator", Lexeme = a.ToString() });

      //All this should be done in the parser probably

      // switch (a)
      // {
      //   case '+':
      //     //CurrentToken complete
      //     break;
      //   case '-':
      //     //CurrentToken complete
      //     break;
      //   case '*':
      //     //CurrentToken complete
      //     break;
      //   case '/':
      //     //CurrentToken complete
      //     break;
      //   case '<':
      //     //CurrentToken complete
      //     break;
      //   case '=':
      //     //CurrentToken complete
      //     break;
      //   case '&':
      //     break;
      //   case '!': //Be careful! This is a super operator
      //     //CurrentToken complete
      //     break;
      //   default:
      //     Console.WriteLine("Unexpected, no operator, shouldn't be here");
      //     break;
      // }

    }

    public void handleUnaryOperators(Char a)
    {
      tokens.Add(new Token() { Kind = "UnOperator", Lexeme = a.ToString() });
    }

    public void handleEndLine(Char a)
    {
      tokens.Add(new Token() { Kind = "End", Lexeme = a.ToString() });
    }

    public Boolean isOperator(Char a)
    {
      foreach (char b in operators)
      {
        if (a == b) return true;
      }
      return false;
    }

    public void handleParenthesis(Char a)
    {
      if (a == '(')
      {
        tokens.Add(new Token() { Kind = "ParenthStart", Lexeme = a.ToString() });

      }
      else if (a == ')')
      {
        tokens.Add(new Token() { Kind = "ParenthClose", Lexeme = a.ToString() });

      }
    }
    public Boolean isWhiteSpace(char a)
    {
      return (a == ' ' || a == '\n' || a == '\t');
    }
    public Boolean isReservedWord(String word)
    {
      foreach (String res in reservedWords)
      {
        if (res == word) return true;
      }
      return false;
    }

    public void handleCompletedToken(String token)
    {
      if (error)
      {
        createErrorToken(sb.ToString(), "BadInt");
        error = false;
      }
      else if (isInteger)
      {
        tokens.Add(new Token() { Kind = "Int", Lexeme = token });
        isInteger = false;
      }
      else if (isReservedWord(token))
      {
        if (token == "int" || token == "string" || token == "bool")
        {
          tokens.Add(new Token() { Kind = "Type", Lexeme = token });
        }
        else if (token == "end" || token == "in" || token == "do")
        {
          tokens.Add(new Token() { Kind = "ForWords", Lexeme = token });
        }
        else if (token == "for")
        {
          tokens.Add(new Token() { Kind = "For", Lexeme = token });
        }
        else if (token == "print")
        {
          tokens.Add(new Token() { Kind = "Print", Lexeme = token });
        }
        else if (token == "assert")
        {
          tokens.Add(new Token() { Kind = "Assert", Lexeme = token });
        }
        else if (token == "read")
        {
          tokens.Add(new Token() { Kind = "Read", Lexeme = token });
        }
        else if (token == "var")
        {
          tokens.Add(new Token() { Kind = "Var", Lexeme = token });
        }
        else
        {
          tokens.Add(new Token() { Kind = "ResWord", Lexeme = token });
        }
      }
      else if (isString)
      {

      }
      else if (token == ":=")
      {
        tokens.Add(new Token() { Kind = "Assign", Lexeme = token });

      }
      else if (token == ":")
      {
        tokens.Add(new Token() { Kind = "Introduce", Lexeme = token });
      }
      else if (token.Length > 0)
      {
        tokens.Add(new Token() { Kind = "Identifier", Lexeme = token });
      }
    }
    public void createErrorToken(String token, String kind)
    {
      if (kind == "BadInt")
      {
        tokens.Add(new Token() { Kind = "Error", Lexeme = "Lexical error: Bad integer: " + token });
      }
    }
  }
}