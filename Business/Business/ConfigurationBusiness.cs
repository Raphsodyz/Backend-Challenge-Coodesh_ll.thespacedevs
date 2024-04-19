﻿using Business.Interface;
using Data.Interface;
using Domain.Entities;
using System.Linq.Expressions;

namespace Business.Business
{
    public class ConfigurationBusiness : BusinessBase<Configuration, IConfigurationRepository>, IConfigurationBusiness, IBusiness
    {
        public ConfigurationBusiness(IUnitOfWork uow):base(uow)
        {
            
        }

        public async Task<IEnumerable<TResult>> ILikeSearch<TResult>(string searchTerm, Expression<Func<Configuration, TResult>> selectColumns, string includedProperties = null)
        {
            return await _repository.ILikeSearch(searchTerm, selectColumns, includedProperties);
        }
    }
}
