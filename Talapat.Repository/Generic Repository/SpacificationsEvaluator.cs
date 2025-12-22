using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talapat.Infrastructure.Generic_Repository
{
    public static class SpecificationsEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> entryQuery,ISpecification<T> spec)
        {
            var query = entryQuery; //dbContext.set<T>
            if (spec.Criteria != null)
                query = query.Where(spec.Criteria); //dbContext.set<T>.where(s => s.Id == id)
            
            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            else if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);
            if (spec.IsPagingEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            query = spec.Inculdes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            //8//dbContext.set<T>.where(s => s.Id == id).Incluse(s => s.Courses ).Include(s => s.Department)
            return query;
        }
    }
}
