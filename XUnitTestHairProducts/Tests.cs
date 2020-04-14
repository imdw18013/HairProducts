using HairProducts.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HairProducts.Controllers;
using HairProducts.Models;

namespace XUnitTestHairProducts
{
    public class Tests
    {
        [Fact]
        public async System.Threading.Tasks.Task TestIndexAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestIndex")
                .Options;

            var context = new ApplicationDbContext(options);
            var controller = new ProductCategoriesController(context);


            using (var context2 = new ApplicationDbContext(options))
            {
                var cats = new List<ProductCategory>();
                cats.Add(new ProductCategory
                {
                    CategoryName = "Hair Wax"
                });
                cats.Add(new ProductCategory
                {
                    CategoryName = "Shapoo"
                });
                cats.Add(new ProductCategory
                {
                    CategoryName = "Thickening Paste"
                });
                context2.ProductCategory.AddRange(cats);
                context2.SaveChanges();
            }


            var result = await controller.Index();


            Assert.IsType<ViewResult>(result);
            using (var context3 = new ApplicationDbContext(options))
            {
                Assert.Equal(3, context3.ProductCategory.Count());
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task TestCreateAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCreate")
                .Options;

            var context = new ApplicationDbContext(options);
            var controller = new ProductCategoriesController(context);
            var result = await controller.Create(
                new ProductCategory { 
                    CategoryName = "Shampoo"
                });


            Assert.IsType<RedirectToActionResult>(result);
            using (var context2 = new ApplicationDbContext(options))
            {
                Assert.Equal(1, context2.ProductCategory.Count());
                Assert.Equal("Shampoo", context2.ProductCategory.FirstOrDefault().CategoryName);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task TestDetailsAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDetails")
                .Options;
            var context = new ApplicationDbContext(options);

            var controller = new ProductCategoriesController(context);


            using (var context2 = new ApplicationDbContext(options))
            {
                context2.ProductCategory.Add(new ProductCategory { 
                CategoryName = "Shampoo"
                });
                context2.SaveChanges();
            }

            var result = await controller.Details(1);
            var vr = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<ProductCategory>(vr.ViewData.Model);
        }


        [Fact]
        public async System.Threading.Tasks.Task TestEditAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestEdit")
                .Options;
            var context = new ApplicationDbContext(options);

            var controller = new ProductCategoriesController(context);



            using (var context2 = new ApplicationDbContext(options))
            {

                context2.ProductCategory.Add(new ProductCategory
                {
                    CategoryName = "Shampoo"
                });
                context2.SaveChanges();
            }

            var result = await controller.Edit(1, new ProductCategory
            {
                CategoryId = 1,
                CategoryName = "Hair Wax"
            });
            Assert.IsType<RedirectToActionResult>(result);
            using (var context2 = new ApplicationDbContext(options))
            {
                var cat = context2.ProductCategory.FirstOrDefault();
                Assert.Equal("Hair Wax", cat.CategoryName);
                context2.SaveChanges();
            }
        }


        [Fact]
        public async System.Threading.Tasks.Task TestDeleteAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDelete")
                .Options;
            var context = new ApplicationDbContext(options);

            var controller = new ProductCategoriesController(context);



            using (var context2 = new ApplicationDbContext(options))
            {
                context2.ProductCategory.Add(new ProductCategory
                {
                    CategoryName = "Shampoo"
                });
                context2.SaveChanges();
            }

            var result = await controller.DeleteConfirmed(1);
            Assert.IsType<RedirectToActionResult>(result);
            using (var context2 = new ApplicationDbContext(options))
            {
                Assert.Equal(0, context2.ProductCategory.Count());
                context2.SaveChanges();
            }
        }

    }
}

