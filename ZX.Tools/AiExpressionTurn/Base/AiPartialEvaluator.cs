﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ZX.Tools
{ /// <summary>
  ///重写一个表达示树,并将其中引用变量转换成常量
  ///去除所附加的类信息
  /// </summary>
    internal static class AiPartialEvaluator
    {
        /// <summary>
        /// Performs evaluation & replacement of independent sub-trees
        /// </summary>
        /// <param name="expression">The root of the expression tree.</param>
        /// <returns>A new tree with sub-trees evaluated and replaced.</returns>
        public static Expression Eval(Expression expression, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            return Eval(expression, null, ref listValue, ref listField);
        }

        /// <summary>
        /// Performs evaluation & replacement of independent sub-trees
        /// </summary>
        /// <param name="expression">The root of the expression tree.</param>
        /// <param name="fnCanBeEvaluated">A function that decides whether a given expression node can be part of the local function.</param>
        /// <returns>A new tree with sub-trees evaluated and replaced.</returns>
        public static Expression Eval(Expression expression, Func<Expression, bool> fnCanBeEvaluated, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
        {
            if (fnCanBeEvaluated == null)
                fnCanBeEvaluated = AiPartialEvaluator.CanBeEvaluatedLocally;
            return SubtreeEvaluator.Eval(Nominator.Nominate(fnCanBeEvaluated, expression, ref listValue, ref listField), expression, ref listValue, ref listField);
        }

        private static bool CanBeEvaluatedLocally(Expression expression)
        {
            return expression.NodeType != ExpressionType.Parameter;
        }

        /// <summary>
        /// Evaluates & replaces sub-trees when first candidate is reached (top-down)
        /// </summary>
        class SubtreeEvaluator : AiExpressionVisitor
        {
            HashSet<Expression> candidates;

            private SubtreeEvaluator(HashSet<Expression> candidates)
            {
                this.candidates = candidates;
            }

            internal static Expression Eval(HashSet<Expression> candidates, Expression exp, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
            {
                return new SubtreeEvaluator(candidates).Visit(exp, ref listValue, ref listField);
            }

            protected override Expression Visit(Expression exp, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
            {
                if (exp == null)
                {
                    return null;
                }

                if (this.candidates.Contains(exp))
                {
                    return this.Evaluate(exp);
                }

                return base.Visit(exp, ref listValue, ref listField);
            }

            private Expression Evaluate(Expression e)
            {
                Type type = e.Type;

                // check for nullable converts & strip them
                if (e.NodeType == ExpressionType.Convert)
                {
                    var u = (UnaryExpression)e;
                    if (AiTypeHelper.GetNonNullableType(u.Operand.Type) == AiTypeHelper.GetNonNullableType(type))
                    {
                        e = ((UnaryExpression)e).Operand;
                    }
                }

                // if we now just have a constant, return it
                if (e.NodeType == ExpressionType.Constant)
                {
                    var ce = (ConstantExpression)e;

                    // if we've lost our nullable typeness add it back
                    if (e.Type != type && AiTypeHelper.GetNonNullableType(e.Type) == AiTypeHelper.GetNonNullableType(type))
                    {
                        e = ce = Expression.Constant(ce.Value, type);
                    }

                    return e;
                }

                var me = e as MemberExpression;
                if (me != null)
                {
                    // member accesses off of constant's are common, and yet since these partial evals
                    // are never re-used, using reflection to access the member is faster than compiling  
                    // and invoking a lambda
                    var ce = me.Expression as ConstantExpression;
                    if (ce != null)
                    {
                        return Expression.Constant(me.Member.GetValue(ce.Value), type);
                    }
                }

                if (type.IsValueType)
                {
                    e = Expression.Convert(e, typeof(object));
                }

                Expression<Func<object>> lambda = Expression.Lambda<Func<object>>(e);
#if NOREFEMIT
                Func<object> fn = ExpressionEvaluator.CreateDelegate(lambda);
#else
                Func<object> fn = lambda.Compile();
#endif
                return Expression.Constant(fn(), type);
            }
        }

        /// <summary>
        /// Performs bottom-up analysis to determine which nodes can possibly
        /// be part of an evaluated sub-tree.
        /// </summary>
        class Nominator : AiExpressionVisitor
        {
            Func<Expression, bool> fnCanBeEvaluated;
            HashSet<Expression> candidates;
            bool cannotBeEvaluated;

            private Nominator(Func<Expression, bool> fnCanBeEvaluated)
            {
                this.candidates = new HashSet<Expression>();
                this.fnCanBeEvaluated = fnCanBeEvaluated;
            }

            internal static HashSet<Expression> Nominate(Func<Expression, bool> fnCanBeEvaluated, Expression expression, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
            {
                Nominator nominator = new Nominator(fnCanBeEvaluated);
                nominator.Visit(expression, ref listValue, ref listField);
                return nominator.candidates;
            }

            protected override Expression VisitConstant(ConstantExpression c, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
            {
                return base.VisitConstant(c, ref listValue, ref listField);
            }

            protected override Expression Visit(Expression expression, ref Dictionary<string, object> listValue, ref Dictionary<string, string> listField)
            {
                if (expression != null)
                {
                    bool saveCannotBeEvaluated = this.cannotBeEvaluated;
                    this.cannotBeEvaluated = false;
                    base.Visit(expression, ref listValue, ref listField);
                    if (!this.cannotBeEvaluated)
                    {
                        if (this.fnCanBeEvaluated(expression))
                        {
                            this.candidates.Add(expression);
                        }
                        else
                        {
                            this.cannotBeEvaluated = true;
                        }
                    }
                    this.cannotBeEvaluated |= saveCannotBeEvaluated;
                }
                return expression;
            }
        }
    }
}
