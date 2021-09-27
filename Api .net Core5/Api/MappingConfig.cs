using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using Api.Modelos.Dto;
using Api.Modelos;

namespace Api
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            //mapeo de las clases
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ClienteDto , Cliente>();
                config.CreateMap<Cliente , ClienteDto>();
            });
            return mappingConfig;
        }
    }
}