// <copyright file="MaxCartItemEntity.cs" company="Lakstins Family, LLC">
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
// <change date="12/18/2014" author="Brian A. Lakstins" description="Updates to follow core data access patterns.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxCartItemEntity : MaxProductSelectionEntity
    {
		/// <summary>
        /// Initializes a new instance of the MaxCartItemEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxCartItemEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxCartItemEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxCartItemEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxCartItemDataModel DataModel
        {
            get
            {
                return (MaxCartItemDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxCartItemEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxCartItemEntity),
                typeof(MaxCartItemDataModel)) as MaxCartItemEntity;
        }

        /// <summary>
        /// Gets a string that can be used to sort a list of this entity.
        /// </summary>
        /// <returns>Lowercase version of Name passed to 100 characters.</returns>
        public override string GetDefaultSortString()
        {
            return this.Name.ToLowerInvariant().PadRight(100, ' ') + base.GetDefaultSortString();
        }
    }
}
