// <copyright file="MaxProductImportRuleEntity.cs" company="Lakstins Family, LLC">
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
// <change date="12/17/2014" author="Brian A. Lakstins" description="Initial Release">
// <change date="12/18/2014" author="Brian A. Lakstins" description="Updates to follow core data access patterns.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxProductImportRuleEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {
        /// <summary>
        /// Initializes a new instance of the MaxProductImportRuleEntity class
        /// </summary>
        /// <param name="loData">object to hold data</param>
        public MaxProductImportRuleEntity() : base(typeof(MaxProductImportRuleDataModel))
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxProductImportRuleEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxProductImportRuleEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxProductImportRuleEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxProductImportRuleEntity(Type loDataModelType)
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

        public int RuleType
        {
            get
            {
                int lnR = this.GetInt(this.DataModel.RuleType);
                if (lnR.Equals(int.MinValue))
                {
                    lnR = 0;
                }

                return lnR;
            }

            set
            {
                this.Set(this.DataModel.RuleType, value);
            }
        }

        public string Key
        {
            get
            {
                return this.GetString(this.DataModel.Key);
            }

            set
            {
                this.Set(this.DataModel.Key, value);
            }
        }

        public string RuleData1
        {
            get
            {
                return this.GetString(this.DataModel.RuleData1);
            }

            set
            {
                this.Set(this.DataModel.RuleData1, value);
            }
        }

        public string RuleData2
        {
            get
            {
                return this.GetString(this.DataModel.RuleData2);
            }

            set
            {
                this.Set(this.DataModel.RuleData2, value);
            }
        }

        public string RuleData3
        {
            get
            {
                return this.GetString(this.DataModel.RuleData3);
            }

            set
            {
                this.Set(this.DataModel.RuleData3, value);
            }
        }

        public int ProcessOrder
        {
            get
            {
                int lnR = this.GetInt(this.DataModel.ProcessOrder);
                if (lnR.Equals(int.MinValue))
                {
                    lnR = 0;
                }

                return lnR;
            }

            set
            {
                this.Set(this.DataModel.ProcessOrder, value);
            }
        }

        public int ImportGroup
        {
            get
            {
                return this.GetInt(this.DataModel.ImportGroup);
            }

            set
            {
                this.Set(this.DataModel.ImportGroup, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxProductImportRuleDataModel DataModel
        {
            get
            {
                return (MaxProductImportRuleDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxProductImportRuleEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxProductImportRuleEntity),
                typeof(MaxProductImportRuleDataModel)) as MaxProductImportRuleEntity;
        }

        public MaxEntityList LoadAllByImportGroup(int lnImportGroup)
        {
            MaxDataList loDataList = MaxCatalogRepository.SelectAllByProperty(this.Data, this.DataModel.ImportGroup, lnImportGroup);
            MaxEntityList loEntityList = MaxEntityList.Create(this.GetType(), loDataList);
            return loEntityList;
        }

        /// <summary>
        /// Gets a string that can be used to sort a list of this entity.
        /// </summary>
        /// <returns>Lowercase version of Name passed to 100 characters.</returns>
        public override string GetDefaultSortString()
        {
            return this.ProcessOrder.ToString().ToLowerInvariant().PadLeft(100, '0') + base.GetDefaultSortString();
        }
    }
}
