﻿using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarModelRents.Create;
public sealed record CarModelRentCreateCommand(
    DateTime ValidFrom,
    DateTime ValidUntil,
    decimal PricePerDay,
    decimal Discount,
    bool IsVisible,
    Guid CarModelId) : ICommand<Guid>;