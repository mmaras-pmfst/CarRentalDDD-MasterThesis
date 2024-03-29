﻿using Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarModels.Create;

public sealed record CarModelCreateCommand(
    string CarModelName,
    decimal PricePerDay,
    decimal Discount,
    Guid CarBrandId, 
    Guid CarCategoryId) : ICommand<Guid>;
