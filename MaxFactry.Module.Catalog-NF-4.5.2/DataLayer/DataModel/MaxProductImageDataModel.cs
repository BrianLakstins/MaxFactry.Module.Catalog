// <copyright file="MaxProductImageDataModel.cs" company="Lakstins Family, LLC">
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
// <change date="8/18/2014" author="Brian A. Lakstins" description="Initial Release">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.DataLayer
{
    using System;
    using System.IO;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Class to define data structure
    /// </summary>
    public class MaxProductImageDataModel : MaxBaseIdFileDataModel
    {
        /// <summary>
        /// Id of the product this image is related to.
        /// </summary>
        public readonly string ProductId = "ProductId";

        /// <summary>
        /// Type of image related to the product.
        /// </summary>
        public readonly string ImageType = "ImageType";

        /// <summary>
        /// Alternate text to show when displaying an image.
        /// </summary>
        public readonly string AlternateText = "AlternateText";

        /// <summary>
        /// Number to indicate how this should be ordered with the product.
        /// </summary>
        public readonly string RelationOrder = "RelationOrder";

        /// <summary>
        /// Initializes a new instance of the MaxProductDataModel class
        /// </summary>
        public MaxProductImageDataModel()
        {
            this.SetDataStorageName("MaxCatalogProductImage");
            this.RepositoryProviderType = typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogFileRepositoryProvider);
            this.RepositoryType = typeof(MaxCatalogFileRepository);
            this.AddType(this.ProductId, typeof(Guid));
            this.AddType(this.ImageType, typeof(MaxShortString));
            this.AddType(this.AlternateText, typeof(string));
            this.AddType(this.RelationOrder, typeof(double));
        }
    }
}
