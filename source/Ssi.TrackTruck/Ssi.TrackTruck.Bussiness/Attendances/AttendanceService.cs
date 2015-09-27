using System;
using System.Linq;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.DAL;

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

        public bool UpdateDailyHit(string username, DateTime time)
        {
            var user = _authService.FindByUsername(username);
            if (user != null)
            {
                user.DailyHitLog.Add(time);
                _repository.Save(user);
                return true;
            }

            return false;
        }

        public DateTime? GetLastDailyHit(string username)
        {
            var user = _authService.FindByUsername(username);
            if (user == null)
            {
                throw new Exception("No user found: " + username);
            }

            if (user.DailyHitLog.Count == 0)
            {
                return null;
            }

            return user.DailyHitLog.OrderBy(time => time).Last();
        }
    }
}
