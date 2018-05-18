using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Reflection;

using Umbraco.Core;
using Umbraco.Web;
using Umbraco.Web.Models;

using RockawayWishCMS.Data.Helpers;
using RockawayWishCMS.Data.Entities;

namespace RockawayWishCMS.Data.Repositories
{
    public class UmbracoRepository
    {
        public UmbracoRepository()
        {
            _UmbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }

        #region Private Properties
        internal UmbracoHelper _UmbracoHelper;
        #endregion

        #region Private Methods
        private void AssignProperties(object source, DynamicPublishedContent content)
        {
            foreach (var contentProperty in content.ContentType.PropertyTypes)
            {
                object propertyValue = null;

                propertyValue = content.GetPropertyValue(contentProperty.PropertyTypeAlias);

                try
                {
                        source.GetType().InvokeMember(
                            contentProperty.PropertyTypeAlias,
                            BindingFlags.SetProperty,
                            null,
                            source,
                            new object[] {
                                            propertyValue
                                         }
                                 );
                }
                catch (Exception ex)
                {
                    _ErrorMessage.Append(ex.Message);
                }

            }
        }
        StringBuilder _ErrorMessage = new StringBuilder();

        private object MapDocumentTypeEntity(int id, object entity)
        {
            try
            {
                DynamicPublishedContent content = _UmbracoHelper.Content(id.ToString());
                AssignProperties(entity, content);

            }
            catch (Exception ex)
            {
                _ErrorMessage.Append(ex.Message);
            }

            return entity;
        }
        private void RecurseAndPopulate(DynamicPublishedContent content)
        {
            // loop through document type enum
            var children = content.Children;
        }
        private void LookupDocumentTypeAndPopulate(string documentTypeAlias)
        {
        }
        #endregion

        #region Public Methods
        public WebsiteEntity GetWebsiteEntity()
        {
            WebsiteEntity websiteEntity = new WebsiteEntity();
            string error = string.Empty;
            try
            {
                DynamicPublishedContentList contentRootList = _UmbracoHelper.ContentAtRoot();

                if (contentRootList != null)
                {
                    // loop through website documenttypes
                    foreach (var websiteItem in contentRootList)
                    {
                        //get website node
                       if (websiteItem.DocumentTypeAlias.ToLower() == DocumentTypeEnum.Website.ToString().ToLower())
                       {
                           // Map Website DocumentType to Website Object
                           websiteEntity = (WebsiteEntity)MapDocumentTypeEntity(websiteItem.Id, websiteEntity);

                           foreach (var websiteItemChild in websiteItem.Children)
                           {
                               if (websiteItemChild.DocumentTypeAlias.ToLower() == DocumentTypeEnum.Pages.ToString().ToLower())
                               {
                                   // Map Website DocumentType to Website Object
                                   websiteEntity.PagesEntity = (PagesEntity)MapDocumentTypeEntity(websiteItemChild.Id, websiteEntity.PagesEntity);

                                   foreach (var pagesItemChild in websiteItemChild.Children)
                                   {
                                       if (pagesItemChild.DocumentTypeAlias.ToLower() == DocumentTypeEnum.HomePage.ToString().ToLower())
                                       {
                                           // Map Website DocumentType to Website Object
                                           websiteEntity.PagesEntity.HomePageEntity = new HomePageEntity();
                                           var homePageEntity = (HomePageEntity)MapDocumentTypeEntity(pagesItemChild.Id, new HomePageEntity());
                                           websiteEntity.PagesEntity.HomePageEntity.headerText = homePageEntity.headerText;
                                           websiteEntity.PagesEntity.HomePageEntity.pageTitle = homePageEntity.pageTitle;
                                           websiteEntity.PagesEntity.HomePageEntity.metaKeywords = homePageEntity.metaKeywords;
                                           websiteEntity.PagesEntity.HomePageEntity.metaDescription = homePageEntity.metaDescription;
                                           websiteEntity.PagesEntity.HomePageEntity.metaDescription = homePageEntity.metaDescription;
                                           websiteEntity.PagesEntity.HomePageEntity.metaDescription = homePageEntity.metaDescription;
                                           websiteEntity.PagesEntity.HomePageEntity.metaDescription = homePageEntity.metaDescription;
                                           websiteEntity.PagesEntity.HomePageEntity.metaDescription = homePageEntity.metaDescription;
                                           websiteEntity.PagesEntity.HomePageEntity.metaDescription = homePageEntity.metaDescription;

                                           foreach (var homePageItemChild in pagesItemChild.Children)
                                           {
                                               if (homePageItemChild.DocumentTypeAlias.ToLower() == DocumentTypeEnum.Carousel.ToString().ToLower())
                                               {
                                                   // Map Website DocumentType to Website Object
                                                  websiteEntity.PagesEntity.HomePageEntity.CarouselEntity = (CarouselEntity)MapDocumentTypeEntity(homePageItemChild.Id, websiteEntity.PagesEntity.HomePageEntity.CarouselEntity);

                                                   foreach (var carouselItemChild in homePageItemChild.Children)
                                                   {
                                                       int carouselSlideCount = 0;
                                                       if (carouselItemChild.DocumentTypeAlias.ToLower() == DocumentTypeEnum.CarouselSlide.ToString().ToLower())
                                                       {
                                                           // Map Website DocumentType to Website Object
                                                           //websiteEntity.PagesEntity.HomePageEntity.CarouselEntity.CarouselSlideEntities[carouselSlideCount] = (CarouselSlideEntity)MapDocumentTypeEntity(carouselItemChild.Id, websiteEntity.PagesEntity.HomePageEntity.CarouselEntity.CarouselSlideEntities[carouselSlideCount]);

                                                       }

                                                   }
                                               }

                                           }
                                       }

                                   }



                               }

                           }


 
                        }

                    }
                }
                return websiteEntity;
            }
            catch (Exception ex) {
                error = ex.Message;

            }
            
            return websiteEntity;
        }

        #endregion

    }
}
