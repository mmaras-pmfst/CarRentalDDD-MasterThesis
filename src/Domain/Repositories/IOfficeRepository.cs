﻿using Domain.Management.Offices;
using Domain.Management.Offices.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;

public interface IOfficeRepository
{
    Task AddAsync(Office office, CancellationToken cancellationToken = default);
    Task<List<Office>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Office?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> AlreadyExists(Address address, CancellationToken cancellationToken = default);

}
