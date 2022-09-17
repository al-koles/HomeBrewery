using AutoMapper;

namespace HomeBrewery.Application.Common.Mappings;

public class GlobalMappings : IMapWith<int>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
        profile.CreateMap<byte?, byte>().ConvertUsing((src, dest) => src ?? dest);
        profile.CreateMap<char?, char>().ConvertUsing((src, dest) => src ?? dest);
        profile.CreateMap<decimal?, decimal>().ConvertUsing((src, dest) => src ?? dest);
        profile.CreateMap<double?, double>().ConvertUsing((src, dest) => src ?? dest);
        profile.CreateMap<float?, float>().ConvertUsing((src, dest) => src ?? dest);
        profile.CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
        profile.CreateMap<long?, long>().ConvertUsing((src, dest) => src ?? dest);
        profile.CreateMap<sbyte?, sbyte>().ConvertUsing((src, dest) => src ?? dest);
        profile.CreateMap<short?, short>().ConvertUsing((src, dest) => src ?? dest);
        profile.CreateMap<uint?, uint>().ConvertUsing((src, dest) => src ?? dest);
        profile.CreateMap<ulong?, ulong>().ConvertUsing((src, dest) => src ?? dest);
        profile.CreateMap<ushort?, ushort>().ConvertUsing((src, dest) => src ?? dest);
    }
}