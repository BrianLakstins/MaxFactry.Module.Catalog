// <copyright file="MaxOrderPaymentTransactionDataModel.cs" company="Lakstins Family, LLC">
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
// <change date="5/27/2015" author="Brian A. Lakstins" description="Initial Release">
// <change date="12/21/2016" author="Brian A. Lakstins" description="Updated to use PrimaryKey Suffix.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Data Model for order related information.
    /// </summary>
    public class MaxOrderPaymentTransactionDataModel : MaxFactry.Base.DataLayer.MaxBaseIdDataModel
    {
        /// <summary>
        /// Id of the Order this Item is associated
        /// </summary>
        public readonly string OrderId = "OrderId";

        /// <summary>
        /// Id of the payment detail assocaited with this payment
        /// </summary>
        public readonly string PaymentId = "PaymentId";

        /// <summary>
        /// Amount of the payment
        /// </summary>
        public readonly string Amount = "Amount";

        /// <summary>
        /// Indicates that the amount has been collected.
        /// </summary>
        public readonly string IsCollected = "IsCollected";

        /// <summary>
        /// Indicates that the date collected.
        /// </summary>
        public readonly string DateCollected = "DateCollected";

        /// <summary>
        /// Log of the payment transaction
        /// </summary>
        public readonly string Log = "Log";

        /// <summary>
        /// Type of the transaction
        /// </summary>
        public readonly string TransactionType = "TransactionType";

        /// <summary>
        /// Error when processing transaction
        /// </summary>
        public readonly string ProcessingError = "ProcessingError";

        /// <summary>
        /// Initializes a new instance of the MaxCatalogDataModel class
        /// </summary>
        public MaxOrderPaymentTransactionDataModel()
        {
            this.SetDataStorageName("MaxCatalogOrderPaymentTransaction");
            this.RepositoryProviderType = typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogRepositoryProvider);
            this.RepositoryType = typeof(MaxCatalogRepository);
            this.AddType(this.OrderId, typeof(Guid));
            this.AddType(this.PaymentId, typeof(Guid));
            this.AddType(this.Amount, typeof(double));
            this.AddType(this.IsCollected, typeof(bool));
            this.AddType(this.DateCollected, typeof(DateTime));
            this.AddType(this.Log, typeof(MaxLongString));
            this.AddNullable(this.TransactionType, typeof(int));
            this.AddType(this.ProcessingError, typeof(string));
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderPaymentTransactionDataModel class.
        /// </summary>
        /// <param name="lsDataStorageName">Name to user for storage</param>
        public MaxOrderPaymentTransactionDataModel(string lsDataStorageName) : this()
        {
            this.SetDataStorageName(lsDataStorageName);
        }

        /// <summary>
        /// Gets a suffix for the primary key based on the data to speed up future queries
        /// </summary>
        /// <param name="loData">Data to use to create the suffix</param>
        /// <returns>String to use as suffix for primary key</returns>
        public override string GetPrimaryKeySuffix(MaxData loData)
        {
            string lsR = base.GetPrimaryKeySuffix(loData);
            if (string.IsNullOrEmpty(lsR))
            {
                lsR = loData.Get(this.OrderId).ToString();
            }

            return lsR;
        }
    }
}
