﻿using Application.Abstractions;
using Application.Mappings.DtoModels;
using AutoMapper;
using Domain.Management.CarBrands;
using Domain.Repositories;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarBrands.GetAll;

internal sealed class CarBrandGetAllQueryHandler : IQueryHandler<CarBrandGetAllQuery, List<CarBrandDto>>
{
    private ILogger<CarBrandGetAllQueryHandler> _logger;
    private readonly ICarBrandRepository _carBrandRepository;
    private IMapper _mapper;


    public CarBrandGetAllQueryHandler(ILogger<CarBrandGetAllQueryHandler> logger, ICarBrandRepository carBrandRepository, IMapper mapper)
    {
        _logger = logger;
        _carBrandRepository = carBrandRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<CarBrandDto>>> Handle(CarBrandGetAllQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarBrandGetAllCommandHandler");

        try
        {
            var dbCarBrands = await _carBrandRepository.GetAllAsync(cancellationToken);

            if (!dbCarBrands.Any())
            {
                _logger.LogWarning("CarBrandGetAllCommandHandler: No CarBrands in database");
                return Result.Failure<List<CarBrandDto>>(new Error(
                        "CarBrand.NoData",
                        "There are no CarBrands to fetch"));
            }

            var resultDto = _mapper.Map<List<CarBrand>, List<CarBrandDto>>(dbCarBrands);

            _logger.LogInformation("Finished CarBrandGetAllCommandHandler");
            return resultDto;
        }
        catch (Exception ex)
        {

            _logger.LogError("CarBrandGetAllCommandHandler error: {0}", ex.Message);
            return Result.Failure<List<CarBrandDto>>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
