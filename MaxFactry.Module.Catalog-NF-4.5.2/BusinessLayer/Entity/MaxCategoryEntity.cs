// <copyright file="MaxCategoryEntity.cs" company="Lakstins Family, LLC">
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
// <change date="6/17/2014" author="Brian A. Lakstins" description="Update to use instance data model instead of static.">
// <change date="6/25/2014" author="Brian A. Lakstins" description="Add properties.">
// <change date="6/28/2014" author="Brian A. Lakstins" description="Change to BaseId.">
// <change date="9/16/2014" author="Brian A. Lakstins" description="Added more loading methods. Added parent reference.">
// <change date="12/18/2014" author="Brian A. Lakstins" description="Updates to follow core data access patterns.">
// <change date="11/8/2017" author="Brian A. Lakstins" description="Remove unnecessary setlist calls">
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

    public class MaxCategoryEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {
        private MaxCategoryEntity _oParent = null;

        private List<MaxCategoryEntity> _oChildList = null;

		/// <summary>
        /// Initializes a new instance of the MaxCategoryEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxCategoryEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxCategoryEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxCategoryEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public string Name
        {
            get
            {
                return this.GetString(this.DataModel.Name);
            }

            set
            {
                this.Set(this.DataModel.Name, value);
            }
        }

        public Guid ParentId
        {
            get
            {
                return this.GetGuid(this.DataModel.ParentId);
            }

            set
            {
                this.Set(this.DataModel.ParentId, value);
            }
        }

        public Guid PrimaryCatalogId
        {
            get
            {
                return this.GetGuid(this.DataModel.PrimaryCatalogId);
            }

            set
            {
                this.Set(this.DataModel.PrimaryCatalogId, value);
            }
        }

        public MaxCategoryEntity Parent
        {
            get
            {
                if (null == this._oParent && !this.ParentId.Equals(Guid.Empty))
                {
                    MaxCategoryEntity loEntity = MaxCategoryEntity.Create();
                    loEntity.LoadByIdCache(this.ParentId);
                    this._oParent = loEntity;
                }

                return this._oParent;
            }

            set
            {
                this._oParent = value;
            }
        }

        public Guid PrimaryImageId
        {
            get
            {
                return this.GetGuid(this.DataModel.PrimaryImageId);
            }

            set
            {
                this.Set(this.DataModel.PrimaryImageId, value);
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

        public string Path
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a list of all entities for this AppId
        /// </summary>
        public List<MaxCategoryEntity> GetChildList(MaxEntityList loList)
        {
            if (null == this._oChildList)
            {
                SortedList<string, MaxCategoryEntity> loSortList = new SortedList<string, MaxCategoryEntity>();
                if (!Guid.Empty.Equals(this.Id))
                {
                    MaxEntityList loEntityList = loList;
                    if (null == loEntityList)
                    {
                        loEntityList = MaxCategoryEntity.Create().LoadAllByParentId(this.Id);
                    }

                    for (int lnE = 0; lnE < loEntityList.Count; lnE++)
                    {
                        MaxCategoryEntity loEntity = loEntityList[lnE] as MaxCategoryEntity;
                        if (loEntity.ParentId.Equals(this.Id))
                        {
                            loSortList.Add(loEntity.GetDefaultSortString(), loEntity);
                        }
                    }
                }

                this._oChildList = new List<MaxCategoryEntity>(loSortList.Values);
            }

            return this._oChildList;
        }

        /// <summary>
        /// Gets a list of all entities for this AppId
        /// </summary>
        public List<MaxCategoryEntity> GetChildList()
        {
            return this.GetChildList(null);
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxCategoryDataModel DataModel
        {
            get
            {
                return (MaxCategoryDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxCategoryEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxCategoryEntity),
                typeof(MaxCategoryDataModel)) as MaxCategoryEntity;
        }

        /// <summary>
        /// Gets a string that can be used to sort a list of this entity.
        /// </summary>
        /// <returns>Lowercase version of Name passed to 100 characters.</returns>
        public override string GetDefaultSortString()
        {
            double lnOrder = this.RelationOrder;
            if (lnOrder < 0)
            {
                lnOrder = 0;
            }

            lnOrder = lnOrder * Math.Pow(10, 5);
            return Convert.ToInt64(lnOrder).ToString("D10") + this.Name.ToLowerInvariant().PadRight(100, ' ') + base.GetDefaultSortString();
        }

        public MaxEntityList LoadAllByCatalogId(Guid loCatalogId)
        {
            MaxEntityList loR = MaxEntityList.Create(this.GetType());
            string lsCacheEntityKey = this.GetCacheKey() + ".LoadAll";
            MaxEntityList loEntityList = MaxCacheRepository.Get(this.GetType(), lsCacheEntityKey, typeof(MaxEntityList)) as MaxEntityList;
            if (null != loEntityList)
            {
                for (int lnE = 0; lnE < loEntityList.Count; lnE++)
                {
                    MaxCategoryEntity loEntity = loEntityList[lnE] as MaxCategoryEntity;
                    if (loEntity.PrimaryCatalogId.Equals(loCatalogId))
                    {
                        loR.Add(loEntity);
                    }
                }
            }
            else
            {
                MaxDataList loDataList = MaxCatalogRepository.SelectAllByProperty(this.Data, this.DataModel.PrimaryCatalogId, loCatalogId);
                loR = MaxEntityList.Create(this.GetType(), loDataList);
            }

            return this.MapParent(loR);
        }

        public MaxEntityList LoadAllByParentId(Guid loParentId)
        {
            MaxEntityList loR = MaxEntityList.Create(this.GetType());
            string lsCacheEntityKey = this.GetCacheKey() + ".LoadAll";
            MaxEntityList loEntityList = MaxCacheRepository.Get(this.GetType(), lsCacheEntityKey, typeof(MaxEntityList)) as MaxEntityList;
            if (null != loEntityList)
            {
                for (int lnE = 0; lnE < loEntityList.Count; lnE++)
                {
                    MaxCategoryEntity loEntity = loEntityList[lnE] as MaxCategoryEntity;
                    if (loEntity.ParentId.Equals(loParentId))
                    {
                        loR.Add(loEntity);
                    }
                }
            }
            else
            {
                MaxDataList loDataList = MaxCatalogRepository.SelectAllByProperty(this.Data, this.DataModel.ParentId, loParentId);
                loR = MaxEntityList.Create(this.GetType(), loDataList);
            }

            return loR;
        }

        public MaxEntityList MapParent(MaxEntityList loList)
        {
            MaxIndex loIndex = new MaxIndex();
            for (int lnE = 0; lnE < loList.Count; lnE++)
            {
                loIndex.Add(((MaxCategoryEntity)loList[lnE]).Id.ToString(), loList[lnE]);
            }

            string[] laKeyList = loIndex.GetSortedKeyList();
            foreach (string lsKey in laKeyList)
            {
                MaxCategoryEntity loEntity = (MaxCategoryEntity)loIndex[lsKey];
                if (!loEntity.ParentId.Equals(Guid.Empty) &&
                    loIndex.Contains(loEntity.ParentId.ToString()))
                {
                    loEntity.Parent = (MaxCategoryEntity)loIndex[loEntity.ParentId.ToString()];
                    SortedList<string, MaxCategoryEntity> loSortedList = new SortedList<string, MaxCategoryEntity>();
                    loSortedList.Add(loEntity.GetDefaultSortString(), loEntity);
                    List<MaxCategoryEntity> loChildList = ((MaxCategoryEntity)loIndex[loEntity.ParentId.ToString()]).GetChildList(loList);
                }
            }

            return loList;
        }

        public MaxEntityList SortByPath(MaxEntityList loList, string lsPathSeparator)
        {
            MaxIndex loIndex = new MaxIndex();
            for (int lnE = 0; lnE < loList.Count; lnE++)
            {
                MaxCategoryEntity loEntity = (MaxCategoryEntity)loList[lnE];
                loEntity.Path = loEntity.Name + lsPathSeparator;
                MaxCategoryEntity loParent = loEntity.Parent;
                while (null != loParent)
                {
                    loEntity.Path = loParent.Name + lsPathSeparator + loEntity.Path;
                    loParent = loParent.Parent;
                }

                loIndex.Add(loEntity.Path.PadRight(2000, ' ') + Guid.NewGuid(), loEntity);
            }

            MaxEntityList loEntityList = MaxEntityList.Create(this.GetType());
            string[] laKeyList = loIndex.GetSortedKeyList();
            foreach (string lsKey in laKeyList)
            {
                loEntityList.Add((MaxCategoryEntity)loIndex[lsKey]);
            }

            return loEntityList;
        }
    }
}
