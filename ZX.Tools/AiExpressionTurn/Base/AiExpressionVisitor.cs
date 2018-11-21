using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Collections.ObjectModel;

namespace ZX.Tools
{
    internal abstract class AiExpressionVisitor
    {
        protected AiExpressionVisitor() { }

        protected virtual Expression Visit(Expression exp, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            if (exp == null)
                return exp;
            switch (exp.NodeType)
            {
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.ArrayLength:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:
                    return this.VisitUnary((UnaryExpression)exp, ref listValue, ref listField);
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.Coalesce:
                case ExpressionType.ArrayIndex:
                case ExpressionType.RightShift:
                case ExpressionType.LeftShift:
                case ExpressionType.ExclusiveOr:
                    return this.VisitBinary((BinaryExpression)exp, ref listValue, ref listField);
                case ExpressionType.TypeIs:
                    return this.VisitTypeIs((TypeBinaryExpression)exp, ref listValue, ref listField);
                case ExpressionType.Conditional:
                    return this.VisitConditional((ConditionalExpression)exp, ref listValue, ref listField);
                case ExpressionType.Constant:
                    return this.VisitConstant((ConstantExpression)exp, ref listValue, ref listField);
                case ExpressionType.Parameter:
                    return this.VisitParameter((ParameterExpression)exp, ref listValue, ref listField);
                case ExpressionType.MemberAccess:
                    return this.VisitMemberAccess((MemberExpression)exp, ref listValue, ref listField);
                case ExpressionType.Call:
                    return this.VisitMethodCall((MethodCallExpression)exp, ref listValue, ref listField);
                case ExpressionType.Lambda:
                    return this.VisitLambda((LambdaExpression)exp, ref listValue, ref listField);
                case ExpressionType.New:
                    return this.VisitNew((NewExpression)exp, ref listValue, ref listField);
                case ExpressionType.NewArrayInit:
                case ExpressionType.NewArrayBounds:
                    return this.VisitNewArray((NewArrayExpression)exp, ref listValue, ref listField);
                case ExpressionType.Invoke:
                    return this.VisitInvocation((InvocationExpression)exp, ref listValue, ref listField);
                case ExpressionType.MemberInit:
                    return this.VisitMemberInit((MemberInitExpression)exp, ref listValue, ref listField);
                case ExpressionType.ListInit:
                    return this.VisitListInit((ListInitExpression)exp, ref listValue, ref listField);
                default:
                    throw new Exception(string.Format("Unhandled expression type: '{0}'", exp.NodeType));
            }
        }
        protected virtual Expression VisitUnknown(Expression expression)
        {
            throw new Exception(string.Format("Unhandled expression type: '{0}'", expression.NodeType));
        }

        protected virtual MemberBinding VisitBinding(MemberBinding binding, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            switch (binding.BindingType)
            {
                case MemberBindingType.Assignment:
                    return this.VisitMemberAssignment((MemberAssignment)binding, ref listValue, ref listField);
                case MemberBindingType.MemberBinding:
                    return this.VisitMemberMemberBinding((MemberMemberBinding)binding, ref listValue, ref listField);
                case MemberBindingType.ListBinding:
                    return this.VisitMemberListBinding((MemberListBinding)binding, ref listValue, ref listField);
                default:
                    throw new Exception(string.Format("Unhandled binding type '{0}'", binding.BindingType));
            }
        }

        protected virtual ElementInit VisitElementInitializer(ElementInit initializer, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            ReadOnlyCollection<Expression> arguments = this.VisitExpressionList(initializer.Arguments, ref listValue, ref listField);
            if (arguments != initializer.Arguments)
            {
                return Expression.ElementInit(initializer.AddMethod, arguments);
            }
            return initializer;
        }

        protected virtual Expression VisitUnary(UnaryExpression u, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            Expression operand = this.Visit(u.Operand, ref listValue, ref listField);
            if (operand != u.Operand)
            {
                return Expression.MakeUnary(u.NodeType, operand, u.Type, u.Method);
            }
            return u;
        }

