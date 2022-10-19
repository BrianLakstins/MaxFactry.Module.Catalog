// <copyright file="MaxProductManageViewModel.cs" company="Lakstins Family, LLC">
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
// <change date="9/16/2014" author="Brian A. Lakstins" description="Initial Release">
// <change date="9/30/2014" author="Brian A. Lakstins" description="Added product update to create searchable products.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.Mvc4.PresentationLayer
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using System.Web;
    using System.Web.Security;
    using System.Xml;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Module.Catalog.BusinessLayer;
    using MaxFactry.Module.Catalog.PresentationLayer;
    using MaxFactry.General.BusinessLayer;
    using MaxFactry.General.PresentationLayer;
    using MaxFactry.General.AspNet.PresentationLayer;
    using MaxFactry.General.AspNet.BusinessLayer;

    /// <summary>
    /// View model for managing clients.
    /// </summary>
    public class MaxProductImportStoreSupplyViewModel : MaxProductImportViewModel
	{
        public const string DataNameUrlList = "MaxFileDownloadEntityUrlList";

        public MaxProductImportStoreSupplyViewModel() : base()
        {
            ImportGroup = 1;
            
        }

        public override string ProcessCommand()
        {
            string lsR = base.ProcessCommand();

            if (this.Command.Equals("DownloadData", StringComparison.InvariantCultureIgnoreCase))
            {
                long lnStart = MaxOwinLibrary.GetTimeSinceBeginRequest();
                MaxLogLibrary.Log(MaxEnumGroup.LogInfo, "Start CrawlWebSite start:" + lnStart.ToString(), "MaxProductImportManageViewModel");
                int lnLinksProcessed = this.CrawlWebSite("http://www.storesupply.com/sitemap.xml");
                long lnDuration = MaxOwinLibrary.GetTimeSinceBeginRequest() - lnStart;
                MaxLogLibrary.Log(MaxEnumGroup.LogInfo, "End CrawlWebSite duration:" + lnDuration.ToString(), "MaxProductImportManageViewModel");
                lsR += lnLinksProcessed.ToString() + " links processed in " + lnDuration.ToString() + " milliseconds";
            }
            else if (this.Command.Equals("Import", StringComparison.InvariantCultureIgnoreCase))
            {
                MaxEntityList loList = MaxFileDownloadEntity.Create().LoadAllByResponseHost("www.storesupply.com");
                //// Create an index so only the latest response for any particular URL will be used.
                Dictionary<string, DateTime> loResponseIndex = new Dictionary<string, DateTime>();
                for (int lnE = 0; lnE < loList.Count; lnE++)
                {
                    if (loResponseIndex.ContainsKey(((MaxFileDownloadEntity)loList[lnE]).ResponseUrl.ToLower()))
                    {
                        if (((MaxFileDownloadEntity)loList[lnE]).CreatedDate > loResponseIndex[((MaxFileDownloadEntity)loList[lnE]).ResponseUrl.ToLower()])
                        {
                            loResponseIndex[((MaxFileDownloadEntity)loList[lnE]).ResponseUrl.ToLower()] = ((MaxFileDownloadEntity)loList[lnE]).CreatedDate;
                        }
                    }
                    else
                    {
                        loResponseIndex.Add(((MaxFileDownloadEntity)loList[lnE]).ResponseUrl.ToLower(), ((MaxFileDownloadEntity)loList[lnE]).CreatedDate);
                    }
                }

                MaxEntityList loCategoryEntityList = MaxCategoryEntity.Create().LoadAllByCatalogId(this.CatalogId);
                Dictionary<string, MaxCategoryEntity> loCategoryIndex = new Dictionary<string, MaxCategoryEntity>();
                for (int lnE = 0; lnE < loCategoryEntityList.Count; lnE++)
                {
                    string lsCategoryPath = ((MaxCategoryEntity)loCategoryEntityList[lnE]).Name;
                    MaxCategoryEntity loParent = ((MaxCategoryEntity)loCategoryEntityList[lnE]).Parent;
                    while (null != loParent)
                    {
                        lsCategoryPath = loParent.Name + "\t" + lsCategoryPath;
                        loParent = loParent.Parent;
                    }

                    if (!loCategoryIndex.ContainsKey(lsCategoryPath))
                    {
                        loCategoryIndex.Add(lsCategoryPath, ((MaxCategoryEntity)loCategoryEntityList[lnE]));
                    }
                }

                MaxEntityList loProductEntityList = MaxProductEntity.Create().LoadAllByCatalogId(this.CatalogId);
                Dictionary<string, MaxProductEntity> loProductIndex = new Dictionary<string, MaxProductEntity>();
                for (int lnE = 0; lnE < loProductEntityList.Count; lnE++)
                {
                    MaxProductEntity loProduct = (MaxProductEntity)loProductEntityList[lnE];
                    if (loProduct.BranchType == "import from storesupply.com")
                    {
                        if (!loProductIndex.ContainsKey(loProduct.Sku))
                        {
                            loProductIndex.Add(loProduct.Sku, loProduct);
                        }
                    }
                }

                MaxEntityList loProductImageEntityList = MaxProductImageEntity.Create().LoadAllCache();
                Dictionary<string, MaxProductImageEntity> loProductImageIndex = new Dictionary<string, MaxProductImageEntity>();
                for (int lnE = 0; lnE < loProductImageEntityList.Count; lnE++)
                {
                    MaxProductImageEntity loProductImage = (MaxProductImageEntity)loProductImageEntityList[lnE];
                    if (!loProductImageIndex.ContainsKey(loProductImage.FromFileName.ToLower()))
                    {
                        loProductImageIndex.Add(loProductImage.FromFileName.ToLower(), loProductImage);
                    }
                }

                MaxLogLibrary.Log(MaxEnumGroup.LogInfo, "Start Processing Product Import List " + loList.Count.ToString(), "MaxProductImportManageViewModel");
                for (int lnE = 0; lnE < loList.Count; lnE++)
                {
                    if (lnE % 100 == 0)
                    {
                        HttpContext.Current.Response.Write("<!--" + MaxOwinLibrary.GetTimeSinceBeginRequest().ToString() + " " + lnE + " of " + loList.Count.ToString() + "-->\r\n");
                        HttpContext.Current.Response.Flush();
                    }

                    MaxLogLibrary.Log(MaxEnumGroup.LogInfo, "Processing Product Import " + lnE.ToString(), "MaxProductImportManageViewModel");
                    MaxFileDownloadEntity loEntity = (MaxFileDownloadEntity)loList[lnE];
                    if (loResponseIndex[loEntity.ResponseUrl.ToLower()] == loEntity.CreatedDate &&
                        loEntity.ContentType.StartsWith("text/html"))
                    {
                        string lsContent = loEntity.ContentString;
                        MaxIndex loIndex = this.ExtractData(lsContent, "www.storesupply.com Product Data");
                        if (loIndex.Contains("Product") && loIndex.Contains("CategoryList") && loIndex.Contains("Sku"))
                        {
                            //// Create the categories related to this product.
                            string lsCategoryPath = this.ParseValue(loIndex, "CategoryList");
                            if (!loCategoryIndex.ContainsKey(lsCategoryPath))
                            {
                                string[] laCategoryPath = lsCategoryPath.Split('\t');
                                lsCategoryPath = string.Empty;
                                MaxCategoryEntity loLastParent = null;
                                for (int lnC = 0; lnC < laCategoryPath.Length; lnC++)
                                {
                                    if (!string.IsNullOrEmpty(lsCategoryPath))
                                    {
                                        lsCategoryPath += "\t";
                                    }

                                    lsCategoryPath += laCategoryPath[lnC];
                                    if (!loCategoryIndex.ContainsKey(lsCategoryPath))
                                    {
                                        MaxCategoryEntity loCategoryNew = MaxCategoryEntity.Create();
                                        loCategoryNew.IsActive = true;
                                        loCategoryNew.PrimaryCatalogId = this.CatalogId;
                                        loCategoryNew.Name = laCategoryPath[lnC];
                                        if (null != loLastParent)
                                        {
                                            loCategoryNew.ParentId = loLastParent.Id;
                                        }

                                        loCategoryNew.Insert();
                                        loCategoryNew.Parent = loLastParent;
                                        loCategoryIndex.Add(lsCategoryPath, loCategoryNew);
                                    }

                                    loLastParent = loCategoryIndex[lsCategoryPath];
                                }
                            }

                            Guid loCategoryId = loCategoryIndex[lsCategoryPath].Id;

                            string lsSku = this.ParseValue(loIndex, "Sku");

                            //// Add and update products based on imported data
                            MaxProductEntity loProduct = MaxProductEntity.Create();
                            if (loProductIndex.ContainsKey(lsSku))
                            {
                                loProduct = loProductIndex[lsSku];
                            }

                            loProduct.IsActive = false;
                            loProduct.Sku = lsSku;
                            loProduct.Name = this.ParseValue(loIndex, "Product");
                            if (MaxDesignLibrary.ContainsHtml(loProduct.Name))
                            {
                                loProduct.Name = MaxDesignLibrary.RemoveHtml(loProduct.Name);
                            }

                            loProduct.PrimaryCategoryId = loCategoryId;
                            loProduct.PrimaryCatalogId = this.CatalogId;

                            string lsDescription = loIndex["Description"] as string;
                            if (null != lsDescription)
                            {
                                lsDescription = lsDescription.Replace("SSW ItemNo " + lsSku, "");
                                if (lsDescription.Contains("•"))
                                {
                                    lsDescription = lsDescription.Replace("•", "• ");
                                }

                                if (lsDescription.Contains("."))
                                {
                                    lsDescription = lsDescription.Replace(".", ". ");
                                }

                                if (lsDescription.Contains("SSW ItemNo"))
                                {
                                    lsDescription = lsDescription.Substring(0, lsDescription.IndexOf("SSW ItemNo"));
                                }

                                lsDescription = lsDescription.Trim();

                                loIndex.Add("Description", lsDescription);
                            }

                            loProduct.Description = this.ParseValue(loIndex, "Description");

                            double lnPrice = double.Parse(this.ParseValue(loIndex, "Price"));
                            loProduct.PriceBase = lnPrice;
                            List<MaxDescriptionListStructure> loDescriptionList = new List<MaxDescriptionListStructure>();
                            //loDescriptionList.Add(new MaxDescriptionListStructure("Description", this.ParseValue(loIndex, "Description")));
                            string lsFeatures = loIndex["Features"] as string;
                            if (null != lsFeatures)
                            {
                                lsFeatures = lsFeatures.Replace("SSW ItemNo " + lsSku, "");
                                if (lsFeatures.Contains("•"))
                                {
                                    lsFeatures = lsFeatures.Replace("•", "• ");
                                }

                                if (lsFeatures.Contains("."))
                                {
                                    lsFeatures = lsFeatures.Replace(".", ". ");
                                }

                                loIndex.Add("Features", lsFeatures);
                            }

                            loDescriptionList.Add(new MaxDescriptionListStructure("Features", this.ParseValue(loIndex, "Features")));
                            loProduct.DescriptionList = loDescriptionList;
                            loProduct.BranchType = "import from storesupply.com";
                            loProduct.PrimaryVendorId = this.VendorId;
                            loProduct.PrimaryVendorSku = lsSku;
                            if (Guid.Empty.Equals(loProduct.Id))
                            {
                                loProduct.Insert();
                            }
                            else
                            {
                                loProduct.Update();
                            }

                            //// Download and add image for products as MaxProductImage
                            if (loIndex.Contains("ImageSrc"))
                            {
                                string lsImageSrc = this.ParseValue(loIndex, "ImageSrc");
                                Uri loImageUrl = new Uri(new Uri(loEntity.ResponseUrl), lsImageSrc);
                                MaxProductImageEntity loProductImage = MaxProductImageEntity.Create();
                                if (!loProductImageIndex.ContainsKey(loImageUrl.ToString().ToLower()))
                                {
                                    MaxFileDownloadEntity loFileDownloadEntity = MaxFileDownloadEntity.Create();
                                    loFileDownloadEntity.Download(loImageUrl.ToString());
                                    MaxProductImageEntity loProductImageNew = MaxProductImageEntity.Create();
                                    loProductImageNew.Content = loFileDownloadEntity.Content;
                                    loProductImageNew.ContentLength = loFileDownloadEntity.ContentLength;
                                    loProductImageNew.ContentType = loFileDownloadEntity.ContentType;
                                    loProductImageNew.Name = loFileDownloadEntity.Name;
                                    loProductImageNew.FromFileName = loImageUrl.ToString();


                                    loProductImageNew.ProductId = loProduct.Id;
                                    loProductImageNew.IsActive = true;
                                    loProductImageNew.Insert();
                                }
                                else
                                {
                                    loProductImage = loProductImageIndex[loImageUrl.ToString().ToLower()] as MaxProductImageEntity;
                                }

                                if (!loProduct.PrimaryImageId.Equals(loProductImage.Id))
                                {
                                    loProduct.PrimaryImageId = loProductImage.Id;
                                    loProduct.Update();
                                }
                            }                            
                        }

                        loEntity = null;
                        //loList[lnE] = null;
                    }
                }

                MaxLogLibrary.Log(MaxEnumGroup.LogInfo, "End Processing Product Import List " + loList.Count.ToString(), "MaxProductImportManageViewModel");
            }

            return lsR;
        }

        /// <summary>
        /// Downloads pages from a web site.
        /// </summary>
        /// <param name="lsStartUrl">Starting URL.</param>
        /// <returns>Number of new pages retrieved</returns>
        public virtual int CrawlWebSite(string lsStartUrl)
        {
            int lnR = 0;
            Queue<string> loQueue = new Queue<string>();
            List<string> loList = new List<string>();
            Uri loBaseUrl = new Uri(lsStartUrl);
            Uri loUrl = new Uri(lsStartUrl);
            MaxFileDownloadEntity loEntity = MaxFileDownloadEntity.Create();
            loEntity.Download(loUrl.ToString());
            loEntity.Insert();
            loEntity.LoadByIdCache(loEntity.Id);

            loList.Add(loUrl.ToString());
            bool lbCrawlSubpages = false;
            if (!loEntity.ResponseUrl.ToLower().EndsWith(".xml"))
            {
                //// Only crawl sub pages if the file is not the sitemap.xml file (or some other xml file that is the list of urls)
                lbCrawlSubpages = true;
            }

            MaxEntityList loCurrentList = MaxFileDownloadEntity.Create().LoadAllByResponseHost(loUrl.Host.ToLower());
            //// Create an index so only the latest entity will be used to determine if url needs to be downloaded.
            Dictionary<string, DateTime> loIndex = new Dictionary<string, DateTime>();
            for (int lnE = 0; lnE < loCurrentList.Count; lnE++)
            {
                if (loIndex.ContainsKey(((MaxFileDownloadEntity)loCurrentList[lnE]).Name))
                {
                    if (((MaxFileDownloadEntity)loCurrentList[lnE]).CreatedDate > loIndex[((MaxFileDownloadEntity)loCurrentList[lnE]).Name])
                    {
                        loIndex[((MaxFileDownloadEntity)loCurrentList[lnE]).Name] = ((MaxFileDownloadEntity)loCurrentList[lnE]).CreatedDate;
                    }
                }
                else
                {
                    loIndex.Add(((MaxFileDownloadEntity)loCurrentList[lnE]).Name, ((MaxFileDownloadEntity)loCurrentList[lnE]).CreatedDate);
                }
            }

            MaxIndex loUrlList = this.ExtractData(loEntity.ContentString, DataNameUrlList);
            //// Add all urls to a queue to be processed.
            string[] laUrlList = loUrlList.GetSortedKeyList();
            foreach (string lsUrl in laUrlList)
            {
                loUrl = new Uri(loBaseUrl, lsUrl);
                if (loUrl.Host.Equals(loBaseUrl.Host, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!loQueue.Contains(loUrl.ToString()))
                    {
                        bool lbAddToQueue = true;
                        if (loIndex.ContainsKey(loUrl.ToString()))
                        {
                            DateTime ldLastDownloadDate = loIndex[loUrl.ToString()];
                            /// Don't update any more often than every 30 days.
                            if (ldLastDownloadDate.AddDays(30) > DateTime.Now)
                            {
                                lbAddToQueue = false;
                            }
                        }

                        if (lbAddToQueue)
                        {
                            loQueue.Enqueue(loUrl.ToString());
                        }
                    }
                }
            }

            while (loQueue.Count > 0)
            {
                loUrl = new Uri(loBaseUrl, loQueue.Dequeue());
                MaxLogLibrary.Log(MaxEnumGroup.LogInfo, "Crawling Url " + loUrl.ToString() + " [" + loQueue.Count.ToString() + "]", "MaxFileDownloadEntity");
                if (!loList.Contains(loUrl.ToString()))
                {
                    loList.Add(loUrl.ToString());
                    MaxLogLibrary.Log(MaxEnumGroup.LogInfo, "Downloading [" + loUrl.ToString() + "]", "MaxFileDownloadEntity");
                    loEntity = MaxFileDownloadEntity.Create();
                    loEntity.Download(loUrl.ToString());
                    loEntity.IsActive = true;
                    loEntity.Insert();
                    if (loIndex.ContainsKey(loUrl.ToString()))
                    {
                        loIndex[loUrl.ToString()] = loEntity.CreatedDate;
                    }
                    else
                    {
                        loIndex.Add(loUrl.ToString(), loEntity.CreatedDate);
                    }

                    lnR++;
                    System.Threading.Thread.Sleep(100);
                    if (lbCrawlSubpages)
                    {
                        loEntity.LoadByIdCache(loEntity.Id);
                        if (loEntity.ContentType.StartsWith("text/html"))
                        {
                            loUrlList = this.ExtractData(loEntity.ContentString, "UrlList");
                            laUrlList = loUrlList.GetSortedKeyList();
                            foreach (string lsUrl in laUrlList)
                            {
                                loUrl = new Uri(loBaseUrl, lsUrl);
                                if (loUrl.Host.Equals(loBaseUrl.Host, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    if (!loQueue.Contains(loUrl.ToString()))
                                    {
                                        bool lbAddToQueue = true;
                                        if (loIndex.ContainsKey(loUrl.ToString()))
                                        {
                                            DateTime ldLastDownloadDate = loIndex[loUrl.ToString()];
                                            if (ldLastDownloadDate.AddDays(30) > DateTime.Now)
                                            {
                                                lbAddToQueue = false;
                                            }
                                        }

                                        if (lbAddToQueue)
                                        {
                                            loQueue.Enqueue(loUrl.ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return lnR;
        }

        /// <summary>
        /// Extracts data from an Html type document.
        /// </summary>
        /// <param name="lsDocument">Html to parse.</param>
        /// <param name="lsDataType">Type of data to extract.</param>
        /// <returns>Data extracted from a document.</returns>
        public virtual MaxIndex ExtractData(string lsDocument, string lsDataType)
        {
            throw new NotImplementedException();
        }
    }
}
