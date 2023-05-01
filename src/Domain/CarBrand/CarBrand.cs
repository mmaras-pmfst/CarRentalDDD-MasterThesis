﻿using Domain.CarBrand.Entities;
using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CarBrand;

public sealed class CarBrand : AggregateRoot
{
    private readonly List<CarModel> _carModels = new();

    public string CarBrandName { get; private set; }
    public IReadOnlyCollection<CarModel> CarModels => _carModels;

    private CarBrand(Guid id, string carBrandName)
        : base(id)
    {
        CarBrandName = carBrandName;
    }
    private CarBrand() { }

    public static CarBrand Create(Guid id, string carBrandName)
    {
        return new CarBrand(id, carBrandName);
    }

    public CarModel CreateCarModel(string carModelName, CarCategory.CarCategory carCategory)
    {
        var carModel = new CarModel(Guid.NewGuid(), carModelName, this, carCategory);
        _carModels.Add(carModel);
        return carModel;
    }
}
