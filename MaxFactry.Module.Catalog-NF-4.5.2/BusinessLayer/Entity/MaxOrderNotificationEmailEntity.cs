// <copyright file="MaxOrderNotificationEmailEntity.cs" company="Lakstins Family, LLC">
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
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxOrderNotificationEmailEntity : MaxFactry.Base.BusinessLayer.MaxBaseEmailEntity
    {
		/// <summary>
        /// Initializes a new instance of the MaxOrderNotificationEmailEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxOrderNotificationEmailEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxOrderNotificationEmailEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxOrderNotificationEmailEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxOrderNotificationEmailDataModel DataModel
        {
            get
            {
                return (MaxOrderNotificationEmailDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxOrderNotificationEmailEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxOrderNotificationEmailEntity),
                typeof(MaxOrderNotificationEmailDataModel)) as MaxOrderNotificationEmailEntity;
        }
    }
}
