// <copyright file="MaxDiscountCodeEntity.cs" company="Lakstins Family, LLC">
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
// <change date="11/1/2015" author="Brian A. Lakstins" description="Initial creation">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxDiscountCodeEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {
        public const int CalculationCart = 1;

        public const int CalculationProduct = 2;

		/// <summary>
        /// Initializes a new instance of the MaxDiscountCodeEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxDiscountCodeEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxDiscountCodeEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxDiscountCodeEntity(Type loDataModelType)
            : base(loDataModelType)
        {
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

        public string ReferType
        {
            get
            {
                return this.GetString(this.DataModel.ReferType);
            }

            set
            {
                this.Set(this.DataModel.ReferType, value);
            }
        }

        public string Description
        {
            get
            {
                return this.GetString(this.DataModel.Description);
            }

            set
            {
                this.Set(this.DataModel.Description, value);
            }
        }

        public string InternalNote
        {
            get
            {
                return this.GetString(this.DataModel.InternalNote);
            }

            set
            {
                this.Set(this.DataModel.InternalNote, value);
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

        public DateTime StartDate
        {
            get
            {
                return this.GetDateTime(this.DataModel.StartDate);
            }

            set
            {
                this.Set(this.DataModel.StartDate, value);
            }
        }

        public DateTime EndDate
        {
            get
            {
                return this.GetDateTime(this.DataModel.EndDate);
            }

            set
            {
                this.Set(this.DataModel.EndDate, value);
            }
        }

        public int UseCount
        {
            get
            {
                int lnR = this.GetInt(this.DataModel.UseCount);
                if (lnR > 0)
                {
                    return lnR;
                }

                return 0;
            }

            set
            {
                this.Set(this.DataModel.UseCount, value);
            }
        }

        public int UsedCount
        {
            get
            {
                int lnR = this.GetInt(this.DataModel.UsedCount);
                if (lnR > 0)
                {
                    return lnR;
                }

                return 0;
            }

            set
            {
                this.Set(this.DataModel.UsedCount, value);
            }
        }

        public double MinimumAmount
        {
            get
            {
                double lnR = this.GetDouble(this.DataModel.MinimumAmount);
                if (lnR > 0)
                {
                    return lnR;
                }

                return 0;
            }

            set
            {
                this.Set(this.DataModel.MinimumAmount, value);
            }
        }

        public double MaximumAmount
        {
            get
            {
                double lnR = this.GetDouble(this.DataModel.MaximumAmount);
                if (lnR > 0)
                {
                    return lnR;
                }

                return 0;

            }

            set
            {
                this.Set(this.DataModel.MaximumAmount, value);
            }
        }

        public int MinimumQuantity
        {
            get
            {
                int lnR = this.GetInt(this.DataModel.MinimumQuantity);
                if (lnR > 0)
                {
                    return lnR;
                }

                return 0;
            }

            set
            {
                this.Set(this.DataModel.MinimumQuantity, value);
            }
        }

        public int MaximumQuanitity
        {
            get
            {
                int lnR = this.GetInt(this.DataModel.MaximumQuanitity);
                if (lnR > 0)
                {
                    return lnR;
                }

                return 0;

            }

            set
            {
                this.Set(this.DataModel.MaximumQuanitity, value);
            }
        }

        public string ProductIdListText
        {
            get
            {
                return this.GetString(this.DataModel.ProductIdListText);
            }

            set
            {
                this.Set(this.DataModel.ProductIdListText, value);
            }
        }

        public string ProductSkuListText
        {
            get
            {
                return this.GetString(this.DataModel.ProductSkuListText);
            }

            set
            {
                this.Set(this.DataModel.ProductSkuListText, value);
            }
        }

        public string CategoryIdListText
        {
            get
            {
                return this.GetString(this.DataModel.CategoryIdListText);
            }

            set
            {
                this.Set(this.DataModel.CategoryIdListText, value);
            }
        }

        public double PercentOff
        {
            get
            {
                return this.GetDouble(this.DataModel.PercentOff);
            }

            set
            {
                this.Set(this.DataModel.PercentOff, value);
            }
        }

        public double AmountOff
        {
            get
            {
                return this.GetDouble(this.DataModel.AmountOff);
            }

            set
            {
                this.Set(this.DataModel.AmountOff, value);
            }
        }

        public bool IsFreeShipping
        {
            get
            {
                return this.GetBoolean(this.DataModel.IsFreeShipping);
            }

            set
            {
                this.Set(this.DataModel.IsFreeShipping, value);
            }
        }

        public int Calculation
        {
            get
            {
                return this.GetInt(this.DataModel.Calculation);
            }

            set
            {
                this.Set(this.DataModel.Calculation, value);
            }
        }

        public string UsernameListText
        {
            get
            {
                return this.GetString(this.DataModel.UsernameListText);
            }

            set
            {
                this.Set(this.DataModel.UsernameListText, value);
            }
        }

        public string UserIdListText
        {
            get
            {
                return this.GetString(this.DataModel.UserIdListText);
            }

            set
            {
                this.Set(this.DataModel.UserIdListText, value);
            }
        }

        public string Group
        {
            get
            {
                return this.GetString(this.DataModel.Group);
            }

            set
            {
                this.Set(this.DataModel.Group, value);
            }
        }

        public bool IsUseAlways
        {
            get
            {
                return this.GetBoolean(this.DataModel.IsUseAlways);
            }

            set
            {
                this.Set(this.DataModel.IsUseAlways, value);
            }
        }

        public string InvalidReasonCurrent { get; set; }

        public double DiscountAmountCurrent { get; set; }

        public double MatchAmountCurrent { get; set; }

        public int MatchQuantityCurrent { get; set; }

        public string ValidReasonCurrent { get; set; }

        public List<MaxProductSelectionEntity> MatchProductCurrentList
        {
            get;
            set;
        }

        public bool IsCurrentlyValid
        {
            get
            {
                if (string.IsNullOrEmpty(this.InvalidReasonCurrent))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxDiscountCodeDataModel DataModel
        {
            get
            {
                return (MaxDiscountCodeDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxDiscountCodeEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxDiscountCodeEntity),
                typeof(MaxDiscountCodeDataModel)) as MaxDiscountCodeEntity;
        }

        /// <summary>
        /// Gets a string that can be used to sort a list of this entity.
        /// </summary>
        /// <returns>Lowercase version of Name passed to 100 characters.</returns>
        public override string GetDefaultSortString()
        {
            return this.Name.ToLowerInvariant().PadRight(100, ' ') + base.GetDefaultSortString();
        }

        public MaxEntityList LoadAllByCode(string lsCode)
        {
            return this.LoadAllByPropertyCache(this.DataModel.Code, lsCode);
        }

        public MaxEntityList LoadAllUseAlways()
        {
            return this.LoadAllByPropertyCache(this.DataModel.IsUseAlways, true);
        }

        public bool CheckValid()
        {
            if (this.StartDate > DateTime.UtcNow)
            {
                this.InvalidReasonCurrent = string.Format("Not yet valid.  Will be valid {0}.", this.StartDate);
            }
            else if (this.EndDate < DateTime.UtcNow)
            {
                this.InvalidReasonCurrent = string.Format("Exipired on {0}.", this.EndDate);
            }
            else if (!this.IsActive)
            {
                this.InvalidReasonCurrent = "Not active.";
            }
            else if (this.UseCount > 0 && this.UsedCount >= this.UseCount)
            {
                this.InvalidReasonCurrent = string.Format("{0} uses have all been used.", this.UseCount);
            }

            if (string.IsNullOrEmpty(this.InvalidReasonCurrent))
            {
                return true;
            }

            return false;
        }

        public bool IsProductSkuMatch(string lsSku)
        {
            bool lbR = false;
            string lsSkuCheck = lsSku.ToLower().Trim();
            //// Separate list by commas
            string[] laList = this.ProductSkuListText.ToLower().Split(new char[] { ',' });
            for (int lnL = 0; lnL < laList.Length; lnL++)
            {
                string lsSkuTest = laList[lnL].Trim();
                if (lsSkuTest.Equals(lsSkuCheck))
                {
                    lbR = true;
                }
                
                if (lsSkuTest.StartsWith("!"))
                {
                    if (lsSkuTest.Substring(1).Equals(lsSkuCheck))
                    {
                        //// !sku1 == sku1
                        return false;
                    }
                    else if (lsSkuTest.EndsWith("*") && lsSkuCheck.StartsWith(lsSkuTest.Substring(1, lsSkuTest.Length - 2))) 
                    {
                        //// !sku* == sku1234
                        return false;
                    }
                    else
                    {
                        lbR = true;
                    }
                }
                else if (lsSkuTest.EndsWith("*"))
                {
                    //// sku* == sku1234
                    if (lsSkuCheck.StartsWith(lsSkuTest.Substring(0, lsSkuTest.Length - 1)))
                    {
                        lbR = true;
                    }
                }
            }

            return lbR;
        }
    }
}
