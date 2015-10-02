using System;
using System.Collections.Generic;
using System.Linq;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.DAL.Users;
using Ssi.TrackTruck.Bussiness.Helpers;

namespace Ssi.TrackTruck.Bussiness.Attendances
{
    public class AttendanceService
    {
        private readonly IRepository _repository;
        private readonly AuthService _authService;

        public AttendanceService(IRepository repository, AuthService authService)
        {
            _repository = repository;
            _authService = authService;
        }

        public bool UpdateDailyHit(string username, DateTimeModel time)
        {
            var user = _authService.FindByUsername(username);

            if (user == null)
            {
                return false;
            }

            var log = _repository.FindOne<DbDailyHit>(hit => hit.UserId == user.Id && hit.Date == time.DateInt) ??
                      new DbDailyHit { UserId = user.Id, Date = time.DateInt, HitTimes = new List<int>() };

            log.HitTimes.Add(time.TimeInt);
            _repository.Save(log);

            return true;
        }

        public IQueryable<DbUser> GetReport(DateTimeModel fromDate, DateTimeModel toDate)
        {
            // TODO: work with bit wise operator
            var custodians = _repository.GetWhere<DbUser>(user => user.Role == Role.BranchCustodian);
            return custodians;
        }
    }
}
