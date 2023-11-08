using CashFlow.Domain.Commons;
using CashFlow.Service.Configurations;
using CashFlow.Service.Exceptions;
using CashFlow.Service.Helpers;
using Newtonsoft.Json;

namespace CashFlow.Service.Extensions;

public static class CollectionExtension
{
    public static IQueryable<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> source, PaginationParams @params)
            where TEntity : Auditable
    {

        var metaData = new PaginationMetaData(source.Count(), @params);

        var json = JsonConvert.SerializeObject(metaData);
        if (HttpContextHelper.ResponseHeaders != null)
        {
            if (HttpContextHelper.ResponseHeaders.ContainsKey("X-Pagination"))
                HttpContextHelper.ResponseHeaders.Remove("X-Pagination");

            HttpContextHelper.ResponseHeaders.Add("X-Pagination", json);
        }

        return @params.PageIndex > 0 && @params.PageSize > 0 ?
            source
            .OrderBy(s => s.Id)
            .Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize)
            : throw new CashFlowException(400, "Please, enter valid numbers");
    }
}
