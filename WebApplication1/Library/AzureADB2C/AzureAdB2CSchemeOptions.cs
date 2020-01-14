// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.using Microsoft.AspNetCore.Authorization;

using System.Collections.Generic;

namespace B2CMyApp.Library.AzureAdB2C {

    /// <summary>
    /// 
    /// </summary>
    internal class AzureADB2CSchemeOptions {

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, AzureADB2COpenIDSchemeMapping> OpenIDMappings { get; set; } = new Dictionary<string, AzureADB2COpenIDSchemeMapping>();

        public IDictionary<string, JwtBearerSchemeMapping> JwtBearerMappings { get; set; } = new Dictionary<string, JwtBearerSchemeMapping>();


        /// <summary>
        /// 
        /// </summary>
        public class AzureADB2COpenIDSchemeMapping {
            public string OpenIdConnectScheme { get; set; }
            public string CookieScheme { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class JwtBearerSchemeMapping {
            public string JwtBearerScheme { get; set; }
        }
    }
}