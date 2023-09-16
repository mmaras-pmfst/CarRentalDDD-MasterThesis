﻿using Application.Abstractions;
using Domain.Management.CarModels;
using Domain.Repositories;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarModels.GetAll;

internal sealed class CarModelGetAllQueryHandler : IQueryHandler<CarModelGetAllQuery, List<CarModel>>
{
    private ILogger<CarModelGetAllQueryHandler> _logger;
    private ICarModelRepository _carModelRepository;

    public CarModelGetAllQueryHandler(
        ILogger<CarModelGetAllQueryHandler> logger,
        ICarModelRepository carModelRepository)
    {
        _logger = logger;
        _carModelRepository = carModelRepository;
    }
    public async Task<Result<List<CarModel>>> Handle(CarModelGetAllQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarModelGetAllCommandHandler");

        try
        {
            var carModels = await _carModelRepository.GetAllAsync(cancellationToken);

            if (!carModels.Any())
            {
                _logger.LogWarning("CarBrandGetAllCommandHandler: No CarModels in database");
                return Result.Failure<List<CarModel>>(new Error(
                        "CarModel.NoData",
                        "There are no CarModels to fetch"));
            }

            _logger.LogInformation("Finished CarModelGetAllCommandHandler");

            return carModels;
        }
        catch (Exception ex)
        {

            _logger.LogError("CarModelCreateCommandHandler error: {0}", ex.Message);
            return Result.Failure<List<CarModel>>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
