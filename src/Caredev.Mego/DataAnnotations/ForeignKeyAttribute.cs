﻿// Copyright (c) CarefreeXT and Caredev Studios. All rights reserved.
// Licensed under the GNU Lesser General Public License v3.0.
// See License.txt in the project root for license information.
namespace Caredev.Mego.DataAnnotations
{
    using System;
    using System.Linq;
    using Res = Properties.Resources;
    /// <summary>
    /// 表示关系中用作外键的属性，标识属性所指向的类为当前关系的主体，属性所
    /// 在的类为当前关系的外键，这与<see cref="InversePropertyAttribute"/>相反。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKeyAttribute : Attribute
    {
        /// <summary>
        /// 创建外键特性。
        /// </summary>
        /// <param name="names">外键名称，多个名称以逗号分割。</param>
        /// <param name="principals">主键名称，多个名称以逗号分割。</param>
        public ForeignKeyAttribute(string names, string principals = "")
        {
            var splitchar = new char[] { ',' };
            Names = names.Split(splitchar, StringSplitOptions.RemoveEmptyEntries).Select(a => a.Trim()).ToArray();
            Principals = principals.Split(splitchar, StringSplitOptions.RemoveEmptyEntries).Select(a => a.Trim()).ToArray();
            if (Names.Length != Principals.Length)
            {
                throw new ArgumentException(Res.ExceptionKeyCountIsNotMatch);
            }
        }
        /// <summary>
        /// 外键属性名称集合。
        /// </summary>
        public string[] Names { get; }
        /// <summary>
        /// 关系主体的唯一键属性名集合。
        /// </summary>
        public string[] Principals { get; }
    }
}