using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Hotels.Data;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Areas.Identity.Data;

public class HotelsUser<T>
{
    private readonly DbContextOptions<HotelsUserContext> options;

    public HotelsUser(DbContextOptions<HotelsUserContext> options)
    {
        this.options = options;
    }

    internal void OnModelCreating(ModelBuilder builder)
    {
        throw new NotFiniteNumberException();
    }
}