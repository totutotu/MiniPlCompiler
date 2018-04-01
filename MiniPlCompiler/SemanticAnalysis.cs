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

            if (tokens[0].Kind == "Assign")
            {
              expression();
            }
          }
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

    }
  }
}