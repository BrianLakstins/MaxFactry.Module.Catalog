// <copyright file="MaxOrderItemDataModel.cs" company="Lakstins Family, LLC">
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
// <change date="5/27/2015" author="Brian A. Lakstins" description="Added Note.">
// <change date="11/30/2018" author="Brian A. Lakstins" description="Updated for changes to base.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Data Model for order related information.
    /// </summary>
    public class MaxOrderPaymentDetailCardDataModel : MaxOrderPaymentDetailDataModel
    {
        /// <summary>
        /// Name on the card
        /// </summary>
        public readonly string Name = "Name";

        /// <summary>
        /// Date the credit card expires
        /// </summary>
        public readonly string ExpirationDate = "ExpirationDate";

        /// <summary>
        /// Number on the credit card
        /// </summary>
        public readonly string CardNumber = "CardNumber";

        /// <summary>
        /// Code on the back of the card for verification
        /// </summary>
        public readonly string CardVerificationCode = "CardVerificationCode";

        /// <summary>
        /// Id of Postal address where credit card is associated
        /// </summary>
        public readonly string AddressId = "AddressId";

        /// <summary>
        /// Note associated with credit card
        /// </summary>
        public readonly string Note = "Note";

        /// <summary>
        /// Initializes a new instance of the MaxCatalogDataModel class
        /// </summary>
        public MaxOrderPaymentDetailCardDataModel()
        {
            this.SetDataStorageName("MaxCatalogOrderPaymentDetailCard");
            this.AddType(this.Name, typeof(MaxShortString));
            this.AddType(this.ExpirationDate, typeof(DateTime));
            this.AddType(this.CardNumber, typeof(string));
            this.AddPropertyAttribute(this.CardNumber, "IsEncrypted", "true");
            this.AddType(this.CardVerificationCode, typeof(string));
            this.AddPropertyAttribute(this.CardVerificationCode, "IsEncrypted", "true");
            this.AddType(this.AddressId, typeof(Guid));
            this.AddNullable(this.Note, typeof(string));
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderPaymentDetailCardDataModel class.
        /// </summary>
        /// <param name="lsDataStorageName">Name to user for storage</param>
        public MaxOrderPaymentDetailCardDataModel(string lsDataStorageName) : this()
        {
            this.SetDataStorageName(lsDataStorageName);
        }
    }
}
