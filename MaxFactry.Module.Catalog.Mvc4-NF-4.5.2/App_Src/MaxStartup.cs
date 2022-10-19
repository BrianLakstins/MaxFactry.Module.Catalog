﻿// <copyright file="MaxWebModule.cs" company="Lakstins Family, LLC">
// Copyright (c) Brian A. Lakstins (http://www.lakstins.com/brian/)
// </copyright>

#region License
// <license>
// This software is provided 'as-is', without any express or implied warranty. In no 
// event will the author be held liable for any damages arising from the use of this 
// software.
//  
// Permission is granted to anyone to use this software for any purpose, including 
// commercial applications, and to alter it and redistribute it freely, subject to the 
// following restrictions:
// 
// 1. The origin of this software must not be misrepresented; you must not claim that 
// you wrote the original software. If you use this software in a product, an 
// acknowledgment (see the following) in the product documentation is required.
// 
// Portions Copyright (c) Brian A. Lakstins (http://www.lakstins.com/brian/)
// 
// 2. Altered source versions must be plainly marked as such, and must not be 
// misrepresented as being the original software.
// 
// 3. This notice may not be removed or altered from any source distribution.
// </license>
#endregion

#region Change Log
// <changelog>
// <change date="6/3/2014" author="Brian A. Lakstins" description="Initial Release">
// <change date="6/20/2014" author="Brian A. Lakstins" description="Moved encryption provider to MaxFactry_System.Web namespace.">
// <change date="6/23/2014" author="Brian A. Lakstins" description="Updates for testing.">
// <change date="6/27/2014" author="Brian A. Lakstins" description="Remove dependency on AppId.">
// <change date="11/30/2018" author="Brian A. Lakstins" description="Add configuration provider for current catalog.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.Mvc4
{
    using System;
    using System.Configuration;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;

    public class MaxStartup : MaxFactry.Base.MaxStartup
    {
        /// <summary>
        /// Internal storage of single object
        /// </summary>
        private static object _oInstance = null;

        /// <summary>
        /// Gets the single instance of this class.
        /// </summary>
        public new static MaxStartup Instance
        {
            get
            {
                _oInstance = CreateInstance(typeof(MaxStartup), _oInstance);
                return _oInstance as MaxStartup;
            }
        }

        public override void RegisterProviders()
        {
            MaxFactry.Module.Catalog.MaxStartup.Instance.RegisterProviders();
            //// Configure provider for MaxConfigurationLibrary
            MaxConfigurationLibrary.Instance.ProviderSet(
                typeof(MaxFactry.Core.Provider.MaxConfigurationLibraryCatalogProvider));
        }

        public override void SetProviderConfiguration(MaxIndex loConfig)
        {
            MaxFactry.Module.Catalog.MaxStartup.Instance.SetProviderConfiguration(loConfig);
        }

        public override void ApplicationStartup()
        {
            MaxFactry.Module.Catalog.MaxStartup.Instance.ApplicationStartup();
            
            RouteTable.Routes.MapHttpRoute(
                name: "MaxCatalogApiRoute",
                routeTemplate: "MaxCatalogApi/{action}/{lsId}",
                defaults: new { controller = "MaxCatalogApi", action = "index", lsId = UrlParameter.Optional }
            );
            
            RouteTable.Routes.MapRoute(
                name: "MaxCatalogRoute",
                url: "MaxCatalog/{action}/{id}",
                defaults: new { controller = "MaxCatalog", action = "Index", id = UrlParameter.Optional }
            );

            RouteTable.Routes.MapRoute(
                name: "MaxCatalogPartialRoute",
                url: "MaxCatalogPartial/{action}",
                defaults: new { controller = "MaxCatalogPartial", action = "Index" }
            );

            RouteTable.Routes.MapRoute(
                name: "MaxCatalogManageRoute",
                url: "MaxCatalogManage/{action}/{id}",
                defaults: new { controller = "MaxCatalogManage", action = "Index", id = UrlParameter.Optional }
            );

            RouteTable.Routes.MapRoute(
                name: "MaxCatalogManagePartialRoute",
                url: "MaxCatalogManagePartial/{action}/{id}",
                defaults: new { controller = "MaxCatalogManagePartial", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}