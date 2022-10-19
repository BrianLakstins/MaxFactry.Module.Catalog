// <copyright file="MaxOrderPaymentDetailCreditCardEntity.cs" company="Lakstins Family, LLC">
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
// <change date="5/27/2015" author="Brian A. Lakstins" description="Updated to require override for card processsing functionality.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxOrderPaymentDetailCardEntity : MaxOrderPaymentDetailEntity
    {
		/// <summary>
        /// Initializes a new instance of the MaxOrderPaymentDetailCreditCardEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxOrderPaymentDetailCardEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxOrderPaymentDetailCreditCardEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxOrderPaymentDetailCardEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public override string DetailType
        {
            get
            {
                return PaymentDetailTypeCard;
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

        public string CardNumber
        {
            get
            {
                return this.GetString(this.DataModel.CardNumber);
            }

            set
            {
                this.Set(this.DataModel.CardNumber, value);
            }
        }

        public string CardVerificationCode
        {
            get
            {
                return this.GetString(this.DataModel.CardVerificationCode);
            }

            set
            {
                this.Set(this.DataModel.CardVerificationCode, value);
            }
        }

        public DateTime ExpirationDate
        {
            get
            {
                return this.GetDateTime(this.DataModel.ExpirationDate);
            }

            set
            {
                this.Set(this.DataModel.ExpirationDate, value);
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
        protected MaxOrderPaymentDetailCardDataModel DataModel
        {
            get
            {
                return (MaxOrderPaymentDetailCardDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxOrderPaymentDetailCardEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxOrderPaymentDetailCardEntity),
                typeof(MaxOrderPaymentDetailCardDataModel)) as MaxOrderPaymentDetailCardEntity;
        }
    }
}
