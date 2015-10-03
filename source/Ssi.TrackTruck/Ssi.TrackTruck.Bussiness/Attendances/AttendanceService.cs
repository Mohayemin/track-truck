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

        public bool UpdateDailyHit(string userId, DateTimeModel time)
        {
            var user = _repository.GetById<DbUser>(userId);

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

        public object GetReport(DateTimeModel fromDate, DateTimeModel toDate)
        {
            var report = _repository
                .GetWhere<DbDailyHit>(hit => hit.Date >= fromDate.DateInt && hit.Date <= toDate.DateInt)
                .Select(hit => new
                {
                    UserId = hit.UserId,
                    Date = hit.Date,
                    HasHit = hit.HitTimes.Count != 0
                })
                .GroupBy(t => t.UserId)
                .Select(g =>
                new {
                    UserId = g.Key,
                    Attendance = g.ToDictionary(arg => arg.Date.ToString(), arg => arg.HasHit)
                });

            return report;
        }
    }
}
