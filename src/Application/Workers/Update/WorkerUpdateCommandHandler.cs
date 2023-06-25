﻿using Application.Abstractions;
using Application.Workers.Create;
using Domain.Management.Office;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Workers.Update;
internal class WorkerUpdateCommandHandler : ICommandHandler<WorkerUpdateCommand, bool>
{
    private ILogger<WorkerUpdateCommandHandler> _logger;
    private readonly IOfficeRepository _officeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public WorkerUpdateCommandHandler(ILogger<WorkerUpdateCommandHandler> logger, IOfficeRepository officeRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _officeRepository = officeRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<bool>> Handle(WorkerUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started WorkerUpdateCommandHandler");

        try
        {
            var officeWorker = await _officeRepository.GetByIdAsync(request.OfficeId, cancellationToken);

            if (officeWorker == null)
            {
                _logger.LogWarning("WorkerUpdateCommandHandler: Office doesn't exist!");
                return Result.Failure<bool>(new Error(
                "Office.NotFound",
                $"The Office with Id {request.OfficeId} was not found"));
            }


            var worker = officeWorker.Workers.Where(x => x.Id == request.WorkerId).SingleOrDefault();

            if (worker == null)
            {
                _logger.LogWarning("WorkerGetByIdQueryHandler: Worker doesn't exist!");
                return Result.Failure<bool>(new Error(
                "Worker.NotFound",
                $"The Worker with Id {request.WorkerId} was not found"));
            }

            officeWorker.UpdateWorker(request.WorkerId, request.Email, request.PhoneNumber);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished WorkerUpdateCommandHandler");
            return true;


        }
        catch (Exception ex)
        {
            _logger.LogError("WorkerUpdateCommandHandler error: {0}", ex.Message);
            return Result.Failure<bool>(new Error(
                "Error",
                ex.Message));
        }

    }
}