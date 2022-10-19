// <copyright file="MaxProductImageEntity.cs" company="Lakstins Family, LLC">
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

    public class MaxProductImageEntity : MaxBaseIdFileEntity
    {
		/// <summary>
        /// Initializes a new instance of the MaxProductEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxProductImageEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxProductEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxProductImageEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public Guid ProductId
        {
            get
            {
                return this.GetGuid(this.DataModel.ProductId);
            }

            set
            {
                this.Set(this.DataModel.ProductId, value);
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
        protected MaxProductImageDataModel DataModel
        {
            get
            {
                return (MaxProductImageDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }
        
        public static MaxProductImageEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxProductImageEntity),
                typeof(MaxProductImageDataModel)) as MaxProductImageEntity;
        }

        /// <summary>
        /// Gets a string that can be used to sort a list of this entity.
        /// </summary>
        /// <returns>Lowercase version of Name passed to 100 characters.</returns>
        public override string GetDefaultSortString()
        {
            return String.Format("{0:F15}", this.RelationOrder * Math.Pow(10, 15)) + this.Name.ToLowerInvariant().PadRight(100, ' ') + base.GetDefaultSortString();
        }

        public override bool LoadByIdCache(Guid loId)
        {
            MaxEntityList loEntityList = this.LoadAllCache();
            if (null != loEntityList)
            {
                for (int lnE = 0; lnE < loEntityList.Count; lnE++)
                {
                    MaxProductImageEntity loEntity = loEntityList[lnE] as MaxProductImageEntity;
                    if (null != loEntity && loEntity.Id == loId)
                    {
                        this.Load(loEntity.Data);
                        return true;
                    }
                }
            }

            return base.LoadByIdCache(loId);
        }

        public MaxEntityList LoadAllByProductId(Guid loProductId)
        {
            MaxEntityList loR = MaxEntityList.Create(this.GetType());
            MaxEntityList loEntityList = this.LoadAllCache();
            for (int lnE = 0; lnE < loEntityList.Count; lnE++)
            {
                MaxProductImageEntity loEntity = loEntityList[lnE] as MaxProductImageEntity;
                if (null != loEntity && loEntity.ProductId == loProductId)
                {
                    loR.Add(loEntity);
                }
            }

            return loR;
        }
    }
}
