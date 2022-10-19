// <copyright file="MaxOrderDataModel.cs" company="Lakstins Family, LLC">
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
// <change date="5/27/2015" author="Brian A. Lakstins" description="Adding tracking of user information and saving of emails content related to the order.">
// <change date="12/21/2016" author="Brian A. Lakstins" description="Updated to remove redundant definitions.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Data Model for order related information.
    /// </summary>
    public class MaxOrderDataModel : MaxCartDataModel
    {
        /// <summary>
        /// Pipe separated list of Ids of carts used to initialize this Order
        /// </summary>
        public readonly string CartIdListText = "CartIdListText";

        /// <summary>
        /// Date and time that the order was confirmed.
        /// </summary>
        public readonly string OrderPlacedDate = "OrderPlacedDate";

        /// <summary>
        /// Status of the processing of this order.
        /// </summary>
        public readonly string ProcessingStatus = "ProcessingStatus";

        /// <summary>
        /// Id of User who is currently responsible for this order
        /// </summary>
        public readonly string ProcessingUserId = "ProcessingUserId";

        /// <summary>
        /// Content of the confirmation notice.
        /// </summary>
        public readonly string ConfirmationNoticeContent = "ConfirmationNoticeContent";

        /// <summary>
        /// Id of the confirmation notice that was sent.
        /// </summary>
        public readonly string ConfirmationNoticeId = "ConfirmationNoticeId";

        /// <summary>
        /// Content of the management notice.
        /// </summary>
        public readonly string ManagementNoticeContent = "ManagementNoticeContent";

        /// <summary>
        ///Id of the management notice that was sent.
        /// </summary>
        public readonly string ManagementNoticeId = "ManagementNoticeId";

        /// <summary>
        /// Type of external order this order is based on.
        /// </summary>
        public readonly string ExternalOrderType = "ExternalOrderType";

        /// <summary>
        /// Id of the external order this order is based on.
        /// </summary>
        public readonly string ExternalOrderId = "ExternalOrderId";

        /// <summary>
        /// Processing status action flags associated with this order.
        /// </summary>
        public readonly string ProcessingActionFlagList = "ProcessingActionFlagList";

        /// <summary>
        /// Initializes a new instance of the MaxOrderDataModel class
        /// </summary>
        public MaxOrderDataModel()
        {
            this.SetDataStorageName("MaxCatalogOrder");
            this.RepositoryProviderType = typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogRepositoryProvider);
            this.RepositoryType = typeof(MaxCatalogRepository);
            this.AddType(this.CartIdListText, typeof(string));
            this.AddType(this.OrderPlacedDate, typeof(DateTime));
            this.AddType(this.ProcessingStatus, typeof(int));
            this.AddNullable(this.ProcessingUserId, typeof(MaxShortString));
            this.AddNullable(this.ConfirmationNoticeContent, typeof(MaxLongString));
            this.AddNullable(this.ConfirmationNoticeId, typeof(Guid));
            this.AddNullable(this.ManagementNoticeContent, typeof(MaxLongString));
            this.AddNullable(this.ManagementNoticeId, typeof(Guid));
            this.AddNullable(this.ExternalOrderType, typeof(MaxShortString));
            this.AddNullable(this.ExternalOrderId, typeof(MaxShortString));
            this.AddNullable(this.ProcessingActionFlagList, typeof(long));
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderDataModel class.
        /// </summary>
        /// <param name="lsDataStorageName">Name to user for storage</param>
        public MaxOrderDataModel(string lsDataStorageName) : this()
        {
            this.SetDataStorageName(lsDataStorageName);
        }
    }
}
