// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.using Microsoft.AspNetCore.Authorization;

using System.Collections.Generic;
using System.Reflection;

using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using B2CMyApp.Controllers;

/// <summary>
/// 
/// </summary>
namespace B2CMyApp.Library.AzureAdB2C {

    /// <summary>
    /// 
    /// </summary>
    internal class AzureADB2CAccountControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>, IApplicationFeatureProvider {

        /// <summary>
        /// Accountコントローラーがないか探す処理
        /// </summary>
        /// <param name="parts"></param>
        /// <param name="feature"></param>
        public void PopulateFeature( IEnumerable<ApplicationPart> parts, ControllerFeature feature ) {
            if ( !feature.Controllers.Contains(typeof(AccountController).GetTypeInfo()) ) {
                feature.Controllers.Add(typeof(AccountController).GetTypeInfo());
            }
        }
    }

}
