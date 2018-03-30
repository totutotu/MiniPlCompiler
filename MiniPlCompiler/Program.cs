using System;
using System.IO;
using FileReader;
using LexicalAnalysis;

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
        Console.Write(program);
        Scanner scanner = new Scanner(program);
        scanner.scan();
      }
    }
  }
}
