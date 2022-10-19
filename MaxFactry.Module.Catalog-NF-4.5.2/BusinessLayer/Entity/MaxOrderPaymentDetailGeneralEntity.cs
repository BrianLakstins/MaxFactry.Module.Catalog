// <copyright file="MaxOrderPaymentDetailGeneralEntity.cs" company="Lakstins Family, LLC">
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
// <change date="6/4/2015" author="Brian A. Lakstins" description="Initial Release">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxOrderPaymentDetailGeneralEntity : MaxOrderPaymentDetailEntity
    {
		/// <summary>
        /// Initializes a new instance of the MaxOrderPaymentDetailCreditCardEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxOrderPaymentDetailGeneralEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxOrderPaymentDetailCreditCardEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxOrderPaymentDetailGeneralEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public override string DetailType
        {
            get
            {
                return PaymentDetailTypeGeneral;
            }
            set
            {
                base.DetailType = value;
            }
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

        public string Code
        {
            get
            {
                return this.GetString(this.DataModel.Code);
            }

            set
            {
                this.Set(this.DataModel.Code, value);
            }
        }

        public Guid AddressId
        {
            get
            {
                return this.GetGuid(this.DataModel.AddressId);
            }

            set
            {
                this.Set(this.DataModel.AddressId, value);
            }
        }

        public string Note
        {
            get
            {
                return this.GetString(this.DataModel.Note);
            }

            set
            {
                this.Set(this.DataModel.Note, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxOrderPaymentDetailGeneralDataModel DataModel
        {
            get
            {
                return (MaxOrderPaymentDetailGeneralDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxOrderPaymentDetailGeneralEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxOrderPaymentDetailGeneralEntity),
                typeof(MaxOrderPaymentDetailGeneralDataModel)) as MaxOrderPaymentDetailGeneralEntity;
        }

        public virtual string TryAuthorize(MaxOrderPaymentTransactionEntity loTransactionEntity)
        {
            loTransactionEntity.Log += DateTime.UtcNow.ToString() + " UTC: Base authorization of general payment detail.\r\n";
            loTransactionEntity.Update();
            return string.Empty;
        }

        public virtual string TrySale(MaxOrderPaymentTransactionEntity loTransactionEntity)
        {
            loTransactionEntity.Log += DateTime.UtcNow.ToString() + " UTC: Base sale of general payment detail.\r\n";
            loTransactionEntity.Update();
            return string.Empty;
        }
    }
}
