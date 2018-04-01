using System;
using System.IO;
using System.Collections.Generic;
using FileReader;
using LexicalAnalysis;
using SyntaxAnalysis;
using Helpers;

namespace MiniPlCompiler
{
  class Program
  {
    static void Main(string[] args)
    {
      Reader reader = new Reader();
      string program = reader.read("../programs/integer_print.mpl");
      if (program != null)
      {
        Scanner scanner = new Scanner(program);
        List<Token> tokens = scanner.scan();
        Parser parser = new Parser(tokens);
        parser.parse();
      }  
    }
  }
}
