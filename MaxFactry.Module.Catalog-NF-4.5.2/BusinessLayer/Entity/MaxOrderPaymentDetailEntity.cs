// <copyright file="MaxOrderPaymentDetailEntity.cs" company="Lakstins Family, LLC">
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
// <change date="11/24/2014" author="Brian A. Lakstins" description="Initial Release">
// <change date="5/27/2015" author="Brian A. Lakstins" description="Added detail type that can be overridded on subclasses.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public abstract class MaxOrderPaymentDetailEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {
        public const string PaymentDetailTypeCard = "Card";

        public const string PaymentDetailTypeGeneral = "General";

		/// <summary>
        /// Initializes a new instance of the MaxOrderPaymentDetailCreditCardEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxOrderPaymentDetailEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxOrderPaymentDetailCreditCardEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxOrderPaymentDetailEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public static MaxOrderPaymentDetailEntity Create(string lsDetailType)
        {
            //// TODO: Add more payment detail types here
            if (null != lsDetailType && lsDetailType.Length > 0)
            {
                if (lsDetailType == PaymentDetailTypeGeneral ||
                    lsDetailType == typeof(MaxOrderPaymentDetailGeneralEntity).ToString())
                {
                    return MaxOrderPaymentDetailGeneralEntity.Create();
                }
            }
            
            //// Default to credit card.
            return MaxOrderPaymentDetailCardEntity.Create();
        }

        public virtual string DetailType
        {
            get
            {
                return this.GetType().ToString();
            }

            set
            {
                //// Once created, the detailtype cannot be changed
            }
        }

        public virtual bool IsAuthorized
        {
            get
            {
                return this.GetBit(this.DataModelPaymentDetail.OptionFlagList, 0);
            }
            set
            {
                this.SetBit(this.DataModelPaymentDetail.OptionFlagList, 0, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxOrderPaymentDetailDataModel DataModelPaymentDetail
        {
            get
            {
                return (MaxOrderPaymentDetailDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }
    }
}
