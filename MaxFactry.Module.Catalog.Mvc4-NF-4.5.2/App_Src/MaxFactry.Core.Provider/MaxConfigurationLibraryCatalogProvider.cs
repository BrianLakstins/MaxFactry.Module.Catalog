// <copyright file="MaxConfigurationLibraryAppProvider.cs" company="Lakstins Family, LLC">
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
// <change date="11/30/2018" author="Brian A. Lakstins" description="Initial creation">
// </changelog>
#endregion

namespace MaxFactry.Core.Provider
{
    using System;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using MaxFactry.Core;
    using MaxFactry.Core.Provider;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Module.App.BusinessLayer;
    using MaxFactry.Module.Catalog.BusinessLayer;
    using MaxFactry.Module.Catalog.PresentationLayer;

    /// <summary>
    /// MaxFactory Provider for use in app based web applications.
    /// </summary>
    public class MaxConfigurationLibraryCatalogProvider : MaxConfigurationLibraryAppProvider
    {
        public override object GetValue(MaxEnumGroup loScope, string lsKey)
        {
            if (loScope == MaxEnumGroup.Scope23)
            {
                MaxCatalogEntity loEntity = MaxCatalogEntity.GetCurrent();
                if (null != loEntity)
                {
                    MaxIndex loAttributeIndex = loEntity.AttributeIndex;
                    if (loAttributeIndex.Contains(lsKey))
                    {
                        return loAttributeIndex[lsKey];
                    }
                }
            }

            return base.GetValue(loScope, lsKey);
        }
    }
}