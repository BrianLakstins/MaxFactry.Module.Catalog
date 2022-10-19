// <copyright file="MaxOrderIdDataModel.cs" company="Lakstins Family, LLC">
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
// <change date="6/16/2014" author="Brian A. Lakstins" description="Initial Release">
// <change date="6/25/2014" author="Brian A. Lakstins" description="Update storage name.">
// <change date="6/28/2014" author="Brian A. Lakstins" description="Change to BaseId.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Data Model for creating an integer based id for orders
    /// </summary>
    public class MaxOrderIdDataModel : MaxFactry.Base.DataLayer.MaxIdIntegerDataModel
    {
        /// <summary>
        /// Initializes a new instance of the MaxOrderIdDataModel class
        /// </summary>
        public MaxOrderIdDataModel()
        {
            this.SetDataStorageName("MaxCatalogOrderId");
            this.RepositoryProviderType = typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogIdRepositoryProvider);
            this.RepositoryType = typeof(MaxCatalogIdRepository);
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderIdDataModel class.
        /// </summary>
        /// <param name="lsDataStorageName">Name to user for storage</param>
        public MaxOrderIdDataModel(string lsDataStorageName) : this()
        {
            this.SetDataStorageName(lsDataStorageName);
        }
    }
}
