﻿// <copyright file="MaxCategoryDataModel.cs" company="Lakstins Family, LLC">
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
    /// Class to define data structure
    /// </summary>
    public class MaxCategoryDataModel : MaxFactry.Base.DataLayer.MaxBaseIdDataModel
    {
        /// <summary>
        /// Id of the primary catalog that this category is related to.
        /// </summary>
        public readonly string PrimaryCatalogId = "PrimaryCatalogId";

        /// <summary>
        /// Name of the category
        /// </summary>
        public readonly string Name = "Name";

        /// <summary>
        /// Category Id for the parent category
        /// </summary>
        public readonly string ParentId = "ParentId";

        /// <summary>
        /// Type of this category.
        /// </summary>
        public readonly string CategoryType = "CategoryType";

        /// <summary>
        /// Options associated with this category
        /// </summary>
        public readonly string OptionList = "OptionList";

        /// <summary>
        /// Id of image to show when showing the product in a list.
        /// </summary>
        public readonly string PrimaryImageId = "PrimaryImageId";

        /// <summary>
        /// Number to indicate how this should be ordered with the category.
        /// </summary>
        public readonly string RelationOrder = "RelationOrder";

        /// <summary>
        /// Initializes a new instance of the MaxCategoryDataModel class
        /// </summary>
        public MaxCategoryDataModel()
        {
            this.SetDataStorageName("MaxCatalogCategory");
            this.RepositoryProviderType = typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogRepositoryProvider);
            this.RepositoryType = typeof(MaxCatalogRepository);
            this.AddType(this.PrimaryCatalogId, typeof(Guid));
            this.AddType(this.Name, typeof(string));
            this.AddNullable(this.ParentId, typeof(Guid));
            this.AddNullable(this.CategoryType, typeof(short));
            this.AddNullable(this.OptionList, typeof(long));
            this.AddNullable(this.PrimaryImageId, typeof(Guid));
            this.AddType(this.RelationOrder, typeof(double));
        }
    }
}
