﻿// Copyright (c) CarefreeXT and Caredev Studios. All rights reserved.
// Licensed under the GNU Lesser General Public License v3.0.
// See License.txt in the project root for license information.
namespace Caredev.Mego.Resolve.Providers
{
    /// <summary>
    /// 访问Nuget组件:MySql.Data
    /// </summary>
    internal class MySqlAccessProvider : DbAccessProvider
    {
        /// <inheritdoc/>
        public override string ProviderName => "MySql.Data.MySqlClient";
    }
}