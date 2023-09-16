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

namespace Application.CarModels.GetById;

internal sealed class CarModelGetByIdQueryHandler : IQueryHandler<CarModelGetByIdQuery, CarModel?>
{
    private ILogger<CarModelGetByIdQueryHandler> _logger;
    private ICarModelRepository _carModelRepository;
    private IUnitOfWork _unitOfWork;

    public CarModelGetByIdQueryHandler(
        ILogger<CarModelGetByIdQueryHandler> logger,
        IUnitOfWork unitOfWork,
        ICarModelRepository carModelRepository)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _carModelRepository = carModelRepository;
    }

    public async Task<Result<CarModel?>> Handle(CarModelGetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarModelGetByIdCommandHandler");

        try
        {
            var carModel = await _carModelRepository.GetByIdAsync(request.CarModelId, cancellationToken);

            if(carModel is null ||carModel == null)
            {
                _logger.LogWarning("CarModelGetByIdCommandHandler: CarModel doesn't exist!");
                return Result.Failure<CarModel?>(new Error(
                    "CarModel.NotFound",
                    $"The CarModel with Id {request.CarModelId} was not found"));
            }

            _logger.LogInformation("Finished CarModelGetByIdCommandHandler");

            return carModel;
        }
        catch (Exception ex)
        {

            _logger.LogError("CarModelGetByIdCommandHandler error: {0}", ex.Message);
            return Result.Failure<CarModel?>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
