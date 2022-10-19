// <copyright file="MaxUSStateEntity.cs" company="Lakstins Family, LLC">
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
// <change date="8/1/2014" author="Brian A. Lakstins" description="Initial Release">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using MaxFactry.Core.BusinessLayer;
    using MaxFactry.Core.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxUSStateEntity : MaxBaseIdEntity
    {
		/// <summary>
        /// Initializes a new instance of the MaxUSStateEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxUSStateEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxUSStateEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxUSStateEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public string Name
        {
            get
            {
                return this.GetString(this.DataModel.Name);
            }
        }

        public string Abbreviation
        {
            get
            {
                return this.GetString(this.DataModel.Abbreviation);
            }
        }

        public int Code
        {
            get
            {
                return this.GetInt(this.DataModel.Code);
            }
        }

        public int Status
        {
            get
            {
                return this.GetInt(this.DataModel.Status);
            }
        }

        /// <summary>Max
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxUSStateDataModel DataModel
        {
            get
            {
                return (MaxUSStateDataModel)MaxFactryDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxUSStateEntity Create()
        {
            MaxDataModel loDataModel = MaxFactryDataLibrary.GetDataModel(typeof(MaxUSStateDataModel));
            MaxEntity loEntity = MaxFactryBusinessLibrary.GetEntity(typeof(MaxUSStateEntity), loDataModel);
            if (loEntity is MaxUSStateEntity)
            {
                return (MaxUSStateEntity)loEntity;
            }

            return null;
        }

        /// <summary>
        /// Loads all entities of this type based on status.
        /// </summary>
        /// <param name="laStatus">Valid status values.</param>
        /// <returns>List of entities</returns>
        public static MaxEntityList LoadAllByStatus(int[] laStatus)
        {
            MaxEntityList loEntityList = MaxEntityList.Create();
            MaxUSStateEntity loEntityBlank = MaxUSStateEntity.Create();
            MaxDataList loDataList = MaxUSStateRepository.SelectAllByStatus(new MaxData(loEntityBlank.DataModel), laStatus);
            for (int lnL = 0; lnL < loDataList.Count; lnL++)
            {
                MaxUSStateEntity loEntity = MaxUSStateEntity.Create();
                loEntity.Load(loDataList[lnL]);
                if (!loEntity.IsDeleted)
                {
                    loEntityList.Add(loEntity);
                }
            }

            return loEntityList;
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
