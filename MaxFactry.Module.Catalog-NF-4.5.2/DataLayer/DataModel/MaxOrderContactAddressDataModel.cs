// <copyright file="MaxOrderContactAddressDataModel.cs" company="Lakstins Family, LLC">
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
// <change date="3/26/2014" author="Brian A. Lakstins" description="Initial Release">
// <change date="12/21/2016" author="Brian A. Lakstins" description="Updated to use PrimaryKey Suffix.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Data Model for order related information.
    /// </summary>
    public class MaxOrderContactAddressDataModel : MaxFactry.Base.DataLayer.MaxBaseUSPostalAddressDataModel
    {
        /// <summary>
        /// Id of the Order this Item is associated
        /// </summary>
        public readonly string OrderId = "OrderId";

        /// <summary>
        /// Id of the Order Contact Person data
        /// </summary>
        public readonly string OrderContactPersonId = "OrderContactPersonId";

        /// <summary>
        /// Initializes a new instance of the MaxOrderContactAddressDataModel class
        /// </summary>
        public MaxOrderContactAddressDataModel()
        {
            this.SetDataStorageName("MaxCatalogOrderContactAddress");
            this.RepositoryProviderType = typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogRepositoryProvider);
            this.RepositoryType = typeof(MaxCatalogRepository);
            this.AddType(this.OrderId, typeof(Guid));
            this.AddType(this.OrderContactPersonId, typeof(Guid));
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderContactAddressDataModel class.
        /// </summary>
        /// <param name="lsDataStorageName">Name to user for storage</param>
        public MaxOrderContactAddressDataModel(string lsDataStorageName) : this()
        {
            this.SetDataStorageName(lsDataStorageName);
        }

        /// <summary>
        /// Gets a suffix for the primary key based on the data to speed up future queries
        /// </summary>
        /// <param name="loData">Data to use to create the suffix</param>
        /// <returns>String to use as suffix for primary key</returns>
        public override string GetPrimaryKeySuffix(MaxData loData)
        {
            string lsR = base.GetPrimaryKeySuffix(loData);
            if (string.IsNullOrEmpty(lsR))
            {
                lsR = loData.Get(this.OrderId).ToString();
            }

            return lsR;
        }
    }
}
