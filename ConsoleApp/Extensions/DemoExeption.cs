using System;
using System.Runtime.Serialization;

namespace ConsoleApp.Extensions
{
  [Serializable()]
  public class DemoException : Exception
  {
    public DemoException() : base() {  }
    public DemoException(string message) : base(message) { }
    public DemoException(string message, Exception innerException) : base(message, innerException) { }
    protected DemoException(SerializationInfo info, StreamingContext context) : base(info, context) { }
  }
}