﻿using Application.Abstractions;
using Domain.Errors;
using Domain.Management.Workers;
using Domain.Repositories;
using Domain.Shared;
using Domain.Shared.ValueObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Workers.Create;
internal class WorkerCreateCommandHandler : ICommandHandler<WorkerCreateCommand, Guid>
{
    private ILogger<WorkerCreateCommandHandler> _logger;
    private readonly IWorkerRepository _workerRepository;
    private readonly IOfficeRepository _officeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public WorkerCreateCommandHandler(
        ILogger<WorkerCreateCommandHandler> logger,
        IWorkerRepository workerRepository,
        IUnitOfWork unitOfWork,
        IOfficeRepository officeRepository)
    {
        _logger = logger;
        _workerRepository = workerRepository;
        _unitOfWork = unitOfWork;
        _officeRepository = officeRepository;
    }

    public async Task<Result<Guid>> Handle(WorkerCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started WorkerCreateCommandHandler");

        try
        {
            var office = await _officeRepository.GetByIdAsync(request.OfficeId, cancellationToken);
            if (office == null || office is null )
            {
                _logger.LogWarning("WorkerCreateCommandHandler: Office doesn't exist!");
                return Result.Failure<Guid>(new Error(
                "Office.NotFound",
                $"The Office with Id {request.OfficeId} was not found"));
            }

            var worker = await _workerRepository.AlreadyExists(request.PersonalIdentificationNumber, cancellationToken);
            if (worker)
            {
                _logger.LogWarning("WorkerCreateCommandHandler: Worker already exist!");
                return Result.Failure<Guid>(DomainErrors.Worker.WorkerAlreadyExists);

            }

            var emailResult = Email.Create(request.Email);
            if (emailResult.IsFailure)
            {
                return Result.Failure<Guid>(emailResult.Error);
            }
            var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
            if (phoneNumberResult.IsFailure)
            {
                return Result.Failure<Guid>(phoneNumberResult.Error);
            }
            var firstNameResult = FirstName.Create(request.FirstName);
            if (firstNameResult.IsFailure)
            {
                return Result.Failure<Guid>(firstNameResult.Error);

            }
            var lastNameResult = LastName.Create(request.LastName);
            if (lastNameResult.IsFailure)
            {
                return Result.Failure<Guid>(lastNameResult.Error);

            }
            var newWorker = Worker.Create(
                Guid.NewGuid(),
                firstNameResult.Value,
                lastNameResult.Value, 
                emailResult.Value,
                phoneNumberResult.Value,
                office,
                request.PersonalIdentificationNumber);

            await _workerRepository.AddAsync(newWorker, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished WorkerCreateCommandHandler");
            return newWorker.Id;

        }
        catch (Exception ex)
        {
            _logger.LogError("WorkerCreateCommandHandler error: {0}", ex.Message);
            return Result.Failure<Guid>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
