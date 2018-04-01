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

    private int errors = 0;

    public Parser(List<Token> scannedTokens)
    {
      tokens = scannedTokens;
    }
    public void parse()
    {
      statements();
      Console.WriteLine("Errors from syntax analysis: " + errors);
    }

    public void statements()
    {
      currToken = tokens[0];
      tokens.RemoveAt(0);
      while (tokens.Count > 0)
      {
        statement();
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
            print();
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
                      errors++;
                      return;
                  }
                default:
                  Console.WriteLine("Expected : for variable declaration, got: " + currToken.Lexeme);
                  errors++;
                  return;
              }
            default:
              Console.WriteLine("Expected an identifier for variable, got: " + currToken.Lexeme);
              errors++;
              return;

          }
        default:
          Console.Write("Variable declaration error: tried to name the variable " + currToken.Lexeme);
          errors++;
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
          if (parentheses != 0)
          {
            Console.WriteLine("Incorrect amount of parentheses");
            errors++;
            return;
          }
          return;
        case "ParenthClose":
          parentheses--;
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
          parentheses++;
          expression();
          return;
        case "UnOperator":
          finalOperator();
          return;
        default:
          Console.WriteLine("Expression error: expected a value, identifier, or (, got:  " + currToken.Lexeme);
          errors++;
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
            if (parentheses != 0)
            {
              Console.WriteLine("Incorrect amount of parentheses");
              errors++;
              return;
            }
            return;
          case "ParenthClose":
            parentheses--;
            currToken = tokens[0];
            tokens.RemoveAt(0);
            switch (currToken.Kind)
            {
              case "End":
                if (parentheses != 0)
                {
                  Console.WriteLine("Incorrect amount of parentheses");
                  errors++;
                  return;
                }
                return;
            }
            return;
          default:
            Console.WriteLine("Expression error: Expected; or ), got: " + currToken.Lexeme);
            errors++;
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
          errors++;
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
            errors++;
            Console.WriteLine("Expected assigning an expression, got " + currToken.Lexeme);
          }
        }
        else
        {
          errors++;
          Console.WriteLine("Expected variable declaration type, got " + currToken.Lexeme);
          return;
        }
      }
      else
      {
        errors++;
        Console.WriteLine("Expected introducing a variable with : , got: " + currToken.Lexeme);
      }
    }
    public void print()
    {
      currToken = tokens[0];
      tokens.RemoveAt(0);
      if (currToken.Kind == "Identifier" || currToken.Kind == "String")
      {
        currToken = tokens[0];
        tokens.RemoveAt(0);
        if (currToken.Kind == "End")
        {
          return;
        }
        else
        {
          errors++;
          Console.WriteLine("Expected ; after print, got: " + currToken.Lexeme);
        }
      }
      else
      {
        errors++;
        Console.WriteLine("Expected something printable, got: " + currToken.Lexeme);
      }
    }
  }
}