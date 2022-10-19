// <copyright file="MaxShippingChargeDataModel.cs" company="Lakstins Family, LLC">
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
// <change date="3/12/2015" author="Brian A. Lakstins" description="Initial Release">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Data Model for order related information.
    /// </summary>
    public class MaxShippingChargeDataModel : MaxFactry.Base.DataLayer.MaxBaseIdDataModel
    {
        /// <summary>
        /// Name of the item
        /// </summary>
        public readonly string Name = "Name";

        /// <summary>
        /// Id of the primary reference
        /// </summary>
        public readonly string PrimaryReferenceId = "PrimaryReferenceId";

        /// <summary>
        /// Type of the primary reference
        /// </summary>
        public readonly string PrimaryReferenceType = "PrimaryReferenceType";

        /// <summary>
        /// Id of the secondary reference
        /// </summary>
        public readonly string SecondaryReferenceId = "SecondaryReferenceId";

        /// <summary>
        /// Type of secondary reference
        /// </summary>
        public readonly string SecondaryReferenceType = "SecondaryReferenceType";

        /// <summary>
        /// Amount of the charge
        /// </summary>
        public readonly string Amount = "Amount";

        /// <summary>
        /// Type of shipping the amount is associated with
        /// </summary>
        public readonly string ShippingType = "ShippingType";

        /// <summary>
        /// Initializes a new instance of the MaxCatalogDataModel class
        /// </summary>
        public MaxShippingChargeDataModel()
        {
            this.SetDataStorageName("MaxCatalogShippingCharge");
            this.RepositoryProviderType = typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogRepositoryProvider);
            this.RepositoryType = typeof(MaxCatalogRepository);
            this.AddType(this.Name, typeof(MaxShortString));
            this.AddType(this.PrimaryReferenceId, typeof(Guid));
            this.AddType(this.PrimaryReferenceType, typeof(MaxShortString));
            this.AddType(this.SecondaryReferenceId, typeof(Guid));
            this.AddType(this.SecondaryReferenceType, typeof(MaxShortString));
            this.AddType(this.Amount, typeof(double));
            this.AddType(this.ShippingType, typeof(int));
        }
    }
}
