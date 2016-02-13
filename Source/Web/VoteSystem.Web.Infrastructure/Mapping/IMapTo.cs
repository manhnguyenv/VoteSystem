﻿namespace VoteSystem.Web.Infrastructure.Mapping
{
    /// <summary>
    /// Map from db model to view model. Must be use only in view models.
    /// </summary>
    /// <typeparam name="T">Db model</typeparam>
    public interface IMapTo<T>
        where T : class
    {
    }
}
