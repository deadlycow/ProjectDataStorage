﻿using Business.Dto;
using Business.Interfaces;

namespace Business.Services;
public interface IEmployeeService : IBaseService<EmployeeDto, string>
{
}