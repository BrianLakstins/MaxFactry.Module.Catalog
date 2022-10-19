// <copyright file="MaxOrderShippingContactPersonEntity.cs" company="Lakstins Family, LLC">
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
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxOrderShippingContactPersonEntity : MaxFactry.Base.BusinessLayer.MaxBasePersonEntity
    {
        /// <summary>
        /// Initializes a new instance of the MaxOrderShippingContactPersonEntity class
        /// </summary>
        /// <param name="loData">object to hold data</param>
        public MaxOrderShippingContactPersonEntity(MaxData loData)
            : base(loData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderShippingContactPersonEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxOrderShippingContactPersonEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public Guid LastShippingInfoId
        {
            get
            {
                return this.GetGuid(this.DataModel.LastShippingInfoId);
            }

            set
            {
                this.Set(this.DataModel.LastShippingInfoId, value);
            }
        }

        public string Notes
        {
            get
            {
                return this.GetString(this.DataModel.Notes);
            }

            set
            {
                this.Set(this.DataModel.Notes, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxOrderShippingContactPersonDataModel DataModel
        {
            get
            {
                return (MaxOrderShippingContactPersonDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxOrderShippingContactPersonEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxOrderShippingContactPersonEntity),
                typeof(MaxOrderShippingContactPersonDataModel)) as MaxOrderShippingContactPersonEntity;
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