        protected virtual Expression VisitBinary(BinaryExpression b, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            Expression left = this.Visit(b.Left, ref listValue, ref listField);
            Expression right = this.Visit(b.Right, ref listValue, ref listField);
            Expression conversion = this.Visit(b.Conversion, ref listValue, ref listField);
            if (left != b.Left || right != b.Right || conversion != b.Conversion)
            {
                if (b.NodeType == ExpressionType.Coalesce && b.Conversion != null)
                    return Expression.Coalesce(left, right, conversion as LambdaExpression);
                else
                    return Expression.MakeBinary(b.NodeType, left, right, b.IsLiftedToNull, b.Method);
            }

            return b;
        }

        protected virtual Expression VisitTypeIs(TypeBinaryExpression b, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            Expression expr = this.Visit(b.Expression, ref listValue, ref listField);
            if (expr != b.Expression)
            {
                return Expression.TypeIs(expr, b.TypeOperand);
            }
            return b;
        }

        protected virtual Expression VisitConstant(ConstantExpression c, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            return c;
        }

        protected virtual Expression VisitConditional(ConditionalExpression c, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            Expression test = this.Visit(c.Test, ref listValue, ref listField);
            Expression ifTrue = this.Visit(c.IfTrue, ref listValue, ref listField);
            Expression ifFalse = this.Visit(c.IfFalse, ref listValue, ref listField);
            if (test != c.Test || ifTrue != c.IfTrue || ifFalse != c.IfFalse)
            {
                return Expression.Condition(test, ifTrue, ifFalse);
            }
            return c;
        }

        protected virtual Expression VisitParameter(ParameterExpression p, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            return p;
        }

        protected virtual Expression VisitMemberAccess(MemberExpression m, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            Expression exp = this.Visit(m.Expression, ref listValue, ref listField);
            if (exp != m.Expression)
            {
                return Expression.MakeMemberAccess(exp, m.Member);
            }
            return m;
        }

        protected virtual Expression VisitMethodCall(MethodCallExpression m, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            Expression obj = this.Visit(m.Object, ref listValue, ref listField);
            IEnumerable<Expression> args = this.VisitExpressionList(m.Arguments, ref listValue, ref listField);
            if (obj != m.Object || args != m.Arguments)
            {
                return Expression.Call(obj, m.Method, args);
            }
            return m;
        }

        protected virtual ReadOnlyCollection<Expression> VisitExpressionList(ReadOnlyCollection<Expression> original, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            List<Expression> list = null;
            for (int i = 0, n = original.Count; i < n; i++)
            {
                Expression p = this.Visit(original[i], ref listValue, ref listField);
                if (list != null)
                {
                    list.Add(p);
                }
                else if (p != original[i])
                {
                    list = new List<Expression>(n);
                    for (int j = 0; j < i; j++)
                    {
                        list.Add(original[j]);
                    }
                    list.Add(p);
                }
            }
            if (list != null)
            {
                return list.AsReadOnly();
            }
            return original;
        }

        protected virtual MemberAssignment VisitMemberAssignment(MemberAssignment assignment, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            Expression e = this.Visit(assignment.Expression, ref listValue, ref listField);
            if (e != assignment.Expression)
            {
                return Expression.Bind(assignment.Member, e);
            }
            return assignment;
        }

        protected virtual MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            IEnumerable<MemberBinding> bindings = this.VisitBindingList(binding.Bindings, ref listValue, ref listField);
            if (bindings != binding.Bindings)
            {
                return Expression.MemberBind(binding.Member, bindings);
            }
            return binding;
        }

        protected virtual MemberListBinding VisitMemberListBinding(MemberListBinding binding, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            IEnumerable<ElementInit> initializers = this.VisitElementInitializerList(binding.Initializers, ref listValue, ref listField);
            if (initializers != binding.Initializers)
            {
                return Expression.ListBind(binding.Member, initializers);
            }
            return binding;
        }

