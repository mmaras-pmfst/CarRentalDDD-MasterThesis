﻿using Domain.Management.CarCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;

public interface ICarCategoryRepository
{
    Task AddAsync(CarCategory carCategory, CancellationToken cancellationToken = default);
    Task<List<CarCategory>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CarCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> AlreadyExists(string shortName, CancellationToken cancellationToken = default);
}
