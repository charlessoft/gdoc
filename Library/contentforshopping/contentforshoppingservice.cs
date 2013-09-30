﻿/* Copyright (c) 2006 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Net;
using Google.GData.Client;
using Google.GData.Extensions;
using System.Collections.Generic;

namespace Google.GData.ContentForShopping {
    /// <summary>
    /// The ContentForShoppingService class extends the basic Service abstraction
    /// to define a service that is preconfigured for access to the
    /// Google Content API for Shopping.
    /// </summary>
    public class ContentForShoppingService : Service {
        /// <summary>The ID of the merchant account</summary>
        private string accountId;
        /// <summary>a projection determining response values
        /// can be one of "schema" and "generic"</summary>
        private string projection = "schema";
        /// <summary>Bool determining if warnings should be returned</summary>
        private bool showWarnings = false;
        /// <summary>Bool determining if dry run mode should be used</summary>
        private bool dryRun = false;

        /// <summary>The Content for Shopping service's name</summary>
        public const String GContentForShoppingService = "structuredcontent";

        private string homepage = "";
        public string Homepage
        {
            get { return homepage; }
            set { homepage = value; }
        }

        private string rootUrl = "";
        public string RootUrl
        {
            get { return rootUrl; }
            set { rootUrl = value; }
        }

        /// <summary>
        /// AccountId of the service object.
        /// </summary>
        public string AccountId
        {
            get { return this.accountId; }
            set { this.accountId = value; }
        }

        /// <summary>
        /// Accessor method for Projection.
        /// </summary>
        public string Projection {
            get { return projection; }
            set { projection = value; }
        }

        /// <summary>
        /// Accessor method for ShowWarnings.
        /// </summary>
        public bool ShowWarnings {
            get { return showWarnings; }
            set { showWarnings = value; }
        }

        /// <summary>
        /// Accessor method for DryRun.
        /// </summary>
        public bool DryRun {
            get { return dryRun; }
            set { dryRun = value; }
        }

        /// <summary>
        /// Constructor which takes account id and application name.
        /// </summary>
        /// <param name="applicationName">The name of the client application</param>
        /// <param name="accountId">The account ID of the user.</param>
        /// using this service.</param>
        public ContentForShoppingService(String applicationName, String accountId)
            : base(GContentForShoppingService, applicationName)
        {
            this.accountId = accountId;

            this.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewEntry);
            this.NewFeed += new ServiceEventHandler(this.OnParsedNewFeed);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationName">The name of the client application</param>
        /// using this service.</param>
        public ContentForShoppingService(String applicationName)
            : base(GContentForShoppingService, applicationName)
        {
            this.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewEntry);
            this.NewFeed += new ServiceEventHandler(this.OnParsedNewFeed);
        }

        /// <summary>
        ///  overwritten Query method
        /// </summary>
        /// <returns>the retrieved ProductFeed</returns>
        public ProductFeed Query()
        {
            return Query(new ProductQuery());
        }

        /// <summary>
        ///  overwritten Query method
        /// </summary>
        /// <param name="accountId">the account Id intended on the request</param>
        /// <returns>the retrieved ProductFeed</returns>
        public ProductFeed Query(string accountId)
        {
            ProductQuery query = new ProductQuery();
            query.AccountId = accountId;
            return Query(query);
        }

        /// <summary>
        ///  overwritten Query method
        /// </summary>
        /// <param name="feedQuery">The FeedQuery to use</param>
        /// <returns>the retrieved ProductFeed</returns>
        public ProductFeed Query(ProductQuery feedQuery)
        {
            if (feedQuery.AccountId == null) {
                feedQuery.AccountId = this.AccountId;
            }
            if (feedQuery.Projection == null) {
                feedQuery.Projection = this.Projection;
            }
            return base.Query(feedQuery) as ProductFeed;
        }

        /// <summary>
        /// Overwrites Get
        /// </summary>
        /// <param name="entryUri">The URI of the Product entry.</param>
        /// <returns>the retrieved ProductEntry</returns>
        public ProductEntry Get(string entryUri)
        {
            return base.Get(entryUri) as ProductEntry;
        }

        /// <summary>
        /// Overwrites Get
        /// </summary>
        /// <param name="channel">The channel of the product. Usually online or local.</param>
        /// <param name="language">The language for the product.</param>
        /// <param name="country">The target country of the product.</param>
        /// <param name="productId">The unique identifier for the product in the given locale.</param>
        /// <returns>the retrieved ProductEntry</returns>
        public ProductEntry Get(string channel, string language, string country, string productId)
        {
            string path = CreateProductIdentifier(channel, language, country, productId);
            Uri entryUri = CreateUri(path);
            return Get(entryUri.ToString());
        }

        /// <summary>
        /// Overwrites Get
        /// </summary>
        /// <param name="language">The language for the product.</param>
        /// <param name="country">The target country of the product.</param>
        /// <param name="productId">The unique identifier for the product in the given locale.</param>
        /// <returns>the retrieved ProductEntry</returns>
        public ProductEntry Get(string language, string country, string productId)
        {
            return Get("online", language, country, productId);
        }

        /// <summary>
        /// Inserts a new product entry.
        /// </summary>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the inserted entry</returns>
        public ProductEntry Insert(ProductEntry entry)
        {
            return base.Insert(CreateUri(null), entry) as ProductEntry;
        }

        /// <summary>
        /// Inserts a new product entry.
        /// </summary>
        /// <param name="entry">the entry to insert</param>
        /// <param name="accountId">The account ID of the user.</param>
        /// <param name="projection">a projection determining response values
        /// <returns>the inserted entry</returns>
        public ProductEntry Insert(ProductEntry entry, string accountId, string projection)
        {
            return base.Insert(CreateUri(accountId, projection, null), entry);
        }

        /// <summary>
        /// Inserts a new product entry into the specified feed.
        /// </summary>
        /// <param name="feed">the feed into which this entry should be inserted</param>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the inserted entry</returns>
        public ProductEntry Insert(ProductFeed feed, ProductEntry entry)
        {
            return base.Insert(feed, entry);
        }

        /// <summary>
        /// Updates an existing product entry with the new values
        /// </summary>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the updated entry returned by the server</returns>
        public ProductEntry Update(ProductEntry entry)
        {
            return base.Update(entry);
        }

        /// <summary>
        /// Deletes an existing product.
        /// </summary>
        /// <param name="entry">the entry to delete</param>
        public void Delete(ProductEntry entry)
        {
            base.Delete(entry);
        }

        /// <summary>
        /// takes a given feed, and does a batch post of that feed
        /// </summary>
        /// <param name="feed">the feed to post</param>
        /// <param name="accountId">The account ID of the user.</param>
        /// <param name="projection">a projection determining response values
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed Batch(ProductFeed feed, string accountId, string projection) {
            Uri batchUri = CreateUri(accountId, projection, "batch");
            return base.Batch(feed, batchUri) as ProductFeed;
        }

        /// <summary>
        /// takes a given feed, and does a batch post of that feed
        /// </summary>
        /// <param name="feed">the feed to post</param>
        /// <param name="accountId">The account ID of the user.</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed Batch(ProductFeed feed, string accountId) {
            return Batch(feed, accountId, this.Projection);
        }

        /// <summary>
        /// takes a given feed, and does a batch post of that feed
        /// </summary>
        /// <param name="feed">the feed to post</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed Batch(ProductFeed feed) {
            return Batch(feed, this.AccountId, this.Projection);
        }

        /// <summary>
        /// takes a list of products, adds batch operation and adds each item to a feed
        /// </summary>
        /// <param name="products">the list of products to add to a feed</param>
        /// <returns>the created ProductFeed</returns>
        protected ProductFeed CreateBatchFeed(List<ProductEntry> products, GDataBatchOperationType operation) {
            ProductFeed feed = new ProductFeed(null, this);

            foreach(ProductEntry product in products)
            {
                if (product.BatchData != null) {
                    product.BatchData.Type = operation;
                }
                else {
                    product.BatchData = new GDataBatchEntryData(operation);
                }

                feed.Entries.Add(product);
            }

            return feed;
        }

        /// <summary>
        /// takes a list of products, adds insert as the batch operation and inserts them
        /// </summary>
        /// <param name="products">the list of products to insert</param>
        /// <param name="accountId">The account ID of the user.</param>
        /// <param name="projection">a projection determining response values</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed InsertProducts(List<ProductEntry> products, string accountId, string projection) {
            ProductFeed feed = CreateBatchFeed(products, GDataBatchOperationType.insert);
            return Batch(feed, accountId, projection);
        }

        /// <summary>
        /// takes a list of products, adds insert as the batch operation and inserts them
        /// </summary>
        /// <param name="products">the list of products to insert</param>
        /// <param name="accountId">The account ID of the user.</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed InsertProducts(List<ProductEntry> products, string accountId) {
            ProductFeed feed = CreateBatchFeed(products, GDataBatchOperationType.insert);
            return Batch(feed, accountId);
        }

        /// <summary>
        /// takes a list of products, adds insert as the batch operation and inserts them
        /// </summary>
        /// <param name="products">the list of products to insert</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed InsertProducts(List<ProductEntry> products) {
            ProductFeed feed = CreateBatchFeed(products, GDataBatchOperationType.insert);
            return Batch(feed);
        }

        /// <summary>
        /// takes a list of products, adds update as the batch operation and updates them
        /// </summary>
        /// <param name="products">the list of products to update</param>
        /// <param name="accountId">The account ID of the user.</param>
        /// <param name="projection">a projection determining response values</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed UpdateProducts(List<ProductEntry> products, string accountId, string projection) {
            ProductFeed feed = CreateBatchFeed(products, GDataBatchOperationType.update);
            return Batch(feed, accountId, projection);
        }

        /// <summary>
        /// takes a list of products, adds update as the batch operation and updates them
        /// </summary>
        /// <param name="products">the list of products to update</param>
        /// <param name="accountId">The account ID of the user.</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed UpdateProducts(List<ProductEntry> products, string accountId) {
            ProductFeed feed = CreateBatchFeed(products, GDataBatchOperationType.update);
            return Batch(feed, accountId);
        }

        /// <summary>
        /// takes a list of products, adds update as the batch operation and updates them
        /// </summary>
        /// <param name="products">the list of products to update</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed UpdateProducts(List<ProductEntry> products) {
            ProductFeed feed = CreateBatchFeed(products, GDataBatchOperationType.update);
            return Batch(feed);
        }

        /// <summary>
        /// takes a list of products, adds delete as the batch operation and delete them
        /// </summary>
        /// <param name="products">the list of products to delete</param>
        /// <param name="accountId">The account ID of the user.</param>
        /// <param name="projection">a projection determining response values</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed DeleteProducts(List<ProductEntry> products, string accountId, string projection) {
            ProductFeed feed = CreateBatchFeed(products, GDataBatchOperationType.delete);
            return Batch(feed, accountId, projection);
        }

        /// <summary>
        /// takes a list of products, adds delete as the batch operation and delete them
        /// </summary>
        /// <param name="products">the list of products to delete</param>
        /// <param name="accountId">The account ID of the user.</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed DeleteProducts(List<ProductEntry> products, string accountId) {
            ProductFeed feed = CreateBatchFeed(products, GDataBatchOperationType.delete);
            return Batch(feed, accountId);
        }

        /// <summary>
        /// takes a list of products, adds delete as the batch operation and delete them
        /// </summary>
        /// <param name="products">the list of products to delete</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed DeleteProducts(List<ProductEntry> products) {
            ProductFeed feed = CreateBatchFeed(products, GDataBatchOperationType.delete);
            return Batch(feed);
        }

        /// <summary>
        /// Creates a product identifier based on four attributes.
        /// </summary>
        /// <param name="channel">The channel of the product. Usually online or local.</param>
        /// <param name="language">The language for the product.</param>
        /// <param name="country">The target country of the product.</param>
        /// <param name="productId">The unique identifier for the product in the given locale.</param>
        /// <returns>the product identifier</returns>
        protected string CreateProductIdentifier(string channel, string language, string country, string productId)
        {
            StringBuilder result = new StringBuilder(channel, 256);
            result.Append(':');
            result.Append(language);
            result.Append(':');
            result.Append(country);
            result.Append(':');
            result.Append(productId);

            return result.ToString();
        }

        /// <summary>
        /// Creates the URI query string based on all set properties.
        /// </summary>
        /// <param name="path">the path after the projection (optional)</param>
        /// <returns>the URI query string</returns>
        protected Uri CreateUri(string path)
        {
            return CreateUri(this.AccountId, this.Projection, path);
        }

        /// <summary>
        /// Creates the URI query string based on all set properties.
        /// </summary>
        /// <param name="accountId">the account Id intended on the request</param>
        /// <param name="projection">a projection determining response values
        /// can be one of "schema" and "generic"</param>
        /// <param name="path">the path after the projection (optional)</param>
        /// <returns>the URI query string</returns>
        protected Uri CreateUri(string accountId, string projection, string path)
        {
            StringBuilder result = new StringBuilder(ContentForShoppingNameTable.AllFeedsBaseUri, 2048);
            result.Append("/");
            result.Append(accountId);
            result.Append("/items/products/");
            result.Append(projection);
            if (path != null) {
                result.Append("/");
                result.Append(path);
            }

            char paramInsertion = '?';
            if (this.ShowWarnings) {
                result.Append(paramInsertion);
                result.Append(ContentForShoppingNameTable.ShowWarningsParameter);
                paramInsertion = '&';
            }
            if (this.DryRun) {
                result.Append(paramInsertion);
                result.Append(ContentForShoppingNameTable.DryRunParameter);
            }

            return new Uri(result.ToString());
        }

        /// <summary>
        /// Event handler. Called when a new list entry is parsed.
        /// </summary>
        /// <param name="sender">the object that's sending the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param>
        protected void OnParsedNewEntry(object sender, FeedParserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (e.CreatingEntry == true)
            {
                e.Entry = new ProductEntry();
            }
        }

        /// <summary>
        /// Feed handler. Instantiates a new <code>ProductFeed</code>.
        /// </summary>
        /// <param name="sender">the object that's sending the event</param>
        /// <param name="e"><code>ServiceEventArgs</code>, holds the feed</param>
        protected void OnParsedNewFeed(object sender, ServiceEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            e.Feed = new ProductFeed(e.Uri, e.Service);
        }
    }
}
