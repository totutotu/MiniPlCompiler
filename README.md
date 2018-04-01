# Interpreter for a Mini-programming language

## Run

```git clone ...```

```cd MiniPlCompiler/MiniPlCompiler```

```dotnet MiniPlComplier.csproj```

# Documentation

*This project and documentation is still in progress.*

## Flow 

The main file is Program.cs. It reads the selected file from the folder /programs, passes the string of the progrman to the Lexical analyser (LexicalAnalysis.cs). 


After the lexical checking the text is passed to the Syntax analysis checking, which creates the AST and checks the validity of the programs syntax.

After passing the syntax check starts the semantical analysing.

If no errors have been found the execution of the program happens itn the Execution.cs file. 


## Lexical analysis

The lexical analysis phase is driven by the Scanner. The scanner scans through the program and creates a List of Tokens (Token consists of type and lexeme), and passes the list to the Parser. Separation so Parser-driven functionality, where each token is asked by the parses might be refactored later. 



Programs in folder /programs

In lexical analysis file the Scanner reads throgh the program char byu char, puts each found Token in the List<Token> with the the tokens Type and Value.

Token Kinds: 

Reserved word
Identifier
Integer
String
Keyword
Comment
End


### Error Handling

Expects an Integer if a token starts with a number. If next char is not :
1. Integer
2. Whitespace
3. Parenthesis
4. Operator
5. '..'
forwards a "Bad integer" error token. 


Regex of acceptable input is something like this: 

``` :=|[+\-*\/<>&;()]|[^(_\d][a-zA-Z][_|\d|[a-zA-Z]*|[\d]|["^"][\w|\s]*" ```

## Syntax Analysis



All of the functions are aimed to be reusable when use more use cases come around except for the for loop, which is handled completely on its own. 

Parentheses at the moment are always accepted. 