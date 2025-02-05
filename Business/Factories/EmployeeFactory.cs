﻿using Business.Dto;
using Data.Entities;
using System.ComponentModel;

namespace Business.Factories;
public static class EmployeeFactory
{
  public static EmployeeDto Create(EmployeeEntity entity) => new()
  {
    Id = entity.Id,
    Name = entity.Name,
  };

  public static IEnumerable<EmployeeDto> CreateList(IEnumerable<EmployeeEntity> employees) => employees.Select(Create); 
}
