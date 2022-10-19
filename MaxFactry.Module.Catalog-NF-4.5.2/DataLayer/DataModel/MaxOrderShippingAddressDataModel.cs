// <copyright file="MaxOrderItemDataModel.cs" company="Lakstins Family, LLC">
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
// <change date="7/13/2014" author="Brian A. Lakstins" description="Initial Release">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Data Model for order related information.
    /// </summary>
    public class MaxOrderShippingAddressDataModel : MaxFactry.Base.DataLayer.MaxBaseUSPostalAddressDataModel
    {
        /// <summary>
        /// Id of the last shipping Info that this address was associated with.
        /// </summary>
        public readonly string LastShippingInfoId = "LastShippingInfoId";

        /// <summary>
        /// Name of this Address.
        /// </summary>
        public readonly string Name = "Name";

        /// <summary>
        /// Notes about shipping to this address.
        /// </summary>
        public readonly string Notes = "Notes";
        
        /// <summary>
        /// Initializes a new instance of the MaxCatalogDataModel class
        /// </summary>
        public MaxOrderShippingAddressDataModel()
        {
            this.SetDataStorageName("MaxCatalogOrderShippingAddress");
            this.RepositoryProviderType = typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogRepositoryProvider);
            this.RepositoryType = typeof(MaxCatalogRepository);
            this.AddType(this.LastShippingInfoId, typeof(Guid));
            this.AddType(this.Name, typeof(MaxShortString));
            this.AddNullable(this.Notes, typeof(string));
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderShippingAddressDataModel class.
        /// </summary>
        /// <param name="lsDataStorageName">Name to user for storage</param>
        public MaxOrderShippingAddressDataModel(string lsDataStorageName) : this()
        {
            this.SetDataStorageName(lsDataStorageName);
        }
    }
}
