using Microsoft.VisualStudio.TestTools.UnitTesting;
using bs.DataTable.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using bs.Data;
using bs.Data.Interfaces;
using bs.DataTableTests.Repositories;
using bs.DataTableTests.Models;
using bs.DataTable.Dtos;

namespace bs.DataTable.Services.Tests
{
    [TestClass()]
    public class PaginatorServiceTests
    {
        private IServiceProvider serviceProvider;
        private IServiceCollection services;

        [TestInitialize]
        public void Init()
        {
            services = new ServiceCollection();
            var dbContext = new DbContext
            {
                ConnectionString = "Data Source=.\\bs.Data.Test.db;Version=3;BinaryGuid=False;",
                DatabaseEngineType = DbType.SQLite,
                Create = false,
                Update = true,
                LookForEntitiesDllInCurrentDirectoryToo = false,
                SetBatchSize = 25
            };

            services.AddBsData(dbContext);
            services.AddScoped<TestRepository>();
            services.AddScoped<PaginatorService>();
            serviceProvider = services.BuildServiceProvider();

            var uow = serviceProvider.GetService<IUnitOfWork>();
            var testRepository = serviceProvider.GetService<TestRepository>();

            uow.BeginTransaction();

            testRepository.CreateCar(new CarModel { Manufactured = "Fiat", Model = "500", Year = 1954 });
            testRepository.CreateCar(new CarModel { Manufactured = "Fiat", Model = "500", Year = 2006 });
            testRepository.CreateCar(new CarModel { Manufactured = "Fiat", Model = "Panda", Year = 1972 });
            testRepository.CreateCar(new CarModel { Manufactured = "Fiat", Model = "Panda", Year = 2006 });
            testRepository.CreateCar(new CarModel { Manufactured = "Fiat", Model = "Uno SX", Year = 1980 });
            testRepository.CreateCar(new CarModel { Manufactured = "Fiat", Model = "Uno DX", Year = 1982 });
            testRepository.CreateCar(new CarModel { Manufactured = "Fiat", Model = "Punto", Year = 1982 });
            testRepository.CreateCar(new CarModel { Manufactured = "Fiat", Model = "Grande Punto", Year = 1982 });
            testRepository.CreateCar(new CarModel { Manufactured = "Seat", Model = "Leon", Year = 1982 });
            testRepository.CreateCar(new CarModel { Manufactured = "Seat", Model = "Ibiza", Year = 1982 });
            testRepository.CreateCar(new CarModel { Manufactured = "Seat", Model = "Ronda", Year = 1982 });
            testRepository.CreateCar(new CarModel { Manufactured = "Seat", Model = "Marbella", Year = 1982 });

            uow.Commit();

        }
     

        [TestMethod()]
        public void GetPageTest()
        {
            var request = new PageRequest
            {
                Draw = 1,
                Length = 5,
                Start = 0
            };
            var uow = serviceProvider.GetService<IUnitOfWork>();
            var testRepository = serviceProvider.GetService<TestRepository>();
            var paginatorService = serviceProvider.GetService<PaginatorService>();

            uow.BeginTransaction();

            var x1 = paginatorService.GetPage(request, testRepository.GetCarsQueryable());
            var x2 = paginatorService.GetPage<CarModel,CarModel,CarModel>(request, testRepository.GetCarsQueryOver());
            var x3 = paginatorService.GetPage<CarModel>(request, testRepository.GetCarsCriteria());


            uow.Commit();
        }
    }
}