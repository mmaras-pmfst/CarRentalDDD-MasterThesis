﻿using Application.Abstractions;
using Domain.CarBrand.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarModels.GetById;

public sealed record CarModelGetByIdQuery(
    Guid CarBrandId, 
    Guid CarModelId) : IQuery<CarModel?>;