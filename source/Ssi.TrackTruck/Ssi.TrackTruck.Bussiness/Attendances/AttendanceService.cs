using System;
using System.Collections.Generic;
using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.DAL.Users;
using Ssi.TrackTruck.Bussiness.Helpers;

namespace Ssi.TrackTruck.Bussiness.Attendances
{
    public class AttendanceService
    {
        private readonly IRepository _repository;

        public AttendanceService(IRepository repository)
        {
            _repository = repository;
        }

        public bool UpdateDailyHit(string userId, DateTime time)
        {
            var user = _repository.GetById<DbUser>(userId);

            if (user == null)
            {
                return false;
            }

            var today = time.Date;
            var log = _repository.FindOne<DbDailyHit>(hit => hit.UserId == user.Id && hit.Date == today) ??
                      new DbDailyHit { UserId = user.Id, Date = today, HitTimes = new List<TimeSpan>() };

            log.HitTimes.Add(time.TimeOfDay);
            _repository.Save(log);

            return true;
        }

        public object GetReport(DateTimeModel fromDate, DateTimeModel toDate)
        {
            /*var report = _repository
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

            return report;*/
            return null;
        }
    }
}
