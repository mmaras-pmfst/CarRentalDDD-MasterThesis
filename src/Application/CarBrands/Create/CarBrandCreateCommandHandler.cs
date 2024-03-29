﻿using Application.Abstractions;
using Domain.Management.CarBrands;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Management.CarBrands.ValueObjects;
using System.Xml.Linq;

namespace Application.CarBrands.Create;

internal sealed class CarBrandCreateCommandHandler : ICommandHandler<CarBrandCreateCommand, Guid>
{
    private ILogger<CarBrandCreateCommandHandler> _logger;
    private readonly ICarBrandRepository _carBrandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CarBrandCreateCommandHandler(ILogger<CarBrandCreateCommandHandler> logger, ICarBrandRepository carBrandRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _carBrandRepository = carBrandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CarBrandCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarBrandCreateCommandHandler");

        try
        {
            var carBrandNameResult = CarBrandName.Create(request.CarBrandName);
            if (carBrandNameResult.IsFailure)
            {
                return Result.Failure<Guid>(carBrandNameResult.Error);
            }
            var exists = await _carBrandRepository.AlreadyExists(carBrandNameResult.Value, cancellationToken);
            if (exists)
            {

                _logger.LogWarning("CarBrandCreateCommandHandler: CarBrand already exists!");
                return Result.Failure<Guid>(DomainErrors.CarBrand.CarBrandAlreadyExists);

            }

            

            var newCarBrand = CarBrand.Create(
                Guid.NewGuid(),
                carBrandNameResult.Value);

            await _carBrandRepository.AddAsync(newCarBrand, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished CarBrandCreateCommandHandler");
            return newCarBrand.Id;
        }
        catch (Exception ex)
        {

            _logger.LogError("CarBrandCreateCommandHandler error: {0}", ex.Message);
            return Result.Failure<Guid>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
