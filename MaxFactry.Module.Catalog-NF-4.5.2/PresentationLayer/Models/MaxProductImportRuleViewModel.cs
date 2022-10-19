// <copyright file="MaxProductImportRuleViewModel.cs" company="Lakstins Family, LLC">
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
// <change date="4/4/2015" author="Brian A. Lakstins" description="Initial creation.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.PresentationLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Module.Catalog.BusinessLayer;

    /// <summary>
    /// View model for content.
    /// </summary>
    public class MaxProductImportRuleViewModel : MaxFactry.Base.PresentationLayer.MaxBaseIdViewModel
    {
        /// <summary>
        /// Internal storage of a sorted list.
        /// Can use Generic List if supported in the framework.
        /// </summary>
        private List<MaxProductImportRuleViewModel> _oSortedList = null;

        /// <summary>
        /// Initializes a new instance of the MaxProductImportRuleViewModel class
        /// </summary>
        public MaxProductImportRuleViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxProductImportRuleViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxProductImportRuleViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the MaxProductImportRuleViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxProductImportRuleViewModel(string lsId) : base(lsId)
        {
        }

        protected override void CreateEntity()
        {
            this.Entity = MaxProductImportRuleEntity.Create();
        }

        /// <summary>
        /// Gets or sets the name property to be stored.
        /// </summary>
        [Display(Name = "Name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name property to be stored.
        /// </summary>
        [Display(Name = "Key")]
        public string Key
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name property to be stored.
        /// </summary>
        [Display(Name = "RuleType")]
        public int RuleType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name property to be stored.
        /// </summary>
        [Display(Name = "ProcessOrder")]
        public string ProcessOrder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name property to be stored.
        /// </summary>
        [Display(Name = "RuleData1")]
        public string RuleData1
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name property to be stored.
        /// </summary>
        [Display(Name = "RuleData2")]
        public string RuleData2
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name property to be stored.
        /// </summary>
        [Display(Name = "RuleData3")]
        public string RuleData3
        {
            get;
            set;
        }

        public int ImportGroup
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a sorted list of all 
        /// Can use Generic List if supported in the framework.
        /// </summary>
        /// <returns>List of ViewModels</returns>
        public List<MaxProductImportRuleViewModel> GetSortedList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxProductImportRuleViewModel>();
                string[] laKey = this.EntityIndex.GetSortedKeyList();
                for (int lnK = 0; lnK < laKey.Length; lnK++)
                {
                    MaxProductImportRuleViewModel loViewModel = new MaxProductImportRuleViewModel(this.EntityIndex[laKey[lnK]] as MaxEntity);
                    loViewModel.Load();
                    this._oSortedList.Add(loViewModel);
                }
            }

            return this._oSortedList;
        }

        /// <summary>
        /// Loads the entity based on the Id property.
        /// Maps the current values of properties in the ViewModel to the Entity.
        /// </summary>
        /// <returns>True if successful. False if it cannot be mapped.</returns>
        protected override bool MapToEntity()
        {
            if (base.MapToEntity())
            {
                MaxProductImportRuleEntity loEntity = this.Entity as MaxProductImportRuleEntity;
                if (null != loEntity)
                {
                    loEntity.Name = this.Name;
                    loEntity.Key = this.Key;
                    loEntity.RuleData1 = this.RuleData1;
                    loEntity.RuleData2 = this.RuleData2;
                    loEntity.RuleData3 = this.RuleData3;
                    loEntity.RuleType = this.RuleType;
                    loEntity.ImportGroup = this.ImportGroup;

                    int lnProcessOrder = 0;
                    if (int.TryParse(this.ProcessOrder, out lnProcessOrder))
                    {
                        loEntity.ProcessOrder = lnProcessOrder;
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Maps the properties of the Entity to the properties of the ViewModel.
        /// </summary>
        /// <returns>True if the entity exists.</returns>
        protected override bool MapFromEntity()
        {
            if (base.MapFromEntity())
            {
                MaxProductImportRuleEntity loEntity = this.Entity as MaxProductImportRuleEntity;
                if (null != loEntity)
                {
                    this.Name = loEntity.Name;
                    this.Key = loEntity.Key;
                    this.RuleData1 = loEntity.RuleData1;
                    this.RuleData2 = loEntity.RuleData2;
                    this.RuleData3 = loEntity.RuleData3;
                    this.RuleType = loEntity.RuleType;
                    this.ProcessOrder = loEntity.ProcessOrder.ToString();
                    this.ImportGroup = loEntity.ImportGroup;

                    this.OriginalValues.Add("Name", this.Name);
                    this.OriginalValues.Add("Key", this.Key);
                    this.OriginalValues.Add("RuleData1", this.RuleData1);
                    this.OriginalValues.Add("RuleData2", this.RuleData2);
                    this.OriginalValues.Add("RuleData3", this.RuleData3);
                    this.OriginalValues.Add("ProcessOrder", this.ProcessOrder);
                    return true;
                }
            }

            return false;
        }
    }
}
