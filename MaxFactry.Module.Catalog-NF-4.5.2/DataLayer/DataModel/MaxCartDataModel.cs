// <copyright file="MaxCartDataModel.cs" company="Lakstins Family, LLC">
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
// <change date="7/8/2014" author="Brian A. Lakstins" description="Initial Release">
// <change date="7/9/2014" author="Brian A. Lakstins" description="Change to ReferenceType.  Remove some storage properties.">
// <change date="5/27/2015" author="Brian A. Lakstins" description="Added Manual Discount.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Data Model for cart related information.
    /// </summary>
    public class MaxCartDataModel : MaxFactry.Base.DataLayer.MaxBaseIdDataModel
    {
        /// <summary>
        /// Type of reference
        /// </summary>
        public readonly string ReferenceType = "ReferenceType";

        /// <summary>
        /// Id used as reference for this Cart
        /// </summary>
        public readonly string ReferenceId = "ReferenceId";

        /// <summary>
        /// Sum of all items in the catalog
        /// </summary>
        public readonly string ItemTotal = "ItemTotal";

        /// <summary>
        /// Number of items in the cart
        /// </summary>
        public readonly string ItemCount = "ItemCount";

        /// <summary>
        /// Sum of all charges associated with shipping
        /// </summary>
        public readonly string ShippingTotal = "ShippingTotal";

        /// <summary>
        /// Sum of all charges associated with shipping
        /// </summary>
        public readonly string IsShippingTotalOverride = "IsShippingTotalOverride";

        /// <summary>
        /// Sum of all discounts
        /// </summary>
        public readonly string DiscountTotal = "DiscountTotal";

        /// <summary>
        /// Explanation of discount total
        /// </summary>
        public readonly string DiscountTotalExplanation = "DiscountTotalExplanation";

        /// <summary>
        /// Manual Discount amount
        /// </summary>
        public readonly string ManualDiscount = "ManualDiscount";

        /// <summary>
        /// Explanation of manual discount
        /// </summary>
        public readonly string ManualDiscountExplanation = "ManualDiscountExplanation";
        
        /// <summary>
        /// Sum of all charges associated with tax
        /// </summary>
        public readonly string TaxTotal = "TaxTotal";

        /// <summary>
        /// Location used to determine Tax.
        /// </summary>
        public readonly string TaxLocation = "TaxLocation";

        /// <summary>
        /// Sum of all charges associated with other 1 charge
        /// </summary>
        public readonly string Other1Total = "Other1Total";

        /// <summary>
        /// Name of Other1 charge
        /// </summary>
        public readonly string Other1Name = "Other1Name";

        /// <summary>
        /// Sum of all charges associated with other 2 charge
        /// </summary>
        public readonly string Other2Total = "Other2Total";

        /// <summary>
        /// Name of Other2 charge
        /// </summary>
        public readonly string Other2Name = "Other2Name";

        /// <summary>
        /// Sum of all charges associated with other 3 charge
        /// </summary>
        public readonly string Other3Total = "Other3Total";

        /// <summary>
        /// Name of Other3 charge
        /// </summary>
        public readonly string Other3Name = "Other3Name";

        /// <summary>
        /// Sum of all charges associated with the cart
        /// </summary>
        public readonly string Total = "Total";

        /// <summary>
        /// Shipping type selected
        /// </summary>
        public readonly string ShippingType = "ShippingType";

        /// <summary>
        /// List of coupon codes entered for this cart.
        /// </summary>
        public readonly string CouponCodeList = "CouponCodeList";
                
        /// <summary>
        /// Rate to charge sales tax
        /// </summary>
        public readonly string SalesTaxRate = "SalesTaxRate";

        /// <summary>
        /// Username that created the cart or changed the cart last
        /// </summary>
        public readonly string Username = "Username";

        /// <summary>
        /// UserId that created the cart or changed the cart last
        /// </summary>
        public readonly string UserId = "UserId";

        /// <summary>
        /// Amount calculated to charge tax on
        /// </summary>
        public readonly string TaxableAmount = "TaxableAmount";

        /// <summary>
        /// Override Sales tax calculations
        /// </summary>
        public readonly string TaxOverride = "TaxOverride";

        /// <summary>
        /// Initializes a new instance of the MaxCatalogDataModel class
        /// </summary>
        public MaxCartDataModel()
        {
            this.SetDataStorageName("MaxCatalogCart");
            this.RepositoryProviderType = typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogRepositoryProvider);
            this.RepositoryType = typeof(MaxCatalogRepository);
            this.AddType(this.ReferenceType, typeof(MaxShortString));
            this.AddType(this.ReferenceId, typeof(Guid));
            this.AddType(this.ItemTotal, typeof(double));
            this.AddType(this.ItemCount, typeof(int));
            this.AddType(this.ShippingTotal, typeof(double));
            this.AddType(this.DiscountTotal, typeof(double));
            this.AddNullable(this.DiscountTotalExplanation, typeof(string));
            this.AddType(this.ManualDiscount, typeof(double));
            this.AddNullable(this.ManualDiscountExplanation, typeof(string));
            this.AddType(this.TaxTotal, typeof(double));
            this.AddType(this.TaxLocation, typeof(MaxShortString));
            this.AddType(this.Other1Total, typeof(double));
            this.AddNullable(this.Other1Name, typeof(MaxShortString));
            this.AddType(this.Other2Total, typeof(double));
            this.AddNullable(this.Other2Name, typeof(MaxShortString));
            this.AddType(this.Other3Total, typeof(double));
            this.AddNullable(this.Other3Name, typeof(MaxShortString));
            this.AddType(this.Total, typeof(double));
            this.AddType(this.ShippingType, typeof(int));
            this.AddNullable(this.CouponCodeList, typeof(string));
            this.AddNullable(this.SalesTaxRate, typeof(double));
            this.AddNullable(this.Username, typeof(MaxShortString));
            this.AddNullable(this.UserId, typeof(MaxShortString));
            this.AddType(this.IsShippingTotalOverride, typeof(bool));
            this.AddNullable(this.TaxableAmount, typeof(double));
            this.AddNullable(this.TaxOverride, typeof(int));
        }

        /// <summary>
        /// Initializes a new instance of the MaxCartDataModel class.
        /// </summary>
        /// <param name="lsDataStorageName">Name to user for storage</param>
        public MaxCartDataModel(string lsDataStorageName) : this()
        {
            this.SetDataStorageName(lsDataStorageName);
        }
    }
}
