using bs.Data.Interfaces.BaseEntities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace bs.DataTableTests.Models
{
    public class CarModel : IPersistentEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string Manufactured { get; set; }
        public virtual string Model { get; set; }
        public virtual int Year { get; set; }
    }

    public class CarModelMap : ClassMapping<CarModel>
    {
        public CarModelMap()
        {
            Table("Cars");

            Id(x => x.Id, x =>
            {
                x.Generator(Generators.GuidComb);
                x.Type(NHibernateUtil.Guid);
                x.Column("Id");
                x.UnsavedValue(Guid.Empty);
            });
            Property(b => b.Manufactured);
            Property(b => b.Model);
            Property(b => b.Year);
           
        }
    }
}
