// <copyright file="MaxDiscountCodeDataModel.cs" company="Lakstins Family, LLC">
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
// <change date="11/1/2015" author="Brian A. Lakstins" description="Initial creation">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Data Model for some type of discount.  
    /// </summary>
    public class MaxDiscountCodeDataModel : MaxFactry.Base.DataLayer.MaxBaseIdDataModel
    {
        /// <summary>
        /// Name
        /// </summary>
        public readonly string Name = "Name";

        /// <summary>
        /// Text used to refer to this type of discount (coupon, gift card, etc)
        /// </summary>
        public readonly string ReferType = "ReferType";

        /// <summary>
        /// Description
        /// </summary>
        public readonly string Description = "Description";

        /// <summary>
        /// Note for internal information about the discount
        /// </summary>
        public readonly string InternalNote = "InternalNote";

        /// <summary>
        /// Code the user has to type in
        /// </summary>
        public readonly string Code = "Code";

        /// <summary>
        /// Date code becomes valid if it is active
        /// </summary>
        public readonly string StartDate = "StartDate";

        /// <summary>
        /// Date code is no longer usable
        /// </summary>
        public readonly string EndDate = "EndDate";

        /// <summary>
        /// Number of times this code can be used
        /// </summary>
        public readonly string UseCount = "UseCount";

        /// <summary>
        /// Number of times this code has be used
        /// </summary>
        public readonly string UsedCount = "UsedCount";

        /// <summary>
        /// Minimum amount of product total needed for discount code to be used
        /// </summary>
        public readonly string MinimumAmount = "MinimumAmount";

        /// <summary>
        /// Maximum amount of product total needed for discount code to be used
        /// </summary>
        public readonly string MaximumAmount = "MaximumAmount";

        /// <summary>
        /// Minimum count of matching product needed for discount code to be used
        /// </summary>
        public readonly string MinimumQuantity = "MinimumQuantity";

        /// <summary>
        /// Maximum count of matching product needed for discount code to be used
        /// </summary>
        public readonly string MaximumQuanitity = "MaximumQuanitity";

        /// <summary>
        /// List of product IDs that this code has to be used for 
        /// </summary>
        public readonly string ProductIdListText = "ProductIdListText";

        /// <summary>
        /// List of skus that this code has to be used for 
        /// </summary>
        public readonly string ProductSkuListText = "ProductSkuListText";

        /// <summary>
        /// List of category IDs that this code has to be used for 
        /// </summary>
        public readonly string CategoryIdListText = "CategoryIdListText";

        /// <summary>
        /// Percentage off the discount code gives
        /// </summary>
        public readonly string PercentOff = "PercentOff";

        /// <summary>
        /// Amount off the discount code gives
        /// </summary>
        public readonly string AmountOff = "AmountOff";

        /// <summary>
        /// Whether this code gives free shipping
        /// </summary>
        public readonly string IsFreeShipping = "IsFreeShipping";

        /// <summary>
        /// Method used to give discount
        /// </summary>
        public readonly string Calculation = "Calculation";

        /// <summary>
        /// Names of user accounts that this discount can be used for
        /// </summary>
        public readonly string UsernameListText = "UsernameListText";

        /// <summary>
        /// Ids of user accounts that this discount can be used for
        /// </summary>
        public readonly string UserIdListText = "UserIdListText";

        /// <summary>
        /// Indicates that the code should be always be used even when not entered
        /// </summary>
        public readonly string IsUseAlways = "IsUseAlways";

        /// <summary>
        /// Group name that limits to only one code per group
        /// </summary>
        public readonly string Group = "Group";

        /// <summary>
        /// Initializes a new instance of the MaxDiscountCodeDataModel class
        /// </summary>
        public MaxDiscountCodeDataModel()
        {
            this.SetDataStorageName("MaxCatalogDiscountCode");
            this.RepositoryProviderType = typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogRepositoryProvider);
            this.RepositoryType = typeof(MaxCatalogRepository);
            this.AddType(this.Name, typeof(string));
            this.AddType(this.ReferType, typeof(string));
            this.AddType(this.Description, typeof(MaxLongString));
            this.AddType(this.InternalNote, typeof(MaxLongString));
            this.AddType(this.Code, typeof(MaxShortString));
            this.AddType(this.StartDate, typeof(DateTime));
            this.AddType(this.EndDate, typeof(DateTime));
            this.AddNullable(this.UseCount, typeof(int));
            this.AddNullable(this.UsedCount, typeof(int));
            this.AddNullable(this.MinimumAmount, typeof(double));
            this.AddNullable(this.MaximumAmount, typeof(double));
            this.AddNullable(this.MinimumQuantity, typeof(int));
            this.AddNullable(this.MaximumQuanitity, typeof(int));
            this.AddNullable(this.ProductIdListText, typeof(MaxLongString));
            this.AddNullable(this.ProductSkuListText, typeof(MaxLongString));
            this.AddNullable(this.CategoryIdListText, typeof(MaxLongString));
            this.AddNullable(this.PercentOff, typeof(double));
            this.AddNullable(this.AmountOff, typeof(double));
            this.AddNullable(this.IsFreeShipping, typeof(bool));
            this.AddType(this.Calculation, typeof(int));
            this.AddNullable(this.UsernameListText, typeof(string));
            this.AddNullable(this.UserIdListText, typeof(string));
            this.AddType(this.IsUseAlways, typeof(bool));
            this.AddNullable(this.Group, typeof(string));
        }
    }
}
