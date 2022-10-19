// <copyright file="MaxShippingChargeEntity.cs" company="Lakstins Family, LLC">
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
// <change date="3/12/2015" author="Brian A. Lakstins" description="Initial Release">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxShippingChargeEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {
		/// <summary>
        /// Initializes a new instance of the MaxShippingChargeEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxShippingChargeEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxShippingChargeEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxShippingChargeEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public string PrimaryReferenceType
        {
            get
            {
                return this.GetString(this.DataModel.PrimaryReferenceType);
            }

            set
            {
                this.Set(this.DataModel.PrimaryReferenceType, value);
            }
        }

        public Guid PrimaryReferenceId
        {
            get
            {
                return this.GetGuid(this.DataModel.PrimaryReferenceId);
            }

            set
            {
                this.Set(this.DataModel.PrimaryReferenceId, value);
            }
        }

        public string SecondaryReferenceType
        {
            get
            {
                return this.GetString(this.DataModel.SecondaryReferenceType);
            }

            set
            {
                this.Set(this.DataModel.SecondaryReferenceType, value);
            }
        }

        public Guid SecondaryReferenceId
        {
            get
            {
                return this.GetGuid(this.DataModel.SecondaryReferenceId);
            }

            set
            {
                this.Set(this.DataModel.SecondaryReferenceId, value);
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

        public double Amount
        {
            get
            {
                return this.GetDouble(this.DataModel.Amount);
            }

            set
            {
                this.Set(this.DataModel.Amount, value);
            }
        }

        public int ShippingType
        {
            get
            {
                return this.GetInt(this.DataModel.ShippingType);
            }

            set
            {
                this.Set(this.DataModel.ShippingType, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxShippingChargeDataModel DataModel
        {
            get
            {
                return (MaxShippingChargeDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxShippingChargeEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxShippingChargeEntity),
                typeof(MaxShippingChargeDataModel)) as MaxShippingChargeEntity;
        }

        /// <summary>
        /// Gets a string that can be used to sort a list of this entity.
        /// </summary>
        /// <returns>Lowercase version of Name passed to 100 characters.</returns>
        public override string GetDefaultSortString()
        {
            return this.Name.ToLowerInvariant().PadRight(100, ' ') + base.GetDefaultSortString();
        }

        public MaxEntityList LoadAllByPrimaryReferenceId(Guid loId)
        {
            MaxDataList loDataList = MaxCatalogRepository.SelectAllByProperty(this.Data, this.DataModel.PrimaryReferenceId, loId);
            MaxEntityList loEntityList = MaxEntityList.Create(this.GetType(), loDataList);
            return loEntityList;
        }

        public virtual double GetShipping(MaxProductEntity loProduct)
        {
            return 0;
        }

        public virtual double GetCartShipping(MaxCartEntity loCart, int lnShippingType)
        {
            double lnR = 0;
            double lnCartTotal = 0;
            MaxShippingTypeEntity loShippingType = MaxShippingTypeEntity.Create();
            loShippingType.LoadByShippingType(lnShippingType);
            double lnShippingPerItem = 0;
            if (double.TryParse(loShippingType.ShippingCalculation, out lnShippingPerItem))
            {
                foreach (MaxProductSelectionEntity loItemEntity in loCart.ItemList)
                {
                    double lnItemShipping = loItemEntity.ItemShipping;
                    //// Only add per item shipping to products that have a shipping charge
                    if (lnItemShipping > 0)
                    {
                        lnCartTotal += lnShippingPerItem * loItemEntity.Quantity * loItemEntity.ShippingCalculationMultiplier;
                    }
                }

                lnR = lnCartTotal + this.GetProductShipping(loCart);
            }         

            return lnR;
        }

        public virtual double GetProductShipping(MaxCartEntity loCart)
        {
            double lnItemTotal = 0;
            foreach (MaxProductSelectionEntity loItemEntity in loCart.ItemList)
            {
                double lnItemShipping = loItemEntity.ItemShipping;
                //// Only add per item shipping to products that have a shipping charge
                if (lnItemShipping > 0)
                {
                    lnItemTotal += lnItemShipping * loItemEntity.Quantity;
                }

                if (loItemEntity.PrimaryReferenceType.Equals("MaxProductEntity"))
                {
                    //// Add any shipping that is specific to the product, but not the quantity or shipping method.
                    MaxProductEntity loProduct = MaxProductEntity.Create();
                    loProduct.LoadByIdCache(loItemEntity.PrimaryReferenceId);
                    lnItemTotal += this.GetShipping(loProduct);
                }
            }

            if (lnItemTotal == 0 && loCart.ShippingTotal > 0 && loCart.ShippingType == MaxShippingTypeEntity.ShippingTypeStandard)
            {
                lnItemTotal = loCart.ShippingTotal;
            }

            return lnItemTotal;
        }
    }
}
