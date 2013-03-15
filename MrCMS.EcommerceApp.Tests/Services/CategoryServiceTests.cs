﻿using System.Linq;
using FakeItEasy;
using FluentAssertions;
using MrCMS.Helpers;
using MrCMS.Services;
using MrCMS.Web.Apps.Ecommerce.Pages;
using MrCMS.Web.Apps.Ecommerce.Services;
using Xunit;

namespace MrCMS.EcommerceApp.Tests.Services
{
    public class CategoryServiceTests :InMemoryDatabaseTest
    {
        private IDocumentService _documentService;

        [Fact]
        public void CategoryService_Search_WithNoSearchTermAndPageReturnsTheFirstPageOfAllCategorys()
        {
            var categoryService = GetCategoryService();
            var categorys = Enumerable.Range(1, 20).Select(i => new Category { Name = "Category " + i }).ToList();
            Session.Transact(session => categorys.ForEach(category => session.Save(category)));

            var pagedList = categoryService.Search();

            pagedList.Should().HaveCount(10);
            pagedList.ShouldBeEquivalentTo(categorys.Take(10));
        }

        [Fact]
        public void CategoryService_Search_WithNoSearchTermAndPageSetReturnsThatPage()
        {
            var categoryService = GetCategoryService();
            var categorys = Enumerable.Range(1, 20).Select(i => new Category { Name = "Category " + i }).ToList();
            Session.Transact(session => categorys.ForEach(category => session.Save(category)));

            var pagedList = categoryService.Search(page: 2);

            pagedList.Should().HaveCount(10);
            pagedList.ShouldBeEquivalentTo(categorys.Skip(10).Take(10));
        }

        [Fact]
        public void CategoryService_Search_WithSearchTermFiltersByThatValue()
        {
            var categoryService = GetCategoryService();
            var categorys1 = Enumerable.Range(1, 5).Select(i => new Category { Name = "Category " + i }).ToList();
            var categorys2 = Enumerable.Range(1, 5).Select(i => new Category { Name = "Other " + i }).ToList();
            Session.Transact(session => categorys1.ForEach(category => session.Save(category)));
            Session.Transact(session => categorys2.ForEach(category => session.Save(category)));

            var pagedList = categoryService.Search("Other");

            pagedList.Should().HaveCount(5);
            pagedList.ShouldBeEquivalentTo(categorys2);
        }

        [Fact]
        public void CategoryService_Search_WithSearchTermAndPageFiltersByThatValueAndPages()
        {
            var categoryService = GetCategoryService();
            var categorys1 = Enumerable.Range(1, 20).Select(i => new Category { Name = "Category " + i }).ToList();
            var categorys2 = Enumerable.Range(1, 20).Select(i => new Category { Name = "Other " + i }).ToList();
            Session.Transact(session => categorys1.ForEach(category => session.Save(category)));
            Session.Transact(session => categorys2.ForEach(category => session.Save(category)));

            var pagedList = categoryService.Search("Other", 2);

            pagedList.Should().HaveCount(10);
            pagedList.ShouldBeEquivalentTo(categorys2.Skip(10).Take(10));
        }
        [Fact]
        public void CategoryService_Search_ReturnsTheIdOfTheCategoryContainerIfItExists()
        {
            var categoryService = GetCategoryService();
            A.CallTo(() => _documentService.GetUniquePage<CategoryContainer>()).Returns(new CategoryContainer { Id = 1 });

            var pagedList = categoryService.Search();

            pagedList.CategoryContainerId.Should().Be(1);
        }

        [Fact]
        public void CategoryService_Search_ReturnsNullContainerIdIfItDoesNotExist()
        {
            var categoryService = GetCategoryService();
            A.CallTo(() => _documentService.GetUniquePage<CategoryContainer>()).Returns(null);

            var pagedList = categoryService.Search();

            pagedList.CategoryContainerId.Should().Be(null);
        }

        CategoryService GetCategoryService()
        {
            _documentService = A.Fake<IDocumentService>();
            return new CategoryService(Session, _documentService);
        } 
    }
}