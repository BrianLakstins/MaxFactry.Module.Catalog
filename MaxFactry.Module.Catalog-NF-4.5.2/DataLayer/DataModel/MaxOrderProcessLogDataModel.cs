﻿// <copyright file="MaxOrderDataModel.cs" company="Lakstins Family, LLC">
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
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Data Model for order related information.
    /// </summary>
    public class MaxOrderProcessLogDataModel : MaxFactry.Base.DataLayer.MaxBaseIdDataModel
    {
        /// <summary>
        /// Id of Order this log entry is related to
        /// </summary>
        public readonly string OrderId = "OrderId";

        /// <summary>
        /// Id of user this log entry is related to
        /// </summary>
        public readonly string UserId = "UserId";

        /// <summary>
        /// User name of user this log entry is related to
        /// </summary>
        public readonly string UserName = "UserName";

        /// <summary>
        /// Log text for this entry
        /// </summary>
        public readonly string LogEntry = "LogEntry";

        /// <summary>
        /// Log text for this entry
        /// </summary>
        public readonly string IsCustomerVisible = "IsCustomerVisible";

        /// <summary>
        /// Initializes a new instance of the MaxOrderDataModel class
        /// </summary>
        public MaxOrderProcessLogDataModel()
        {
            this.SetDataStorageName("MaxCatalogOrderProcessLog");
            this.RepositoryProviderType = typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogRepositoryProvider);
            this.RepositoryType = typeof(MaxCatalogRepository);
            this.AddType(this.OrderId, typeof(Guid));
            this.AddType(this.UserId, typeof(Guid));
            this.AddType(this.UserName, typeof(string));
            this.AddType(this.LogEntry, typeof(MaxLongString));
            this.AddType(this.IsCustomerVisible, typeof(bool));
        }
    }
}
