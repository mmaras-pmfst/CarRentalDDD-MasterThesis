﻿using Application.Abstractions;
using Domain.Management.CarModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarModels.GetAll;

public sealed record CarModelGetAllQuery() : IQuery<List<CarModel>>;
