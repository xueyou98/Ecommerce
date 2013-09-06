﻿using System.Collections.Generic;
using System.Web.Mvc;
using MrCMS.Models;
using MrCMS.Paging;
using MrCMS.Web.Apps.Ecommerce.Models;
using MrCMS.Web.Apps.Ecommerce.Pages;
using MrCMS.Web.Apps.Ecommerce.Services.Products;

namespace MrCMS.Web.Apps.Ecommerce.Services.Categories
{
    public interface ICategoryService
    {
        CategoryPagedList Search(string query = null, int page = 1,int pageSize=10);
        IEnumerable<AutoCompleteResult> Search(string query, List<int> ids);
        IPagedList<Category> GetCategories(Product product, string query, int page);
        IList<Category> GetAll();
        IList<SelectListItem> GetOptions();
        CategoryContainer GetSiteCategoryContainer();
        Category Get(int id);
        List<Category> GetRootCategories();
        CategorySearchModel GetCategoriesForSearch(ProductSearchQuery query);
    }
}