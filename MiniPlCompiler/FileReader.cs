using System;
using System.IO;

namespace FileReader
{
  class Reader
  {
    public string read(string path)
    {
      try
      {
        string full = "";
        using (StreamReader sr = new StreamReader(path))
        {
          string line;
          while ((line = sr.ReadLine()) != null)
          {
            full += line;
            Console.WriteLine(line);
          }
        }
        return full;
      }
      catch (Exception e)
      {
        // Let the user know what went wrong.
        Console.WriteLine("The file could not be read:");
        Console.WriteLine(e.Message);
        return null;
      }
    }
  }
}
