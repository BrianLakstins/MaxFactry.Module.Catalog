// <copyright file="MaxInventorySupplyEntity.cs" company="Lakstins Family, LLC">
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
// <change date="9/2/2016" author="Brian A. Lakstins" description="Initial creation">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxInventorySupplyEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {
        private static object _oLock = new object();

        private long _nAmountOnOrder = long.MinValue;

		/// <summary>
        /// Initializes a new instance of the MaxInventoryEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxInventorySupplyEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxInventoryEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxInventorySupplyEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public string SupplySku
        {
            get
            {
                return this.GetString(this.DataModel.SupplySku);
            }

            set
            {
                this.Set(this.DataModel.SupplySku, value);
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


        public Guid SiteId
        {
            get
            {
                return this.GetGuid(this.DataModel.SiteId);
            }

            set
            {
                this.Set(this.DataModel.SiteId, value);
            }
        }

        public string Location
        {
            get
            {
                return this.GetString(this.DataModel.Location);
            }

            set
            {
                this.Set(this.DataModel.Location, value);
            }
        }


        public long AmountCurrent
        {
            get
            {
                return this.GetLong(this.DataModel.AmountCurrent);
            }

            set
            {
                this.Set(this.DataModel.AmountCurrent, value);
            }
        }


        public long AmountReplenish
        {
            get
            {
                return this.GetLong(this.DataModel.AmountReplenish);
            }

            set
            {
                this.Set(this.DataModel.AmountReplenish, value);
            }
        }

        public long ReplenishAlertLevel
        {
            get
            {
                return this.GetLong(this.DataModel.ReplenishAlertLevel);
            }

            set
            {
                this.Set(this.DataModel.ReplenishAlertLevel, value);
            }
        }

        public int LeadTime
        {
            get
            {
                return this.GetInt(this.DataModel.LeadTime);
            }

            set
            {
                this.Set(this.DataModel.LeadTime, value);
            }
        }

        public int UnitOfMeasure
        {
            get
            {
                return this.GetInt(this.DataModel.UnitOfMeasure);
            }

            set
            {
                this.Set(this.DataModel.UnitOfMeasure, value);
            }
        }


        public Guid VendorId
        {
            get
            {
                return this.GetGuid(this.DataModel.VendorId);
            }

            set
            {
                this.Set(this.DataModel.VendorId, value);
            }
        }


        public string VendorSku
        {
            get
            {
                return this.GetString(this.DataModel.VendorSku);
            }

            set
            {
                this.Set(this.DataModel.VendorSku, value);
            }
        }

        public Guid ManufacturerId
        {
            get
            {
                return this.GetGuid(this.DataModel.ManufacturerId);
            }

            set
            {
                this.Set(this.DataModel.ManufacturerId, value);
            }
        }


        public string ManufacturerSku
        {
            get
            {
                return this.GetString(this.DataModel.ManufacturerSku);
            }

            set
            {
                this.Set(this.DataModel.ManufacturerSku, value);
            }
        }

        public bool IsBackOrderAllowed
        {
            get
            {
                return this.GetBit(this.DataModel.OptionFlagList, 0);
            }

            set
            {
                this.SetBit(this.DataModel.OptionFlagList, 0, value);
            }
        }

        public long AmountOnOrder
        {
            get
            {
                if (this._nAmountOnOrder == long.MinValue)
                {
                    this._nAmountOnOrder = 0;
                    //// TODO: Restrict product list to only those that have an amount on order and match this sku list and are active
                    MaxEntityList loProductList = MaxInventoryProductEntity.Create().LoadAllCache();
                    for (int lnE = 0; lnE < loProductList.Count; lnE++)
                    {
                        MaxInventoryProductEntity loEntity = loProductList[lnE] as MaxInventoryProductEntity;
                        if (loEntity.SupplySkuList.ToLower().Contains(this.SupplySku.ToLower()) && loEntity.IsActive && loEntity.AmountOnOrder > 0)
                        {
                            string[] laSupplyOnOrder = loEntity.SupplySkuList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string lsSupplyOnOrderSku in laSupplyOnOrder)
                            {
                                if (lsSupplyOnOrderSku.Equals(this.SupplySku, StringComparison.InvariantCultureIgnoreCase) ||
                                    lsSupplyOnOrderSku.ToLower().StartsWith(this.SupplySku.ToLower() + ":"))
                                {
                                    long lnSupplyPerProductAmount = 1;
                                    if (lsSupplyOnOrderSku.Contains(":"))
                                    {
                                        string[] laSupplyOnOrderSku = lsSupplyOnOrderSku.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                                        lnSupplyPerProductAmount = MaxConvertLibrary.ConvertToLong(typeof(object), laSupplyOnOrderSku[1]);
                                    }

                                    this._nAmountOnOrder += lnSupplyPerProductAmount * loEntity.AmountOnOrder;
                                }
                            }
                        }
                    }
                }

                return this._nAmountOnOrder;
            }
        }


        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxInventorySupplyDataModel DataModel
        {
            get
            {
                return (MaxInventorySupplyDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxInventorySupplyEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxInventorySupplyEntity),
                typeof(MaxInventorySupplyDataModel)) as MaxInventorySupplyEntity;
        }


        public static bool ChangeAmountCurrent(Guid loId, int lnAmount, string lsReason, string lsUserName)
        {
            bool lbR = false;
            lock (_oLock)
            {
                MaxInventorySupplyEntity loEntity = MaxInventorySupplyEntity.Create();
                if (loEntity.LoadByIdCache(loId))
                {
                    loEntity.ChangeAmountCurrent(lnAmount, lsReason, lsUserName);
                    lbR = true;
                }
            }

            return lbR;
        }

        public static bool ChangeAmountReplenish(Guid loId, int lnAmount, string lsReason, string lsUserName)
        {
            bool lbR = false;
            lock (_oLock)
            {
                MaxInventorySupplyEntity loEntity = MaxInventorySupplyEntity.Create();
                if (loEntity.LoadByIdCache(loId))
                {
                    loEntity.AmountReplenish = MaxInventoryLogEntity.LogInventoryChange(loEntity.Id, lsReason, lnAmount, MaxInventoryLogEntity.LogEntryTypeReplenish, loEntity.AmountReplenish, lsUserName);
                    loEntity.Update();
                    lbR = true;
                }
            }

            return lbR;
        }

        public override bool Insert()
        {
            this.AmountCurrent = 0;
            this.AmountReplenish = 0;
            return base.Insert();
        }

        public void ChangeAmountCurrent(int lnAmount, string lsReason, string lsUserName)
        {
            this.AmountCurrent = MaxInventoryLogEntity.LogInventoryChange(this.Id, lsReason, lnAmount, MaxInventoryLogEntity.LogEntryTypeCurrent, this.AmountCurrent, lsUserName);
            this.Update();
        }

        /// <summary>
        /// Gets a string that can be used to sort a list of this entity.
        /// </summary>
        /// <returns>Lowercase version of Name passed to 100 characters.</returns>
        public override string GetDefaultSortString()
        {
            return this.SupplySku.ToLowerInvariant().PadRight(100, ' ') + base.GetDefaultSortString();
        }

    }
}
