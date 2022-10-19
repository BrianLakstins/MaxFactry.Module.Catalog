// <copyright file="MaxCatalogApiController.cs" company="Lakstins Family, LLC">
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
// <change date="12/19/2015" author="Brian A. Lakstins" description="Initial creation">
// <change date="10/10/2018" author="Brian A. Lakstins" description="Updated for change in MaxOwinLibrary">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.Mvc4.PresentationLayer
{

    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using MaxFactry.Core;
    using MaxFactry.Module.Catalog.BusinessLayer;
    using MaxFactry.Module.Catalog.PresentationLayer;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.General.AspNet.BusinessLayer;
    using MaxFactry.General.AspNet.PresentationLayer;
    using MaxFactry.General.AspNet.IIS.Mvc4.PresentationLayer;
    using MaxFactry.General.BusinessLayer;
    using MaxFactry.General.PresentationLayer;
    using MaxFactry.Module.Crm.PresentationLayer;

    [System.Web.Http.AllowAnonymous]
    [MaxEnableCorsAttribute]
    public class MaxCatalogApiController : MaxFactry.General.AspNet.IIS.Mvc4.PresentationLayer.MaxBaseApiController
    {
        [HttpGet]
        [HttpPost]
        [ActionName("index")]
        public async Task<HttpResponseMessage> Index()
        {
            Guid loId = Guid.NewGuid();
            string lsR = string.Empty;

            lsR = string.Empty;
            try
            {
                Stream loContent = await this.Request.Content.ReadAsStreamAsync();
                StreamReader loReader = new StreamReader(loContent);
                lsR = loReader.ReadToEnd();
                loReader.Close();
            }
            catch (Exception loE)
            {
                MaxLogLibrary.Log(new MaxLogEntryStructure(MaxEnumGroup.LogError, "Catalog API Error.", loE));
            }

            HttpResponseMessage loR = new HttpResponseMessage();
            loR.Content = new StringContent(lsR);
            loR.Content.Headers.Remove("Content-Type");
            loR.Content.Headers.Add("Content-Type", "text/plain");

            MaxOwinLibrary.SetStorageKeyForClient(HttpContext.Current.Request.Url, false, string.Empty);
            return loR;
        }

        [HttpGet]
        [HttpPost]
        [ActionName("index2")]
        public string Index2(string lsId)
        {
            Guid loId = Guid.NewGuid();
            string lsR = lsId;

            lsR = lsId;

            return lsR;
        }

        /// <summary>
        /// This does not work.  ViewModel is not serializable because MaxIndex does not implement GetEnumerator
        /// </summary>
        /// <param name="lsId"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [ActionName("cart")]
        public MaxCartViewModel Cart(string lsId)
        {
            MaxCartViewModel loR = new MaxCartViewModel();
            loR.Id = lsId;
            loR.EntityLoad();
            return loR;
        }

        [HttpGet]
        [HttpPost]
        [ActionName("cartarchive")]
        public int CartArchive()
        {
            MaxCartEntity loCart = MaxCartEntity.Create();
            int lnR = loCart.ArchiveAbandoned();
            return lnR;
        }

        [HttpGet]
        [HttpPost]
        [ActionName("orderarchive")]
        public int OrderArchive()
        {
            MaxOrderEntity loOrder = MaxOrderEntity.Create();
            int lnR = loOrder.ArchiveAbandoned();
            return lnR;
        }

        [HttpGet]
        [ActionName("productfile")]
        public MaxProductFileEntity ProductFile(string lsId)
        {
            MaxProductFileEntity loR = MaxProductFileEntity.Create();
            if (!string.IsNullOrEmpty(lsId))
            {
                Guid loId = MaxConvertLibrary.ConvertToGuid(typeof(object), lsId);
                loR.LoadByIdCache(loId);
            }

            return loR;
        }

        [HttpGet]
        [ActionName("productfileemailpopcontainer")]
        [MaxEnableCorsAttributeWebApi]
        public string ProductFileEmailPopUpContainer()
        {
            string lsR = string.Empty;
            lsR = this.Render("ProductFileEmailPopUpContainer", null, null);

            return lsR;
        }

        [HttpGet]
        [ActionName("productfileemailform")]
        [MaxEnableCorsAttributeWebApi]
        public string ProductFileEmailForm(string lsId)
        {
            string lsR = string.Empty;
            MaxProductFileViewModel loModel = new MaxProductFileViewModel();
            loModel.Id = lsId;
            if (loModel.EntityLoad() && loModel.Load())
            {
                object loEmailAddress = MaxConfigurationLibrary.GetValue(MaxEnumGroup.ScopeProfile, "ProductFileEmailAddress");
                MaxIndex loMetaIndex = new MaxIndex();
                loMetaIndex.Add("EmailAddress", loEmailAddress);

                lsR = this.Render("ProductFileEmailForm", loModel, loMetaIndex);
            }

            return lsR;
        }

        [HttpGet]
        [HttpOptions]
        [ActionName("productfileemail")]
        [MaxEnableCorsAttributeWebApi]
        public string ProductFileEmail(string lsId)
        {
            return string.Empty;
        }

        [HttpPost]
        [ActionName("productfileemail")]
        [MaxEnableCorsAttributeWebApi]
        public string ProductFileEmail(string lsId, [FromBody] MaxEmailInfo loEmailInfo)
        {
            string lsR = string.Empty;
            MaxProductFileViewModel loModel = new MaxProductFileViewModel();
            loModel.Id = lsId;
            if (loModel.EntityLoad() && loModel.Load())
            {
                MaxEmailEntity loEmail = MaxEmailEntity.Create();
                if (!MaxEmailEntity.IsValidEmail(loEmailInfo.ToEmail))
                {
                    lsR = "The email address is not valid.";
                }
                else
                {
                    if (string.IsNullOrEmpty(loEmailInfo.FromName))
                    {
                        loEmailInfo.FromName = "Product Catalog";
                    }

                    if (string.IsNullOrEmpty(loEmailInfo.FromEmail))
                    {
                        loEmailInfo.FromEmail = "catalog@efactorysolutions.com";
                    }

                    if (string.IsNullOrEmpty(loEmailInfo.Subject))
                    {
                        loEmailInfo.Subject = "File " + loModel.Name;                    
                    }

                    MaxConfigurationLibrary.SetValue(MaxEnumGroup.ScopeProfile, "ProductFileEmailAddress", loEmailInfo.ToEmail);
                    string lsContent = this.Render("ProductFileEmail", loModel, new MaxIndex());
                    string lsContentInline = MaxDesignLibrary.ConvertToInlineCSS(lsContent, string.Empty);
                    if (!loModel.SendAsEmailAttachment(loEmailInfo.ToEmail, loEmailInfo.FromName, loEmailInfo.FromEmail, loEmailInfo.Subject, lsContentInline))
                    {
                        lsR = "There was an error sending the email.";
                    }
                }
            }
            else
            {
                lsR = "The file as not found.";
            }

            return lsR;
        }

        [HttpGet]
        [HttpOptions]
        [ActionName("productfileemailcheck")]
        [MaxEnableCorsAttributeWebApi]
        public string ProductFileEmailCheck(string lsId)
        {
            return string.Empty;
        }

        [HttpPost]
        [ActionName("productfileemailcheck")]
        [MaxEnableCorsAttributeWebApi]
        public string ProductFileEmailCheck(string lsId, [FromBody] MaxEmailInfo loEmailInfo)
        {
            string lsR = string.Empty;
            MaxProductFileViewModel loModel = new MaxProductFileViewModel();
            loModel.Id = lsId;
            if (loModel.EntityLoad() && loModel.Load())
            {
                MaxEmailEntity loEmail = MaxEmailEntity.Create();
                if (!MaxEmailEntity.IsValidEmail(loEmailInfo.ToEmail))
                {
                    lsR = "The email address is not valid.";
                }
            }
            else
            {
                lsR = "The file as not found.";
            }

            return lsR;
        }

        [HttpGet]
        [ActionName("contactpersonlist")]
        public List<MaxCrmPersonViewModel> ContactPersonList()
        {
            var loQS = this.Request.GetQueryNameValuePairs();
            foreach (var loQSPair in loQS)
            {
                if (loQSPair.Key.ToLower() == "reload")
                {
                    string lsValue = loQSPair.Value;
                    if (!string.IsNullOrEmpty(lsValue))
                    {
                        bool lbValue = MaxConvertLibrary.ConvertToBoolean(typeof(object), lsValue);
                        if (lbValue)
                        {
                            MaxCrmPersonViewModel.UpdateCache();
                        }
                    }
                }
            }

            List<MaxCrmPersonViewModel> loR = MaxCrmPersonViewModel.GetContactPersonList();
            return loR;
        }
    }

    public class MaxEmailInfo
    {
        public string ToEmail {get; set;}
        public string FromEmail {get; set;}
        public string FromName {get; set;}
        public string Subject {get; set;}


    }
}