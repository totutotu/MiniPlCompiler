using System;
using Helpers;
using System.Text;
using System.Collections.Generic;

namespace SyntaxAnalysis
{

  class Parser

  {
    private String program;
    private List<Token> tokens;
    private Token currToken;
    private int parentheses = 0;

    public Parser(List<Token> scannedTokens)
    {
      tokens = scannedTokens;
    }
    public void parse()
    {
      statements();
    }

    public void statements()
    {
      currToken = tokens[0];
      tokens.RemoveAt(0);
      while (tokens.Count > 0)
      {
        statement();
        Console.WriteLine(currToken.Lexeme);
      }
    }
    public void statement()
    {
      currToken = tokens[0];
      tokens.RemoveAt(0);
      while (currToken.Lexeme != ";")
      {
        switch (currToken.Kind)
        {
          case "Var":
            variableDeclaration();
            return;
          case "Identifier":
            identifierAssignment();
            return;
          case "For":
            return;
          case "Print":
            return;
          case "Assert":
            return;
        }
      }
    }

    public void variableDeclaration()
    {
      currToken = tokens[0];
      tokens.RemoveAt(0);
      switch (currToken.Kind)
      {
        case "Identifier":
          currToken = tokens[0];
          tokens.RemoveAt(0);
          switch (currToken.Kind)
          {
            case "Colon":
              currToken = tokens[0];
              tokens.RemoveAt(0);
              switch (currToken.Kind)
              {
                case "Type":
                  currToken = tokens[0];
                  tokens.RemoveAt(0);
                  switch (currToken.Kind)
                  {
                    case "Assign":
                      expression();
                      return;
                    case "End":
                      return;
                    default:
                      Console.WriteLine("Expected ; or Assign after type decleration, got " + currToken.Lexeme);
                      return;
                  }
                default:
                  Console.WriteLine("Expected : for variable declaration, got: " + currToken.Lexeme);
                  return;
              }
            default:
              Console.WriteLine("Expected an identifier for variable, got: " + currToken.Lexeme);
              return;

          }
        default:
          Console.Write("Variable declaration error: tried to name the variable " + currToken.Lexeme);
          return;
      }
    }
    public void operand()
    {
      currToken = tokens[0];
      tokens.RemoveAt(0);
      switch (currToken.Kind)
      {
        case "Operator":
          finalOperator();
          return;
        case "End": //this was finalOperator
          return;
        case "ParenthClose":
          return;
      }
    }
    public void expression()
    {
      currToken = tokens[0];
      tokens.RemoveAt(0);
      switch (currToken.Kind)
      {
        case "Int":
          operand();
          return;
        case "String":
          operand();
          return;
        case "Identifier":
          operand();
          return;
        case "ParenthStart":
          expression();
          return;
        case "UnOperator":
          finalOperator();
          return;
        default:
          Console.WriteLine("Expression error: expected a value, identifier, or (, got:  " + currToken.Lexeme);
          return;
      }
    }

    public void finalOperator() //after this expression finished
    {
      currToken = tokens[0];
      tokens.RemoveAt(0);

      if (currToken.Kind == "Int" || currToken.Kind == "String" || currToken.Kind == "Identifier")
      {
        currToken = tokens[0];
        tokens.RemoveAt(0);
        switch (currToken.Kind)
        {
          case "End":
            return;
          case "ParenthClose":
            parentheses--;
            return;
          default:
            Console.WriteLine("Expression error: Expected; or ), got: " + currToken.Lexeme);
            return;
        }
      }
      switch (currToken.Kind)
      {
        case "ParenthStart":
          parentheses++;
          expression();
          return;
        default:
          Console.WriteLine("Unary Operand error: expected opreand, found : " + currToken.Lexeme);
          return;
      }
    }
    public void identifierAssignment()
    {
      currToken = tokens[0];
      tokens.RemoveAt(0);
      if (currToken.Kind == "Introduce")
      {
        currToken = tokens[0];
        tokens.RemoveAt(0);
        if (currToken.Kind == "Type")
        {
          currToken = tokens[0];
          tokens.RemoveAt(0);
          if (currToken.Kind == "Assign")
          {
            expression();
            return;
          }
          else
          {
            Console.WriteLine("Expected assigning an expression, got " + currToken.Lexeme);
          }
        }
        else
        {
          Console.WriteLine("Expected variable declaration type, got " + currToken.Lexeme);
          return;
        }
      }
      else
      {
        Console.WriteLine("Expected introducing a variable with : , got: " + currToken.Lexeme);
      }
    }
  }


}