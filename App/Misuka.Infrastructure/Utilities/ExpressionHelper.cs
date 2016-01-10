using System;
using System.Linq.Expressions;
using System.Text;

namespace Misuka.Infrastructure.Utilities
{
  public class ExpressionHelper
  {
    public static string GetPropertyName<T>(Expression<Func<T, object>> expr)
    {
      StringBuilder sb = new StringBuilder();

      MemberExpression me;
      switch (expr.Body.NodeType)
      {
        case ExpressionType.Convert:
        case ExpressionType.ConvertChecked:
          var ue = expr.Body as UnaryExpression;
          me = ((ue != null) ? ue.Operand : null) as MemberExpression;
          break;
        default:
          me = expr.Body as MemberExpression;
          break;
      }

      if (me != null)
      {
        while (me != null)
        {
          sb.Insert(0, string.Format("{0}.", me.Member.Name));
          me = me.Expression as MemberExpression;
        }
        return sb.ToString(0, sb.Length - 1);
      }
      return string.Empty;
    }

    public static Type GetPropertyType<T>(Expression<Func<T, object>> expr)
    {
      var exp = (LambdaExpression)expr;
      MemberExpression mExp;
      if (exp.Body.NodeType == ExpressionType.MemberAccess)
      {
        mExp = (MemberExpression)exp.Body;
      }
      else
      {
        mExp = (MemberExpression)((UnaryExpression)exp.Body).Operand;
      }
      return mExp.Type;
    }
  }
}