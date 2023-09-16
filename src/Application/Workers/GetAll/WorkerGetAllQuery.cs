﻿using Application.Abstractions;
using Domain.Management.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Workers.GetAll;
public sealed record WorkerGetAllQuery() : IQuery<List<Worker>>;