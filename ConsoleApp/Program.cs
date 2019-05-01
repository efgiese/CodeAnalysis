using System;
using System.Diagnostics.CodeAnalysis;
using ConsoleApp.Extensions;

namespace ConsoleApp
{
  class Program
  {
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters")]
    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
    static void Main(string[] args)
    {
      Console.WriteLine("Bad implementation:");
      try
      {
        BadImplementation();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }

      Console.WriteLine();
      Console.WriteLine("Good implementation:");
      try
      {
        GoodImplementation();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }

      Console.WriteLine();
      Console.WriteLine("Better implementation:");
      try
      {
        BetterImplementation();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }

    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
    [SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails")]
    private static void BadImplementation()
    {
      try
      {
        ExceptionMethod();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private static void GoodImplementation()
    {
      try
      {
        ExceptionMethod();
      }
      catch (Exception)
      {
        throw;
      }
    }

    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
    private static void BetterImplementation()
    {
      try
      {
        ExceptionMethod();
      }
      catch (Exception ex)
      {
        throw new DemoException("my exception", ex);
      }
    }

    private static void ExceptionMethod()
    {
      System.IO.File.ReadAllLines("noFile");
    }
  }
}
