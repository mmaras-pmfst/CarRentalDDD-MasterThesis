﻿using Application.Abstractions;
using Application.Mappings.DtoModels;
using Domain.Management.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Workers.GetById;
public sealed record WorkerGetByIdQuery(Guid WorkerId) : IQuery<WorkerDetailDto?>;