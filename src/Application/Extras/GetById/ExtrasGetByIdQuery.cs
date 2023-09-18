﻿using Application.Abstractions;
using Application.Mappings.DtoModels;
using Domain.Sales.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extras.GetById;
public sealed record ExtrasGetByIdQuery(Guid ExtraId) : IQuery<ExtraDto?>;