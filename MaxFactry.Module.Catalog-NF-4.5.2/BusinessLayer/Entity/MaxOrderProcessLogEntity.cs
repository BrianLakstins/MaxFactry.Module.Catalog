// <copyright file="MaxOrderProcessLogEntity.cs" company="Lakstins Family, LLC">
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
// <change date="7/13/2014" author="Brian A. Lakstins" description="Initial Release">
// <change date="12/18/2014" author="Brian A. Lakstins" description="Updates to follow core data access patterns.">
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

    public class MaxOrderProcessLogEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {
        /// <summary>
        /// Initializes a new instance of the MaxOrderProcessLogEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxOrderProcessLogEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxOrderProcessLogEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxOrderProcessLogEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public Guid OrderId
        {
            get
            {
                return this.GetGuid(this.DataModel.OrderId);
            }

            set
            {
                this.Set(this.DataModel.OrderId, value);
            }
        }

        public Guid UserId
        {
            get
            {
                return this.GetGuid(this.DataModel.UserId);
            }

            set
            {
                this.Set(this.DataModel.UserId, value);
            }
        }

        public string UserName
        {
            get
            {
                return this.GetString(this.DataModel.UserName);
            }

            set
            {
                this.Set(this.DataModel.UserName, value);
            }
        }

        public string LogEntry
        {
            get
            {
                return this.GetString(this.DataModel.LogEntry);
            }

            set
            {
                this.Set(this.DataModel.LogEntry, value);
            }
        }

        public bool IsCustomerVisible
        {
            get
            {
                return this.GetBoolean(this.DataModel.IsCustomerVisible);
            }

            set
            {
                this.Set(this.DataModel.IsCustomerVisible, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxOrderProcessLogDataModel DataModel
        {
            get
            {
                return (MaxOrderProcessLogDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxOrderProcessLogEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxOrderProcessLogEntity),
                typeof(MaxOrderProcessLogDataModel)) as MaxOrderProcessLogEntity;
        }

        public MaxEntityList LoadAllByOrderId(Guid loOrderId)
        {
            MaxDataList loDataList = MaxCatalogRepository.SelectAllByProperty(this.Data, this.DataModel.OrderId, loOrderId);
            MaxEntityList loEntityList = MaxEntityList.Create(this.GetType(), loDataList);
            return loEntityList;
        }
    }
}
