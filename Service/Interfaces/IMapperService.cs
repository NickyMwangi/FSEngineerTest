using System.Linq;

namespace Service.Interfaces
{
    public interface IMapperService
    {
        TDestination MapConfig<TSource, TDestination>(TSource _source);

        TDestination MapConfig<TSource, TDestination>(TSource _source, TDestination _destination);

        IQueryable<TDestination> MapConfig<TSource, TDestination>(IQueryable<TSource> _source);
    }
}
