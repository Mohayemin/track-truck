using System;
using System.Collections.Generic;
using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
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

        public bool UpdateDailyHit(string userId)
        {
            var user = _repository.GetById<DbUser>(userId);

            if (user == null)
            {
                return false;
            }

            var nowAtPhilippines = DateTime.UtcNow.ToPhilppines();
            var today = nowAtPhilippines.DateInt();
            
            var log = _repository.FindOne<DbDailyHit>(hit => hit.UserId == user.Id && hit.DatePhilippine == today) ??
                      new DbDailyHit { UserId = user.Id, DatePhilippine = today, HitTimesPhilippine = new List<TimeSpan>() };

            log.HitTimesPhilippine.Add(nowAtPhilippines.TimeOfDay);
            _repository.Save(log);

            return true;
        }

        public object GetReport(DateTime fromDate, DateTime toDate)
        {
            var fromPhil = fromDate.DateInt();
            var toPhil = toDate.DateInt();

            var dbDailyHits = _repository
                .GetWhere<DbDailyHit>(hit => hit.DatePhilippine >= fromPhil && hit.DatePhilippine <= toPhil).ToList();

            var report = dbDailyHits
                .Select(hit => new
                {
                    UserId = hit.UserId,
                    Date = hit.DatePhilippine,
                    HasHit = hit.HitTimesPhilippine.Count != 0
                })
                .GroupBy(t => t.UserId)
                .Select(g =>
                new {
                    UserId = g.Key,
                    Attendance = g.ToDictionary(arg => arg.Date, arg => arg.HasHit)
                });

            return report;
        }
    }
}
