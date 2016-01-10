using System;
using System.Net;
using System.Reflection;
using System.Web;

namespace Misuka.Infrastructure.Utilities
{
  public static class ThrowError
  {
    public static void ArgumentIsNull(object value, string paramName)
    {
      if (value == null)
      {
        throw new ArgumentNullException(paramName);
      }
    }

    /// <summary>
    /// Throws a HttpException with HttpStatusCode.NotFound if the assertion is true. This will result in the HttpStatus 404 status being returned to the client. 
    /// If a customErrors section is configued in web.config the user will be redirected accordingly
    /// </summary>
    /// <param name="assertion">The assetion that will be validated</param>
    public static void WebResourceNotFound(bool assertion)
    {
      if (assertion)
      {
        throw new HttpException((int)HttpStatusCode.NotFound, "The requested resource was not found");
      }
    }

    /// <summary>
    /// Throws an exception of type <typeparamref name="TException"/> with the specified message
    /// when the assertion statement is true.
    /// </summary>
    /// <typeparam name="TException">The type of exception to throw.</typeparam>
    /// <param name="assertion">The assertion to evaluate. If true then the <typeparamref name="TException"/> exception is thrown.</param>
    /// <param name="message">string. The exception message to throw.</param>
    public static void Against<TException>(bool assertion, string message) where TException : Exception
    {
      if (assertion)
        throw (TException)Activator.CreateInstance(typeof(TException), message);
    }

    /// <summary>
    /// Throws an exception of type <typeparamref name="TException"/> with the specified message
    /// when the assertion
    /// </summary>
    /// <typeparam name="TException"></typeparam>
    /// <param name="assertion"></param>
    /// <param name="message"></param>
    public static void Against<TException>(Func<bool> assertion, string message) where TException : Exception
    { 
      //Execute the lambda and if it evaluates to true then throw the exception.
      if (assertion())
        throw (TException)Activator.CreateInstance(typeof(TException), message);
    }

  }
}

