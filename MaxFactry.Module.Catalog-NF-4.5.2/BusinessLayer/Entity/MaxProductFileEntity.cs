﻿// <copyright file="MaxProductFileEntity.cs" company="Lakstins Family, LLC">
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
// <change date="12/14/2015" author="Brian A. Lakstins" description="Initial creation">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxProductFileEntity : MaxBaseIdFileEntity
    {
        /// <summary>
        /// Initializes a new instance of the MaxProductEntity class
        /// </summary>
        /// <param name="loData">object to hold data</param>
        public MaxProductFileEntity(MaxData loData)
            : base(loData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxProductEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxProductFileEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public Guid ProductId
        {
            get
            {
                return this.GetGuid(this.ProductFileDataModel.ProductId);
            }

            set
            {
                this.Set(this.ProductFileDataModel.ProductId, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxProductFileDataModel ProductFileDataModel
        {
            get
            {
                return (MaxProductFileDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        /// <summary>
        /// Creates a new instance of the MaxProductFileEntity class.
        /// </summary>
        /// <returns>a new instance of the MaxVirtualTextFileEntity class.</returns>
        public static MaxProductFileEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxProductFileEntity),
                typeof(MaxProductFileDataModel)) as MaxProductFileEntity;
        }


    }
}
