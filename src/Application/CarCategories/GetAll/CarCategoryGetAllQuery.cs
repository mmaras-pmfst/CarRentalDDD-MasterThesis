﻿using Application.Abstractions;
using Domain.Management.CarCategories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarCategories.GetAll;

public sealed record CarCategoryGetAllQuery() : IQuery<List<CarCategory>>;
