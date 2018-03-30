using System;
using System.Text;

namespace LexicalAnalysis
{
  class Scanner
  {
    private String[] reservedWords = { "var", "int", "print", "for",
    "print", "end", "in", "do", "bool", "assert", "read", "string" };
    private Char[] operators = { '+', '-', '*', '/', '<', '=', '&', '!' };
    private String program;
    private String currentToken = "";
    private StringBuilder sb = new StringBuilder();
    private Char currentChar;
    public Scanner(String text)
    {
      program = text;
    }

    public void scan()
    {
      for (int i = 0; i < program.Length; i++)
      {
        currentChar = program[i];

        if (currentChar == ' ' || currentChar == ';')
        {
          handleCompletedToken(sb.ToString());
          sb.Clear();
        }

        else if (isOperator(currentChar))
        {
          //Is an operator
          handleOperators(currentChar);
        }
        else if (true) // read the next char to complete token
        {
          sb.Append(currentChar);
        }
        else if (true) // read the next char to complete token
        {
          sb.Append(currentChar);
        }
      }
    }

    public void handleOperators(Char a)
    {
      switch (a)
      {
        case '+':
          //CurrentToken complete
          break;
        case '-':
          //CurrentToken complete
          break;
        case '*':
          //CurrentToken complete
          break;
        case '/':
          //CurrentToken complete
          break;
        case '<':
          //CurrentToken complete
          break;
        case '=':
          //CurrentToken complete
          break;
        case '&':
          break;
        case '!':
          //CurrentToken complete
          break;
        default:
          Console.WriteLine("Unexpected, no operator, shouldn't be here");
          break;
      }
    }
    public Boolean isOperator(Char a)
    {
      foreach (char b in operators)
      {
        if (a == b) return true;
      }
      return false;
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
      //CurrentToken complete
      if (isReservedWord(token))
      {
        Console.WriteLine("found a resword");
        Console.WriteLine(token);

      }
      else if (true)
      { //it is integer? String? God?

      }
    }
  }

  class Parser
  {
    public string read(string path)
    {
      return "";

    }
  }

}
