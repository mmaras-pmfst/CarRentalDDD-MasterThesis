﻿using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extras.Update;
public sealed record ExtrasUpdateCommand(
    Guid ExtraId,
    string Name,
    string Description,
    decimal PricePerDay) : ICommand<bool>;