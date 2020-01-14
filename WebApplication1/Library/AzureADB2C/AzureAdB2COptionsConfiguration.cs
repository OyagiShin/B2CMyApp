// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.using Microsoft.AspNetCore.Authorization;

using Microsoft.Extensions.Options;

namespace B2CMyApp.Library.AzureAdB2C {

    /// <summary>
    /// 
    /// </summary>
    internal class AzureADB2COptionsConfiguration : IConfigureNamedOptions<AzureADB2COptions> {
        private readonly IOptions<AzureADB2CSchemeOptions> _schemeOptions;

        public AzureADB2COptionsConfiguration( IOptions<AzureADB2CSchemeOptions> schemeOptions ) {
            _schemeOptions = schemeOptions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options"></param>
        public void Configure( string name, AzureADB2COptions options ) {
            // This can be called because of someone configuring JWT or someone configuring
            // Open ID + Cookie.
            if ( _schemeOptions.Value.OpenIDMappings.TryGetValue(name, out var webMapping) ) {
                options.OpenIdConnectSchemeName = webMapping.OpenIdConnectScheme;
                options.CookieSchemeName = webMapping.CookieScheme;
                return;
            }
            if ( _schemeOptions.Value.JwtBearerMappings.TryGetValue(name, out var mapping) ) {
                options.JwtBearerSchemeName = mapping.JwtBearerScheme;
                return;
            }
        }

        public void Configure( AzureADB2COptions options ) {
        }
    }
}