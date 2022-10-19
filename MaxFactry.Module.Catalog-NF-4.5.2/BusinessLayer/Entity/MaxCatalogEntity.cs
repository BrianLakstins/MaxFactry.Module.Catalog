// <copyright file="MaxCatalogEntity.cs" company="Lakstins Family, LLC">
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
// <change date="12/18/2014" author="Brian A. Lakstins" description="Updates to follow core data access patterns.">
// <change date="11/30/2018" author="Brian A. Lakstins" description="Added getting current catalog.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxCatalogEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {
        private const string _sIdKey = "__MaxCatalogIdCurrent";

        private const string _sEntityKey = "__MaxCatalogEntityCurrent";

        /// <summary>
        /// Initializes a new instance of the MaxCatalogEntity class
        /// </summary>
        /// <param name="loData">object to hold data</param>
        public MaxCatalogEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxCatalogEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxCatalogEntity(Type loDataModelType)
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

        public Guid ClientId
        {
            get
            {
                return this.GetGuid(this.DataModel.ClientId);
            }

            set
            {
                this.Set(this.DataModel.ClientId, value);
            }
        }


        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxCatalogDataModel DataModel
        {
            get
            {
                return (MaxCatalogDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxCatalogEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxCatalogEntity),
                typeof(MaxCatalogDataModel)) as MaxCatalogEntity;
        }

        public static MaxCatalogEntity GetCurrent()
        {
            object loObject = MaxConfigurationLibrary.GetValue(MaxEnumGroup.ScopeProcess, _sEntityKey);
            if (null != loObject && loObject is MaxCatalogEntity)
            {
                return (MaxCatalogEntity)loObject;
            }

            // Look up the current id in the profile.
            loObject = MaxConfigurationLibrary.GetValue(MaxEnumGroup.ScopeProfile, _sIdKey);
            if (null != loObject)
            {
                Guid loId = MaxConvertLibrary.ConvertToGuid(typeof(object), loObject);
                if (!Guid.Empty.Equals(loId))
                {
                    MaxCatalogEntity loEntity = MaxCatalogEntity.Create();
                    if (loEntity.LoadByIdCache(loId))
                    {
                        if (loEntity.IsActive)
                        {
                            MaxConfigurationLibrary.SetValue(MaxEnumGroup.ScopeProcess, _sEntityKey, loEntity);
                            return loEntity;
                        }
                    }
                }
            }

            return null;
        }

        public void SetCurrent()
        {
            if (this.IsActive)
            {
                MaxConfigurationLibrary.SetValue(MaxEnumGroup.ScopeProcess, _sEntityKey, this);
                if (Guid.Empty != this.Id)
                {
                    MaxConfigurationLibrary.SetValue(MaxEnumGroup.ScopeProfile, _sIdKey, this.Id);
                }
            }
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
