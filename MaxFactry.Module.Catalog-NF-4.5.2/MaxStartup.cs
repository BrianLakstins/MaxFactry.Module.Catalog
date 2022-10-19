// <copyright file="MaxModule.cs" company="Lakstins Family, LLC">
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
// <change date="6/7/2015" author="Brian A. Lakstins" description="Initial creation">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog
{
    using System;
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
            MaxFactry.Module.Catalog.BusinessLayer.MaxPaymentLibrary.Instance.ProviderAdd(typeof(MaxFactry.Module.Catalog.BusinessLayer.Provider.MaxPaymentLibraryDefaultProvider));
            MaxFactry.Module.Catalog.BusinessLayer.MaxMailingListLibrary.Instance.ProviderAdd(typeof(MaxFactry.Module.Catalog.BusinessLayer.Provider.MaxMailingListLibraryDefaultProvider));
            MaxFactry.Module.Catalog.BusinessLayer.MaxCatalogLibrary.Instance.ProviderAdd(typeof(MaxFactry.Module.Catalog.BusinessLayer.Provider.MaxCrmLibraryDefaultProvider));
            MaxFactry.Module.Catalog.BusinessLayer.MaxCatalogLibrary.Instance.ProviderAdd(typeof(MaxFactry.Module.Catalog.BusinessLayer.Provider.MaxMailingListLibraryDefaultProvider));
            MaxFactry.Module.Catalog.BusinessLayer.MaxCatalogLibrary.Instance.ProviderAdd(typeof(MaxFactry.Module.Catalog.BusinessLayer.Provider.MaxPaymentLibraryDefaultProvider));
        }

        public override void SetProviderConfiguration(MaxIndex loConfig)
        {
            MaxFactryLibrary.SetValue(typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogRepositoryProvider) + "-Config", loConfig);
            MaxFactryLibrary.SetValue(typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogIdRepositoryProvider) + "-Config", loConfig);
            MaxFactryLibrary.SetValue(typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogFileRepositoryProvider) + "-Config", loConfig);
            MaxFactryLibrary.SetValue(typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxClientRepositoryProvider) + "-Config", loConfig);
            MaxFactryLibrary.SetValue(typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogSearchRepositoryProvider) + "-Config", loConfig);
        }

        public override void ApplicationStartup()
        {
        }
    }
}