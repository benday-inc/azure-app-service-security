using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Benday.EasyAuthDemo.Api.DataAccess.Entities;

namespace Benday.EasyAuthDemo.Api.DataAccess
{
    public partial interface IEasyAuthDemoDbContext
    {
        DbSet<ConfigurationItemEntity> ConfigurationItemEntities { get; set; }
        DbSet<FeedbackEntity> FeedbackEntities { get; set; }
        DbSet<LogEntryEntity> LogEntryEntities { get; set; }
        DbSet<LookupEntity> LookupEntities { get; set; }
        DbSet<PersonEntity> PersonEntities { get; set; }
        DbSet<UserEntity> UserEntities { get; set; }
        DbSet<UserClaimEntity> UserClaimEntities { get; set; }
        
    }
}