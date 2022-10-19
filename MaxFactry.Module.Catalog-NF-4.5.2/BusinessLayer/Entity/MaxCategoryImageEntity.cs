// <copyright file="MaxCategoryImageEntity.cs" company="Lakstins Family, LLC">
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
// <change date="9/16/2014" author="Brian A. Lakstins" description="Added loading method. Renames for changes to core.">
// <change date="12/18/2014" author="Brian A. Lakstins" description="Updates to follow core data access patterns.">
// <change date="11/8/2017" author="Brian A. Lakstins" description="Remove unnecessary setlist calls">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using System.IO;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxCategoryImageEntity : MaxBaseIdFileEntity
    {
		/// <summary>
        /// Initializes a new instance of the MaxCategoryEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxCategoryImageEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxCategoryEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxCategoryImageEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public Guid CategoryId
        {
            get
            {
                return this.GetGuid(this.DataModel.CategoryId);
            }

            set
            {
                this.Set(this.DataModel.CategoryId, value);
            }
        }

        public string AlternateText
        {
            get
            {
                return this.GetString(this.DataModel.AlternateText);
            }

            set
            {
                this.Set(this.DataModel.AlternateText, value);
            }
        }

        public string ImageType
        {
            get
            {
                return this.GetString(this.DataModel.ImageType);
            }

            set
            {
                this.Set(this.DataModel.ImageType, value);
            }
        }

        public double RelationOrder
        {
            get
            {
                return this.GetDouble(this.DataModel.RelationOrder);
            }

            set
            {
                this.Set(this.DataModel.RelationOrder, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxCategoryImageDataModel DataModel
        {
            get
            {
                return (MaxCategoryImageDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }
        
        public static MaxCategoryImageEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxCategoryImageEntity),
                typeof(MaxCategoryImageDataModel)) as MaxCategoryImageEntity;
        }

        /// <summary>
        /// Gets a string that can be used to sort a list of this entity.
        /// </summary>
        /// <returns>Lowercase version of Name passed to 100 characters.</returns>
        public override string GetDefaultSortString()
        {
            return String.Format("{0:F15}", this.RelationOrder * Math.Pow(10,15)) + this.Name.ToLowerInvariant().PadRight(100, ' ') + base.GetDefaultSortString();
        }

        public MaxEntityList LoadAllByCategoryId(Guid loCategoryId)
        {
            MaxEntityList loR = MaxEntityList.Create(this.GetType());
            string lsCacheEntityKey = this.GetCacheKey() + ".LoadAll";
            MaxEntityList loEntityList = MaxCacheRepository.Get(this.GetType(), lsCacheEntityKey, typeof(MaxEntityList)) as MaxEntityList;
            if (null != loEntityList)
            {
                for (int lnE = 0; lnE < loEntityList.Count; lnE++)
                {
                    MaxCategoryImageEntity loEntity = loEntityList[lnE] as MaxCategoryImageEntity;
                    if (loEntity.CategoryId.Equals(loCategoryId))
                    {
                        loR.Add(loEntity);
                    }
                }
            }
            else
            {
                MaxDataList loDataList = MaxCatalogRepository.SelectAllByProperty(this.Data, this.DataModel.CategoryId, loCategoryId);
                loR = MaxEntityList.Create(this.GetType(), loDataList);
            }

            return loR;
        }
    }
}
