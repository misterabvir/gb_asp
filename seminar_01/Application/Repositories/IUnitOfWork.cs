﻿namespace Application.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