        protected virtual IEnumerable<MemberBinding> VisitBindingList(ReadOnlyCollection<MemberBinding> original, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            List<MemberBinding> list = null;
            for (int i = 0, n = original.Count; i < n; i++)
            {
                MemberBinding b = this.VisitBinding(original[i], ref listValue, ref listField);
                if (list != null)
                {
                    list.Add(b);
                }
                else if (b != original[i])
                {
                    list = new List<MemberBinding>(n);
                    for (int j = 0; j < i; j++)
                    {
                        list.Add(original[j]);
                    }
                    list.Add(b);
                }
            }
            if (list != null)
                return list;
            return original;
        }

        protected virtual IEnumerable<ElementInit> VisitElementInitializerList(ReadOnlyCollection<ElementInit> original, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            List<ElementInit> list = null;
            for (int i = 0, n = original.Count; i < n; i++)
            {
                ElementInit init = this.VisitElementInitializer(original[i], ref listValue, ref listField);
                if (list != null)
                {
                    list.Add(init);
                }
                else if (init != original[i])
                {
                    list = new List<ElementInit>(n);
                    for (int j = 0; j < i; j++)
                    {
                        list.Add(original[j]);
                    }
                    list.Add(init);
                }
            }
            if (list != null)
                return list;
            return original;
        }

        protected virtual Expression VisitLambda(LambdaExpression lambda, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            Expression body = this.Visit(lambda.Body, ref listValue, ref listField);
            if (body != lambda.Body)
            {
                return Expression.Lambda(lambda.Type, body, lambda.Parameters);
            }
            return lambda;
        }

        protected virtual NewExpression VisitNew(NewExpression nex, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            IEnumerable<Expression> args = this.VisitExpressionList(nex.Arguments, ref listValue, ref listField);
            if (args != nex.Arguments)
            {
                if (nex.Members != null)
                    return Expression.New(nex.Constructor, args, nex.Members);
                else
                    return Expression.New(nex.Constructor, args);
            }
            return nex;
        }

        protected virtual Expression VisitMemberInit(MemberInitExpression init, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            NewExpression n = this.VisitNew(init.NewExpression, ref listValue, ref listField);
            IEnumerable<MemberBinding> bindings = this.VisitBindingList(init.Bindings, ref listValue, ref listField);
            if (n != init.NewExpression || bindings != init.Bindings)
            {
                return Expression.MemberInit(n, bindings);
            }
            return init;
        }

        protected virtual Expression VisitListInit(ListInitExpression init, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            NewExpression n = this.VisitNew(init.NewExpression, ref listValue, ref listField);
            IEnumerable<ElementInit> initializers = this.VisitElementInitializerList(init.Initializers, ref listValue, ref listField);
            if (n != init.NewExpression || initializers != init.Initializers)
            {
                return Expression.ListInit(n, initializers);
            }
            return init;
        }

        protected virtual Expression VisitNewArray(NewArrayExpression na, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            IEnumerable<Expression> exprs = this.VisitExpressionList(na.Expressions, ref listValue, ref listField);
            if (exprs != na.Expressions)
            {
                if (na.NodeType == ExpressionType.NewArrayInit)
                {
                    return Expression.NewArrayInit(na.Type.GetElementType(), exprs);
                }
                else
                {
                    return Expression.NewArrayBounds(na.Type.GetElementType(), exprs);
                }
            }
            return na;
        }

        protected virtual Expression VisitInvocation(InvocationExpression iv, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            IEnumerable<Expression> args = this.VisitExpressionList(iv.Arguments, ref listValue, ref listField);
            Expression expr = this.Visit(iv.Expression, ref listValue, ref listField);
            if (args != iv.Arguments || expr != iv.Expression)
            {
                return Expression.Invoke(expr, args);
            }
            return iv;
        }
    }
}
