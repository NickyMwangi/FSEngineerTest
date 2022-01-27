using Mapster;
using Service.Interfaces;
using System;
using System.Linq;

namespace Service
{
    public class MapperService : IMapperService
    {
        public TDestination MapConfig<TSource, TDestination>(TSource _source)
        {
            var config = new TypeAdapterConfig();
            //var forked = TypeAdapterConfig.GlobalSettings.Fork(config => config.Default.PreserveReference(true));
            config.NewConfig<TSource, TDestination>()
                .AddDestinationTransform((int? x) => IntToNullable(x))
                .AddDestinationTransform((DateTime? x) => DateToNullable(x));

            var _destination = _source.Adapt<TDestination>(config);
            return _destination;
        }

        public TDestination MapConfig<TSource, TDestination>(TSource _source, TDestination _destination)
        {
            var config = new TypeAdapterConfig();
            //var forked = TypeAdapterConfig.GlobalSettings.Fork(config => config.Default.PreserveReference(true));
            config.NewConfig<TSource, TDestination>()
                .AddDestinationTransform((int? x) => IntToNullable(x))
                .AddDestinationTransform((DateTime? x) => DateToNullable(x));

            _destination = _source.Adapt(_destination, config);
            return _destination;
        }

        public IQueryable<TDestination> MapConfig<TSource, TDestination>(IQueryable<TSource> _source)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<TSource, TDestination>()
                .AddDestinationTransform((int? x) => IntToNullable(x))
                .AddDestinationTransform((DateTime? x) => DateToNullable(x));

            var _destination = _source.ProjectToType<TDestination>(config);
            return _destination;
        }

        private static int? IntToNullable(int? val)
        {
            var nullVal = val == default(int) ? null : val;
            return nullVal;
        }
        private static DateTime? DateToNullable(DateTime? val)
        {
            var nullVal = val < new DateTime(1900, 1, 1) ? null : val;
            return nullVal;
        }
    }
}
