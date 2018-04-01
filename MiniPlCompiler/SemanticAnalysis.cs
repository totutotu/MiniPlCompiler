using System;
using System.Collections.Generic;
using Helpers;


namespace SemanticAnalysis
{
  class SemanticAnalyser
  {
    /*To check whether declaration has happenend
      and if tokens are the right type
    */
    private Token currToken;
    private Token value;
    private List<Token> tokens;
    private String kindExpected; //used when checking expression variable types
    private List<String> declaredVars = new List<String>();

    private Dictionary<String, String> varTypes = new Dictionary<String, String>();
    // Used for checking whether the vars are being used properly.
    public SemanticAnalyser(List<Token> tokenList)
    {
      tokens = tokenList;
      while (tokens.Count > 0)
      {
        currToken = tokens[0];
        tokens.RemoveAt(0);
        if (currToken.Kind == "Var")
        {
          newVar();
        }
        else if (currToken.Kind == "Identifier")
        {
          if (identifier())
          {
            kindExpected = varTypes[currToken.Lexeme];
          }
        }
        else if (currToken.Kind == "String")
        {
          kindExpected = "String";
        }
        else if (currToken.Kind == "Int")
        {
          kindExpected = "Int";
        }
        else if (currToken.Kind == "Boolean")
        {
          kindExpected = "Boolean";
        }
        else if (currToken.Kind == "Assign" || currToken.Kind == "print")
        {
          expression();
        }
      }
    }

    public void newVar()
    {
      declaredVars.Add(currToken.Lexeme);
      tokens.RemoveAt(0);
      tokens.RemoveAt(0); // remove the following : 
      value = tokens[0];
      tokens.RemoveAt(0);
      varTypes[currToken.Lexeme] = value.Lexeme;
    }

    public Boolean identifier()
    {
      if (!declaredVars.Contains(currToken.Lexeme))
      {
        Console.WriteLine("Semantical error: tried to use before declaration: " + currToken.Lexeme);
        return false;
      }
      return true;
      // Otherwise OK
    }

    public void expression()
    {
      // this doesn't work since an expression can start with a (
      if (currToken.Kind == "Print")
      {
        kindExpected = tokens[0].Kind; // If printing, the expression can be any type
      }
      // as long as the expression continues; Until ';' , '..', operator, 'do'
      // expect the same kind of variables
      while (true)
      {
        currToken = tokens[0];
        tokens.RemoveAt(0);
      }
    }
  }
}