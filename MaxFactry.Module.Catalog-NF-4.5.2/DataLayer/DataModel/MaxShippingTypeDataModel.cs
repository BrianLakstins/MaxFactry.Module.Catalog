// <copyright file="MaxShippingTypeDataModel.cs" company="Lakstins Family, LLC">
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
// <change date="5/27/2015" author="Brian A. Lakstins" description="Added Description">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Data Model for order related information.
    /// </summary>
    public class MaxShippingTypeDataModel : MaxFactry.Base.DataLayer.MaxBaseIdDataModel
    {
        /// <summary>
        /// Name of the Shipping Type
        /// </summary>
        public readonly string Name = "Name";

        /// <summary>
        /// Description of the Shipping Type
        /// </summary>
        public readonly string Description = "Description";

        /// <summary>
        /// Reference number used for the shipping type
        /// </summary>
        public readonly string ShippingType = "ShippingType";

        /// <summary>
        /// Number to indicate how this should be ordered with the category.
        /// </summary>
        public readonly string RelationOrder = "RelationOrder";

        /// <summary>
        /// Shipping Calculation
        /// </summary>
        public readonly string ShippingCalculation = "ShippingCalculation";

        /// <summary>
        /// Can be selected
        /// </summary>
        public readonly string IsSelectable = "IsSelectable";

        /// <summary>
        /// Initializes a new instance of the MaxCatalogDataModel class
        /// </summary>
        public MaxShippingTypeDataModel()
        {
            this.SetDataStorageName("MaxCatalogShippingType");
            this.RepositoryProviderType = typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogRepositoryProvider);
            this.RepositoryType = typeof(MaxCatalogRepository);
            this.AddType(this.Name, typeof(MaxShortString));
            this.AddNullable(this.Description, typeof(string));
            this.AddType(this.ShippingType, typeof(int));
            this.AddType(this.RelationOrder, typeof(double));
            this.AddType(this.ShippingCalculation, typeof(string));
            this.AddNullable(this.IsSelectable, typeof(bool));
        }
    }
}
