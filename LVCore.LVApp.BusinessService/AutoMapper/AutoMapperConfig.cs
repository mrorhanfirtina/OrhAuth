using AutoMapper;
using LVCore.LVApp.BusinessService.AutoMapper.Profiles;
using System;

namespace LVCore.LVApp.BusinessService.AutoMapper
{
    public static class AutoMapperConfig
    {
        private static IMapper _mapper;

        public static void RegisterMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new StockMappingProfile()); // 📌 Profil ekleniyor
            });

            _mapper = config.CreateMapper();
        }

        public static IMapper GetMapper()
        {
            if (_mapper == null)
            {
                throw new InvalidOperationException("AutoMapper henüz başlatılmadı. Lütfen `RegisterMappings` çağır.");
            }
            return _mapper;
        }
    }
}
