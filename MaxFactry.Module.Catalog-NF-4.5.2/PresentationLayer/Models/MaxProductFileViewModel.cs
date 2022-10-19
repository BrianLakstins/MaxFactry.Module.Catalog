// <copyright file="MaxProductFileViewModel.cs" company="Lakstins Family, LLC">
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
// <change date="12/14/2015" author="Brian A. Lakstins" description="Initial creation.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.PresentationLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.PresentationLayer;
    using MaxFactry.General.BusinessLayer;
    using MaxFactry.Module.Catalog.BusinessLayer;

    /// <summary>
    /// View model for content.
    /// </summary>
    public class MaxProductFileViewModel : MaxBaseIdFileViewModel
    {
        /// <summary>
        /// Internal storage of a sorted list.
        /// Can use Generic List if supported in the framework.
        /// </summary>
        private List<MaxProductFileViewModel> _oSortedList = null;

        /// <summary>
        /// Initializes a new instance of the MaxProductFileViewModel class
        /// </summary>
        public MaxProductFileViewModel()
            : base()
        {
            this.Entity = MaxProductFileEntity.Create();
        }

        /// <summary>
        /// Initializes a new instance of the MaxProductFileViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxProductFileViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the MaxProductFileViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxProductFileViewModel(string lsId)
        {
            this.Entity = MaxProductFileEntity.Create();
            this.Id = lsId;
            if (this.EntityLoad())
            {
                this.Load();
            }
        }

        public bool SendAsEmailAttachment(string lsEmail, string lsFromName, string lsFromAddress, string lsSubject, string lsContent)
        {
            bool lbR = false;
            try
            {
                MaxEmailEntity loEmail = MaxEmailEntity.Create();
                loEmail.ToAddressList.Add(lsEmail);
                loEmail.FromName = lsFromName;
                loEmail.FromAddress = lsFromAddress;
                loEmail.Subject = lsSubject;
                loEmail.Content = lsContent;
                string lsName = this.FileName;
                if (string.IsNullOrEmpty(lsName))
                {
                    lsName = this.Name;
                }

                if (null != this.Content && !string.IsNullOrEmpty(lsName) && !string.IsNullOrEmpty(this.MimeType))
                {
                    loEmail.AddAttachment(this.Content, lsName, this.MimeType);
                }
                else
                {
                    loEmail.Subject += " Missing File!";
                    MaxLogLibrary.Log(new MaxLogEntryStructure(MaxEnumGroup.LogError, "Error with email to {ToEmail} for file {File}.", new MaxException("Missing File"), lsEmail, lsName));
                }

                loEmail.Send();
                lbR = true;

                MaxIndex loIndex = new MaxIndex();
                loIndex.Add("VAR-MERGE30", "Yes");
                MaxMailingListLibrary.Subscribe("Mailing List", lsEmail, loIndex);

            }
            catch (Exception loE)
            {
                MaxLogLibrary.Log(new MaxLogEntryStructure(MaxEnumGroup.LogError, "Error sending product file email to {ToEmail}.", loE, lsEmail));
            }

            return lbR;
        }

        /// <summary>
        /// Gets a sorted list of all 
        /// Can use Generic List if supported in the framework.
        /// </summary>
        /// <returns>List of ViewModels</returns>
        public List<MaxProductFileViewModel> GetSortedList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxProductFileViewModel>();
                string[] laKey = this.EntityIndex.GetSortedKeyList();
                for (int lnK = 0; lnK < laKey.Length; lnK++)
                {
                    MaxProductFileViewModel loViewModel = new MaxProductFileViewModel(this.EntityIndex[laKey[lnK]] as MaxEntity);
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
                MaxProductFileEntity loEntity = this.Entity as MaxProductFileEntity;
                if (null != loEntity)
                {
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
                MaxProductFileEntity loEntity = this.Entity as MaxProductFileEntity;
                if (null != loEntity)
                {
                    return true;
                }
            }

            return false;
        }


    }
}
