// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.using Microsoft.AspNetCore.Authorization;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using B2CMyApp.Library.AzureAdB2C;

namespace B2CMyApp.Controllers {

    /// <summary>
    /// 
    /// </summary>
    //[AllowAnonymous]
    public class AccountController : Controller {

        /// <summary>
        /// 
        /// </summary>
        private readonly IOptionsMonitor<AzureADB2COptions> _options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AzureADB2COptions"></param>
        public AccountController( IOptionsMonitor<AzureADB2COptions> AzureADB2COptions ) {
            _options = AzureADB2COptions;
        }

        public IActionResult SignIn( ) {

            var scheme = AzureADB2CDefaults.AuthenticationScheme; // 本来はここでいろんなIDaaSの種類が選べるということ？
            var options = _options.Get(scheme);

            var redirectUrl = Url.Content("~/Home/Index");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            properties.Items[AzureADB2CDefaults.PolicyKey] = options.SignUpSignInPolicyId;

            return Challenge(
                new AuthenticationProperties { RedirectUri = redirectUrl },
                scheme);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult ResetPassword( ) {
            var scheme = AzureADB2CDefaults.AuthenticationScheme;
            var options = _options.Get(scheme);

            var redirectUrl = Url.Content("~/");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            properties.Items[AzureADB2CDefaults.PolicyKey] = options.ResetPasswordPolicyId;
            return Challenge(properties, scheme);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheme"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditProfile( [FromRoute] string scheme ) {
            scheme = scheme ?? AzureADB2CDefaults.AuthenticationScheme;
            var authenticated = await HttpContext.AuthenticateAsync(scheme);
            if ( !authenticated.Succeeded ) {
                return Challenge(scheme);
            }

            var options = _options.Get(scheme);

            var redirectUrl = Url.Content("~/");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            properties.Items[AzureADB2CDefaults.PolicyKey] = options.EditProfilePolicyId;
            return Challenge(properties, scheme);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheme"></param>
        /// <returns></returns>
        public async Task<IActionResult> SignOut( ) {
            var scheme = AzureADB2CDefaults.AuthenticationScheme;

            var hoge = User.Identity.IsAuthenticated;

            // 
            var authenticated = await HttpContext.AuthenticateAsync(scheme);
            if ( !authenticated.Succeeded ) {
                return Challenge(scheme);
            }

            var options = _options.Get(scheme);

            var callbackUrl = Url.Content("~/Account/SignedOut");
            return SignOut(
                new AuthenticationProperties { RedirectUri = callbackUrl },
                options.AllSchemes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult SignedOut( ) {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult AccessDenied( ) {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Error( ) {
            return View();
        }
    }
}