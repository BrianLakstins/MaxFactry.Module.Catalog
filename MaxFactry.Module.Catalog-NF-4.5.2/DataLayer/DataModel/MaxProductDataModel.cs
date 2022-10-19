// <copyright file="MaxProductDataModel.cs" company="Lakstins Family, LLC">
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
// <change date="9/16/2014" author="Brian A. Lakstins" description="Updated image reference.  Added descriptionlist.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Class to define data structure
    /// </summary>
    public class MaxProductDataModel : MaxFactry.Base.DataLayer.MaxBaseIdBranchDataModel
    {
        /// <summary>
        /// Id of the primary catalog that this product is related to.
        /// </summary>
        public readonly string PrimaryCatalogId = "PrimaryCatalogId";

        /// <summary>
        /// Id of the primary category that this product is related to.
        /// </summary>
        public readonly string PrimaryCategoryId = "PrimaryCategoryId";

        /// <summary>
        /// Id of the primary vendor for this product.
        /// </summary>
        public readonly string PrimaryVendorId = "PrimaryVendorId";

        /// <summary>
        /// Sku of the product at the primary vendor.
        /// </summary>
        public readonly string PrimaryVendorSku = "PrimaryVendorSku";

        /// <summary>
        /// Id of the primary manufacturer for this product.
        /// </summary>
        public readonly string PrimaryManufacturerId = "PrimaryManufacturerId";

        /// <summary>
        /// Sku of the product at the primary manufacturer.
        /// </summary>
        public readonly string PrimaryManufacturerSku = "PrimaryManufacturerSku";

        /// <summary>
        /// Name of the product
        /// </summary>
        public readonly string Name = "Name";

        /// <summary>
        /// Sku for the product
        /// </summary>
        public readonly string Sku = "Sku";

        /// <summary>
        /// Short description of the product
        /// </summary>
        public readonly string DescriptionShort = "DescriptionShort";

        /// <summary>
        /// Description of the product
        /// </summary>
        public readonly string Description = "Description";

        /// <summary>
        /// Key words associated with the product
        /// </summary>
        public readonly string Keywords = "Keywords";

        /// <summary>
        /// Notes associated with the product
        /// </summary>
        public readonly string Notes = "Notes";

        /// <summary>
        /// Amount of insurance needed when shipping the product
        /// </summary>
        public readonly string InsuranceAmount = "InsuranceAmount";

        /// <summary>
        /// Base price of the product
        /// </summary>
        public readonly string PriceBase = "PriceBase";

        /// <summary>
        /// Comparison or Retail price of the product
        /// </summary>
        public readonly string PriceCompare = "PriceCompare";

        /// <summary>
        /// Text used for per pricing
        /// </summary>
        public readonly string PerText = "PerText";

        /// <summary>
        /// Quantity of items included in the PerText
        /// </summary>
        public readonly string PerQuantity = "PerQuantity";

        /// <summary>
        /// Weight of the base product
        /// </summary>
        public readonly string Weight = "Weight";

        /// <summary>
        /// Volume of the base product
        /// </summary>
        public readonly string Volume = "Volume";

        /// <summary>
        /// Width of the product
        /// </summary>
        public readonly string Width = "Width";

        /// <summary>
        /// Height of the product
        /// </summary>
        public readonly string Height = "Height";

        /// <summary>
        /// Depth of the product
        /// </summary>
        public readonly string Depth = "Depth";

        /// <summary>
        /// Minimum quantity for the product
        /// </summary>
        public readonly string MinimumQuantity = "MinimumQuantity";

        /// <summary>
        /// Maximum quantity for the product
        /// </summary>
        public readonly string MaximumQuantity = "MaximumQuantity";

        /// <summary>
        /// Options associated with this product
        /// </summary>
        public readonly string OptionList = "OptionList";

        /// <summary>
        /// Information to determine price by quanity
        /// </summary>
        public readonly string PriceByQuantityList = "PriceByQuantityList";

        /// <summary>
        /// Information to determine extra prices
        /// </summary>
        public readonly string PriceExtra = "PriceExtra";

        /// <summary>
        /// Id of image to show when showing the product in a list.
        /// </summary>
        public readonly string PrimaryImageId = "PrimaryImageId";

        /// <summary>
        /// Multiple descriptions with a different header for each description.
        /// </summary>
        public readonly string DescriptionList = "DescriptionList";

        /// <summary>
        /// Id of parent product for variations
        /// Used to create a "Parent / Child" relationship where the Child Product
        /// is always related to the Parent product (like a Variation of the Parent).
        /// </summary>
        public readonly string ParentId = "ParentId";

        /// <summary>
        /// List of option names and their values.
        /// </summary>
        public readonly string OptionByNameList = "OptionByNameList";

        /// <summary>
        /// Multiple category id separated by spaces.
        /// </summary>
        public readonly string CategoryIdList = "CategoryIdList";

        /// <summary>
        /// Base shipping price of the product
        /// </summary>
        public readonly string PriceBaseShipping = "PriceBaseShipping";

        /// <summary>
        /// Initializes a new instance of the MaxProductDataModel class
        /// </summary>
        public MaxProductDataModel()
        {
            this.SetDataStorageName("MaxCatalogProduct");
            this.RepositoryProviderType = typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogRepositoryProvider);
            this.RepositoryType = typeof(MaxCatalogRepository);
            this.AddType(this.PrimaryCatalogId, typeof(Guid));
            this.AddType(this.PrimaryCategoryId, typeof(Guid));
            this.AddNullable(this.PrimaryManufacturerId, typeof(Guid));
            this.AddNullable(this.PrimaryManufacturerSku, typeof(string));
            this.AddNullable(this.PrimaryVendorId, typeof(Guid));
            this.AddNullable(this.PrimaryVendorSku, typeof(string));
            this.AddType(this.Name, typeof(string));
            this.AddType(this.Sku, typeof(string));
            this.AddNullable(this.DescriptionShort, typeof(string));
            this.AddNullable(this.Description, typeof(string));
            this.AddNullable(this.Keywords, typeof(string));
            this.AddNullable(this.Notes, typeof(string));
            this.AddNullable(this.InsuranceAmount, typeof(double));
            this.AddType(this.PriceBase, typeof(double));
            this.AddNullable(this.PriceCompare, typeof(double));
            this.AddNullable(this.PerText, typeof(string));
            this.AddNullable(this.PerQuantity, typeof(int));
            this.AddNullable(this.Weight, typeof(double));
            this.AddNullable(this.Volume, typeof(double));
            this.AddNullable(this.Width, typeof(double));
            this.AddNullable(this.Height, typeof(double));
            this.AddNullable(this.Depth, typeof(double));
            this.AddNullable(this.MinimumQuantity, typeof(int));
            this.AddNullable(this.MaximumQuantity, typeof(int));
            this.AddNullable(this.OptionList, typeof(long));
            this.AddNullable(this.PriceByQuantityList, typeof(string));
            this.AddNullable(this.PriceExtra, typeof(string));
            this.AddNullable(this.PrimaryImageId, typeof(Guid));
            this.AddNullable(this.DescriptionList, typeof(string));
            this.AddNullable(this.ParentId, typeof(Guid));
            this.AddNullable(this.OptionByNameList, typeof(string));
            this.AddNullable(this.CategoryIdList, typeof(string));
            this.AddNullable(this.PriceBaseShipping, typeof(double));
        }
    }
}
