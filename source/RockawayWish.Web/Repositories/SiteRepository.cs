using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

using RockawayWish.Core.Entities;
using RockawayWish.Web.Helpers;
using RockawayWish.Web.Models;

namespace RockawayWish.Web.Repositories
{
    public class SiteRepository : ISiteRepository
    {
        const string _UmbracoApiEndpoint = "https://rockawaywishcms-local.tiradointeractive.com/umbraco/api/site/get";
        readonly Uri _baseUri;
        readonly IDictionary<string, string> _headers;
        WebsiteEntity _WebSiteEntity;
        HomePageVM _HomePageVM;

        public HomePageVM HomePageVM { get { return _HomePageVM; } }
        public WebsiteEntity WebsiteEntity { get { return _WebSiteEntity; } }

        public SiteRepository()
        {
            _baseUri = new Uri(_UmbracoApiEndpoint);
            _headers = new Dictionary<string, string>();
            _WebSiteEntity = Get().Result;
        }

        private async Task<WebsiteEntity> Get()
        {

            _HomePageVM = new HomePageVM();
            try
            {
                //var url = new Uri(_UmbracoApiEndpoint);
                var result = await new HTTPHelper().SendRequestAsync<WebsiteEntity>(_baseUri).ConfigureAwait(false);

                if (result != null && result.PagesEntity != null)
                {
                    if (result != null && result.PagesEntity != null)
                    {
                        if (result.PagesEntity.HomePageEntity != null)
                        {
                            // set HomePageVM object
                            _HomePageVM.headerText = result.PagesEntity.HomePageEntity.headerText;
                            _HomePageVM.pageTitle = result.PagesEntity.HomePageEntity.pageTitle;
                            _HomePageVM.metaKeywords = result.PagesEntity.HomePageEntity.metaKeywords;
                            _HomePageVM.metaDescription = result.PagesEntity.HomePageEntity.metaDescription;
                            if (result.PagesEntity.HomePageEntity.CarouselEntity != null)
                            {
                                if (result.PagesEntity.HomePageEntity.CarouselEntity.CarouselSlideEntities != null)
                                {
                                    if (result.PagesEntity.HomePageEntity.CarouselEntity.CarouselSlideEntities.Count > 0)
                                    {
                                        foreach (var slide in result.PagesEntity.HomePageEntity.CarouselEntity.CarouselSlideEntities)
                                        {
                                            var carouselSlide = new CarouselSlideVM();
                                            carouselSlide.buttonALinkToAURL = slide.buttonALinkToAURL;
                                            carouselSlide.buttonCSSClass = slide.buttonCSSClass;
                                            carouselSlide.buttonFileDownload = slide.buttonFileDownload;
                                            carouselSlide.buttonLinkOpenNewWindow = slide.buttonLinkOpenNewWindow;
                                            carouselSlide.buttonLinkToAFile = slide.buttonLinkToAFile;
                                            carouselSlide.buttonLinkURL = slide.buttonLinkURL;
                                            carouselSlide.buttonText = slide.buttonText;
                                            carouselSlide.headerText = slide.headerText;
                                            carouselSlide.headerTextCSSClass = slide.headerTextCSSClass;
                                            carouselSlide.showButton = slide.showButton;
                                            carouselSlide.sliderImage = slide.sliderImage;
                                            carouselSlide.sliderImageCSSClass = slide.sliderImageCSSClass;
                                            carouselSlide.subheaderText = slide.subheaderText;
                                            carouselSlide.subHeaderTextCSSClass = slide.subHeaderTextCSSClass;
                                            _HomePageVM.Carousel.Slides.Add(carouselSlide);
                                        }
                                    }
                                    else
                                    {
                                        // there are no home page carousel slide entities
                                    }
                                }
                                else
                                {
                                    // not able to get home page carousel slide entities
                                }
                            }
                            else
                            {
                                // not able to get home page carousel entity
                            }
                        }
                        else
                        {
                            // not able to get home page entity
                        }
                    }
                    else
                    {
                        // not able to get pages entity
                    }

                    
                    //_HomePageVM = new HomePageVM();
                   // _HomePageVM.headerText = result.

                    return result;
                }
            }
            catch (Exception ex)
            {
                string err = ex.ToString();
            }


            //var result = HTTPHelper.GetAsync(new Di)

            //await Task.Delay(0);
            return null;

        }

    }
}