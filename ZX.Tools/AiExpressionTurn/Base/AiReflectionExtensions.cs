﻿using System;
using System.Reflection;

namespace ZX.Tools
{
    /// <summary>
    /// 成员反射操作
    /// </summary>
    internal static class AiReflectionExtensions
    {
        public static object GetValue(this MemberInfo member, object instance)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Property:
                    return ((PropertyInfo)member).GetValue(instance, null);
                case MemberTypes.Field:
                    return ((FieldInfo)member).GetValue(instance);
                default:
                    throw new InvalidOperationException();
            }
        }

        public static void SetValue(this MemberInfo member, object instance, object value)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Property:
                    var pi = (PropertyInfo)member;
                    pi.SetValue(instance, value, null);
                    break;
                case MemberTypes.Field:
                    var fi = (FieldInfo)member;
                    fi.SetValue(instance, value);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
