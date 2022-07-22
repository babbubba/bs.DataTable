using bs.Data;
using bs.Data.Interfaces;
using bs.DataTableTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bs.DataTableTests.Repositories
{
    public class TestRepository : Repository
    {
        private readonly IUnitOfWork unitOfwork;

        public TestRepository(IUnitOfWork unitOfwork) : base(unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public void CreateCar(CarModel car)
        {
            Create(car);
        }

        public IQueryable<CarModel> GetCarsQueryable()
        {
            return Query<CarModel>();
        }

        public NHibernate.IQueryOver<CarModel,CarModel> GetCarsQueryOver()
        {
            return unitOfwork.Session.QueryOver<CarModel>();
        }

        public NHibernate.ICriteria GetCarsCriteria()
        {
            return unitOfwork.Session.CreateCriteria<CarModel>();
        }

    }
}
