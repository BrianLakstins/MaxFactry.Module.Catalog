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
// <change date="6/6/2015" author="Brian A. Lakstins" description="Initial creation">
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
    public class MaxOrderPaymentDetailGeneralDataModel : MaxOrderPaymentDetailDataModel
    {
        /// <summary>
        /// Name on the card
        /// </summary>
        public readonly string Name = "Name";

        /// <summary>
        /// A code related to the payment
        /// </summary>
        public readonly string Code = "Code";

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
        public MaxOrderPaymentDetailGeneralDataModel()
        {
            this.SetDataStorageName("MaxCatalogOrderPaymentDetailGeneral");
            this.AddType(this.Name, typeof(MaxShortString));
            this.AddType(this.Code, typeof(string));
            this.AddPropertyAttribute(this.Code, "IsEncrypted", "true");
            this.AddType(this.AddressId, typeof(Guid));
            this.AddNullable(this.Note, typeof(string));
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderPaymentDetailGeneralDataModel class.
        /// </summary>
        /// <param name="lsDataStorageName">Name to user for storage</param>
        public MaxOrderPaymentDetailGeneralDataModel(string lsDataStorageName) : this()
        {
            this.SetDataStorageName(lsDataStorageName);
        }
    }
}
