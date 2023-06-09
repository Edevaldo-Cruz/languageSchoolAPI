﻿using languageSchoolAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace languageSchoolAPI.Context
{
    public class LogEntryContext : DbContext
    {
        public LogEntryContext(DbContextOptions<LogEntryContext> options) : base(options)
        {

        }
        public DbSet<LogEntryModel> LogEntry { get; set; }

    }
}
