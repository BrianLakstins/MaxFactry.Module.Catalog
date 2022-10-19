// <copyright file="MaxOrderIdEntity.cs" company="Lakstins Family, LLC">
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
// <change date="6/23/2014" author="Brian A. Lakstins" description="Fix stack overflow.">
// <change date="6/25/2014" author="Brian A. Lakstins" description="Add default sorting.">
// <change date="6/28/2014" author="Brian A. Lakstins" description="Change to BaseId.">
// <change date="12/18/2014" author="Brian A. Lakstins" description="Updates to follow core data access patterns.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxOrderIdEntity : MaxFactry.Base.BusinessLayer.MaxIdIntegerEntity
    {
		/// <summary>
        /// Initializes a new instance of the MaxOrderIdEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxOrderIdEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxOrderIdEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxOrderIdEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxOrderIdDataModel DataModel
        {
            get
            {
                return (MaxOrderIdDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxOrderIdEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxOrderIdEntity),
                typeof(MaxOrderIdDataModel)) as MaxOrderIdEntity;
        }

        /// <summary>
        /// Gets a string that can be used to sort a list of this entity.
        /// </summary>
        /// <returns>Lowercase version of Name passed to 100 characters.</returns>
        public override string GetDefaultSortString()
        {
            return this.Id.ToString().ToLowerInvariant().PadRight(100, ' ') + base.GetDefaultSortString();
        }

        public override bool Insert()
        {
            DateTime ldIdDate = DateTime.UtcNow;
            //// Format of start Id Integer is YYMMDD000
            long lnId = (ldIdDate.Year - 2000) * 10000000;
            lnId += ldIdDate.Month * 100000;
            lnId += ldIdDate.Day * 1000;

            MaxDataList loList = MaxCatalogIdRepository.SelectAllByCreatedDateRange(this.Data, ldIdDate.AddDays(-1), ldIdDate.AddHours(1));
            if (loList.Count > 0)
            {
                for (int lnD = 0; lnD < loList.Count; lnD++)
                {
                    long lnIdTest = MaxFactry.Core.MaxConvertLibrary.ConvertToLong(typeof(object), loList[lnD].Get(this.DataModel.Id));
                    if (lnIdTest >= lnId)
                    {
                        lnId = lnIdTest + 1;
                    }
                }
            }

            int lnLimit = 10;
            int lnTry = 0;
            bool lbR = false;
            while (!lbR && lnTry < lnLimit)
            {
                try
                {
                    lbR = this.Insert(lnId);
                }
                catch
                {
                    lbR = false;
                }

                lnTry++;
                lnId++;
            }

            return lbR;
        }
    }
}
